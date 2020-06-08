using static System.Console;
using static System.Math;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

class main {
	public static void Main(string[] args) {
		// parse arguments
		var pars = args.Select(s => s.Split('=')).ToDictionary(s => s[0], s => double.Parse(s[1]));
		double xa = pars["xa"], xb = pars["xb"], y0 = pars["y0"], y1 = pars["y1"], eps = pars["eps"];
		vector ya = new vector(y0, y1);

		// y0 = u, y1 = u'
		// y[0] = y0, y[1] = y1
		// y0' = y1, y1' = 1-y0 + e*y0^2, so
		Func<double, vector, vector> f = delegate(double x, vector y) {
			return new vector(y[1], 1-y[0] + eps*y[0]*y[0]);
		};
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();
		ode.rk23(f, xa, ya, xb, xlist:xs, ylist:ys);
		WriteLine(string.Format("# {0, -8} {1, -10}", "phi", "u"));
		for (int i = 0; i < xs.Count; i++) {
			WriteLine($"{xs[i], -10:F3} {ys[i][0], -10:F3}");
		}
	}
}
