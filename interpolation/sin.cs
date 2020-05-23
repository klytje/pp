using static System.Console;
using static System.Math;

class quadratic {
	public static void Main(string[] args) {
		double b = 2*PI; // the range is from 0 to this
		int len = 10;
		double step = b/len;
		vector x = new vector(len);
		vector y = new vector(len);
		for (int i = 0; i < len; i++) {
			x[i] = i*step;
			y[i] = Sin(x[i]);
		}
		
		double steps = 4; // interpolates 4 steps between points
	
		WriteLine("# DATA");
		WriteLine($"{"# x", -8} {"y", -8}");
		for (int i = 0; i < y.size; i++)
			WriteLine($"{x[i], -8:F3} {y[i], -8:F3}");

		WriteLine("\n\n# LINEAR");
		WriteLine($"{"# x", -8} {"sin(x)", -8} {"int", -8} {"expint", -8}");
		for (int i = 0; i < x.size-1; i++) {
			for (double z = x[i]; z < x[i+1]; z += (x[i+1] - x[i])/steps) {
				WriteLine($"{z, -8:F3} {interpolate.linear.spline(x, y, z), -8:F3} {interpolate.linear.integrate(x, y, z) - 1, -8:F3} {-Cos(z), -8:F3}");
			}
		}

		interpolate.quadratic qsin = new interpolate.quadratic(x, y);
		WriteLine("\n\n# QUADRATIC");
		WriteLine($"{"# x", -8} {"sin(x)", -8} {"int", -8} {"expint", -8} {"diff", -8} {"expdiff", -8}");
		for (int i = 0; i < x.size-1; i++) {
			for (double z = x[i]; z < x[i+1]; z += (x[i+1] - x[i])/steps) {
				WriteLine($"{z, -8:F3} {qsin.spline(z), -8:F3} {qsin.integrate(z) - 1, -8:F3} {-Cos(z), -8:F3} {qsin.differentiate(z), -8:F3} {Cos(z), -8:F3}");
			}
		}
		

		interpolate.cubic csin = new interpolate.cubic(x, y);
		WriteLine("\n\n# CUBIC");
		WriteLine($"{"# x", -8} {"sin(x)", -8} {"int", -8} {"expint", -8} {"diff", -8} {"expdiff", -8}");
		for (int i = 0; i < x.size-1; i++) {
			for (double z = x[i]; z < x[i+1]; z += (x[i+1] - x[i])/steps) {
				WriteLine($"{z, -8:F3} {csin.spline(z), -8:F3} {csin.integrate(z) - 1, -8:F3} {-Cos(z), -8:F3} {csin.differentiate(z), -8:F3} {Cos(z), -8:F3}");
			}
		}
	}
}
