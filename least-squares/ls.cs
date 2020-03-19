using static qr_decomp;
using static System.Math;
using System;

public class ls {
	public class fit {
		public vector p;
		public matrix cov;
		public Func<double, double>[] funcs;
		public fit(vector p, matrix cov, Func<double, double>[] funcs) {
			this.p = p;
			this.cov = cov;
			this.funcs = funcs;
		}

		// uses the best fit parameters to evaluate t
		public double best(double t) {
			return eval(t, p);
		}

		// uses the upper bounds of the fit parameters to evaluate t
		public double upper(double t) {
			vector p_up = p.copy();
			for (int i = 0; i < p_up.size; i++)
				p_up[i] += Sqrt(cov[i, i]);
			return eval(t, p_up);
		}

		// uses the lower bounds of the fit parameters to evaluate t
		public double lower(double t) {
			vector p_dn = p.copy();
			for (int i = 0; i < p_dn.size; i++)
				p_dn[i] -= Sqrt(cov[i, i]);
			return eval(t, p_dn);
		}

		// evaluate t with custom parameters
		private double eval(double t, vector p) {
			double val = 0;
			for (int i = 0; i < funcs.Length; i++)
				val += p[i]*funcs[i](t);
			return val;
		}
	}
	public static fit qrfit(vector x, vector y, vector dy, Func<double, double>[] funcs) {
		// we want to set the system up in a more familiar way
		int n = x.size, m = funcs.Length;
		matrix Q = new matrix(n, m);
		matrix R = new matrix(m, m);
		vector b = new vector(n);
		for (int i = 0; i < n; i++) {
			b[i] = y[i]/dy[i]; // eq 7
			for (int j = 0; j < m; j++)
				Q[i, j] = funcs[j](x[i])/dy[i]; // eq 7
		}
		gs.decomp(Q, R); // set up Q and R
		matrix Qi = gs.inverse(Q, R);
		vector c = gs.solve(Q, R, b); // eq 8
		matrix cov = Qi*Qi.transpose(); // eq 14
		return new fit(c, cov, funcs); 
	}
}
