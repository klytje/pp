using static System.Console;
using static System.Math;
using System;

public class main {
	private static Func<double, double> g = (x) => x*Cos(x)*Cos(x); // fit function
	private static Func<double, double> dg = (x) => Cos(x)*(Cos(x) - 2*x*Sin(x));
	private static Func<double, double> G = (x) => (2*x*(x + Sin(2*x)) + Cos(2*x))/8;

	public static void Main(string[] args) {	
		int n = 10; // number of hidden network nodes
		neuralnetwork network = new neuralnetwork(n, "Gaussian wavelet");
		double a = -PI, b = PI; // interval to generate points for
		int len = 40; // length of the training data set

		// computing the training data set
		WriteLine($"\n{"# x", -8} {"y", -8} {"dy", -8}, {"Gy", -8}");
		double[] xs = new double[len], ys = new double[len];
		for (int i = 0; i < len; i++) {
			xs[i] = a + i*(b - a)/(len - 1);
			ys[i] = g(xs[i]);
			WriteLine($"{xs[i], -8:F3} {ys[i], -8:F3} {dg(xs[i]), -8:F3} {G(xs[i]), -8:F3}");
		}
		
		// generating some slightly better starting parameters
		vector p = new vector(3*n);
		for (int i = 0; i < n; i++) {
			p[3*i] = a + i*(b - a)/(n - 1);
			p[3*i+1] = 1;
			p[3*i+2] = 1;
		}
		network.update(p); // update the network state with these new parameters
		network.train(xs, ys);

		double offset = G(xs[0]) - network.outputInt(xs[0]);
		WriteLine($"\n\nNetwork interpolation");
		WriteLine($"{"# input", -8} {"output", -8} {"diff", -8} {"int", -8}");
		for (double z = a; z <= b; z += 1.0/64)
			WriteLine($"{z, -8:F3} {network.output(z), -8:F3} {network.outputDiff(z), -8:F3} {network.outputInt(z) + offset, -8:F3}");
	}
}
