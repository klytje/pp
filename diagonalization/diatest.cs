using static System.Console;
using static jacobi;
using System;

public class main {
	static void Main(string[] args) {
		int n = 5;
		matrix D = GenRandSymMatrix(n, n);
		matrix A = D.copy();
		A.print($"Symmetric matrix A:");
		(vector e, matrix V, _) = cyclic(D);
		for (int i = 0; i < n; i++) {
			for (int j = i+1; j < n; j++) {
				D[j, i] = D[i, j]; // complete the matrix
			}
		}
		matrix VTAV = V.transpose()*A*V;
		e.print($"Eigenvalues e:");
		V.print($"Eigenvectors V:");
		A.print($"D matrix: ");
		VTAV.print($"Matrix V^TAV:");
		if (D.approx(VTAV)) WriteLine("V^TAV = D, test passed.");
		else WriteLine("V^TAV != D, test failed.");

		(vector e2, _, _) = lowest_eigens(A, 3);
		e.print($"Eigenvalues from cyclic method: ");
		e2.print($"Eigenvalues from row-by-row method: ");
	}

	private static matrix GenRandSymMatrix(int n, int m) {
		var rand = new Random();
		matrix M = new matrix(n, m);
		double d;
		for (int i = 0; i < n; i++) {
			for (int j = i; j < m; j++) {
				d = 10.0*rand.NextDouble();
				M[i, j] = d;
				M[j, i] = d;
			}
		}
		return M;
	}
}

