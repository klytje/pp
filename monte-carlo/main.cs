using static System.Console;
using static System.Math;
using System;

class main {
	static void Main(string[] args) {
		Func<vector, double> f = (x) => Sin(x[0])/x[0];
		vector a = new vector(0.0), b = new vector(PI);
		int N = (int) 1e4;
		double exp = 1.852; // expected 
		(double res, _) = mcint.plainmc(f, a, b, N); // result

		WriteLine(string.Format("\n{0, -28} {1, -8} {2, -8} {3, -8} {4, -8}", "function", "a", "b", "expected", "result"));
		WriteLine($"{"Sin(x)/x", -28} {0, -8} {PI, -8:F3} {exp, -8:F3} {res, -8:F3}");
		
		f = (x) => Pow(PI*PI*PI*(1 - Cos(x[0])*Cos(x[1])*Cos(x[2])), -1);
		a = new vector(0, 0, 0); b = new vector(PI, PI, PI);
		N = (int) 1e5;
		exp = 1.392;
		(res, _) = mcint.plainmc(f, a, b, N);
		WriteLine($"{"(pi³(1-cosx*cosy*cosz))⁻¹", -28} {0, -8} {PI, -8:F3} {exp, -8:F3} {res, -8:F3}");

		WriteLine("\n\n# Error dependence on N");
		WriteLine($"{"# N", -12} {"err", -12} {"sqrtN*c", -12}");
		f = (x) => Sin(x[0])/x[0]; // same function as earlier
		a = new vector(0.0); b = new vector(PI);
		int Nmin = (int) 1e2, Nmax = (int) 1e5;
		int steps = (int) 1e3, stepsize = (Nmax - Nmin)/steps;
		double err, scale = 0.102*Sqrt(Nmin);
		for (N = Nmin; N <= Nmax; N += stepsize) {
			(_, err) = mcint.plainmc(f, a, b, N);
			WriteLine($"{N, -12:F6} {err, -12:F6} {scale/Sqrt(N), -12:F6}");
		}
	}
}
