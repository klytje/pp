using static System.Console;
using static System.Math;
using System;

public class main {
	public static void Main() {
		Func<vector, vector> f = delegate(vector z) {
			double x = z[0];
			return new vector((2 - x)*(6 - x));
		};
		vector x0 = new vector(1); x0[0] = 0; // start pos
		vector root = roots.newton(f, x0);
		root.print("The root is: ");
		f(root).print("The function value at the root:");

		f = delegate(vector z) {
			double x = z[0], y = z[1];
			return new vector((4 - x)*(6 - x), (y - x)*(y - 3));
		};
		x0 = new vector(0, 0);
		root = roots.newton(f, x0);
		root.print("The roots are: ");
		f(root).print("The function values at these roots: ");

		// Gradient of Rosenbrock
		WriteLine("\nExtremums of Rosenbrock's valley function");
		f = delegate(vector z) {
			double x = z[0], y = z[1];
			return new vector(2*(1-x) + 2*100*(y-x*x)*2*x, 2*100*(y-x*x)); // the gradient
		};
		x0 = new vector(2, 2);
		root = roots.newton(f, x0);
		root.print("The roots are");
		f(root).print("The function values at these roots: ");

		// Hydrogen atom
		WriteLine("\n# Hydrogen atom");
		double rmax = 8;
		Func<vector, vector> aux = delegate(vector z) {
			double eps = z[0];
			double frmax = Feps(eps, rmax);
			return new vector(frmax);
		};
		x0 = new vector(-1.0);
		root = roots.newton(aux, x0);
		WriteLine($"# rmax is {rmax}, energy is {root[0]}");
		WriteLine(string.Format("# {0, -6} {1, -8} {2, -8}", "r", "Feps(r)", "exact"));
		for (double r = 0; r <= rmax; r += rmax/100)
			WriteLine($"{r, -8:F3} {Feps(root[0], r), -8:F3} {r*Exp(-r), -8:F3}");
	}

	private static double Feps(double eps, double r) {
		double rmin = 1e-3; // dmitris idea
		Func<double, vector, vector> s = delegate(double x, vector y) {
			// -f''/2 - (1/r)f = eps*f =>
			// f'' = -2*(1/r + eps)f
			// define y0 = f, y1 = f', so
			// y0' = y1
			// y1' = -2*(-1/r + eps)y0
			return new vector(y[1], -2*(1/x + eps)*y[0]);
		};
		vector frmin = new vector(rmin*(1 - rmin), 1 - 2*rmin), frmax;
		(_, _, frmax) = ode.rk4(s, rmin, frmin, r, 0.001);
		return frmax[0]; // return f''
	}
}
