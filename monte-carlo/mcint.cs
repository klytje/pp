using static System.Console;
using static System.Math;
using System;

public class mcint {
	private static Random rand = new Random();
	private static int Nmin = 50; // minimum number of points for the stratified sampling
	private static double Nfrac = 1.0/20; // fraction of points used to estimate variance

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

	public static (double, double) stratified(Func<vector, double> f, vector a, vector b, int N, double sig=1e-3, double eps=1e-3) {
		(double res, double err) = plainmc(f, a, b, N);
		// check if result is acceptable
		if (err < sig + eps*Abs(res) || N <= Nmin)
			return (res, err);

		// otherwise we need to split along a dimension
		double lv, hv;
		int Ns = (int) (N*Nfrac); // number of samples in each dimension
		double bvar = 0, blvar = 0, bhvar = 0; // best (highest) variance found, best lower var, best higher var
		int bdim = 0; // best dimension index to split
		double lvar, hvar; // error of lower and upper (higher) half
		double len; // half length of dimension (b[i] - a[i])
		for (int i = 0; i < a.size; i++) {
			len = (b[i] - a[i])/2; // the original length of the dimension
			b[i] -= len; // lower half
			(lv, lvar) = plainmc(f, a, b, Ns);
			b[i] += len; // restore original b
			a[i] += len; // upper half
			(hv, hvar) = plainmc(f, a, b, Ns);
			a[i] -= len; // restore original a
			lvar = lvar*lvar*Ns; // convert error to variance
			hvar = hvar*hvar*Ns;
			double dv = Abs(lv - hv);
			if (bvar < dv) {
				blvar = Sqrt(lvar);
				bhvar = Sqrt(hvar);
				bvar = dv;
				bdim = i;
			}
		}

		double lerr, herr; // lower and higher errors
		int lN = (int) (blvar/(blvar + bhvar)*N);
		int hN = N - lN;
		len = (b[bdim] - a[bdim])/2;
		b[bdim] -= len;
		(lv, lerr) = stratified(f, a, b, lN);
		b[bdim] += len;
		a[bdim] += len;
		(hv, herr) = stratified(f, a, b, hN);
		a[bdim] -= len;
		return ((lv + hv)/2, Sqrt(Pow(lerr + herr, 2)));
	}

	// generates a random point between a and b
	private static vector randomx(vector a, vector b) {
		vector x = new vector(a.size);
		for (int i = 0; i < a.size; i++)
			x[i] = a[i] + rand.NextDouble()*(b[i] - a[i]);
		return x;
	}
}
