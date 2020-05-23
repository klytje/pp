using static System.Console;
using static System.Math;
using System.Linq;
using System.IO;
using System;

public class main {
	public static void Main(string[] args) {
		var lines = File.ReadAllLines("rutherford.txt").Select(line => line.Split('\t')).Select(line => new {x = double.Parse(line[0]), y = double.Parse(line[1])}).ToList();
		int l = lines.Count(); // number of items
		vector x = new vector(l), y = new vector(l), dy = new vector(l); 
		for (int i = 0; i < l; i++) {
			x[i] = lines[i].x;
			y[i] = Log(lines[i].y); // logarithm of the input
			dy[i] = 0.05; // 5% uncertainty
		}
		Func<double, double>[] funcs = new Func<double, double>[]{t => 1, t => t}; 
		var fit = ls.qrfit(x, y, dy, funcs);
		double lambda = -fit.p[1]; 
		double dlambda = Sqrt(fit.cov[1, 1]);
		
		int N = 100; // number of points for the model
		double step = (x[x.size-1] - x[0])/N;
		WriteLine($"# Evaluating fit with parameters: lambda: {lambda:F4}, dlambda: {dlambda:F4}");
		WriteLine($"# This corresponds to a half-life of {Log(2)/lambda:F4} with uncertainty {dlambda/lambda*Log(2)/lambda:F4}");
		WriteLine($"# Compared to the listed Wikipedia value of: 3.6319(23)");
		WriteLine(string.Format("# {0, -8} {1, -10} {2, -10} {3, -10}", "x", "y", "y_up", "y_low"));
		for (double z = x[0]; z < x[x.size-1]; z += step) {
			WriteLine($"{z, -10:F3} {Exp(fit.best(z)), -10:F3} {Exp(fit.upper(z)), -10:F3} {Exp(fit.lower(z)), -10:F3}");
		}
	}
}
