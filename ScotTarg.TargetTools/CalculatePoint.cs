using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScotTarg.TargetTools
{
    public class CalculatePoint
    {
        /// <summary>
        /// return a list of all possible [integer] x points for a graph on a horizontal axis.
        /// IE a time diference of horizontal pair of sensors
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="dif"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public static Coordinates[] GetGraphPointsH(int height, int width, double dif, Side side)
        {
            List<Coordinates> points = new List<Coordinates>();
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
                points.Add(new Coordinates(x, y));
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
        public static Coordinates[] GetGraphPointsV(int height, int width, double dif, Side side)
        {
            List<Coordinates> points = new List<Coordinates>();
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
                points.Add(new Coordinates(x, y));
            }
            return points.ToArray();
        }

        /// <summary>
        /// Same as GetXCoordinate(), but "Slow" because it starts at the top and compares every 
        /// Y point until the cross point is found. instead of hunting for it.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static int GetXCoordinateSlow(int height, int width, double top, double bottom)
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

        /// <summary>
        /// Get the X coordinate by checking where the hyperbolas from the top and bottom sides cross.
        /// Because the the cross is quite flat they often cross over several Y points and is therefor 
        /// not suitable for getting the Y axis.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static int GetXCoordinate(int height, int width, double top, double bottom)
        {
            bool best = false;
            double hA = width / 2;
            double vertexT = top / 2;
            double vertexB = bottom / 2;

            int loopCount = 0;
            int y = height / 2;
            int yl = y - (y / 2);
            int yh = y + (y / 2);
            double lastX1 = 0;
            double lastX2 = 0;
            double x1 = 0;
            double x2 = 0;
            double x3 = 0;
            double x4 = 0;
            double lastDif = width;
            double dif = 0;
            double difl = 0;
            double difh = 0;

            while (!best)
            {
                loopCount += 1;
                x1 = hA + Hyperbola.GetXHorizAxis(yh, vertexT, hA);
                x2 = hA + Hyperbola.GetXHorizAxis(height - yh, vertexB, hA);
                difh = Math.Abs(x1 - x2);

                x3 = hA + Hyperbola.GetXHorizAxis(yl, vertexT, hA);
                x4 = hA + Hyperbola.GetXHorizAxis(height - yl, vertexB, hA);
                difl = Math.Abs(x3 - x4);

                if (difh < difl)
                {
                    yl = yh - ((yh - y) / 2);
                    y = yh;
                    yh = y + (y - yl);
                    dif = difh;
                    lastX1 = x1;
                    lastX2 = x2;
                }
                else
                {
                    yh = yl + ((y - yl) / 2);
                    y = yl;
                    yl = y - (yh - y);
                    dif = difl;
                    lastX1 = x3;
                    lastX2 = x4;
                }

                if (dif < lastDif)
                {
                    lastDif = dif;
                }
                else if (yl == yh)
                {
                    best = true;
                }
            }
            return (int)Math.Round((lastX1 + lastX2) / 2, 0);
        }

        /// <summary>
        /// Same as GetYCoordinate(), but "Slow" because it starts on the left and compares every 
        /// X point until the cross point is found. instead of hunting for it.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int GetYCoordinateSlow(int height, int width, double left, double right)
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

        /// <summary>
        /// Get the Y coordinate by checking where the hyperbolas from the left and right sides cross.
        /// Because the the cross is quite flat they often cross over several X points and is therefor 
        /// not suitable for getting the X axis.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int GetYCoordinate(int height, int width, double left, double right)
        {
            bool best = false;
            double hA = height / 2;
            double vertexL = left / 2;
            double vertexR = right / 2;

            int loopCount = 0;
            int x = height / 2;
            int xl = x - (x / 2);
            int xh = x + (x / 2);
            double lastY1 = 0;
            double lastY2 = 0;
            double y1 = 0;
            double y2 = 0;
            double y3 = 0;
            double y4 = 0;
            double lastDif = height;
            double dif = 0;
            double difl = 0;
            double difh = 0;

            while (!best)
            {
                loopCount += 1;
                y1 = hA - Hyperbola.GetYVertAxis(xh, vertexL, hA);
                y2 = hA - Hyperbola.GetYVertAxis(width - xh, vertexR, hA);
                difh = Math.Abs(y1 - y2);

                y3 = hA - Hyperbola.GetYVertAxis(xl, vertexL, hA);
                y4 = hA - Hyperbola.GetYVertAxis(width - xl, vertexR, hA);
                difl = Math.Abs(y3 - y4);

                if (difh < difl)
                {
                    xl = xh - ((xh - x) / 2);
                    x = xh;
                    xh = x + (x - xl);
                    dif = difh;
                    lastY1 = y1;
                    lastY2 = y2;
                }
                else
                {
                    xh = xl + ((x - xl) / 2);
                    x = xl;
                    xl = x - (xh - x);
                    dif = difl;
                    lastY1 = y3;
                    lastY2 = y4;
                }


                if (dif < lastDif)
                {
                    lastDif = dif;
                }
                else if (xl == xh)
                {
                    best = true;
                }

            }

            return (int)Math.Round((lastY1 + lastY2) / 2, 0);
        }

        /// <summary>
        /// Same as GetPointTopLeft() but "Slow" because it starts on the left and tries every X point
        /// until the cross point is found instead of hunting for it.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static Coordinates GetPointTopLeftSlow(int height, int width, double left, double top)
        {
            double hA = height / 2;
            double vertexL = left / 2;
            double vertexT = top / 2;

            double y = 0;
            double x1 = 0;
            double newX = 0;
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
                    newX = (x + x1) / 2;
                }
                else
                {
                    break;
                }

            }

            return new Coordinates((int)Math.Round(newX, 0), (int)Math.Round(y, 0));
        }

        /// <summary>
        /// Find the point where the hyperbolas from the left and top sides cross.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static Coordinates GetPointTopLeft(int height, int width, double left, double top)
        {
            bool best = false;
            double hA = height / 2;
            double vertexL = left / 2;
            double vertexT = top / 2;

            int loopCount = 0;
            int x = height / 2;
            int xl = x - (x / 2);
            int xh = x + (x / 2);
            double x1 = 0;
            double x2 = 0;
            double lastX = 0;
            double lastY = 0;
            double y1 = 0;
            double y2 = 0;
            double lastDif = height;
            double dif = 0;
            double difl = 0;
            double difh = 0;

            while (!best)
            {
                loopCount += 1;
                y1 = hA - Hyperbola.GetYVertAxis(xh, vertexL, hA);
                x1 = hA + Hyperbola.GetXHorizAxis(y1, vertexT, hA);
                difh = Math.Abs(xh - x1);

                y2 = hA - Hyperbola.GetYVertAxis(xl, vertexL, hA);
                x2 = hA + Hyperbola.GetXHorizAxis(y2, vertexT, hA);
                difl = Math.Abs(xl - x2);

                if (difh < difl)
                {
                    xl = xh - ((xh - x) / 2);
                    x = xh;
                    xh = x + (x - xl);
                    dif = difh;
                    lastY = y1;
                    lastX = (xh + x1) / 2;
                }
                else
                {
                    xh = xl + ((x - xl) / 2);
                    x = xl;
                    xl = x - (xh - x);
                    dif = difl;
                    lastY = y2;
                    lastX = (xl + x2) / 2;
                }

                if (dif < lastDif)
                {
                    lastDif = dif;
                }
                else if (xl == xh)
                {
                    best = true;
                }

            }

            return new Coordinates((int)Math.Round(lastX,0), (int)Math.Round(lastY, 0));
        }

        /// <summary>
        /// Find the point where the hyperbolas from the Right and bottom sides cross.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static Coordinates GetPointBottomRight(int height, int width, double right, double bottom)
        {
            double hA = height / 2;
            double vertexB = bottom / 2;
            double vertexR = right / 2;

            double y = 0;
            double x1 = 0;
            double newX = 0;
            double lastDif = height;
            double dif = 0;

            for (int x = 0; x < height; x++)
            {
                y = hA - Hyperbola.GetYVertAxis(width - x, vertexR, hA);
                x1 = hA + Hyperbola.GetXHorizAxis(height - y, vertexB, hA);

                dif = Math.Abs(x - x1);
                if (dif < lastDif)
                {
                    lastDif = dif;
                    newX = (x + x1) / 2;
                }
                else
                {
                    break;
                }

            }

            Coordinates p = new Coordinates((int)Math.Round(newX, 0), (int)Math.Round(y, 0));
            return p;
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
        public static Coordinates GetPoint(int constant, double left, double right, double top, double bottom)
        {
            int x = GetXCoordinate(constant, constant, top, bottom);
            int y = GetYCoordinate(constant, constant, left, right);

            return new Coordinates(x,y);
        }

        /// <summary>
        /// Compares the X value from GetXCoordinate() and the X value from GetPointTopLeft() and mkes an adjustment 
        /// based on the difference between the two x values. Then it repeats the process untill the two X values 
        /// are the same. The final adjustment value compensates for the fact that the sound originates from the 
        /// circumference of the projectile and not from the centre.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static int GetCorrectionValue(int height, int width, double left, double right, double top, double bottom)
        {
            int magic = 0;
            for (int x = 0; x < 20; x++)
            {
                double tmpLeft = left - magic;
                double tmpTop = top + magic;
                double tmpRight = right + magic;
                double tmpBottom = bottom - magic;


                double x1 = CalculatePoint.GetXCoordinate(height, width, tmpTop, tmpBottom);
                double x2 = CalculatePoint.GetPointTopLeft(height, width, tmpLeft, tmpTop).X;
                if (Math.Abs(x1 - x2) < 1)
                {
                    break;
                }
                else
                {
                    magic += (int)Math.Round(x1 - x2, 0);
                    if (x < 5)
                    {
                        magic = magic * 2;
                    }
                }
            }

            return magic;
        }
    }
}
