using static inverse_iteration;
using static System.Console;
using static System.Math;
using System;

public class main {
	public static void Main(string[] args) {
		// MANUAL TESTS
		writetitle("\nManual tests");
		WriteLine("e is short for eigenvalue, v for eigenvector.\nThese tests were performed with randomly generated 'approximate' eigenvectors.\n");
		vector b; double mu, e; int iters;
		matrix A = new matrix(3, 3); 

		A[0, 0] = 1; A[0, 1] = -3; A[0, 2] = 3;
		A[1, 0] = 3; A[1, 1] = -5; A[1, 2] = 3;
		A[2, 0] = 6; A[2, 1] = -6; A[2, 2] = 4;
		A.print("Using the matrix:");
		
		WriteLine($"\n{"e guess", -10} {"e found", -10} {"e expected", -10} {"v found", -22} {"v expected", -22}");
		mu = 0; // vals: -2, 4	vectors: (x-y, x, y), (.5, .5, 1)
		(e, b, _) = eigens(A, mu); 
		writeresult(mu, e, -2, b, "(x-y, x, y)");
		mu = 2;
		(e, b, _) = eigens(A, mu);
		writeresult(mu, e, 4, b, "(0.5, 0.5, 1)");


		A[0, 0] = 2; A[0, 1] = 0; A[0, 2] = 0;
		A[1, 0] = 0; A[1, 1] = 4; A[1, 2] = 5;
		A[2, 0] = 0; A[2, 1] = 4; A[2, 2] = 3;
		A.print("\n\nUsing the matrix:");
		
		WriteLine($"\n{"e guess", -10} {"e found", -10} {"e expected", -10} {"v found", -22} {"v expected", -22}");
		mu = 0; // vals: -1, 2, 8	vectors: (0, 1, -1), (1, 0, 0), (0, 1, 0.8)
		(e, b, _) = eigens(A, mu);
		writeresult(mu, e, -1, b, "(0, 1, -1)");
		mu = 1;
		(e, b, _) = eigens(A, mu);
		writeresult(mu, e, 2, b, "(1, 0, 0)");
		mu = 6;
		(e, b, _) = eigens(A, mu);
		writeresult(mu, e, 8, b, "(0, 1, 0.8)");

		// RANDOM TESTS
		writetitle("\nRandom tests");
		WriteLine("As one of the values of the eigenvector is scaled to 1, these can quickly be verified by eye.");
		A = genRandSymMatrix(3);
		A.print("\nUsing the matrix A:");
		WriteLine($"\n{"e guess", -10} {"e found", -10} {"v found", -22} {"Av", -22}");
		for (int i = -10; i <= 10; i +=2) {
			(e, b, iters) = eigens(A, i);
			// check if the iteration cap was reached. this shouldn't happen anymore
			if (iters == inverse_iteration.imax) {
				Console.ForegroundColor = ConsoleColor.DarkRed;
				writeresult(i, e, b, A*b);
				Console.ResetColor();
			}
			else writeresult(i, e, b, A*b);
		}

		A = genRandSymMatrix(2);
		A.print("\nUsing the matrix A:");
		WriteLine($"\n{"e guess", -10} {"e found", -10} {"v found", -15} {"Av", -15}");
		for (int i = -10; i <= 10; i +=2) {
			(e, b, iters) = eigens(A, i);
			if (iters == inverse_iteration.imax) {
				Console.ForegroundColor = ConsoleColor.DarkRed;
				writeresult(i, e, b, A*b);
				Console.ResetColor();
			}
			else writeresult(i, e, b, A*b);
		}
	}

	// apparently all truly random matrices should be invertible, so this should work just fine
	private static matrix genRandSymMatrix(int n) {
		var rand = new Random();
		matrix M = new matrix(n, n);
		double d;
		for (int i = 0; i < n; i++) {
			for (int j = i; j < n; j++) {
				d = 10.0*rand.NextDouble();
				M[i, j] = d;
				M[j, i] = d;
			}
		}
		return M;
	}

	private static void writeresult(double eguess, double eres, vector vres, vector av) {
		Write($"{eguess, -10:F3} {eres, -10:F3} ({vres[0], -6:F3}");
		// the following just prints the vectors nicely
		for (int i = 1; i < vres.size; i++)
			Write($" {vres[i], -6:F3}");
		Write($") ({av[0], -6:F3}");
		for (int i = 1; i < av.size; i++)
			Write($" {av[i], -6:F3}");
		Write(")\n");
	}

	private static void writeresult(double eguess, double eres, double eexp, vector vres, string vexp) {
		Write($"{eguess, -10:F3} {eres, -10:F3} {eexp, -10:F3} ({vres[0], -6:F3}");
		// the following just prints the vectors nicely
		for (int i = 1; i < vres.size; i++)
			Write($" {vres[i], -6:F3}");
		Write($") {vexp, -22}\n");
	}

	private static void writetitle(string title) {
		Console.ForegroundColor = ConsoleColor.DarkBlue;
		WriteLine(title);
		Console.ResetColor();
	}
}
