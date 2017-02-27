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

        //find the intersection given TDOA(time distance of arrival) between points ab and bc 
        //(note: these need to already have been multiplied by the propagation speed)
        public FourPoints FindCoords(double d_ab, double d_bc, double d_cd, double d_ad, ref double x, ref double y)
        {
            double min_x, max_x, d_ab_sub1, d_ab_sub2, d_bc_sub1, d_bc_sub2, d_cd_sub1, d_cd_sub2, d_ad_sub1, d_ad_sub2;
            double xa = 0, ya = 0, xb = 0, yb = 0, xc = 0, yc = 0, xd = 0, yd = 0;
            double[] xVals = new double[4];
            double[] yVals = new double[4];
            int level;

            d_ab = (d_ab == 0) ? 1 : d_ab;
            d_bc = (d_bc == 0) ? 1 : d_bc;
            d_cd = (d_cd == 0) ? -1 : d_cd;
            d_ad = (d_ad == 0) ? 1 : d_ad;

            for (int a = 0; a < 4; a++)
            {
                badCorners[a] = 0;
            }

            //compute some substitutions (these are basically parts A and B of the standard hyperbola) 
            d_ab_sub1 = Math.Pow(((d_ab) / 2), 2);
            d_ab_sub2 = Math.Pow(((d_ab) / 2), 2) - Math.Pow(CalcConst / 2, 2);

            d_bc_sub1 = Math.Pow(((d_bc) / 2), 2) - Math.Pow(CalcConst / 2, 2);
            d_bc_sub2 = Math.Pow(((d_bc) / 2), 2);

            d_cd_sub1 = Math.Pow(((d_cd) / 2), 2);
            d_cd_sub2 = Math.Pow(((d_cd) / 2), 2) - Math.Pow(CalcConst / 2, 2);

            d_ad_sub1 = Math.Pow(((d_ad) / 2), 2) - Math.Pow(CalcConst / 2, 2);
            d_ad_sub2 = Math.Pow(((d_ad) / 2), 2);

            if (d_ad < 0)
            {
                min_x = (CalcConst / 2) - Math.Sqrt(d_ad_sub2 * (1 - Math.Pow((CalcConst * 2), 2) / d_ad_sub1));
                max_x = (2 * (CalcConst / 2) + d_ad) / 2;
            }
            else if (d_ad > 0)
            {
                min_x = (2 * (CalcConst / 2)) - (2 * (CalcConst / 2) + d_ad) / 2;
                max_x = (CalcConst / 2) + Math.Sqrt(d_ad_sub2 * (1 - Math.Pow(((CalcConst / 2) * 2), 2) / d_ad_sub1));
            }
            else
            {
                min_x = max_x = 0;
            }

            level = 0;
            intersect_perp(A, ref d_ab, ref d_ab_sub1, ref d_ab_sub2, ref d_ad_sub1, ref d_ad_sub2, ref xa, ref ya, min_x, max_x, level);
            level = 0;
            intersect_perp(D, ref d_cd, ref d_cd_sub1, ref d_cd_sub2, ref d_ad_sub1, ref d_ad_sub2, ref xd, ref yd, min_x, max_x, level);

            //arrived at b before c 
            if (d_bc < 0)
            {
                min_x = (CalcConst / 2) - Math.Sqrt(d_bc_sub2 * (1 - Math.Pow(((CalcConst / 2) * 2), 2) / d_bc_sub1));
                max_x = (2 * (CalcConst / 2) + d_bc) / 2;
            }
            else
            {
                min_x = (2 * (CalcConst / 2)) - (2 * (CalcConst / 2) + d_bc) / 2;
                max_x = (CalcConst / 2) + Math.Sqrt(d_bc_sub2 * (1 - Math.Pow(((CalcConst / 2) * 2), 2) / d_bc_sub1));
            }
            level = 0;
            intersect_perp(B, ref d_ab, ref d_ab_sub1, ref d_ab_sub2, ref d_bc_sub1, ref d_bc_sub2, ref xb, ref yb, min_x, max_x, level);
            level = 0;
            intersect_perp(C, ref d_cd, ref d_cd_sub1, ref d_cd_sub2, ref d_bc_sub1, ref d_bc_sub2, ref xc, ref yc, min_x, max_x, level);

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
            diff1 = Math.Abs(y_horiz1 - y_vert1);
            y_horiz2 = calc_y_horiz(corner, d_horiz_sub1, d_horiz_sub2, x2);
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

        //calculates the y value for a given x value and the hyperbola generated from points a and b 
        double calc_y_vert_neg(int corner, double sub1, double sub2, double x)
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

        double calc_y_vert_pos(int corner, double sub1, double sub2, double x)
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
        double calc_y_horiz(int corner, double sub1, double sub2, double x)
        {
            if (sub2 == 0)
            {
                //sub2 = 0.001;
            }
            if (corner == A)
            {
                return (2 * (CalcConst / 2)) - Math.Sqrt(sub1 * (1 - Math.Pow((x - (CalcConst / 2)), 2) / sub2));
            }
            else if (corner == B)
            {
                return Math.Sqrt(sub1 * (1 - Math.Pow((x - (CalcConst / 2)), 2) / sub2));
            }
            else if (corner == C)
            {
                return Math.Sqrt(sub1 * (1 - Math.Pow((x - (CalcConst / 2)), 2) / sub2));
            }
            else //if (corner == 'd')
            {
                return (2 * (CalcConst / 2)) - Math.Sqrt(sub1 * (1 - Math.Pow((x - (CalcConst / 2)), 2) / sub2));
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
                    Timings t = GetTimingsForPointB(new Point(x, y));
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

        Timings GetTimingsForPoint(int x, int y)
        {
            int gridWidth = CalcConst;
            Timings times = new Timings();

            double addToX = 0;
            double addToY = 0;

            // Time1 Calculation (Bottom Left corner)
            addToX = Math.Pow(x, 2);
            addToY = Math.Pow(CalcConst - y, 2);
            times.TimeA = Math.Sqrt(addToX + addToY);

            // Time2 Calculation (Top Left corner)
            addToX = Math.Pow(x, 2);
            addToY = Math.Pow(y, 2);
            times.TimeB = Math.Sqrt(addToX + addToY);

            // Time3 Calculation (Top Right corner)
            addToX = Math.Pow(CalcConst - x, 2);
            addToY = (y >= 0) ? 0 : Math.Abs(y) * 2;
            times.TimeC = Math.Sqrt(addToX + addToY);

            // Time4 Calculation (Bottom Right corner)
            addToX = Math.Pow(CalcConst - x, 2);
            addToY = Math.Pow(CalcConst - y, 2);
            times.TimeD = Math.Sqrt(addToX + addToY);

            double deduct = GetLowestValue(times);

            times.TimeA = Math.Round(times.TimeA - deduct, 0);
            times.TimeB = Math.Round(times.TimeB - deduct, 0);
            times.TimeC = Math.Round(times.TimeC - deduct, 0);
            times.TimeD = Math.Round(times.TimeD - deduct, 0);
            return times;
        }

        Timings GetTimingsForPointB(Point e)
        {
            Timings ret = new CalculatePoint.Timings();
            int RES_INCREASE_FACTOR = 1;
            int gridWidth = CalcConst;

            int cornerX = gridWidth / 2 * RES_INCREASE_FACTOR;
            int cornerY = gridWidth / 2 * RES_INCREASE_FACTOR;
            int x = (e.X - (gridWidth / 2)) * RES_INCREASE_FACTOR;
            int y = (e.Y - (gridWidth / 2)) * -RES_INCREASE_FACTOR;

            int addToX = 0;
            int addToY = 0;

            // Time1 Calculation (Bottom Left corner)
            addToX = (x <= 0) ? 0 : Math.Abs(x) * 2;
            addToY = (y <= 0) ? 0 : Math.Abs(y) * 2;
            ret.TimeA = Math.Sqrt(Math.Pow(cornerX + addToX - Math.Abs(x), 2) + Math.Pow(cornerY + addToY - Math.Abs(y), 2));

            // Time2 Calculation (Top Left corner)
            addToX = (x <= 0) ? 0 : Math.Abs(x) * 2;
            addToY = (y >= 0) ? 0 : Math.Abs(y) * 2;
            ret.TimeB = Math.Sqrt(Math.Pow(cornerX + addToX - Math.Abs(x), 2) + Math.Pow(cornerY + addToY - Math.Abs(y), 2));

            // Time3 Calculation (Top Right corner)
            addToX = (x >= 0) ? 0 : Math.Abs(x) * 2;
            addToY = (y >= 0) ? 0 : Math.Abs(y) * 2;
            ret.TimeC = Math.Sqrt(Math.Pow(cornerX + addToX - Math.Abs(x), 2) + Math.Pow(cornerY + addToY - Math.Abs(y), 2));

            // Time4 Calculation (Bottom Right corner)
            addToX = (x >= 0) ? 0 : Math.Abs(x) * 2;
            addToY = (y <= 0) ? 0 : Math.Abs(y) * 2;
            ret.TimeD = Math.Sqrt(Math.Pow(cornerX + addToX - Math.Abs(x), 2) + Math.Pow(cornerY + addToY - Math.Abs(y), 2));


            double deduct = GetLowestValue(ret);

            ret.TimeA = Math.Round(ret.TimeA - deduct, 0);
            ret.TimeB = Math.Round(ret.TimeB - deduct, 0);
            ret.TimeC = Math.Round(ret.TimeC - deduct, 0);
            ret.TimeD = Math.Round(ret.TimeD - deduct, 0);

            return ret;
        }

        private double GetLowestValue(Timings t)
        {
            double val = 0;
            if (t.TimeA <= t.TimeB && t.TimeA <= t.TimeC && t.TimeA <= t.TimeD)
            {
                val = t.TimeA;
            }
            else if (t.TimeB <= t.TimeA && t.TimeB <= t.TimeC && t.TimeB <= t.TimeD)
            {
                val = t.TimeB;
            }
            else if (t.TimeC <= t.TimeA && t.TimeC <= t.TimeB && t.TimeC <= t.TimeD)
            {
                val = t.TimeC;
            }
            else
            {
                val = t.TimeD;
            }
            return val;
        }
    }
}
