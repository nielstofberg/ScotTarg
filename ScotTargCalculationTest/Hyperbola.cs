using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg
{
    public class Hyperbola
    {
        /// <summary>
        /// Calculate the Y value for the given X on a Hyperbola with a vertical axis
        /// </summary>
        /// <param name="x">X value of the point being calculated</param>
        /// <param name="vertex">A value of the equation(Dist from centre to vertex)</param>
        /// <param name="focus">C value of the equation(Distance from centre to focus point)</param>
        /// <returns></returns>
        public static double GetYVertAxis(double x, double vertex, double focus)
        {
            double B = GetBVal(vertex, focus);
            int sign = (int)(Math.Abs(vertex) / vertex);

            return Math.Sqrt(Math.Pow(vertex, 2) * (1 + (Math.Pow(x, 2) / Math.Pow(B, 2)))) * sign;
        }

        public static double GetXHorizAxis(double x, double vertex, double focus)
        {
            double B = GetBVal(vertex, focus);
            int sign = (int)(Math.Abs(vertex) / vertex);
            return Math.Sqrt(Math.Pow(vertex, 2) * (1 + (Math.Pow(x, 2) / (Math.Pow(B, 2))))) * sign;
        }

        private static double GetBVal(double vertex, double focus)
        {
            return Math.Sqrt(Math.Pow(focus, 2) - Math.Pow(vertex, 2));
        }
    }
}
