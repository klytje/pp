using static System.Math;
using static System.Console;
using System;

public class jacobi {
	public static double tol = 1e-6; // tolerance for deciding when to stop
	public static (vector, matrix, int) lowest_eigens(matrix A, int r) {
		int n = A.size1;
		matrix V = new matrix(n, n); // eigenvectors
		vector e = new vector(r); // eigenvalues
		for (int i = 0; i < n; i++) {
			V[i, i] = 1; // initialized to I
		}
		int iterations = 0;
		for (int i = 0; i < r; i++) {
			bool change = true;
			while (change) {
				iterations += 1;
				e[i] = A[i, i];
				rowsweep(A, V, i);
				change = !(e[i]-tol <= A[i, i] & A[i, i] <= e[i]);
			}
		}
		return (e, V, iterations);

	}

	public static (vector, matrix, int) cyclic(matrix A) {
		int n = A.size1;
		matrix V = new matrix(n, n); // eigenvectors
		vector e = new vector(n); // eigenvalues
		for (int i = 0; i < n; i++) {
			V[i, i] = 1; // initialized to I
		}
		bool change = true;
		int sweeps = 0;
		while (change) {
			sweeps += 1;
			for (int i = 0; i < n; i++)
				e[i] = A[i, i]; // update eigenvalues
			sweep(A, V); // perform a sweep
			change = changed(A, e); // check if the eigenvalues were actually changed in this iteration
		}
		return (e, V, sweeps);
	}

	// compare the diagonal of A with the entries of e
	private static bool changed(matrix A, vector e) {
		for (int i = 0; i < e.size; i++) {
			if (!(e[i]-tol <= A[i, i] & A[i, i] <= e[i]+tol))
				return true;
		}
		return false;
	}

	// perform a 'sweep' for a single row
	private static void rowsweep(matrix A, matrix V, int r) {
		for (int q = r+1; q < A.size1; q++) // p+1 ensures we only get the off-diagonal elements
			rotate(A, V, r, q);
	}

	private static void sweep(matrix A, matrix V) {
		int n = A.size1;
		for (int p = 0; p < n; p++)
			for (int q = p+1; q < n; q++) // p+1 ensures we only get the off-diagonal elements
				rotate(A, V, p, q);
	}

	private static void rotate(matrix A, matrix V, int p, int q) {
		int n = A.size1;
		double theta = Atan2(2*A[p, q], A[q, q] - A[p, p])/2;
		//double theta = Atan2(2*A[q, p], A[p, p] - A[q, q])/2;
		double c = Cos(theta), s = Sin(theta);
		double App = A[p, p], Aqq = A[q, q], Apq = A[p, q];
		A[p, p] = c*c*App + s*s*Aqq - 2*s*c*Apq;
		A[q, q] = s*s*App + c*c*Aqq + 2*s*c*Apq;
		A[p, q] = s*c*(App - Aqq) + (c*c - s*s)*Apq;
		for (int i = 0; i < p; i++) { // 0 < i < p
			double Aip = A[i, p], Aiq = A[i, q];
			A[i, p] = c*Aip - s*Aiq;
			A[i, q] = s*Aip + c*Aiq;
		}
		for (int i = p+1; i < q; i++) { // p < i < q
			double Api = A[p, i], Aiq = A[i, q];
			A[p, i] = c*Api - s*Aiq;
			A[i, q] = s*Api + c*Aiq;
		}
		for (int i = q+1; i < n; i++) { // q < i < n
			double Api = A[p, i], Aqi = A[q, i];
			A[p, i] = c*Api - s*Aqi;
			A[q, i] = s*Api + c*Aqi;
		}
		for (int i = 0; i < n; i++) { // rotate the eigenvectors
			double Vip = V[i, p], Viq = V[i, q];
			V[i, p] = c*Vip - s*Viq;
			V[i, q] = s*Vip + c*Viq;
		}
	}
}
