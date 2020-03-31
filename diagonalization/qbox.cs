using static System.Console;
using static System.Math;
using static jacobi;
using System;

public class main {
	static void Main(string[] args) {
		int n = 21;
		double s=1.0/(n+1);
		matrix H = new matrix(n, n);
		for (int i = 0; i < n-1; i++){
  			H[i, i] = -2;
			H[i, i+1] = 1;
			H[i+1, i] = 1;
  		}
		H[n-1, n-1] = -2;
		H *= -1/s/s;

		(vector e, matrix V, int sweeps) = cyclic(H);
		matrix VTHV = V.transpose()*H*V;
		WriteLine(string.Format("# {0, -2}  {1, -8} {2, -8}", "k", "calc", "exact"));
		for (int k = 0; k < n/3; k++) {
			double exact = PI*PI*(k+1)*(k+1);
			double calculated = e[k];
			WriteLine($"# {k, -2}: {calculated, -8:F3} {exact, -8:F3}");
		}
		
		double factor;
		WriteLine("");
		WriteLine(string.Format("# {0, -6} {1, -8}", "x", "y"));
		for(int k = 0; k < 4; k++){
  			WriteLine($"{0, -8} {0, -8}");
  			for(int i = 0; i < n; i++) {
				factor = Sign(V[0, k])/Sqrt(s)/Sqrt(2);
				WriteLine($"{(i+1.0)/(n+1), -8:F3} {V[i, k]*factor, -8:F3}");
			}
  			WriteLine($"{1, -8} {0, -8}\n");
  		}
	}
}

