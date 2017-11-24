using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ScotTargCalculationTest.Comms;

namespace ScotTargCalculationTest
{
    public class CommandHandler
    {
        public EventHandler<HitRecordedEventArgs> OnHitRecorded;
        public EventHandler<HitRecordedEventArgs> OnFailedShot;

        public CommandHandler()
        {

        }

        public void ProcessCommand(Message msg)
        {
            if (msg.Command == Command.ACK) //ACK
            {
                switch ((Command)msg.Data[0])
                {

                    case Command.SHOT_PACKET:
                        processShotData(msg.Data);
                        break;
                }
            }
        }

        private void processShotData(byte[] data)
        {
            if (data.Length >= 10)
            {
                int id = data[1] << 8 | data[2];
                int t1 = data[3] << 8 | data[4];
                int t2 = data[5] << 8 | data[6];
                int t3 = data[7] << 8 | data[8];
                int t4 = data[9] << 8 | data[10];
                if (OnHitRecorded != null)
                {
                    RaiseEventOnUIThread(OnHitRecorded, new HitRecordedEventArgs(id, t1, t2, t3, t4, true));
                }
            }
            else if (data.Length >= 2)
            {
                int id = data[0] << 8 | data[1];
                if (OnFailedShot != null)
                {
                    RaiseEventOnUIThread(OnFailedShot, new HitRecordedEventArgs(id, false));
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
