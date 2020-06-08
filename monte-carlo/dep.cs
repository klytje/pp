using static System.Console;
using static System.Math;
using System;

class main {
	static void Main(string[] args) {
		WriteLine("# Error dependence on N");
		WriteLine($"{"# N", -12} {"err", -12} {"sqrtN*c", -12}");
		Func<vector, double> f = (x) => Sin(x[0])/x[0]; // same function as earlier
		vector a = new vector(0.0), b = new vector(PI);
		int Nmin = (int) 1e2, Nmax = (int) 1e5, N;
		int steps = (int) 1e3, stepsize = (Nmax - Nmin)/steps;
		double err, scale = 0.102*Sqrt(Nmin);
		for (N = Nmin; N <= Nmax; N += stepsize) {
			(_, err) = mcint.plainmc(f, a, b, N);
			WriteLine($"{N, -12:F6} {err, -12:F6} {scale/Sqrt(N), -12:F6}");
		}

		WriteLine("\n# Sample map for a circle");
		f = (x) => {
			WriteLine($"{x[0], -8:F3} {x[1], -8:F3}");
			if ((x[0]*x[0] + x[1]*x[1]) < 1)
				return 1;
			return 0;
		};
		N = (int) 1e5;
		a = new vector(-1.4, 1.4);
		b = new vector(1.4, 1.4);
		mcint.stratified(f, a, b, N);
	}
}
