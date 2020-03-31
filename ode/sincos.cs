using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System;

public class main {
	static bool approx(double x, double y, double acc=1e-6, double eps=1e-6) {
		if (Abs(x-y) < acc) return true;
		if (Abs(x-y) < eps*Max(Abs(x), Abs(y))) return true;
		return false;
	}

	// ode: y''(x) = -y(x)
	// defining u0 = y', u1 = y''
	// then u0' = u1, u1' = -u0
	static Func<double, vector, vector> f = delegate(double x, vector y) {
		return new vector(y[1], -y[0]);
	};

	static void Main(string[] args) {
		double a = 0, b = 2*PI, h = 0.1, acc=1e-3, eps=1e-3;
		vector ya = new vector(0, 1);
		(List<double> xs, List<vector> ys, vector y) = ode.rk4(f, a, ya, b, h, acc, eps);
		WriteLine($"# xs[-1]: {xs[xs.Count-1]:F3}, b: {b:F3}; y[0]: {y[0]:F3}, sin(b): {Sin(b):F3}; y[1]: {y[1]:F3}, cos(b): {Cos(b):F3}");
		if (approx(y[0], Sin(b), acc, eps) & approx(y[1], Cos(b), acc, eps))
			WriteLine("# u'(b) = sin(b) and u''(b) = cos(b), test passed");
		else
			WriteLine("# u'(b) != sin(b) or u''(b) != cos(b), test failed");
		WriteLine(string.Format("# {0, -6} {1, -8} {2, -8} {3, -8} {4, -8}", "x", "u'", "sin(x)", "u''", "cos(x)"));
		for (int i = 0; i < xs.Count; i++) {
			WriteLine($"{xs[i], -8:F3} {ys[i][0], -8:F3} {Sin(xs[i]), -8:F3} {ys[i][1], -8:F3} {Cos(xs[i]), -8:F3}");
		}
	}
}
