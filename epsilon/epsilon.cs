using static System.Console;
using static System.Math;

class main{
	public static int Main() {
		double x = 1;
		float y = 1F;

		while (1+x != 1) {
			x /= 2;
		}
		while ((float)(1F + y) != 1F) {
			y /= 2;
		}
		x *= 2;
		y *= 2F;

		Write($"double epsilon: {x}, float epsilon: {y}\n");
		Write($"2^-52: {Pow(2, -52)}, 2^-23: {Pow(2, -23)}\n");
		return 0;
	}
}
