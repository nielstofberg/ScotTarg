using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.Dhcp
{
    public static class Extentions
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static byte[] GetBytes(this UInt16 val)
        {
            byte[] ret = new byte[2];
            ret[0] = (byte)(val >> 8);
            ret[1] = (byte)(val & 0xFF);
            return ret;
        }

        public static byte[] GetBytes(this UInt32 val)
        {
            byte[] ret = new byte[4];
            ret[0] = (byte)(val >> 24);
            ret[1] = (byte)(val >> 16);
            ret[2] = (byte)(val >> 8);
            ret[3] = (byte)(val & 0xFF);
            return ret;
        }

        public static byte[] GetBytes(this Flag val)
        {
            return ((UInt16)val).GetBytes();
        }
    }
}
