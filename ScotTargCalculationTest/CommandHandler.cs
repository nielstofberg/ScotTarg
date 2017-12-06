using ScotTarg.TargetTools;
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
        public event EventHandler<ShotRecordedEventArgs> OnHitRecorded;

        public CommandHandler()
        {

        }

        public void ProcessCommand(TargetCommand msg)
        {
            switch (msg.CommandByte)
            {
                case CommandByte.GET_LAST_SHOT:
                case CommandByte.GET_SHOT:
                    processShotData(msg.Data);
                    break;
            }

        }

        private void processShotData(byte[] data)
        {
            if (data.Length >= 9)
            {
                uint id = (uint)(data[0] << 8 | data[1]);
                int t1 = data[2] << 8 | data[3];
                int t2 = data[4] << 8 | data[5];
                int t3 = data[6] << 8 | data[7];
                int t4 = data[8] << 8 | data[9];
                ShotData shot = new ShotData(1, id, DateTime.Now, t1, t2, t3, t4);
                if (OnHitRecorded != null)
                {
                    RaiseEventOnUIThread(OnHitRecorded, new ShotRecordedEventArgs(shot));
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
