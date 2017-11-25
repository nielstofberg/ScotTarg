using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.TargetTools
{
    public class ShotRecordedEventArgs : EventArgs
    {
        public ShotData ShotData { get; private set; }

        public ShotRecordedEventArgs(ShotData shot)
        {
            this.ShotData = shot;
        }
    }

    public class PacketReceivedEventArgs : EventArgs
    {
        public TargetCommand Received { get; private set; }

        public PacketReceivedEventArgs(TargetCommand cmd)
        {
            Received = cmd;
        }
    }

}
