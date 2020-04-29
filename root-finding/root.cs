using static System.Console;
using static System.Math;
using System;
using static ode; // for some reason the test.cs does not run without this

public class roots {
	private static int limit = 5; // maximum number of times we halve the step size
	/* find the roots of an equation. 
	 * f: the equation
	 * x: the starting point
	 * epsilon: accuracy goal
	 * dx: the length-scale used to calculate the Jacobian
	 */
	public static vector newton(Func<vector, vector> f, vector x, double epsilon=1e-3, double dx=1e-7) {
		vector fx = f(x), s, fs;
		while (fx.norm() > epsilon) {
			matrix J = jacobian(f, x, dx), R = new matrix(J.size2, J.size2);
			qr_decomp.gs.decomp(J, R);
			matrix B = qr_decomp.gs.inverse(J, R); // B = J^-1
			vector Dx = -B*f(x);
		   	double min = 1.0/Pow(2, limit), l = 1.0; // l is lambda
			do { // backtracking
				s = x + l*Dx; // current step
				fs = f(s);
				if (fs.norm() < (1 - l/2)*fx.norm())
					break; // accept the step
				l /= 2; // halve the step size
			} while (l > min);
			x = s;
			fx = fs;
		}
		return x;
	}

	public static matrix jacobian(Func<vector, vector> f, vector x, double dx=1e-7) {
		int n = x.size;
		matrix J = new matrix(n, n);
		vector fx = f(x), df;
		for (int k = 0; k < n; k++) {
			x[k] += dx; // update xk
			df = f(x) - fx; // eq 7
			for (int i = 0; i < n; i++)
				J[i, k] = df[i]/dx; // calculate the derivative
			x[k] -= dx; // clean up
		}
		return J;
	}
}
