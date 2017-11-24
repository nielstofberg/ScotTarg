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
        private const int READ_TIMEOUT = 700;
        private const int BUFFER_SIZE = 50;
        private const byte OPEN_CHAR = 60;
        private const byte CLOSE_CHAR = 62;
        private const int MSG_LENGTH = 15;
        private TcpClient sock;
        private NetworkStream stream;
        private int index = 0;
        private string tcpAddress = string.Empty;

        public enum Command
        {
            ACK = 0x06,
            NAK = 0x15,
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
        }

        public void StartListening(string portName, int baud)
        {
            openTcpPort(portName);
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
            sock.ReceiveTimeout = READ_TIMEOUT;
            try
            {
                sock.Connect(address, port);
                stream = sock.GetStream();
                //stream.BeginRead(buffer, 0, 1, streamReadCallback, null);
            }
            catch
            {
                closeTcpPort();
                return false;
            }
            return true;
        }

        public void StopListening()
        {
            closeTcpPort();
        }

        private void closeTcpPort()
        {
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

        private bool decodePacket(byte[] packet)
        {
            Message msg = new Comms.Message();
            int startIndex = 0;
            int endIndex = 0;
            int readIndex = 0;
            List<byte> data = new List<byte>();

            if (!ValidatePacket(packet))
            {
                return false;
            }
            while (packet[readIndex] != OPEN_CHAR)
            {
                readIndex += 1;
                if (readIndex == index)
                {
                    return false;
                }
            }

            startIndex = readIndex;
            endIndex = startIndex + packet[startIndex + 1] - 1;
            readIndex += 2;
            msg.Command = (Command)packet[readIndex++];
            for (int n = 0; n < endIndex - startIndex - 3; n++)
            {
                data.Add(packet[readIndex++]);
            }

            msg.Data = data.ToArray();
            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs(msg));


            /*
            closeTcpPort();
            Thread.Sleep(1000);
            openTcpPort(tcpAddress);
            */
            return true;
        }

        public void KeepAlive()
        {
            Message cmd = new Message();
            cmd.Command = Command.SHOT_PACKET;
            cmd.Data = new byte[0];
            SendCommandAsynch(cmd);
        }

        bool asyncBusy = false;
        public void SendCommandAsynch(Message cmd)
        {
            if (!asyncBusy)
            {
                Thread thread = new Thread(GetTargetDataWorker);
                thread.Start(cmd);
            }
        }

        private void GetTargetDataWorker(object args)
        {
            asyncBusy = true;
            Message cmd = (Message)args;
            SendCommand(cmd);
            byte[] reply = GetReply();
            decodePacket(reply);
            asyncBusy = false;
        }

        public void SendCommand(Message cmd)
        {
            List<byte> packet = new List<byte>();
            packet.Add(OPEN_CHAR);
            packet.Add(0x00);
            packet.Add((byte)cmd.Command);
            foreach (byte b in cmd.Data)
            {
                packet.Add(b);
            }
            packet.Add(CLOSE_CHAR);
            packet[1] = (byte)packet.Count;

            try
            {
                foreach (byte b in packet.ToArray())
                {
                    stream.WriteByte(b);
                }
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

        public byte[] GetReply()
        {
            List<byte> packet = new List<byte>();
            bool receiving = true;
            DateTime to = DateTime.Now.AddMilliseconds(READ_TIMEOUT);

            while (receiving)
            {
                try
                {
                    packet.Add((byte)stream.ReadByte());
                    if (ValidatePacket(packet.ToArray()))
                    {
                        receiving = false;
                        break;
                    }
                }
                catch (Exception ex)
                { }
                if (DateTime.Now > to)
                {
                    break;
                }
                //Thread.Sleep(1);
            }
            return packet.ToArray();
        }

        private bool ValidatePacket(byte[] packet)
        {
            int startIndex = 0;
            for (int x=0; x< packet.Length; x++)
            {
                if (packet[x] == OPEN_CHAR)
                {
                    startIndex = x;
                    break;
                }
            }
            if (packet.Length < startIndex + 4)
            {
                return false;
            }
            else if (packet.Length < packet[startIndex + 1])
            {
                return false;
            }
            else if (packet[packet[startIndex + 1] -1] != CLOSE_CHAR)
            {
                return false;
            }
            return true;
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
