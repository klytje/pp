using static System.Console;
using static System.Math;

class quadratic {
	public static void Main(string[] args) {
		vector x = new vector(0, 1, 2, 3);
		vector y = new vector(0, 2, 4, 6);
		vector siny = new vector(x.size);
		for (int i = 0; i < x.size; i++)
			siny[i] = Sin(x[i]);
		WriteLine(string.Format("# {0, -8} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10} {6, -10} {7, -10} {8, -10}", "x", "y", "integral", " different", "sin(x)", "integral", "different", "exactint", "exactdiff"));
		double steps = 4; // interpolates 4 steps between points
		interpolate.cubic cube = new interpolate.cubic(x, y);
		interpolate.cubic sin = new interpolate.cubic(x, siny);
		for (int i = 0; i < x.size-1; i++) {
			for (double z = x[i]; z < x[i+1]; z += (x[i+1] - x[i])/steps) {
				WriteLine($"{z, -10:F3} {cube.spline(z), -10:F3} {cube.integrate(z), -10:F3} {cube.differentiate(z), -10:F3} {sin.spline(z), -10:F3} {sin.integrate(z), -10:F3} {sin.differentiate(z), -10:F3} {-Cos(z) + 1, -10:F3} {Cos(z), -10:F3}");
			}
		}
	}
}
