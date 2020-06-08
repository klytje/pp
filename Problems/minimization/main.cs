using static System.Math;
using static System.Console;
using System.Linq;
using System.IO;
using System;

public class main {
	public static void Main(string[] args) {
		// Rosenbrock's valley function
		WriteLine("Rosenbrock's valley function:");
		WriteLine($"Expected minimum: (a, a*a) = (1, 1)");
		double eps = 1e-4;
		Func<vector, double> f = delegate(vector z) {
			double x = z[0], y = z[1];
			return Pow(1 - x, 2) + 100*Pow(y - x*x, 2);
		};
		vector x0 = new vector(0, 0), min;
		int n; // steps
		x0.print("Starting point:");
		(min, n) = mini.qnewton(f, x0, eps);
		min.print($"Minimum reached in {n} steps:");

		// Himmeblau's function
		WriteLine("\nHimmelblau's function:");
		WriteLine("Reference minimums: (3.0, 2.0), (-2.81, 3.13), (-3.78, -3.28), (3.58, -1.85)");
		f = delegate(vector z) {
			double x = z[0], y = z[1];
			return Pow(x*x + y - 11, 2) + Pow(x + y*y - 7, 2);
		};
		x0.print("\nStarting point:");
		(min, n) = mini.qnewton(f, x0, eps);
		min.print($"Minimum reached in {n} steps:");

		x0 = new vector(-6, -6);	
		x0.print("\nStarting point:");
		(min, n) = mini.qnewton(f, x0, eps);
		min.print($"Minimum reached in {n} steps:");

		// Higgs discovery
		WriteLine("\nHiggs discovery");
		var lines = File.ReadAllLines("higgs.txt").Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)).Select(line => new {x = double.Parse(line[0]), y = double.Parse(line[1]), z = double.Parse(line[2])}).ToList(); // maps every line in higgs.txt to a (x, y, z) double value. RemoveEmptyEntries is required for some reason - probably due to the format of the .txt file?
		int l = lines.Count(); // number of lines
		vector E = new vector(l), sig = new vector(l), err = new vector(l);
		for (int i = 0; i < l; i++) { // not really necessary, but it tidies up the rest of the code
			E[i] = lines[i].x;
			sig[i] = lines[i].y;
			err[i] = lines[i].z;
		}
		Func<double, double, double, double, double> F = (e, m, g, a) => a/(Pow(e-m, 2) + g*g/4); // Breit-Wigner
		Func<vector, double> chi2 = delegate(vector z) {
			double m = z[0], gamma = z[1], A = z[2];
			double sum = 0;
			for (int i = 0; i < l; i++) {
				sum += Pow(F(E[i], m, gamma, A) - sig[i], 2)/Pow(err[i], 2);
			}
			return sum;
		};
		x0 = new vector(120, 2, 6); // i have no idea which starting point to use, so these are dmitris values
		(min, n) = mini.qnewton(chi2, x0, eps);
		WriteLine($"Fitted values in {n} steps: mass: {min[0]:F3}, gamma: {min[1]:F3}, sqrt(chi2/n): {chi2(min)/l:F3}");
	}
}
