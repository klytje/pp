using static System.Console;
using static qr_decomp;
using System;

public class main {
	static void Main(string[] args) {
		int n = 3, m = 3;
		matrix A = GenRandMatrix(n, m);
		matrix R = new matrix(m, m);
		matrix Q = A.copy();
		A.print($"QR-decomposition of the matrix A:");
		gs.decomp(Q, R);
		R.print($"Matrix R:");
		var QQT = Q.T*Q; // does not work within parantheses
		var QR = Q*R; // same
		QQT.print("Q^T Q:");
		QR.print("QR:");
		if (A.approx(QR)) WriteLine("QR = A, test passed.");
		else WriteLine("QR != A, test failed.");
		
		WriteLine("-------------------------------------\n");
		A = GenRandMatrix(n, m);
		vector b = GenRandVector(m);
		R = new matrix(m, m);
		//A[0, 0] = 1; A[0, 1] = 1; A[0, 2] = 1;
	   	//A[1, 0] = 0; A[1, 1] = 2; A[1, 2] = 5;
		//A[2, 0] = 2; A[2, 1] = 5; A[2, 2] = -1;
		//vector b = new vector(6, -4, 27);
		A.print($"Solving the equation Ax = b, with A:");
		b.print($"and b:\n");
		Q = A.copy();
		gs.decomp(Q, R);
		vector x = gs.solve(Q, R, b);
		x.print($"\nSolution is x:\n");
		var Ax = A*x;
		Ax.print($"Ax:\n");
		if (b.approx(Ax)) WriteLine("Ax = Q^T b, test passed.");
		else WriteLine("Ax != Q^T b, test failed.");

		WriteLine("--------------------------------------\n");
		A = GenRandMatrix(n, m);
		R = new matrix(m, m);
		Q = A.copy();
		gs.decomp(Q, R);
		matrix B = gs.inverse(Q, R); // B = A^-1
		A.print($"Solving for the inverse of A:");
		B.print($"Inverse is B:");
		matrix AB = A*B;
		AB.print($"AB:");
		matrix I = new matrix(n, m);
		for (int i = 0; i < n; i++)
			I[i, i] = 1;
		if (AB.approx(I)) WriteLine("AB = I, test passed.");
		else WriteLine("AB != I, test failed.");
	}

	private static vector GenRandVector(int m) {
		var rand = new Random(1);
		vector v = new vector(m);
		for (int i = 0; i < m; i++)
			v[i] = rand.NextDouble();
		return v;
	}

	private static matrix GenRandMatrix(int n, int m) {
		var rand = new Random(1);
		matrix M = new matrix(n, m);
		for (int i = 0; i < n; i++)
			for (int j = 0; j < m; j++)
				M[i, j] = rand.NextDouble();
		return M;
	}
}
