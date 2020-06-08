using static System.Console;
using static System.Math;

class linear {
	public static void Main(string[] args) {
		vector x = new vector(0, 1, 2, 3);
		vector y = new vector(0, 2, 4, 6);
		vector siny = new vector(x.size);
		for (int i = 0; i < x.size; i++)
			siny[i] = Sin(x[i]);

		WriteLine(string.Format("# {0, -8} {1, -10} {2, -10} {3, -10} {4, -10}, {5, -10}", "x", "y", "integral", "sin(x)", "integral", "exactint"));
		double z;
		double steps = 4; // interpolates 4 steps between points
		for (int i = 0; i < x.size-1; i++) {
			for (int j = 0; j < steps; j++) {
				z = x[i] + (x[i+1] - x[i])*j/steps;
				WriteLine($"{z, -10:F3} {interpolate.linear.spline(x, y, z), -10:F3} {interpolate.linear.integrate(x, y, z), -10:F3} {interpolate.linear.spline(x, siny, z), -10:F3} {interpolate.linear.integrate(x, siny, z), -10:F3} {-Cos(z) + 1, -10:F3}");
			}
		}
	}
}
