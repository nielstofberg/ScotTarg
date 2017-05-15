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
        private const int MAX_REP = 10;

        //public int CalcConst { get; set; }

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

        private static int GetXCrossPoint(Point[] topPoints, Point[] bottomPoints)
        {
            int minDif = 999999;
            int bestX = -1;
            foreach (Point tp in topPoints)
            {
                try
                {
                    Point fp = bottomPoints.First(p => p.X == tp.X);
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

        private static int GetYCrossPoint(Point[] leftPoints, Point[] rightPoints)
        {
            int minDif = 999999;
            int bestY = -1;
            foreach (Point tp in leftPoints)
            {
                try
                {
                    Point fp = rightPoints.First(p => p.X == tp.X);
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
        public static Point[] GetGraphPoints(int constant, double dif, Side side)
        {
            List<Point> retPoints = new List<Point>();
            double minX = 0;
            double maxX = constant;
            double difA = 0;
            double difB = 0;
            //dif = (dif == 0) ? 1 : dif;
            if (side == Side.Left || side == Side.Right)
            {
                difA = calc_hyperbola_A(dif);
                difB = calc_hyperbola_B(dif, constant);
            }
            else
            {
                difA = calc_hyperbola_B(dif, constant);
                difB = calc_hyperbola_A(dif);
                calc_min_max_x(dif, difA, difB, constant, ref minX, ref maxX);
            }
            for (int x = (int)minX; x<=maxX; x++)
            {
                double y = 0;
                if (side == Side.Left || side == Side.Right)
                {
                    y = calc_y_vert(constant, dif, difA, difB, x, side);
                }
                else
                {
                    y = calc_y_horiz(constant, dif, difA, difB, x, side);
                }
                if (!Double.IsNaN(y))
                {
                    retPoints.Add(new Point(x, (int)Math.Round(y)));
                }
            }
            return retPoints.ToArray();
        }

        private static int GetXCoordinate(int constant, double top, double bottom)
        {
            int c = constant / 4;
            double topA = calc_hyperbola_B(top, constant);
            double topB = calc_hyperbola_A(top);
            double bottomA = calc_hyperbola_B(bottom, constant);
            double bottomB = calc_hyperbola_A(bottom);
            if (top == 0 || bottom == 0)
            {
                // If either of the difs are 0, the fraph will be a vertical line in the middle of the grid. 
                // Therefore, regardless of what the other line looks like, the X axis where it crosses will always be the middle of the grid.
                return constant / 2;
            }

            int x1 = 0;
            int x2 = 0;
            int y1 = constant / 2 - c;
            int y2 = constant / 2 + c;
            int x = 0;
            int y = 0;
            int rep = 0;
            double xDif = constant;

            for (rep = 0; rep <= MAX_REP; rep++)
            {
                double tx1 = Math.Round(calc_x_horiz(constant, top, topA, topB, y1, Side.Top), 2);
                double bx1 = Math.Round(calc_x_horiz(constant, bottom, bottomA, bottomB, y1, Side.Bottom), 2);

                double tx2 = Math.Round(calc_x_horiz(constant, top, topA, topB, y2, Side.Top), 2);
                double bx2 = Math.Round(calc_x_horiz(constant, bottom, bottomA, bottomB, y2, Side.Bottom), 2);


                x1 = (int)Math.Abs(tx1 - bx1);
                x2 = (int)Math.Abs(tx2 - bx2);

                if (x1 < x2)
                {
                    y2 = y1 + c;
                    y1 = y1 - c;
                    if (x1 < xDif)
                    {
                        xDif = x1;
                        y = y1;
                        x = (int)((tx1 + bx1) / 2);
                    }

                }
                else //if (x1 > x2)
                {
                    y2 = y2 + c;
                    y1 = y2 - c;
                    if (x2 < xDif)
                    {
                        xDif = x2;
                        y = y2;
                        x = (int)((tx2 + bx2) / 2);
                    }
                }

                if (xDif <= 0.5f)
                {
                    break;
                }
            }

            return x;
        }

        private static int GetYCoordinate(int constant, double left, double right)
        {
            int c = constant / 4;
            double leftA = calc_hyperbola_A(left);
            double leftB = calc_hyperbola_B(left, constant);
            double rightA = calc_hyperbola_A(right);
            double rightB = calc_hyperbola_B(right, constant);
            if (left == 0 || right == 0)
            {
                // If either of the difa are 0, the fraph will be a horizontal line in the middle of the grid. 
                // Therefore, regardless of what the other line looks like, the Y axis where it crosses will always be the middle of the grid.
                return constant / 2;
            }

            int x1 = constant / 2 - c;
            int x2 = constant / 2 + c;
            int y1 = 0;
            int y2 = 0;
            int yDif = constant;
            int x = 0;
            int y = 0;
            int rep = 0;

            for (rep = 0; rep < MAX_REP; rep++)
            {
                int lY1 = (int)Math.Round(calc_y_vert(constant, left, leftA, leftB, x1, Side.Left), 0);
                int rY1 = (int)Math.Round(calc_y_vert(constant, right, rightA, rightB, x1, Side.Right), 0);

                int lY2 = (int)Math.Round(calc_y_vert(constant, left, leftA, leftB, x2, Side.Left), 0);
                int rY2 = (int)Math.Round(calc_y_vert(constant, right, rightA, rightB, x2, Side.Right), 0);
                y1 = (int)Math.Abs(lY1 - rY1);
                y2 = (int)Math.Abs(lY2 - rY2);

                if (y1 < y2)
                {
                    x2 = x1 + c;
                    x1 = x1 - c;
                    if (y1 < yDif)
                    {
                        yDif = y1;
                        x = x1;
                        y = (int)((lY1 + rY1) / 2);
                    }
                }
                else
                {
                    x2 = x2 + c;
                    x1 = x2 - c;
                    if (y2 < yDif)
                    {
                        yDif = y2;
                        x = x2;
                        y = (int)((lY2 + rY2) / 2);
                    }
                }
                c = (c > 2) ? c / 2 : 1;

                if (yDif == 0)
                {
                    break;
                }
            }

            return y;
        }

        /// <summary>
        /// Get the point on the graph where the sound originated based on the time differences for each side.
        /// </summary>
        /// <param name="constant"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static Point GetPoint(int constant, double left, double right, double top, double bottom)
        {
            int x = GetXCoordinate(constant, top, bottom);
            int y = GetYCoordinate(constant, left, right);

            return new Point(x,y);
        }

        /// <summary>
        /// Calculate the A value of the hyperbola from a vertical time difference
        /// (If the difference is for a horizontal axis, this will return a B value)
        /// </summary>
        /// <param name="dif"></param>
        /// <returns></returns>
        private static double calc_hyperbola_A(double dif)
        {
            return Math.Pow(((dif) / 2), 2);
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
            if (maxX > width)
            {
                maxX = width;
            }
        }

        /// <summary>
        /// Calculate the minimum and maximum X values for a Hyperbola based on horizontal timings
        /// </summary>
        /// <param name="h1"></param>
        /// <param name="h1A"></param>
        /// <param name="h1B"></param>
        /// <param name="h2"></param>
        /// <param name="h2A"></param>
        /// <param name="h2B"></param>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <param name="width"></param>
        private static void calc_min_max_x(double h1, double h1A, double h1B,double h2, double h2A, double h2B, out int minX, out int maxX, int width)
        {
            int min1 = 0;
            int min2 = 0;
            int max1 = width;
            int max2 = width;
            if (h1 < 0)
            {
                min1 = (int)((width / 2) - Math.Sqrt(h1B * (1 - Math.Pow(width, 2) / h1A)));
                max1 = (int)((width + h1) / 2);
            }
            else if (h1 > 0)
            {
                min1 = (int)((width) - (width + h1) / 2);
                max1 = (int)((width / 2) + Math.Sqrt(h1B * (1 - Math.Pow(width, 2) / h1A)));
            }
            else
            {
                min1 = max1 = width * 2;
            }

            if (h2 < 0)
            {
                min2 = (int)((width / 2) - Math.Sqrt(h2B * (1 - Math.Pow(width, 2) / h2A)));
                max2 = (int)((width + h2) / 2);
            }
            else if (h2 > 0)
            {
                min2 = (int)((width) - (width + h2) / 2);
                max2 = (int)((width / 2) + Math.Sqrt(h2B * (1 - Math.Pow(width, 2) / h2A)));
            }
            else
            {
                min2 = max2 = width * 2;
            }

            minX = (min1 < min2) ? min1 : min2;
            maxX = ((max1 > max2) ? max1 : max2) + 1;
        }

        private static double calc_y_vert(int constant, double dif, double valA, double valB, double x, Side side)
        {
            if (dif == 0)
            {
                return constant / 2;
            }
            if (side == Side.Left)
            {
                if (dif < 0)
                {
                    return (constant / 2) + Math.Sqrt(valA * (1 - (Math.Pow(x, 2) / valB)));
                }
                else
                {
                    return (constant / 2) - Math.Sqrt(valA * (1 - (Math.Pow(x, 2) / valB)));
                }
            }
            else //if (side == Side.Right)
            {
                if (dif < 0)
                {
                    return (constant / 2) - Math.Sqrt(valA * (1 - (Math.Pow(x - constant, 2) / valB)));
                }
                else
                {
                    return (constant / 2) + Math.Sqrt(valA * (1 - (Math.Pow(x - constant, 2) / valB)));
                }
            }
        }

        private static double calc_y_horiz(int constant, double dif, double valA, double valB, double x, Side side)
        {
            double y = -1;
            if (valB == 0)
            {
                //sub2 = 0.001;
            }
            if (side == Side.Bottom)
            {
                y = constant - Math.Sqrt(valA * (1 - Math.Pow(x - (constant / 2), 2) / valB));
            }
            else // if (side == Side.Top)
            {
                y = Math.Sqrt(valA * (1 - Math.Pow((x - (constant / 2)), 2) / valB));
            }
            return y;
        }

        private static double calc_x_horiz(int constant, double dif, double valA, double valB, double y, Side side)
        {
            double x = 0;
            if (valB == 0)
            {
                //sub2 = 0.001;
            }
            if (side == Side.Bottom)
            {
                if (dif < 0)
                {
                    x = (constant / 2) - Math.Sqrt(valB * (1 - (Math.Pow(constant - y, 2) / valA)));
                }
                else
                {
                    x = (constant / 2) + Math.Sqrt(valB * (1 - (Math.Pow(constant - y, 2) / valA)));
                }
            }
            else // if (side == Side.Top)
            {
                if (dif < 0)
                {
                    x = (constant / 2) - Math.Sqrt(valB * (1 - (Math.Pow(y, 2) / valA))); ;
                }
                else
                {
                    x = (constant / 2) + Math.Sqrt(valB * (1 - (Math.Pow(y, 2) / valA))); ;
                }
            }
            return x;
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
