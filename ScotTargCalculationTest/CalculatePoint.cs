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

        /// <summary>
        /// return a list of all possible [integer] x points for a graph on a horizontal axis.
        /// IE a time diference of horizontal pair of sensors
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="dif"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public static Point[] GetGraphPointsH(int height, int width, double dif, Side side)
        {
            List<Point> points = new List<Point>();
            double vA = height / 2;
            double vertex = dif / 2;
            for (int x = 0; x <= width; x++)
            {
                int y = 0;
                if (side == Side.Left)
                {
                    y = (int)Math.Round(vA - Hyperbola.GetYVertAxis(x, vertex, vA),0);
                }
                else if (side == Side.Right)
                {
                    y = (int)Math.Round(vA - Hyperbola.GetYVertAxis(width - x, vertex, vA),0);
                }
                points.Add(new Point(x, y));
            }
            return points.ToArray();
        }

        /// <summary>
        /// return a list of all possible [integer] Y points for a graph on a vertical axis.
        /// IE a time diference of vertical pair of sensors
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="dif"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public static Point[] GetGraphPointsV(int height, int width, double dif, Side side)
        {
            List<Point> points = new List<Point>();
            double hA = width / 2;
            double vertex = dif / 2;
            for (int y = 0; y <= height; y++)
            {
                int x = 0;
                if (side == Side.Top)
                {
                    x = (int)Math.Round(hA + Hyperbola.GetXHorizAxis(y, vertex, hA), 0);
                }
                else if (side == Side.Bottom)
                {
                    x = (int)Math.Round(hA + Hyperbola.GetXHorizAxis(height - y, vertex, hA),0);
                }
                points.Add(new Point(x, y));
            }
            return points.ToArray();
        }

        private static int GetXCoordinate(int height, int width, double top, double bottom)
        {
            double hA = width / 2;
            double vertexT = top / 2;
            double vertexB = bottom / 2;

            double x1 = 0;
            double x2 = 0;
            double lastDif = width;
            double dif = 0;
            for (int y = 0; y < height; y++)
            {
                x1 = hA + Hyperbola.GetXHorizAxis(y, vertexT, hA);
                x2 = hA + Hyperbola.GetXHorizAxis(height - y, vertexB, hA);
                dif = Math.Abs(x1 - x2);
                if (dif < lastDif)
                {
                    lastDif = dif;
                }
                else
                {
                    break;
                }
            }
            return (int)Math.Round((x1 + x2) / 2, 0);
        }

        private static int GetYCoordinate(int height, int width, double left, double right)
        {
            double hA = height / 2;
            double vertexL = left / 2;
            double vertexR = right / 2;

            double y1 = 0;
            double y2 = 0;
            double lastDif = height;
            double dif = 0;

            for (int x = 0; x < height; x++)
            {
                y1 = hA - Hyperbola.GetYVertAxis(x, vertexL, hA);
                y2 = hA - Hyperbola.GetYVertAxis(width - x, vertexR, hA);
                dif = Math.Abs(y1 - y2);
                if (dif < lastDif)
                {
                    lastDif = dif;
                }
                else
                {
                    break;
                }

            }

            return (int)Math.Round((y1 + y2) / 2, 0);
        }

        public static int GetYTopLeft(int height, int width, double left, double top)
        {
            double hA = height / 2;
            double vertexL = left / 2;
            double vertexT = top / 2;

            double y = 0;
            double x1 = 0;
            double lastDif = height;
            double dif = 0;

            for (int x = 0; x < height; x++)
            {
                y = hA - Hyperbola.GetYVertAxis(x, vertexL, hA);
                x1 = hA + Hyperbola.GetXHorizAxis(y, vertexT, hA);
                dif = Math.Abs(x - x1);
                if (dif < lastDif)
                {
                    lastDif = dif;
                }
                else
                {
                    break;
                }

            }

            return (int)Math.Round(y,0);
        }

        public static int GetXTopLeft(int height, int width, double left, double top)
        {
            double hA = width / 2;
            double vertexT = top / 2;
            double vertexL = left / 2;

            double x = 0;
            double y1 = 0;
            double lastDif = width;
            double dif = 0;
            for (int y = 0; y < height; y++)
            {
                x = hA + Hyperbola.GetXHorizAxis(y, vertexT, hA);
                y1 = hA - Hyperbola.GetYVertAxis(x, vertexL, hA);
                dif = Math.Abs(y - y1);
                if (dif < lastDif)
                {
                    lastDif = dif;
                }
                else
                {
                    break;
                }
            }
            return (int)Math.Round(x, 0);
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
            int x = GetXCoordinate(constant, constant, top, bottom);
            int y = GetYCoordinate(constant, constant, left, right);

            return new Point(x,y);
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
