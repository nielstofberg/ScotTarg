using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.IpTools
{
    public class DeviceFoundEventArgs : EventArgs
    {
        public DeviceConfig NewDevice { get; private set; }

        public DeviceFoundEventArgs(DeviceConfig device)
        {
            NewDevice = device;
        }
    }
}
