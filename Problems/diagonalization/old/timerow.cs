using static System.Console;
using static System.Math;
using static jacobi;
using System.Diagnostics; 
using System;

public class main {
	public static void Main(string[] args) {
		//Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
		int max = 1000; // max n
		int step = 20;
		double scale = 2.0/625; // 1000*1000*x = 3200
		Stopwatch timer = new Stopwatch();
		WriteLine(string.Format("# {0, -8} {1, -8} {2, -8}", "n", "1row", "n^2"));
		tol = 1e-6;
		for (int n = 10; n < max; n += step) {
			matrix A = GenRandSymMatrix(n, n);
			timer.Start();
			lowest_eigens(A, 1); // only the first eigenvalue
			timer.Stop();

			WriteLine($"{n, -8} {timer.ElapsedMilliseconds, -8:F3} {Pow(n, 2)*scale, -8:F3}");
			timer.Reset();
		}
	}

	private static matrix GenRandSymMatrix(int n, int m) {
		var rand = new Random();
		matrix M = new matrix(n, m);
		double d;
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++) {
				d = 10.0*rand.NextDouble();
				M[i, j] = d;
				M[j, i] = d;
			}
		}
		return M;
	}
}
