using static System.Console;
using static System.Math;
using static qr_decomp;
using static System.Double;
using System;

public class inverse_iteration {
	private static Random rand = new Random();
	private static double acc = 1e-3; // absolute error
	private static double eps = 1e-3; // relative error

	// Maximum number of iterations. This value was chosen such that the random tests often succeed. 
	private static int imax = (int) 1e6;

	public static (double, vector, int) eigens(matrix A, double mu, bool verbose = false) {
		vector b = new vector(A.size1);
		for (int i = 0; i < b.size; i++)
			b[i] = rand.NextDouble();
		return eigens(A, mu, b, verbose);
	}

	public static (double, vector, int) eigens(matrix A, double mu, vector b, bool verbose = false) {
		// Initialize the required matrices and objects.
		matrix I = new matrix(A.size1, A.size1);
		vector zero = new vector(A.size1);
		for (int i = 0; i < I.size1; i++)
			I[i, i] = 1;
		
		vector bu = b; // iterative update; b+1
		double C; 
		gs QR = new gs(A - mu*I);
		matrix inv = QR.inverse();
		if (Double.IsNaN(inv[0, 0]))
			WriteLine("Matrix is singular, cannot continue.");

		// Calculate the eigenvector through iteration. 
		int iterations = 0;
		do{
			iterations++;
			b = bu;
			C = (inv*b).norm();
			bu = inv*b/C;
			//vector abu2 = A*bu;
			//WriteLine($"{iterations, -10} {getEigenval(A, abu2, bu)}");
		} while (!sapprox(bu, b, verbose) && iterations < imax);
		
		// Calculate the eigenvalue. This is calculated as the average factor x required for Av = xv.
		vector abu = A*bu;
		return (getEigenval(A, abu, bu), rescale(bu), iterations);
	}
	private static double getEigenval(matrix A, vector abu, vector bu) {
		double e = 0; // eigenvalue
		int num = 0; // how many numbers have been averaged
		for (int i = 0; i < abu.size; i++)
			if (bu[i] != 0) { // cannot divide by 0
				num++; 
				e += abu[i]/bu[i];
			}
		return e /= num;
	}

	// check if two vectors are approximately equal up to a sign
	private static bool sapprox(vector v, vector u, bool verbose = false) {
		if (approx(v, u)) return true;
		if (approx(v, -1*u)) return true;
		if (verbose) {
			v.print("bk  :", "{0,10:f3}");
			u.print("bk+1:", "{0,10:f3}");
			WriteLine();
		}
		return false;
	}

	public static vector eigens2(matrix A, double mu) {
		vector b = new vector(A.size1);
		for (int i = 0; i < b.size; i++)
			b[i] = rand.NextDouble();
		return eigens2(A, mu, b);
	}

	public static vector eigens2(matrix A, double mu, vector b) {
		matrix I = new matrix(A.size1, A.size1);
		vector zero = new vector(A.size1);
		for (int i = 0; i < I.size1; i++) {
			I[i, i] = 1;
		}

		vector bu = b;
		int count = 0;
		matrix temp = A - mu*I;
		gs QR = new gs(temp);
		do {
			count++;
			b = bu;
			bu = QR.solve(b);
			if (count % 5 == 0) { // rayleigh update
				mu = bu.dot(b)/b.dot(b);
				temp = A - mu*I;
				QR = new gs(temp);
			}
			b.print("b:");
			bu.print("bu:");
		} while (!approx(zero, (A - mu*I)*bu));
		//} while (!approx(bu, b));
		return rescale(bu);
	}

	// checks if two vectors are approximately equal. also returns true if either contains NaN. 
	private static bool approx(vector u, vector v) {
		for (int i = 0; i < u.size; i++) {
			if (Double.IsNaN(u[i]) || Double.IsNaN(v[i])) {
				WriteLine("NaN encountered, unable to continue.");
				return true;
			}
			if (acc < Abs(u[i] - v[i]))
				return false;
			if (eps < Abs(u[i] - v[i])/Max(Abs(u[i]), Abs(v[i])))
				return false;
		}
		return true;
	}

	// tries to rescale a given eigenvector such that the largest entry is 1
	private static vector rescale(vector v) {
		double largest = 0;
		for (int i = 0; i < v.size; i++)
			largest = Max(largest, Abs(v[i]));

		for (int i = 0; i < v.size; i++)
			v[i] /= largest;
		return v;
	}
}
