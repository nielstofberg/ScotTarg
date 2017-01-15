using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScotTargCalculationTest
{
    class Comms
    {
        private const byte OPEN_CHAR = 60;
        private const byte CLOSE_CHAR = 62;
        private const int MSG_LENGTH = 15;
        private SerialPort sp1;
        private byte[] buffer = new byte[0];

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
            int index = buffer.Length;

            Array.Resize(ref buffer, index + length);
            sp1.Read(buffer, index, length);
            decodeBuffer();
        }

        void sp1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
        }

        public void StartListening(string portName, int baud)
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
        }

        public void StopListening()
        {
            sp1.Close();
        }

        private void decodeBuffer()
        {
            int index = 0;
            if (buffer.Length <= index)
            {
                return;
            }

            while (buffer[index] != OPEN_CHAR)
            {
                index += 1;
                if (index >= buffer.Length)
                {
                    return;
                }
            }
            if (buffer.Length - index < MSG_LENGTH)
            {
                return;
            }
            else if (buffer[MSG_LENGTH - 1] != CLOSE_CHAR)
            {
                buffer = new byte[0];
                return;
            }

            index += 1;
            int t1 = GetInt24(ref index);
            int t2 = GetInt24(ref index);
            int t3 = GetInt24(ref index);
            int t4 = GetInt24(ref index);

            buffer = new byte[0];

            if (OnHitRecorded != null)
            {
                RaiseEventOnUIThread(OnHitRecorded,new HitRecordedEventArgs(t1, t2, t3, t4));
            }
        }

        private int GetInt24(ref int index)
        {
            int t1 = 0;
            for (int a = 0; a < 3; a++)
            {
                t1 |= (int)(buffer[index]) << (16 - (8 * a));
                index += 1;
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
