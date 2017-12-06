using ScotTarg.TargetTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.SingleTarget
{
    public class SessionShot
    {
        public string SessionState = "Sighter";
        public int StringId = 0;
        public int ShotInString = 0;
        public float Score = 0;
        public float Direction = 0;
        public double DistPerCount { get; private set; }
        public int CalcWidth { get; private set; }
        public ShotData ShotData { get; set; }

        public double X_mm { get { return Math.Round((ShotData.Calculated_X - (CalcWidth / 2)) * DistPerCount, 2); } }
        public double Y_mm { get { return Math.Round((ShotData.Calculated_Y - (CalcWidth / 2)) * DistPerCount, 2); } }
        public double DistFromCentre
        {
            get
            {
                int xDif = Math.Abs(CalcWidth / 2 - ShotData.Calculated_X);
                int yDif = Math.Abs(CalcWidth / 2 - ShotData.Calculated_Y);
                return Math.Sqrt(Math.Pow(xDif,2)+ Math.Pow(yDif, 2))* DistPerCount;
            }
        }

        public SessionShot(double distpcount, int cwidth, ShotData shot )
        {
            DistPerCount = distpcount;
            CalcWidth = cwidth;
            ShotData = shot;
            shot.DoCalculation(CalcWidth, CalcWidth);
        }
    }
}
