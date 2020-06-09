using static System.Console;
using static jacobi;
using System;

public class main {
	static void Main(string[] args) {
		int n = 5;
		matrix D = GenRandSymMatrix(n, n);
		matrix A = D.copy();
		A.print($"Symmetric matrix A:");
		(vector e, matrix V, int n1) = cyclic(D);
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
		
		matrix B = A.copy();
		(vector e2, _, int n2) = lowest_eigens(A, 3);
		(vector e4, _, int n4) = highest_eigens(A, 3);
		e.print($"\nEigenvalues from cyclic method found after {n1, 3} rotations:               ");
		e2.print($"3 lowest eigenvalues from row-by-row method found after {n2, 3} rotations:  ");
		e4.print($"3 highest eigenvalues from row-by-row method found after {n4, 3} rotations: ");	

		(vector e3, _, int n3) = classic(B);
		e3.print($"Eigenvalues from the classic method found after {n3, 3} rotations:          ");
		WriteLine();
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

