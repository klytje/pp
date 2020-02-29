using static System.Console;
using static System.Math;
using static System.Double;
using static quad;
using System.IO;
using System;

class main {
	// format is Main [a] [b] [num]
	public static void Main(string[] args) {
		if (args.Length != 3) {
			WriteLine("Wrong number of arguments. Usage: Main [a] [b] [num]");
			return;
		}

		// UNHANDLED EXCEPTIONS FROM PARSE
		double a = double.Parse(args[0]);
		double b = double.Parse(args[1]);
		int num = int.Parse(args[2]);

		Func<double, double, double> gauss = (x, y) => Exp(-y*Pow(x, 2));
		Func<double, double> f, g; // temporary functions for the loop
        double norm, hamint; // temporary doubles for the loop
		double[] c = new double[num], E = new double[num]; // c is alpha
		double step = (b - a)/num;
		using (StreamWriter file = new StreamWriter("results.txt")) {
			file.WriteLine(string.Format("# {0, -5} {1, -7}", "a", "E"));
			for (int i = 0; i < num; i++) {
				c[i] = a + i*step;
				f = (x) => gauss(x, c[i]);
				g = (x) => (-Pow(c[i]*x, 2)/2 + c[i]/2 + Pow(x, 2)/2)*f(x); // probably works
				norm = o8av(f, NegativeInfinity, PositiveInfinity);
				hamint = o8av(g, NegativeInfinity, PositiveInfinity);
				E[i] = hamint/norm;
				file.WriteLine($"{c[i], -7:F3} {E[i], -7:F3}");
			}
		}
	}
}
