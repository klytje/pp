using static System.Math;
using System.Collections.Generic;
using System.Linq;
using System;

public class neuralnetwork {
	private Func<double, double> f; // activation function
	private Func<double, double> df; // differentiated activation function
	private Func<double, double> F; // integrated activation function
	private neuron[] neurons;

	private void initGaussianWavelet() {
		f = (x) => x*Exp(-x*x);
		df = (x) => (1 - 2*x*x)*Exp(-x*x);
		F = (x) => -Exp(-x*x)/2;
	}

	// create a new neural network with n neurons and the activation function f
	public neuralnetwork(int n, string type) {
		Dictionary<string, Action> typeDict = new Dictionary<string, Action>{
			{"Gaussian wavelet", initGaussianWavelet}
		};
		typeDict[type](); // initialize to the requested type
		neurons = new neuron[n];
		for (int i = 0; i < n; i++) {
			neurons[i] = new neuron();
		}
	}

	public double output(double x) {
		double sum = 0;
		foreach (neuron n in neurons) // iterate through all the hidden neurons
			sum += n.w*f((x - n.a)/n.b);
		return sum;
	}

	// outputs the differentiated function
	public double outputDiff(double x) {
		double sum = 0;
		foreach (neuron n in neurons)
			sum += n.w*df((x - n.a)/n.b)/n.b; // chain rule
		return sum;
	}

	public double outputInt(double x) {
		double sum = 0;
		foreach (neuron n in neurons)
			sum += n.w*F((x - n.a)/n.b)*n.b; // inverse chain rule
		return sum;
	}

	public void train(double[] xs, double[] ys) {
		Func<vector, double> chi2 = delegate(vector x) {
			update(x);
			double sum = 0;
			for (int i = 0; i < xs.Length; i++)
				sum += Pow(output(xs[i]) - ys[i], 2);
			return sum/xs.Length; // this is just the chi2 statistic
		};
		vector p = plist();
		(vector min, _) = mini.qnewton(chi2, p, 1e-4);
		update(min);
	}

	// I really dislike storing the parameters in a single list, but it is necessary for the minimization. this method just extracts the parameters from each neuron and returns a vector with them
	public vector plist() {
		List<List<double>> plist = new List<List<double>>();
		foreach (neuron n in neurons)
			plist.Add(n.getp());
		return new vector(plist.SelectMany(x => x).ToList().ToArray()); // SelectMany just flattens the list of lists
	}

	// likewise, this method just updates the parameter state of the network to the given vector
	public void update(vector p) {
		int i = 0;
		foreach (neuron n in neurons) {
			n.setp(p[i], p[i+1], p[i+2]);
			i += 3;
		}
	}

	private class neuron {
		public double a = 1;
		public double b = 1;
		public double w = 1;
		
		public neuron(){}
		public neuron(double a, double b, double w) {
			setp(a, b, w);
		}

		public void setp(double a, double b, double w) {
			this.a = a;
			this.b = b;
			this.w = w;
		}

		public List<double> getp() {
			return new List<double>(){a, b, w};
		}
	}
}
