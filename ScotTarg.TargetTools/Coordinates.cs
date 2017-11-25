using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.TargetTools
{
    public class Coordinates
    {
        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public Coordinates()
        {
            X = 0;
            Y = 0;
        }
        public Coordinates(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }
    }
}
