using static inverse_iteration;
using static System.Console;
using static System.Math;
using System;

public class main {
	public static void Main(string[] args) {
		matrix A = new matrix(3, 3);	
		A[0, 0] = 2; A[0, 1] = 0; A[0, 2] = 0;
		A[1, 0] = 0; A[1, 1] = 4; A[1, 2] = 5;
		A[2, 0] = 0; A[2, 1] = 4; A[2, 2] = 3;
		
		int iters;
		vector b0 = new vector(1, 1, 1);
		for (double i = -10; i <= 10; i+=0.1) {
			(_, _, iters) = eigens(A, i, b0);
			WriteLine($"{i, -10:F3} {iters, -10:F3}");
		}

		WriteLine("\n\n");
		A = new matrix(2, 2);
		A[0, 0] = 0; A[0, 1] = 1;
		A[1, 0] = -2; A[1, 1] = -3;

		b0 = new vector(1, -10);
		double step = 0.1;
		for (double i = -10; i <= 10; i+=step) {
			b0[1] = i;
			(_, _, iters) = eigens(A, 0, b0);
			WriteLine($"{i, -10:F3} {iters, -10:F3}");
		}	
	}
}
