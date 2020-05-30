using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System;

public class main {
	private static double K1 = 1; // the scale is irrelevant

	// method adapted from my previous python script
	public static Func<double, vector, vector> f = delegate(double x, vector y) {
		vector r1 = new vector(new double[]{y[0], y[1]});
		vector r2 = new vector(new double[]{y[2], y[3]});
		vector r3 = new vector(new double[]{y[4], y[5]});
		
		// only need these cubed anyway
		double r12 = Pow((r2 - r1).norm(), 3);
		double r13 = Pow((r3 - r1).norm(), 3);
		double r23 = Pow((r3 - r2).norm(), 3);

		vector dv1 = K1*(r2 - r1)/r12 + K1*(r3 - r1)/r13;
		vector dv2 = K1*(r1 - r2)/r12 + K1*(r3 - r2)/r23;
		vector dv3 = K1*(r1 - r3)/r13 + K1*(r2 - r3)/r23;

		vector z = new double[]{y[6], y[7], y[8], y[9], y[10], y[11], dv1[0], dv1[1], dv2[0], dv2[1], dv3[0], dv3[1]};
		return new vector(z);
	};

	public static void Main(string[] args) {
		// values are also adapted from my previous python script
		double xs1 = -1.0024277970, xs2 = 0.0041695061, vs1 = 0.3489048974, vs2 = 0.5306305100;
		vector ya = new vector(new double[]{xs1, xs2, -xs1, -xs2, 0, 0, vs1, vs2, vs1, vs2, -2*vs1, -2*vs2});

		double ta = 0, tb = 100, acc = 1e-3, eps = 1e-3, h=1e-3;
		(List<double> ts, List<vector> ys, vector y) = ode.rk4(f, ta, ya, tb, h, acc, eps);
		WriteLine($"{"# t", -8:F3} {"x1", -8:F3} {"y1", -8:F3} {"x2", -8:F3} {"y2", -8:F3} {"x3", -8:F3} {"y3", -8:F3}");
		for (int i = 0; i < ts.Count; i++)
			WriteLine($"{ts[i], -8:F3} {ys[i][0], -8:F3} {ys[i][1], -8:F3} {ys[i][2], -8:F3} {ys[i][3], -8:F3} {ys[i][4], -8:F3} {ys[i][5], -8:F3}\n\n");
	}
}
