using static System.Console;
using static System.Math;
using static quad;
using System.Linq;
using System;

class main {
	public static void Main(string[] args) {
		var arg = args.Select(s => s.Split('=')).ToDictionary(s => s[0], s => double.Parse(s[1]));
		double za = arg["za"], zb = arg["zb"], step = arg["step"];
		Func<double, double> C = (s) => Sin(Pow(s, 2));
		Func<double, double> S = (s) => Cos(Pow(s, 2));
		WriteLine(string.Format("# {0, -8} {1, -10}", "x", "y"));
		for (double z = za; z < zb; z += step) {
			WriteLine($"{o8av(C, 0, z), -10:F3} {o8av(S, 0, z), -10:F3}");
		}
	}
}
