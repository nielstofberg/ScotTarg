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
        private const byte OPEN_CHAR = 60;
        private const byte CLOSE_CHAR = 62;
        private const int MSG_LENGTH = 15;
        private SerialPort sp1;
        private TcpClient sock;
        private NetworkStream stream;
        private byte[] buffer = new byte[255];
        private int index = 0;
        private int commsType = 0;

        public class HitRecordedEventArgs : EventArgs
        {
            public int TimeA { get; private set; }
            public int TimeB { get; private set; }
            public int TimeC { get; private set; }
            public int TimeD { get; private set; }

            public HitRecordedEventArgs(int a, int b, int c, int d)
            {
                TimeA = a;
                TimeB = b;
                TimeC = c;
                TimeD = d;
            }
        }

        public EventHandler<HitRecordedEventArgs> OnHitRecorded;

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

        private void openTcpPort(string address)
        {
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
            sock.Connect(address, port);
            stream = sock.GetStream();
            stream.BeginRead(buffer, 0, 1, streamReadCallback, null);
            commsType = 2;
        }

        private void streamReadCallback(IAsyncResult ar)
        {
            if (commsType == 2)
            {
                try
                {
                    index++;
                    decodeBuffer();
                    stream.BeginRead(buffer, index, 1, streamReadCallback, null);
                }
                catch
                { }
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
            stream.Close();
            sock.Close();
            stream = null;
        }

        private void decodeBuffer()
        {
            int readIndex = 0;
            if (index == readIndex)
            {
                return;
            }

            while (buffer[readIndex] != OPEN_CHAR)
            {
                readIndex += 1;
                if (readIndex == index)
                {
                    return;
                }
            }
            if (index - readIndex < MSG_LENGTH)
            {
                return;
            }
            else if (buffer[readIndex + MSG_LENGTH - 1] != CLOSE_CHAR)
            {
                index = 0;
                return;
            }

            readIndex += 1;
            int t1 = GetInt24(ref readIndex);
            int t2 = GetInt24(ref readIndex);
            int t3 = GetInt24(ref readIndex);
            int t4 = GetInt24(ref readIndex);

            index = 0;

            if (OnHitRecorded != null)
            {
                RaiseEventOnUIThread(OnHitRecorded,new HitRecordedEventArgs(t1, t2, t3, t4));
            }
        }

        private int GetInt24(ref int readIndex)
        {
            int t1 = 0;
            for (int a = 0; a < 3; a++)
            {
                t1 |= (int)(buffer[readIndex]) << (16 - (8 * a));
                readIndex += 1;
            }

            return t1;
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
