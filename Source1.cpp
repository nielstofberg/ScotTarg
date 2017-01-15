//half of the distance between sensors (note: the target must be square) 
float d = .5;

//calculates the y value for a given x value and the hyperbola generated from points a and b 
float calc_y_vert_neg(char corner, float sub1, float sub2, float x) 
{
	if (corner == 'a' || corner == 'b') 
	{ 
		return d + sqrt(sub1 * (1 - (pow(x, 2) / sub2))); 
	} 
	if (corner == 'c' || corner == 'd') 
	{
		return d - sqrt(sub1 * (1 - (pow((x - (2 * d)), 2) / sub2))); 
	} 
}

float calc_y_vert_pos(char corner, float sub1, float sub2, float x) 
{
	if (corner == 'a' || corner == 'b') 
	{
		return d - sqrt(sub1 * (1 - (pow(x, 2) / sub2)));
	}
	if (corner == 'c' || corner == 'd')
	{
		return d + sqrt(sub1 * (1 - (pow((x - (2 * d)), 2) / sub2)));
	}
}

//calculates the y value for a given x value and the hyperbola generated from points b and c 
float calc_y_horiz(char corner, float sub1, float sub2, float x) 
{
	if (corner == 'a')
	{
		return (2 * d) - sqrt(sub1 * (1 - pow((x - d), 2) / sub2));
	}
	if (corner == 'b')
	{
		return sqrt(sub1 * (1 - pow((x - d), 2) / sub2));
	}
	if (corner == 'c')
	{
		return sqrt(sub1 * (1 - pow((x - d), 2) / sub2));
	}
	if (corner == 'd')
	{
		return (2 * d) - sqrt(sub1 * (1 - pow((x - d), 2) / sub2)); 
	}
}

//this will run recursively to find the closest value using a sort of binary search 
//the basic idea here is that we'll calculate y for both hyperbolas for two different 
//values to figure out which is closer 
void intersect_perp( char corner, float *d_vert, float *d_horiz_sub1, float *d_horiz_sub2, float *d_bc_sub1, float *d_bc_sub2, float *x, float *y, float lower, float upper, unsigned char level) 
{
	float x1, x2, y_horiz1, y_horiz2, y_vert1, y_vert2, diff1, diff2, quarter = (upper - lower) / 4; 
	level++; 
	//printf("\nlevel %i: %f to %f, midpoint: %f\n", level, lower, upper, lower + (upper - lower) / 2); 
	//we're going to try two different x values, one at 25% and the other at 75% of the way between the bounds 
	x1 = lower + quarter; 
	x2 = upper - quarter; //we need to adjust the equation a bit based on whether the signal hit a first (negative) 
	if (*d_vert < 0) { y_horiz1 = calc_y_vert_neg(corner, *d_horiz_sub1, *d_horiz_sub2, x1); 
	y_horiz2 = calc_y_vert_neg(corner, *d_horiz_sub1, *d_horiz_sub2, x2);
	}
	else 
	{
		y_horiz1 = calc_y_vert_pos(corner, *d_horiz_sub1, *d_horiz_sub2, x1); 
		y_horiz2 = calc_y_vert_pos(corner, *d_horiz_sub1, *d_horiz_sub2, x2); 
	}
	y_vert1 = calc_y_horiz(corner, *d_bc_sub1, *d_bc_sub2, x1); 
	diff1 = fabs(y_vert1 - y_horiz1); 
	y_vert2 = calc_y_horiz(corner, *d_bc_sub1, *d_bc_sub2, x2); 
	diff2 = fabs(y_vert2 - y_horiz2);
	//printf("x1 = %f, y_vert1 = %f, y_horiz1 = %f, diff1 = %f\n", x1, y_horiz1, y_vert1, diff1); 
	//printf("x2 = %f, y_vert2 = %f, y_horiz2 = %f, diff2 = %f\n", x2, y_horiz2, y_vert2, diff2); 
	
	//check which x value got us closer (in this case, x1 did) 
	//we will then recurse to 0% to 50% of the current range 
	if (diff1 < diff2) 
	{
		*x = x1; 
		//close enough 
		if (diff1 < accuracy) 
		{
			*y = (y_horiz1 + y_vert1) / 2; 
			return; 
		} 
		//check to make sure we don't recurse too far 
		if (level > max_level) 
		{
			//printf("too far...\n");
			*y = (y_horiz1 + y_vert1) / 2;
			return; 
		} 
		//recurse with limits on either side of the better x value 
		intersect_perp(corner, d_vert, d_horiz_sub1, d_horiz_sub2, d_bc_sub1, d_bc_sub2, x, y, lower, upper - 2 * quarter, level); 
	} //x2 is closer, so recurse to 50% to 100% of the current range 
	else 
	{ 
		*x = x2; 
		//do the same stuff as above but with the other x value 
		if (diff2 < accuracy) 
		{
			*y = (y_horiz2 + y_vert2) / 2; 
			return; 
		} 
		if (level > max_level) 
		{
			//printf("too far...\n");
			*y = (y_horiz2 + y_vert2) / 2; 
			return; 
		}
		intersect_perp(corner, d_vert, d_horiz_sub1, d_horiz_sub2, d_bc_sub1, d_bc_sub2, x, y, lower + 2 * quarter, upper, level); 
	} 
}

//find the intersection given TDOA differences between points ab and bc 
//(note: these need to already have been multiplied by the propagation speed) 
void find_coords(float d_ab, float d_bc, float d_cd, float d_ad, float *x, float *y) 
{ 
	float min_x, max_x, d_ab_sub1, d_ab_sub2, d_bc_sub1, d_bc_sub2, d_cd_sub1, d_cd_sub2, d_ad_sub1, d_ad_sub2; 
	unsigned char level; //compute some substitutions (these are basically parts A and B of the standard hyperbola) 
	d_ab_sub1 = pow(((d_ab) / 2), 2); 
	d_ab_sub2 = pow(((d_ab) / 2), 2) - pow(d, 2); 
	printf("d_ab_sub1=%f, d_ab_sub2=%f\n", d_ab_sub1, d_ab_sub2); 
	d_bc_sub1 = pow(((d_bc) / 2), 2) - pow(d, 2); 
	d_bc_sub2 = pow(((d_bc) / 2), 2); 
	printf("d_bc_sub1=%f, d_bc_sub2=%f\n", d_bc_sub1, d_bc_sub2); 
	d_cd_sub1 = pow(((d_cd) / 2), 2); 
	d_cd_sub2 = pow(((d_cd) / 2), 2) - pow(d, 2); 
	printf("d_cd_sub1=%f, d_cd_sub2=%f\n", d_cd_sub1, d_cd_sub2); 
	d_ad_sub1 = pow(((d_ad) / 2), 2) - pow(d, 2); 
	d_ad_sub2 = pow(((d_ad) / 2), 2); 
	printf("d_ad_sub1=%f, d_ad_sub2=%f\n", d_ad_sub1, d_ad_sub2); max_x = (2 * d + d_ad) / 2;

	if (d_ad < 0) 
	{ 
		min_x = d - sqrt(d_ad_sub2 * (1 - pow((d * 2), 2) / d_ad_sub1)); 
		max_x = (2 * d + d_ad) / 2; 
	} 
	else 
	{ 
		min_x = (2 * d) - (2 * d + d_ad) / 2; 
		max_x = d + sqrt(d_ad_sub2 * (1 - pow((d * 2), 2) / d_ad_sub1)); 
	} 
	printf("\nd_ad= %f, d_ab=%f, min_x=%f, max_x=%f\n", d_ad, d_ab, min_x, max_x); //start the recrusive intersection finder 
	level = 0; 
	intersect_perp('a', &d_ab, &d_ab_sub1, &d_ab_sub2, &d_ad_sub1, &d_ad_sub2, x, y, min_x, max_x, level); 
	printf("final answer: %f, %f\n", *x, *y); 
	printf("\nd_ad= %f, d_cd=%f, min_x=%f, max_x=%f\n", d_ad, d_cd, min_x, max_x); //start the recrusive intersection finder 
	level = 0; 
	intersect_perp('d', &d_cd, &d_cd_sub1, &d_cd_sub2, &d_ad_sub1, &d_ad_sub2, x, y, min_x, max_x, level); 
	printf("final answer: %f, %f\n", *x, *y); //at this point, this may be the min or it may be the max x value to use 
	max_x = (2 * d + d_bc) / 2; //arrived at b before c 
	if (d_bc < 0) 
	{ 
		min_x = d - sqrt(d_bc_sub2 * (1 - pow((d * 2), 2) / d_bc_sub1)); 
		max_x = (2 * d + d_bc) / 2; 
	} 
	else 
	{
		min_x = (2 * d) - (2 * d + d_bc) / 2; 
		max_x = d + sqrt(d_bc_sub2 * (1 - pow((d * 2), 2) / d_bc_sub1)); 
	}
	printf("\nd_ab= %f, d_bc=%f, min_x=%f, max_x=%f\n", d_ab, d_bc, min_x, max_x); //start the recrusive intersection finder 
	level = 0; 
	intersect_perp('b', &d_ab, &d_ab_sub1, &d_ab_sub2, &d_bc_sub1, &d_bc_sub2, x, y, min_x, max_x, level); 
	printf("final answer: %f, %f\n", *x, *y); printf("\nd_bc= %f, d_cd=%f, min_x=%f, max_x=%f\n", d_bc, d_cd, min_x, max_x); //start the recrusive intersection finder 
	level = 0; 
	intersect_perp('c', &d_cd, &d_cd_sub1, &d_cd_sub2, &d_bc_sub1, &d_bc_sub2, x, y, min_x, max_x, level); 
	printf("final answer: %f, %f\n", *x, *y); 
}