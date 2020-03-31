using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System.Linq;
using System;

public class main {
	public static void Main(string[] args) {
		var pars = args.Select(s => s.Split('=')).ToDictionary(s => s[0], s=> double.Parse(s[1]));
		double N = pars["N"], Tc = pars["Tc"], Tr = pars["Tr"], b = pars["dur"];

		// ode: 
		// S' = -I*S / N*Tc
		// I' = I*S / N*Tc - I/Tc
		// R' = I/Tr
		Func<double, vector, vector> f = delegate(double x, vector y) {
			double dSdt = -y[0]*y[1]/(N*Tc);
			double dIdt = y[0]*y[1]/(N*Tc) - y[1]/Tr;
			double dRdt = y[1]/Tr;
			return new vector(dSdt, dIdt, dRdt);
		};
		double a = 0, h = 0.1, acc=1e-3, eps=1e-3;
		double S0 = N; // assuming the whole population is susceptible
		double I0 = 400; // initial number of infected
		double R0 = 0; // nobody are initially recovered / immune 
		vector ya = new vector(S0, I0, R0);
		(List<double> xs, List<vector> ys, _) = ode.rk4(f, a, ya, b, h, acc, eps);
		WriteLine("\n"); // required for some fancy gnuplot hacks
		WriteLine(string.Format("# {0, -10} {1, -12} {2, -12} {3, -12}", "t", "S", "I", "R"));
		for (int i = 0; i < xs.Count; i++) {
			WriteLine($"{xs[i], -12:F3} {ys[i][0], -12:F3} {ys[i][1], -12:F3} {ys[i][2], -12:F3}");
		}
	}
}
