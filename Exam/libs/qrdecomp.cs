using static matrix;
using static System.Math;
using static System.Console;

public class qr_decomp {
public class gs {
	
	// made a basic object so I don't have to remember how to do everything later
	matrix r;
	matrix q;
	public gs(matrix A) {
		r = new matrix(A.size2, A.size2);
		q = A.copy();
		decomp(q, r);
	}

	public vector solve(vector b) {
		return solve(q, r, b);
	}

	public matrix inverse() {
		return inverse(q, r);
	}

	public static matrix inverse(matrix Q, matrix R) {
		// basically we just solve m equations of the form Ax = e_i, and construct A^-1 from the m solutions to this equation
		int n = Q.size1, m = Q.size2;
		matrix A = new matrix(m, n);
		vector e = new vector(n);
		for (int i = 0; i < n; i++) {
			e[i] = 1;
			A[i] = solve(Q, R, e);
			e[i] = 0;
		}
		return A;
	}

	public static vector solve(matrix Q, matrix R, vector b) {
		// solve an equation of the form QRx = b
		b = Q.transpose()*b;
		vector x = new vector(b.size);
		for (int i = x.size-1; i >= 0; i--) {
			x[i] = b[i];
			for (int j = i+1; j < b.size; j++)
				x[i] -= R[i, j]*x[j];
			x[i] /= R[i, i];
		}
		return x;
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
