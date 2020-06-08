using static System.Console;
using static System.Math;
using static jacobi;
using System.Diagnostics; 
using System;

public class main {
	public static void Main(string[] args) {
		timeRow();
		WriteLine("\n");
		timeCyclic();
		WriteLine("\n");
		timeAllRows();
		WriteLine("\n");
		timeClassic();
	}

	public static void timeRow() {
		//Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
		int max = 800; // max n
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

	public static void timeAllRows() {
		//Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
		int max = 150; // max n
		int step = 10;
		double scale = 1.0/450; // 150^3*scale = 6500
		Stopwatch timer = new Stopwatch();
		WriteLine(string.Format("# {0, -6} {1, -8} {2, -8}", "n", "cyclic", "n^3"));
		for (int n = 10; n < max; n += step) {
			matrix A = GenRandSymMatrix(n, n);
			timer.Start();
			(_, _, _) = lowest_eigens(A, n); // all eigenvalues
			timer.Stop();
			WriteLine($"{n, -8} {timer.ElapsedMilliseconds, -8:F3} {Pow(n, 3)*scale, -8:F3}");
			timer.Reset();
		}
	}

	public static void timeCyclic() {
		//Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
		int max = 300; // max n
		int step = 10;
		double scale = 1.0/21600; // 300^3*scale = 1250
		Stopwatch timer = new Stopwatch();
		WriteLine(string.Format("# {0, -6} {1, -8} {2, -8}", "n", "cyclic", "n^3"));
		for (int n = 10; n < max; n += step) {
			matrix A = GenRandSymMatrix(n, n);
			timer.Start();
			cyclic(A);
			timer.Stop();

			WriteLine($"{n, -8} {timer.ElapsedMilliseconds, -8:F3} {Pow(n, 3)*scale, -8:F3}");
			timer.Reset();
		}
	}

	public static void timeClassic() {
		//Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
		int max = 300; // max n
		int step = 10;
		double scale = 1.0/21600; // 300^3*scale = 1250
		Stopwatch timer = new Stopwatch();
		WriteLine(string.Format("# {0, -6} {1, -8} {2, -8}", "n", "cyclic", "n^3"));
		for (int n = 10; n < max; n += step) {
			matrix A = GenRandSymMatrix(n, n);
			timer.Start();
			classic(A);
			timer.Stop();

			WriteLine($"{n, -8} {timer.ElapsedMilliseconds, -8:F3} {Pow(n, 3)*scale, -8:F3}");
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
