using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTargCalculation_test
{
    class CalculatePoint
    {
        private float accuracy = 0.0001f;   //accuracy level desired
        private int max_level = 10;         //maximum recursions (regardless of accuracy reached) 

        public int CalcConstH { get; set; }
        public int CalcConstV { get; set; }


        //find the intersection given TDOA differences between points ab and bc 
        //(note: these need to already have been multiplied by the propagation speed)
        public void FindCoords(double d_ab, double d_bc, double d_cd, double d_ad, ref double x, ref double y)
        {
            double min_x, max_x, d_ab_sub1, d_ab_sub2, d_bc_sub1, d_bc_sub2, d_cd_sub1, d_cd_sub2, d_ad_sub1, d_ad_sub2;
            double xa = 0, ya = 0, xb = 0, yb = 0, xc = 0, yc = 0, xd = 0, yd = 0;
            int level; 
            
            //compute some substitutions (these are basically parts A and B of the standard hyperbola) 
            d_ab_sub1 = Math.Pow(((d_ab) / 2), 2);
            d_ab_sub2 = Math.Pow(((d_ab) / 2), 2) - Math.Pow(CalcConstV / 2, 2);
            //printf("d_ab_sub1=%f, d_ab_sub2=%f\n", d_ab_sub1, d_ab_sub2);
            d_bc_sub1 = Math.Pow(((d_bc) / 2), 2) - Math.Pow(CalcConstH / 2, 2);
            d_bc_sub2 = Math.Pow(((d_bc) / 2), 2);
            //printf("d_bc_sub1=%f, d_bc_sub2=%f\n", d_bc_sub1, d_bc_sub2);
            d_cd_sub1 = Math.Pow(((d_cd) / 2), 2);
            d_cd_sub2 = Math.Pow(((d_cd) / 2), 2) - Math.Pow(CalcConstV / 2, 2);
            //printf("d_cd_sub1=%f, d_cd_sub2=%f\n", d_cd_sub1, d_cd_sub2); 
            d_ad_sub1 = Math.Pow(((d_ad) / 2), 2) - Math.Pow(CalcConstH / 2, 2);
            d_ad_sub2 = Math.Pow(((d_ad) / 2), 2);
            //printf("d_ad_sub1=%f, d_ad_sub2=%f\n", d_ad_sub1, d_ad_sub2); 
            max_x = (2 * (CalcConstH / 2) + d_ad) / 2;

            if (d_ad < 0)
            {
                min_x = (CalcConstH / 2) - Math.Sqrt(d_ad_sub2 * (1 - Math.Pow((CalcConstH * 2), 2) / d_ad_sub1));
                max_x = (2 * (CalcConstH / 2) + d_ad) / 2;
            }
            else
            {
                min_x = (2 * (CalcConstH / 2)) - (2 * (CalcConstH / 2) + d_ad) / 2;
                max_x = (CalcConstH / 2) + Math.Sqrt(d_ad_sub2 * (1 - Math.Pow(((CalcConstH / 2) * 2), 2) / d_ad_sub1));
            }

            //printf("\nd_ad= %f, d_ab=%f, min_x=%f, max_x=%f\n", d_ad, d_ab, min_x, max_x); //start the recrusive intersection finder 
            level = 0;
            intersect_perp('a', ref d_ab, ref d_ab_sub1, ref d_ab_sub2, ref d_ad_sub1, ref d_ad_sub2, ref xa, ref ya, min_x, max_x, level);
            //printf("final answer: %f, %f\n", *x, *y);
            //printf("\nd_ad= %f, d_cd=%f, min_x=%f, max_x=%f\n", d_ad, d_cd, min_x, max_x); //start the recrusive intersection finder 
            level = 0;
            intersect_perp('d', ref d_cd, ref d_cd_sub1, ref d_cd_sub2, ref d_ad_sub1, ref d_ad_sub2, ref xb, ref yb, min_x, max_x, level);
            //printf("final answer: %f, %f\n", *x, *y); //at this point, this may be the min or it may be the max x value to use 
            max_x = (2 * (CalcConstH / 2) + d_bc) / 2; //arrived at b before c 
            if (d_bc < 0)
            {
                min_x = (CalcConstH / 2) - Math.Sqrt(d_bc_sub2 * (1 - Math.Pow(((CalcConstH / 2) * 2), 2) / d_bc_sub1));
                max_x = (2 * (CalcConstH / 2) + d_bc) / 2;
            }
            else
            {
                min_x = (2 * (CalcConstH / 2)) - (2 * (CalcConstH / 2) + d_bc) / 2;
                max_x = (CalcConstH / 2) + Math.Sqrt(d_bc_sub2 * (1 - Math.Pow(((CalcConstH / 2) * 2), 2) / d_bc_sub1));
            }
            //printf("\nd_ab= %f, d_bc=%f, min_x=%f, max_x=%f\n", d_ab, d_bc, min_x, max_x); //start the recrusive intersection finder 
            level = 0;
            intersect_perp('b', ref d_ab, ref d_ab_sub1, ref d_ab_sub2, ref d_bc_sub1, ref d_bc_sub2, ref xc, ref yc, min_x, max_x, level);
            //printf("final answer: %f, %f\n", *x, *y); printf("\nd_bc= %f, d_cd=%f, min_x=%f, max_x=%f\n", d_bc, d_cd, min_x, max_x); //start the recrusive intersection finder 
            level = 0;
            intersect_perp('c', ref d_cd, ref d_cd_sub1, ref d_cd_sub2, ref d_bc_sub1, ref d_bc_sub2, ref xd, ref yd, min_x, max_x, level);
            //printf("final answer: %f, %f\n", *x, *y); 

            x = (xa + xb ) / 2;
            y = (ya + yb ) / 2;
        }

        //this will run recursively to find the closest value using a sort of binary search 
        //the basic idea here is that we'll calculate y for both hyperbolas for two different 
        //values to figure out which is closer 
        void intersect_perp(char corner, ref double d_vert, ref double d_horiz_sub1, ref double d_horiz_sub2, ref double d_bc_sub1, ref double d_bc_sub2, ref double x, ref double y, double lower, double upper, int level)
        {
            double x1, x2, y_horiz1, y_horiz2, y_vert1, y_vert2, diff1, diff2, quarter = (upper - lower) / 4;
            level++;
            //printf("\nlevel %i: %f to %f, midpoint: %f\n", level, lower, upper, lower + (upper - lower) / 2); 
            //we're going to try two different x values, one at 25% and the other at 75% of the way between the bounds 
            x1 = lower + quarter;
            x2 = upper - quarter; //we need to adjust the equation a bit based on whether the signal hit a first (negative) 
            if (d_vert < 0)
            {
                y_horiz1 = calc_y_vert_neg(corner, d_horiz_sub1, d_horiz_sub2, x1);
                y_horiz2 = calc_y_vert_neg(corner, d_horiz_sub1, d_horiz_sub2, x2);
            }
            else
            {
                y_horiz1 = calc_y_vert_pos(corner, d_horiz_sub1, d_horiz_sub2, x1);
                y_horiz2 = calc_y_vert_pos(corner, d_horiz_sub1, d_horiz_sub2, x2);
            }
            y_vert1 = calc_y_horiz(corner, d_bc_sub1, d_bc_sub2, x1);
            diff1 = Math.Abs(y_vert1 - y_horiz1);
            y_vert2 = calc_y_horiz(corner, d_bc_sub1, d_bc_sub2, x2);
            diff2 = Math.Abs(y_vert2 - y_horiz2);
            //printf("x1 = %f, y_vert1 = %f, y_horiz1 = %f, diff1 = %f\n", x1, y_horiz1, y_vert1, diff1); 
            //printf("x2 = %f, y_vert2 = %f, y_horiz2 = %f, diff2 = %f\n", x2, y_horiz2, y_vert2, diff2); 

            //check which x value got us closer (in this case, x1 did) 
            //we will then recurse to 0% to 50% of the current range 
            if (diff1 == diff2)
            {
                x = (x1 + x2) / 2;
                y = y_horiz2;
                return;
            }
            else if (diff1 < diff2)
            {
                x = x1;
                //close enough 
                if (diff1 < accuracy)
                {
                    y = (y_horiz1 + y_vert1) / 2;
                    return;
                }
                //check to make sure we don't recurse too far 
                if (level > max_level)
                {
                    //printf("too far...\n");
                    y = (y_horiz1 + y_vert1) / 2;
                    return;
                }
                //recurse with limits on either side of the better x value 
                intersect_perp(corner, ref d_vert, ref d_horiz_sub1, ref d_horiz_sub2, ref d_bc_sub1, ref d_bc_sub2, ref x, ref y, lower, upper - 2 * quarter, level);
            } //x2 is closer, so recurse to 50% to 100% of the current range 
            else
            {
                x = x2;
                //do the same stuff as above but with the other x value 
                if (diff2 < accuracy)
                {
                    y = (y_horiz2 + y_vert2) / 2;
                    return;
                }
                if (level > max_level)
                {
                    //printf("too far...\n");
                    y = (y_horiz2 + y_vert2) / 2;
                    return;
                }
                intersect_perp(corner, ref d_vert, ref d_horiz_sub1, ref d_horiz_sub2, ref d_bc_sub1, ref d_bc_sub2, ref x, ref y, lower + 2 * quarter, upper, level);
            }
        }

        //calculates the y value for a given x value and the hyperbola generated from points a and b 
        double calc_y_vert_neg(char corner, double sub1, double sub2, double x)
        {
            if (sub2 == 0)
            {
                sub2 = 1;
            }
            if (corner == 'a' || corner == 'b')
            {
                return (CalcConstH / 2) + Math.Sqrt(Math.Abs(sub1 * (1 - (Math.Pow(x, 2) / sub2))));
            }
            else //if (corner == 'c' || corner == 'd')
            {
                return (CalcConstH / 2) - Math.Sqrt(Math.Abs(sub1 * (1 - (Math.Pow((x - (2 * (CalcConstH / 2))), 2) / sub2))));
            }
        }

        double calc_y_vert_pos(char corner, double sub1, double sub2, double x)
        {
            if (sub2 == 0)
            {
                sub2 = 1;
            }
            if (corner == 'a' || corner == 'b')
            {
                return (CalcConstH / 2) - Math.Sqrt(Math.Abs(sub1 * (1 - (Math.Pow(x, 2) / sub2))));
            }
            else //if (corner == 'c' || corner == 'd')
            {
                return (CalcConstH / 2) + Math.Sqrt(Math.Abs(sub1 * (1 - (Math.Pow((x - (2 * (CalcConstH / 2))), 2) / sub2))));
            }
        }

        //calculates the y value for a given x value and the hyperbola generated from points b and c 
        double calc_y_horiz(char corner, double sub1, double sub2, double x)
        {
            if (sub2 == 0)
            {
                sub2 = 1;
            }
            if (corner == 'a')
            {
                return (2 * (CalcConstV / 2)) - Math.Sqrt(Math.Abs(sub1 * (1 - Math.Pow((x - (CalcConstV / 2)), 2) / sub2)));
            }
            else if (corner == 'b')
            {
                return Math.Sqrt(sub1 * (1 - Math.Pow((x - (CalcConstV / 2)), 2) / sub2));
            }
            else if (corner == 'c')
            {
                return Math.Sqrt(sub1 * (1 - Math.Pow((x - (CalcConstV / 2)), 2) / sub2));
            }
            else //if (corner == 'd')
            {
                return (2 * (CalcConstV / 2)) - Math.Sqrt(Math.Abs(sub1 * (1 - Math.Pow((x - (CalcConstV / 2)), 2) / sub2)));
            }
        }

    }
}
