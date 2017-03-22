using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScotTargCalculationTest
{
    public class CalculatePoint
    {
        private const int A = 0;
        private const int B = 1;
        private const int C = 2;
        private const int D = 3;

        private float accuracy = 0.5f;   //accuracy level desired
        private int max_level = 10;         //maximum recursions (regardless of accuracy reached) 
        private int[] badCorners = new int[4]; // Corner names: a=0, b=1, c=2, d=3

        public int CalcConst { get; set; }

        public struct FourPoints
        {
            public int Ax { get; set; }
            public int Bx { get; set; }
            public int Cx { get; set; }
            public int Dx { get; set; }
            public int Ay { get; set; }
            public int By { get; set; }
            public int Cy { get; set; }
            public int Dy { get; set; }
        }

        public struct Timings
        {
            public double TimeA { get; set; }
            public double TimeB { get; set; }
            public double TimeC { get; set; }
            public double TimeD { get; set; }

            public int GetDiff(Timings t)
            {
                int v1 = (int)Math.Abs(TimeA - t.TimeA);
                int v2 = (int)Math.Abs(TimeB - t.TimeB);
                int v3 = (int)Math.Abs(TimeC - t.TimeC);
                int v4 = (int)Math.Abs(TimeD - t.TimeD);
                return (v1 + v2 + v3 + v4);
            }

        }

        public enum Side
        {
            Left = 0,
            Top = 1,
            Right = 2,
            Bottom = 3
        }

        public static int GetXCrossPoint(Point[] horizPoints, Point[] vertPoints)
        {
            int minDif = 999999;
            int bestX = -1;
            foreach (Point tp in horizPoints)
            {
                try
                {
                    Point fp = vertPoints.First(p => p.X == tp.X);
                    int yVal = Math.Abs(tp.Y - fp.Y);
                    if (yVal < minDif)
                    {
                        minDif = yVal;
                        bestX = tp.X;
                        if (minDif < 1)
                        {
                            break;
                        }
                    }
                }
                catch { }
            }
            return bestX;
        }

        public static int GetYCrossPoint(Point[] horizPoints, Point[] vertPoints)
        {
            int minDif = 999999;
            int bestY = -1;
            foreach (Point tp in horizPoints)
            {
                try
                {
                    Point fp = vertPoints.First(p => p.X == tp.X);
                    int yVal = Math.Abs(tp.Y - fp.Y);
                    if (yVal < minDif)
                    {
                        minDif = yVal;
                        bestY = tp.Y;
                        if (minDif < 1)
                        {
                            break;
                        }
                    }
                }
                catch { }
            }
            return bestY;
        }

        /// <summary>
        /// Starting with the time difference of a given side this function calculates 
        /// the Y position for each X position of the hyperbola that would fit on the grid
        /// </summary>
        /// <param name="dif"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public Point[] GetGraphPoints(double dif, Side side)
        {
            List<Point> retPoints = new List<Point>();
            double minX = 0;
            double maxX = CalcConst;
            double difA = 0;
            double difB = 0;
            dif = (dif == 0) ? 1 : dif;
            if (side == Side.Left || side == Side.Right)
            {
                difA = calc_hyperbola_A(dif);
                difB = calc_hyperbola_B(dif, CalcConst);
            }
            else
            {
                difA = calc_hyperbola_B(dif, CalcConst);
                difB = calc_hyperbola_A(dif);
                calc_min_max_x(dif, difA, difB, CalcConst, ref minX, ref maxX);
            }
            for (int x = (int)minX; x<=maxX; x++)
            {
                double y = 0;
                if (side == Side.Left || side == Side.Right)
                {
                    if (dif < 0)
                    {
                        y = calc_y_vert_neg((int)side, difA, difB, x);
                    }
                    else
                    {
                        y = calc_y_vert_pos((int)side, difA, difB, x);
                    }
                }
                else
                {
                    y = calc_y_horiz((int)side, difA, difB, x);
                }
                if (!Double.IsNaN(y))
                {
                    retPoints.Add(new Point(x, (int)Math.Round(y)));
                }
            }
            return retPoints.ToArray();
        }

        /// <summary>
        /// Find the intersection given TDOA(time distance of arrival) between points ab and bc 
        /// (note: these need to already have been multiplied by the propagation speed)
        /// </summary>
        /// <param name="d_ab"></param>
        /// <param name="d_bc"></param>
        /// <param name="d_cd"></param>
        /// <param name="d_ad"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public FourPoints FindCoords(double d_ab, double d_bc, double d_cd, double d_ad, ref double x, ref double y)
        {
            double min_x=0, max_x=0, d_ab_A, d_ab_B, d_bc_A, d_bc_B, d_cd_A, d_cd_B, d_ad_A, d_ad_B;
            double xa = 0, ya = 0, xb = 0, yb = 0, xc = 0, yc = 0, xd = 0, yd = 0;
            double[] xVals = new double[4];
            double[] yVals = new double[4];
            int level;

            //! Change all 0 values to 1 (or -1)
            d_ab = (d_ab == 0) ? 1 : d_ab;
            d_bc = (d_bc == 0) ? 1 : d_bc;
            d_cd = (d_cd == 0) ? -1 : d_cd;
            d_ad = (d_ad == 0) ? 1 : d_ad;

            for (int a = 0; a < 4; a++)
            {
                badCorners[a] = 0;
            }

            //! Calculate the A and B values of the standard hyperbola
            d_ab_A = calc_hyperbola_A(d_ab);
            d_ab_B = calc_hyperbola_B(d_ab, CalcConst);
            d_cd_A = calc_hyperbola_A(d_cd);
            d_cd_B = calc_hyperbola_B(d_cd, CalcConst);

            //! For the horizontal timings, the A and B calculations are reversed
            d_bc_A = calc_hyperbola_B(d_bc, CalcConst);
            d_bc_B = calc_hyperbola_A(d_bc);
            d_ad_A = calc_hyperbola_B(d_ad, CalcConst);
            d_ad_B = calc_hyperbola_A(d_ad);

            calc_min_max_x(d_ad, d_ad_A, d_ad_B, CalcConst, ref min_x, ref max_x);
            level = 0;
            intersect_perp(A, ref d_ab, ref d_ab_A, ref d_ab_B, ref d_ad_A, ref d_ad_B, ref xa, ref ya, min_x, max_x, level);
            level = 0;
            intersect_perp(D, ref d_cd, ref d_cd_A, ref d_cd_B, ref d_ad_A, ref d_ad_B, ref xd, ref yd, min_x, max_x, level);

            calc_min_max_x(d_bc, d_bc_A, d_bc_B, CalcConst, ref min_x, ref max_x);
            level = 0;
            intersect_perp(B, ref d_ab, ref d_ab_A, ref d_ab_B, ref d_bc_A, ref d_bc_B, ref xb, ref yb, min_x, max_x, level);
            level = 0;
            intersect_perp(C, ref d_cd, ref d_cd_A, ref d_cd_B, ref d_bc_A, ref d_bc_B, ref xc, ref yc, min_x, max_x, level);

            FourPoints fp = new FourPoints();
            fp.Ax = (int)Math.Round(xa);
            fp.Bx = (int)Math.Round(xb);
            fp.Cx = (int)Math.Round(xc);
            fp.Dx = (int)Math.Round(xd);
            fp.Ay = (int)Math.Round(ya);
            fp.By = (int)Math.Round(yb);
            fp.Cy = (int)Math.Round(yc);
            fp.Dy = (int)Math.Round(yd);
            x = GetAverageOfGoodValues(new double[] { xa, xb, xc, xd });
            y = GetAverageOfGoodValues(new double[] { ya, yb, yc, yd });
            return fp;
        }

        private double GetAverageOfGoodValues(double[] vals)
        {
            double valBad = 0;
            double valGood = 0;
            int goodCount = 0;

            for (int a = 0; a < 4; a++)
            {
                if (badCorners[a] == 1)
                {
                    if (vals[a] > 0)
                    {
                        valBad += vals[a];
                    }
                    else
                    {
                    }
                }
                else
                {
                    valGood += vals[a];
                    goodCount += 1;
                }
            }

            if (valGood > 0)
            {
                return valGood / goodCount;
            }
            else if (valBad > 0)
            {
                return valBad / 4;
            }

            return 0;
        }

        //this will run recursively to find the closest value using a sort of binary search 
        //the basic idea here is that we'll calculate y for both hyperbolas for two different 
        //values to figure out which is closer 
        void intersect_perp(int corner, ref double d_vert, ref double d_vert_sub1, ref double d_vert_sub2,
            ref double d_horiz_sub1, ref double d_horiz_sub2, ref double x, ref double y, double lower, double upper, int level)
        {
            double x1, x2, y_vert1, y_vert2, y_horiz1, y_horiz2, diff1, diff2, quarter = (upper - lower) / 4;
            level++;
            //printf("\nlevel %i: %f to %f, midpoint: %f\n", level, lower, upper, lower + (upper - lower) / 2); 
            //we're going to try two different x values, one at 25% and the other at 75% of the way between the bounds 
            x1 = lower + quarter;
            x2 = upper - quarter; //we need to adjust the equation a bit based on whether the signal hit a first (negative) 
            if (d_vert < 0)
            {
                y_vert1 = calc_y_vert_neg(corner, d_vert_sub1, d_vert_sub2, x1);
                y_vert2 = calc_y_vert_neg(corner, d_vert_sub1, d_vert_sub2, x2);
            }
            else
            {
                y_vert1 = calc_y_vert_pos(corner, d_vert_sub1, d_vert_sub2, x1);
                y_vert2 = calc_y_vert_pos(corner, d_vert_sub1, d_vert_sub2, x2);
            }
            y_horiz1 = calc_y_horiz(corner, d_horiz_sub1, d_horiz_sub2, x1);
            y_horiz2 = calc_y_horiz(corner, d_horiz_sub1, d_horiz_sub2, x2);
            diff1 = Math.Abs(y_horiz1 - y_vert1);
            diff2 = Math.Abs(y_horiz2 - y_vert2);

            //check which x value got us closer (in this case, x1 did) 
            //we will then recurse to 0% to 50% of the current range 
            if (diff1 == diff2)
            {
                x = (x1 + x2) / 2;
                y = y_vert2;
                return;
            }
            else if (diff1 < diff2) //x1 is closer than x2. So adjust the range to 0% - 50% of the current range
            {
                x = x1;
                //close enough 
                if (diff1 < accuracy)
                {
                    y = (y_vert1 + y_horiz1) / 2;
                }
                //check to make sure we don't recurse too far 
                else if (level > max_level)
                {
                    y = (y_vert1 + y_horiz1) / 2; // Use a y value half way between the two for x1
                    badCorners[corner] = 1;
                }
                else
                {
                    //recurse with limits on either side of the better x value 
                    intersect_perp(corner, ref d_vert, ref d_vert_sub1, ref d_vert_sub2, ref d_horiz_sub1, ref d_horiz_sub2, ref x, ref y, lower, upper - (2 * quarter), level);
                }
            }
            else // if (diff1 < diff2)  //x2 is closer, so recurse to 50% to 100% of the current range
            {
                x = x2;
                //close enough 
                if (diff2 < accuracy)
                {
                    y = (y_vert2 + y_horiz2) / 2;
                }
                //check to make sure we don't recurse too far 
                else if (level > max_level)
                {
                    y = (y_vert2 + y_horiz2) / 2;  // Use a y value half way between the two for x1
                    badCorners[corner] = 1;
                }
                else
                {
                    //recurse with limits on either side of the better x value 
                    intersect_perp(corner, ref d_vert, ref d_vert_sub1, ref d_vert_sub2, ref d_horiz_sub1, ref d_horiz_sub2, ref x, ref y, lower + 2 * quarter, upper, level);
                }
            }
        }

        /// <summary>
        /// Calculate the A value of the hyperbola from a vertical time difference
        /// (If the difference is for a horizontal axis, this will return a B value)
        /// </summary>
        /// <param name="dif"></param>
        /// <returns></returns>
        private static double calc_hyperbola_A(double dif)
        {
            return  Math.Pow(((dif) / 2), 2);
        }

        /// <summary>
        /// Calculate the B value of the hyperbola from a vertical time difference
        /// (If the difference is for a horizontal axis, this will return a A value)
        /// </summary>
        /// <param name="dif"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private static double calc_hyperbola_B(double dif, int width)
        {
            return Math.Pow(((dif) / 2), 2) - Math.Pow(width / 2, 2);
        }

        /// <summary>
        /// Calculate the minimum and maximum X values for a Hyperbola based on horizontal timings
        /// </summary>
        /// <param name="dif"></param>
        /// <param name="horiz_A"></param>
        /// <param name="horiz_B"></param>
        /// <param name="width"></param>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        private static void calc_min_max_x(double dif, double horiz_A, double horiz_B, int width, ref double minX, ref double maxX)
        {
            if (dif < 0)
            {
                minX = (width / 2) - Math.Sqrt(horiz_B * (1 - Math.Pow(width, 2) / horiz_A));
                maxX = (2 * (width / 2) + dif) / 2;
            }
            else if (dif > 0)
            {
                minX = (2 * (width / 2)) - (2 * (width / 2) + dif) / 2;
                maxX = (width / 2) + Math.Sqrt(horiz_B * (1 - Math.Pow(width, 2) / horiz_A));
            }
            else
            {
                minX = maxX = 0;
            }
        }

        //calculates the y value for a given x value and the hyperbola generated from points a and b 
        private double calc_y_vert_neg(int corner, double sub1, double sub2, double x)
        {
            if (sub2 == 0)
            {
                //sub2 = 0.001;
            }
            if (corner == A || corner == B)
            {
                return (CalcConst / 2) + Math.Sqrt(sub1 * (1 - (Math.Pow(x, 2) / sub2)));
            }
            else //if (corner == 'c' || corner == 'd')
            {
                return (CalcConst / 2) - Math.Sqrt(sub1 * (1 - (Math.Pow((x - (2 * (CalcConst / 2))), 2) / sub2)));
            }
        }

        private double calc_y_vert_pos(int corner, double sub1, double sub2, double x)
        {
            if (sub2 == 0)
            {
                //sub2 = 0.001;
            }
            if (corner == A || corner == B)
            {
                return (CalcConst / 2) - Math.Sqrt(sub1 * (1 - (Math.Pow(x, 2) / sub2)));
            }
            else //if (corner == 'c' || corner == 'd')
            {
                return (CalcConst / 2) + Math.Sqrt(sub1 * (1 - (Math.Pow((x - (2 * (CalcConst / 2))), 2) / sub2)));
            }
        }

        //calculates the y value for a given x value and the hyperbola generated from points b and c 
        private double calc_y_horiz(int corner, double sub1, double sub2, double x)
        {
            if (sub2 == 0)
            {
                //sub2 = 0.001;
            }
            if (corner == A || corner == D)
            {
                return (2 * (CalcConst / 2)) - Math.Sqrt(sub1 * (1 - Math.Pow((x - (CalcConst / 2)), 2) / sub2));
            }
            else // if (corner == B || corner == C)
            {
                return Math.Sqrt(sub1 * (1 - Math.Pow((x - (CalcConst / 2)), 2) / sub2));
            }
        }

        public Point GetBestTimings(Timings times)
        {
            int dif = 999999999;
            Point p = new Point(0, 0);
            for (int x = 0; x < CalcConst; x++)
            {
                for (int y = 0; y < CalcConst; y++)
                {
                    Timings t = GetTimingsForPoint(x, y, CalcConst);
                    int newDif = times.GetDiff(t);
                    if (newDif <= dif)
                    {
                        dif = newDif;
                        p.X = x;
                        p.Y = y;
                        if (dif == 0)
                        {
                            return p;
                        }
                    }
                }
            }
            return p;
        }

        /// <summary>
        /// This function takes the actual coordinates of the mouse click and then calculates the direct distance from each microphone
        /// For simplicity (and accuracy), there is no conversion from distance to time. IE time = distance.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Timings GetTimingsForPoint(int x, int y, int width)
        {
            int gridWidth = width;
            Timings times = new Timings();

            double addToX = 0;
            double addToY = 0;

            // Time1 Calculation (Bottom Left corner)
            addToX = Math.Pow(x, 2);
            addToY = Math.Pow(width - y, 2);
            times.TimeA = Math.Sqrt(addToX + addToY);

            // Time2 Calculation (Top Left corner)
            addToX = Math.Pow(x, 2);
            addToY = Math.Pow(y, 2);
            times.TimeB = Math.Sqrt(addToX + addToY);

            // Time3 Calculation (Top Right corner)
            addToX = Math.Pow(width - x, 2);
            addToY = Math.Pow(y,2);
            times.TimeC = Math.Sqrt(addToX + addToY);

            // Time4 Calculation (Bottom Right corner)
            addToX = Math.Pow(width - x, 2);
            addToY = Math.Pow(width - y, 2);
            times.TimeD = Math.Sqrt(addToX + addToY);

            double deduct = GetLowestValue(times);

            times.TimeA = Math.Round(times.TimeA - deduct, 0);
            times.TimeB = Math.Round(times.TimeB - deduct, 0);
            times.TimeC = Math.Round(times.TimeC - deduct, 0);
            times.TimeD = Math.Round(times.TimeD - deduct, 0);
            return times;
        }

        /// <summary>
        /// Return the lowest value in in the Timings struct
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static double GetLowestValue(Timings t)
        {
            double val = t.TimeA;
            if (t.TimeB<val)
            {
                val = t.TimeB;
            }
            if (t.TimeC<val)
            {
                val = t.TimeC;
            }
            if (t.TimeD<val)
            {
                val = t.TimeD;
            }
            return val;
        }
    }
}
