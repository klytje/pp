using static System.Console;
using static System.Math;
using System;

public class mini {
	private static int limit = 5; // maximum number of times we can halve the step size
	private static double a = 1e-4; // alpha for the Armijo condition

	private static vector gradient(Func<vector, double> f, vector x, double dx=1e-7) {
		vector g = new vector(x.size); // final result
		double fx = f(x);
		for (int i = 0; i < x.size; i++) {
			fx = f(x);
			x[i] += dx; // increment
			g[i] = (f(x) - fx)/dx; // calculate dfdx
			x[i] -= dx; // decrement
		}
		return g;
	}

	// returns (min, steps)
	public static (vector, int) qnewton(Func<vector, double> f, vector x0, double eps) {
		int n = 0; // total number of steps
		vector x = x0.copy(), s; // position vectors
		double fx = f(x), fxs; // function values
		vector gx = gradient(f, x), gxs; // gradient vectors
		matrix B = matrix.id(x.size); // inverse Hessian, initialized to I
		while(eps < gx.norm()) { // continue until the sum of diffs is almost zero
			n++;
			vector Dx = -B*gx; // eq 6
			double min = 1.0/Pow(2, limit), l = 1.0; // min is the smallest step allowed, l is lambda
			do { // backtracking
				s = l*Dx; // step, eq 8
				fxs = f(x+s);
				if (l < min) {
					B = matrix.id(x.size); // advised to reset B if l < min by the text
					break;
				}
				l /= 2; // halve the step size
			}
			while (!(fxs < fx + a*s.dot(gx))); // armijo condition
			gxs = gradient(f, x+s);
			vector y = gxs - gx; // eq 12
			vector u = s - B*y;
			double denom = u.dot(y);
			if (Abs(denom) > eps) // eq 18, abs very important
				B.update(u, u, 1/denom); // dmitri has apparently already specified this operation
				// B += u.dot(u)/denom;
			
			// prepare for next iteration
			x = x+s;
			gx = gxs;
			fx = fxs;
		}
		return (x, n);
	}
}
