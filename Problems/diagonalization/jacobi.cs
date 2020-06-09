using static System.Math;
using static System.Console;
using System;

public class jacobi {
	public static double tol {get; set;} = 1e-6; // tolerance for deciding when to stop
	private static int rotations;
	
	private static double rev = 1; // 1: lowest eigens, -1: highest eigens
	public static (vector, matrix, int) highest_eigens(matrix A, int r) {
		rev = -1;
		(vector e, matrix V, int rotations) = lowest_eigens(A, r);
		rev = 1;
		return (e, V, rotations);
	}

	public static (vector, matrix, int) lowest_eigens(matrix A, int r) {
		rotations = 0;
		int n = A.size1;
		matrix V = new matrix(n, n); // eigenvectors
		vector e = new vector(r); // eigenvalues
		for (int i = 0; i < n; i++)
			V[i, i] = 1; // initialized to I
		for (int i = 0; i < r; i++) {
			bool change = true;
			while (change) {
				e[i] = A[i, i];
				rowsweep(A, V, i);
				change = !(e[i]-tol <= A[i, i] & A[i, i] <= e[i]);
			}
		}
		return (e, V, rotations);
	}

	public static (vector, matrix, int) cyclic(matrix A) {
		rotations = 0;
		int n = A.size1;
		matrix V = new matrix(n, n); // eigenvectors
		vector e = new vector(n); // eigenvalues
		for (int i = 0; i < n; i++)
			V[i, i] = 1; // initialized to I
		bool change = true;
		while (change) {
			for (int i = 0; i < n; i++)
				e[i] = A[i, i]; // update eigenvalues
			sweep(A, V); // perform a sweep
			change = changed(A, e); // check if the eigenvalues were actually changed in this iteration
		}
		return (e, V, rotations);
	}

	public static (vector, matrix, int) classic(matrix A) {
		rotations = 0;
		int n = A.size1;
		matrix V = new matrix(n, n); // eigenvectors
		vector e = new vector(n); // eigenvalues
		int[] qi = new int[n-1]; // highest row indices
		for (int p = 0; p < n-1; p++) {
			V[p, p] = 1; // initialize to I
			qi[p] = n-1; // initialize to the final value
			for (int q = p+1; q < n; q++) { // only upper diagonal
				if (Abs(A[p, qi[p]]) < Abs(A[p, q]))
					qi[p] = q;
			}
		}
		V[n-1, n-1] = 1; // does not get set in the loop above
		bool change = true; 
		while (change) {
			for (int i = 0; i < n; i++)
				e[i] = A[i, i]; // update eigenvalues
			for (int p = 0; p < n-1; p++) 
				rotate(A, V, p, qi[p], qi);
			change = changed(A, e);
		}
		return (e, V, rotations);

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
		rotations += 1;
		int n = A.size1;
		double theta = Atan2(rev*2*A[p, q], rev*(A[q, q] - A[p, p]))/2;
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

	// overloaded rotate, updates the provided highest row vector as well
	private static void rotate(matrix A, matrix V, int p, int q, int[] qi) {
		rotations += 1;
		int n = A.size1;
		double theta = Atan2(rev*2*A[p, q], rev*(A[q, q] - A[p, p]))/2;
		double c = Cos(theta), s = Sin(theta);
		double App = A[p, p], Aqq = A[q, q], Apq = A[p, q];
		A[p, p] = c*c*App + s*s*Aqq - 2*s*c*Apq;
		A[q, q] = s*s*App + c*c*Aqq + 2*s*c*Apq;
		A[p, q] = s*c*(App - Aqq) + (c*c - s*s)*Apq;
		for (int i = 0; i < p; i++) { // 0 < i < p
			double Aip = A[i, p], Aiq = A[i, q];
			A[i, p] = c*Aip - s*Aiq;
			A[i, q] = s*Aip + c*Aiq;
			if (Abs(A[i, qi[i]]) < Abs(A[i, q])) qi[i] = q;
			if (Abs(A[i, qi[i]]) < Abs(A[i, p])) qi[i] = p;	
		}
		for (int i = p+1; i < q; i++) { // p < i < q
			double Api = A[p, i], Aiq = A[i, q];
			A[p, i] = c*Api - s*Aiq;
			A[i, q] = s*Api + c*Aiq;
			if (Abs(A[p, qi[p]]) < Abs(A[p, i])) qi[p] = i;
			if (Abs(A[i, qi[i]]) < Abs(A[i, q])) qi[i] = q;
		}
		for (int i = q+1; i < n; i++) { // q < i < n
			double Api = A[p, i], Aqi = A[q, i];
			A[p, i] = c*Api - s*Aqi;
			A[q, i] = s*Api + c*Aqi;
			if (Abs(A[p, qi[p]]) < Abs(A[p, i])) qi[p] = i;
			if (Abs(A[q, qi[q]]) < Abs(A[q, i])) qi[q] = i;
		}
		for (int i = 0; i < n; i++) { // rotate the eigenvectors
			double Vip = V[i, p], Viq = V[i, q];
			V[i, p] = c*Vip - s*Viq;
			V[i, q] = s*Vip + c*Viq;
		}
	}
}
