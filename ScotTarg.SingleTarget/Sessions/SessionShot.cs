using ScotTarg.TargetTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.Sessions
{
    public class SessionShot
    {
        public int SeriesId { get; private set; } = 0;
        public int ShotInSeries { get; set; } = 0;
        public float Score { get; private set; } = 0f;
        public float Direction { get; private set; } = 0f;
        public double DistPerCount { get; private set; }
        public int CalcWidth { get; private set; }
        public ShotData ShotData { get; set; }
        public Discipline Discipline { get; private set; }

        public double X_mm { get { return Math.Round((ShotData.Calculated_X - (CalcWidth / 2)) * DistPerCount, 2); } }
        public double Y_mm { get { return Math.Round((ShotData.Calculated_Y - (CalcWidth / 2)) * DistPerCount, 2); } }
        public double DistFromCentre
        {
            get
            {
                int xDif = Math.Abs(CalcWidth / 2 - ShotData.Calculated_X);
                int yDif = Math.Abs(CalcWidth / 2 - ShotData.Calculated_Y);
                return Math.Round(Math.Sqrt(Math.Pow(xDif,2)+ Math.Pow(yDif, 2))* DistPerCount,4);
            }
        }

        public SessionShot(double distpcount, int cwidth, ShotData shot, Discipline disc)
        {
            DistPerCount = distpcount;
            CalcWidth = cwidth;
            ShotData = shot;
            Discipline = disc;
            shot.DoCalculation(CalcWidth, CalcWidth);
            Score = CalcScore(disc, (float)DistFromCentre);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static float CalcScore(Discipline disc, float dfc)
        {
            float ret = 0;
            float adj = (disc.Direction == Discipline.ScoringDirection.Inward) ? disc.AmmoDiameter : (0 - disc.AmmoDiameter);
            int counter = 10;
            float dec = ((disc.Rings[1] - disc.Rings[0]) / 2) / 10;
            foreach (float dia in disc.Rings)
            {
                float diadj = (dia + adj) / 2;
                if (dfc < diadj)
                {
                    ret = counter;
                    ret += (int)((diadj - dfc) / dec) * 0.1f;
                    break;
                }
                counter -= 1;
            }
            if (ret == 11) ret = 9.9f;
            return ret;
        }
    }
}
