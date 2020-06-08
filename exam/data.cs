using static inverse_iteration;
using static System.Console;
using static System.Math;
using System;

public class main {
	public static void Main(string[] args) {
		matrix A = new matrix(3, 3);
		A[0, 0] = 9.93; A[0, 1] = 1.39; A[0, 2] = 5.77;
		A[1, 0] = 9.38; A[1, 1] = 6.29; A[1, 2] = 2.16;
		A[2, 0] = 5.83; A[2, 1] = 7.3; A[2, 2] = 6.81;
		
		A[0, 0] = 2; A[0, 1] = 0; A[0, 2] = 0;
		A[1, 0] = 0; A[1, 1] = 4; A[1, 2] = 5;
		A[2, 0] = 0; A[2, 1] = 4; A[2, 2] = 3;
		
		vector b; double e; int iters;
		(e, b, iters) = eigens(A, 20); // -10, 20
	}
}
