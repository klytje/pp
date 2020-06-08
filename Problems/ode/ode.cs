using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System.Collections;
using System;

public class ode {
	/* param f: from the equation dydx = f(x, y)
	 * param x: current x value
	 * param y: other parameter values
	 * param h: step size
	 * returns (y(t+h), err)
	*/
	public static (vector, vector) rkstep4(Func<double, vector, vector> f, double x, vector y, double h) {
		vector k0 = f(x, y);
		vector k1 = f(x+h/2, y+h*k0/2);
		vector k2 = f(x+h/2, y+h*k1/2);
		vector k3 = f(x+h, y+h*k2);
		vector k = k0/6 + k1/3 + k2/3 + k3/6;
		return (y+k*h, (k-k2)*h); // not so sure about the error
	}

	/* param f: from the equation dydx = f(x, y)
	 * param a: start point
	 * param y: parameter values for x=a
	 * param b: end point
	 * param h: initial step size
	 * param acc: absolute accuracy
	 * param eps: relative accuracy
	*/ 
	public static (List<double>, List<vector>, vector) rk4(Func<double, vector, vector> f, double a, vector y, double b, double h, double acc=1e-6, double eps=1e-6) {
		int n = y.size;
		List<double> xs = new List<double>(); // we don't know the sizes of these vectors, so we use lists
		List<vector> ys = new List<vector>(); // same goes here
		while(a < b) {
			if (b < a+h) // fixes the final step to fit within the bounds
				h = b-a;
			(vector yh, vector err) = rkstep4(f, a, y, h);
			vector tol = new vector(n);
			for (int i = 0; i < n; i++) {
				tol[i] = (eps*Abs(yh[i]) + acc)*Sqrt(h/(b-a)); // eq 41
				if (err[i] == 0)
					err[i] = tol[i]/4; // we divide by err later, so it cannot be zero. this is Dmitris way of turning it into something meaningful. 
			}
			double factor = Abs(tol[0]/err[0]);
			for (int i = 0; i < n; i++) { 
				factor = Min(factor, Abs(tol[i]/err[i])); // we have to take the smallest required step (hence min)
			}
			double hnew = h*Pow(factor, 0.25)*0.95;
			bool reject = false;
			for (int i = 0; i < n; i++) {
				if (Abs(err[i]) > tol[i]) {
					reject = true;
					break;
				}
			}
			
			if (!reject) {
				a += h; y = yh;
				xs.Add(a); ys.Add(y);
			}
			h = hnew; // we always want to update the step size
		}
		return (xs, ys, y);
	}
}
