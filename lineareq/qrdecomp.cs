using static matrix;
using static System.Math;
using static System.Console;

public class qr_decomp {
	public class gs {
	public static matrix inverse(matrix Q, matrix R) {
		// basically we just solve m equations of the form Ax = e_i, and construct A^-1 from the m solutions to this equation
		int m = R.size2;
		matrix A = new matrix(m, m);
		vector e = new vector(m);
		for (int i = 0; i < m; i++) {
			e[i] = 1;
			A[i] = solve(Q, R, e);
			e[i] = 0;
		}
		return A;
	}

	public static vector solve(matrix Q, matrix R, vector b) {
		vector x = new vector(b.size);
		b = Q.transpose()*b;
		for (int i = x.size-1; i >= 0; i--) {
			x[i] = b[i];
			for (int j = i+1; j < x.size; j++) {
				x[i] -= R[i, j]*x[j];
			}
			x[i] /= R[i, i];
		}
		return x; // seems like b is being passed by value instead of reference
	}

	public static void decomp(matrix A, matrix R) {
		int m = A.size2;
		double ujvi, ujuj;
		for (int i = 0; i < m; i++) {
			for (int j = 0; j < i; j++) {
				ujvi = A[j].dot(A[i]); ujuj = A[j].dot(A[j]);
				R[j, i] = ujvi/ujuj; // a_ij
				A[i] -= R[j, i]*A[j];
				R[j, i] *= R[j, j];
			}
			R[i, i] = A[i].norm();
		}
		for (int i = 0; i < m; i++)
			A[i] = A[i]/R[i, i]; // At this point, Q[i] = A[i]
	}
	}
}
