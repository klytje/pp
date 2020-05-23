using static System.Console;
using static System.Math;
using System;

public class mcint {
	private static Random rand = new Random();
	// f: integration function, a: start vector, b: end vector, N: number of sampled points
	public static (double, double) plainmc(Func<vector, double> f, vector a, vector b, int N) {
		double V = 1.0; // integration volume
		for (int i = 0; i < a.size; i++)
			V *= b[i] - a[i];
		double sum1 = 0, sum2 = 0; // sum and sum of squares
		for (int i = 0; i < N; i++) { // sample N random points
			vector rx = randomx(a, b); // get a random point
			double frx = f(rx);
			sum1 += frx;
			sum2 += frx*frx;
		}
		double mean = sum1/N;
		double sig = Sqrt(sum2/N - mean*mean)/Sqrt(N);
		return (mean*V, sig*V);
	}

	// generates a random point between a and b
	private static vector randomx(vector a, vector b) {
		vector x = new vector(a.size);
		for (int i = 0; i < a.size; i++)
			x[i] = a[i] + rand.NextDouble()*(b[i] - a[i]);
		return x;

	}
}
