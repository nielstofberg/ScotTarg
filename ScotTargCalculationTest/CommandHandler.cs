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
            switch (msg.Command)
            {
                case Command.SHOT_PACKET:
                    processShotData(msg.Data);
                    break;
            }
        }

        private void processShotData(byte[] data)
        {
            if (data.Length >= 9)
            {
                int id = data[0] << 8 | data[1];
                int t1 = data[2] << 8 | data[3];
                int t2 = data[4] << 8 | data[5];
                int t3 = data[6] << 8 | data[7];
                int t4 = data[8] << 8 | data[9];
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
