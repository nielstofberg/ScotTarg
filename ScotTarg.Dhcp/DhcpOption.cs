using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.Dhcp
{
    internal class DhcpOption
    {
        private byte[] _data = new byte[0];

        /// <summary>
        /// Option Code: A single octet that specifies the option type.
        /// </summary>
        internal OptionCode Code { get; set; }

        /// <summary>
        /// Option Length: The number of bytes in this particular option. 
        /// This does not include the two bytes for the Code and Len fields.
        /// </summary>
        internal byte Length { get; set; }

        /// <summary>
        /// Option Data: The data being sent, which has a length indicated by the 
        /// Len subfield, and which is interpreted based on the Code subfield.
        /// </summary>
        internal byte[] Data
        {
            get { return _data; }
            set
            {
                _data = value;
                Length = (byte)Data.Length;
            }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        internal DhcpOption()
        {
            Code = (OptionCode)0x00;
            Length = 0;
        }

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="len"></param>
        /// <param name="dat"></param>
        internal DhcpOption(OptionCode opt, int len, byte[] dat)
        {
            Code = opt;
            Length = (byte)len;
            _data = dat;
        }

        internal DhcpOption(byte[] data)
        {
            Code = (OptionCode)data[0];
            Length = data[1];
            _data = data.SubArray(2,Length);
        }

        internal byte[] GetBytes()
        {
            List<byte> ret = new List<byte>();
            ret.Add((byte)Code);
            ret.Add(Length);
            ret.AddRange(_data);
            return ret.ToArray();
        }

        public static bool operator ==(DhcpOption o1, DhcpOption o2)
        {
            bool retval = o1.Code == o2.Code && o1.Length == o2.Length && o1.Data.Length == o2.Data.Length;
            if (retval)
            {
                for (int n=0; n< o1.Data.Length; n++)
                {
                    if (o1.Data[n] != o2.Data[n])
                    {
                        retval = false;
                        break;
                    }
                }
            }
            return retval;
        }

        public static bool operator !=(DhcpOption o1, DhcpOption o2)
        {
            return !(o1 == o2);
        }

        public override bool Equals(object obj)
        {
            return (this == (DhcpOption)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
