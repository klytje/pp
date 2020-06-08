using static System.Console;
using static System.Math;
using System.Numerics;
using System.Collections.Generic;
using System;
using System.Linq;

class main {
	static void Main(string[] args) {
		// y[0] = y, y[1] = y'
		// y'(x) = y(x)(1 - y(x)), so 
		Func<double, vector, vector> f = delegate(double x, vector y) {
			return new vector(y[0]*(1 - y[0]), 0);
		};
		double xa = 0, xb = 3;
		vector ya = new vector(0.5, 0);
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();
		ode.rk23(f, xa, ya, xb, xlist:xs, ylist:ys); // we don't care about the return value
		List<double> res = xs.Select(x => 1/(1 + Exp(-x))).ToList(); // expected
		WriteLine(string.Format("{0, -8:F3} {1, -8:F3} {2, -8:F3}", "x", "y", "expected"));
		for (int i = 0; i < xs.Count; i++)
			WriteLine($"{xs[i], -8:F3} {ys[i][0], -8:F3} {res[i], -8:F3}");
	}
}
