using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScotTargCalculationTest
{
    public class Comms
    {
        private const int BUFFER_SIZE = 50;
        private const byte OPEN_CHAR = 60;
        private const byte CLOSE_CHAR = 62;
        private const int MSG_LENGTH = 15;
        private SerialPort sp1;
        private TcpClient sock;
        private NetworkStream stream;
        private byte[] buffer = new byte[255];
        private int index = 0;
        private int commsType = 0;
        private string tcpAddress = string.Empty;

        public enum Command
        {
            SHOT_PACKET = 0x20,
            SHOT_RESEND = 0x21,
            SET_ADVANCE = 0x22,
            GET_ADVANCE = 0x23
        }

        public struct Message
        {
            public Command Command { get; set; }
            public byte[] Data { get; set; }
        }

        public class HitRecordedEventArgs : EventArgs
        {
            public bool Success { get; private set; }
            public int ShotId { get; private set; }
            public int TimeA { get; private set; }
            public int TimeB { get; private set; }
            public int TimeC { get; private set; }
            public int TimeD { get; private set; }

            public HitRecordedEventArgs(int id, int a, int b, int c, int d, bool succ)
            {
                ShotId = id;
                TimeA = a;
                TimeB = b;
                TimeC = c;
                TimeD = d;
                Success = succ;
            }

            public HitRecordedEventArgs(int id, bool succ)
            {
                ShotId = id;
                TimeA = 0;
                TimeB = 0;
                TimeC = 0;
                TimeD = 0;
                Success = succ;
            }

        }

        public class MessageReceivedEventArgs : EventArgs
        {
            public Message Received { get; private set; }

            public MessageReceivedEventArgs(Message msg)
            {
                Received = msg;
            }
        }

        public EventHandler<HitRecordedEventArgs> OnHitRecorded;
        public EventHandler<MessageReceivedEventArgs> OnMessageReceived;

        public Comms()
        {
            sp1 = new SerialPort();
            sp1.DtrEnable = true;

            sp1.DataReceived += sp1_DataReceived;
            sp1.ErrorReceived += sp1_ErrorReceived;
        }

        void sp1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int length = sp1.BytesToRead;
            sp1.Read(buffer, index, length);
            index += length;
            decodeBuffer();
        }

        void sp1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
        }

        public void StartListening(string portName, int baud)
        {
            if (portName.ToLower().StartsWith("com"))
            {
                openSerialPort(portName, baud);
            }
            else
            {
                openTcpPort(portName);
            }
        }

        private void openSerialPort(string portName, int baud)
        {
            if (sp1.IsOpen)
            {
                sp1.Close();
            }
            sp1.PortName = portName;
            sp1.BaudRate = baud;
            sp1.Parity = Parity.None;
            sp1.DataBits = 8;
            sp1.StopBits = StopBits.One;
            sp1.Open();
            commsType = 1;
        }

        private bool openTcpPort(string address)
        {
            tcpAddress = address;
            int port = 0;
            if (address.IndexOf(":")<0)
            {
                throw new Exception("No TCP port specified");
            }
            if (!int.TryParse(address.Substring(address.IndexOf(":")+1), out port))
            {
                throw new Exception("No TCP port not valid");
            }
            address = address.Substring(0, address.IndexOf(":"));

            sock = new TcpClient();
            try
            {
                sock.Connect(address, port);
                stream = sock.GetStream();
                ClearBuffer();
                stream.BeginRead(buffer, 0, 1, streamReadCallback, null);
                commsType = 2;
            }
            catch
            {
                closeTcpPort();
                return false;
            }
            return true;
        }

        private void streamReadCallback(IAsyncResult ar)
        {
            if (commsType == 2)
            {
                try
                {
                    index++;
                    if (!decodeBuffer())
                    {
                        stream.BeginRead(buffer, index, 1, streamReadCallback, null);
                    }
                }
                catch(Exception ex)
                {
                }
            }
        }

        public void StopListening()
        {
            switch (commsType)
            {
                case 1:
                    closeSerialPort();
                    break;
                case 2:
                    closeTcpPort();
                    break;
            }
        }

        private void closeSerialPort()
        {
            sp1.Close();
            commsType = 0;
        }

        private void closeTcpPort()
        {
            commsType = 0;
            try
            {
                stream.Close();
                stream.Dispose();
            }
            catch
            {
            }
            stream = null;
            try
            {
                sock.Close();
            }
            catch
            {
                sock = null;
            }
        }

        private bool decodeBuffer()
        {
            Message msg = new Comms.Message();
            int startIndex = 0;
            int endIndex = 0;
            int readIndex = 0;
            List<byte> data = new List<byte>();

            if (index == 0)
            {
                return false;
            }

            while (buffer[readIndex] != OPEN_CHAR)
            {
                readIndex += 1;
                if (readIndex == index)
                {
                    ClearBuffer();
                    return false;
                }
            }

            readIndex = startIndex + 1;

            if (readIndex >= index)
            {
                return false;
            }
            else if (buffer[readIndex] > buffer.Length) // If the length byte is bigger than the length of the buffer, this cannot be a valid packet clear the buffer and start over.
            {
                ClearBuffer();
                return false;
            }
            else if (buffer[readIndex] > (index - startIndex + 1)) //If the length byte is more that what has already been received, return and wait for the rest of the message.
            {
                return false;
            }
            else
            {
                endIndex = startIndex + buffer[readIndex] - 1;
            }
            if (index <= endIndex)
            {
                return false;
            }
            else if (buffer[endIndex] != CLOSE_CHAR) //If there is no end byte at the end of the message, this is not a valid command. Clear the buffer and start over. 
            {
                ClearBuffer();
                return false;
            }
            // From this point we know that it is a valid command;
            readIndex++;
            msg.Command = (Command)buffer[readIndex++];


            for (int n = 0; n < endIndex - startIndex - 3; n++)
            {
                data.Add(buffer[readIndex++]);
            }

            msg.Data = data.ToArray();
            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(msg));

            //if (msg.Data.Length > 9)
            //{
            //    readIndex = 0;
            //    int id = msg.Data[0] << 8 | msg.Data[1];
            //    int t1 = msg.Data[2] << 8 | msg.Data[3];
            //    int t2 = msg.Data[4] << 8 | msg.Data[5];
            //    int t3 = msg.Data[6] << 8 | msg.Data[7];
            //    int t4 = msg.Data[8] << 8 | msg.Data[9];
            //    if (OnHitRecorded != null)
            //    {
            //        RaiseEventOnUIThread(OnHitRecorded, new HitRecordedEventArgs(t1, t2, t3, t4));
            //    }
            //}
            //else
            //{

            //}

            ClearBuffer();

            if (commsType == 2)
            {
                closeTcpPort();
                Thread.Sleep(1000);
                openTcpPort(tcpAddress);
            }
            return true;
        }

        private void ClearBuffer()
        {
            for(int n=0; n<buffer.Length;n++)
            {
                buffer[n] = 0;
            }
            index = 0;
        }

        public void KeepAlive()
        {
            try
            {
                stream.WriteByte(0x00);
            }
            catch
            {
                string str = tcpAddress;
                closeTcpPort();
                Thread.Sleep(1000);
                while (!openTcpPort(str))
                {
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// Raise an event on a different thread than the current one
        /// </summary>
        private void RaiseEventOnUIThread(Delegate theEvent, object args)
        {
            try
            {
                foreach (Delegate d in theEvent.GetInvocationList())
                {
                    ISynchronizeInvoke syncer = d.Target as ISynchronizeInvoke;
                    if (syncer == null)
                    {
                        d.DynamicInvoke(args);
                    }
                    else
                    {
                        syncer.Invoke(d, new[] { this, args });  // cleanup omitted    
                    }
                    //_thread.Join();
                }
            }
            catch //(Exception ex)
            {
                //Exception code
            }
        }
        

    }
}
