using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.TargetTools
{
    public enum CommandByte
    {
        GET_LAST_SHOT = 0x20,
        GET_SHOT = 0x21,
        SET_ADVANCE = 0x22,
        GET_ADVANCE = 0x23
    }

    public enum ReplyBytes
    {
        ACK = 0x06,
        NAK = 0x15
    }

    public enum Side
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
    }

}
