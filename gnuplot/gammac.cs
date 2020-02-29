using static System.Console;
using static System.Math;
using static cmath;
using System.IO;
using System;
using System.Numerics;
using System.Linq;

class main {
	public static void Main(string[] args) {
        using (StreamWriter file = new StreamWriter($"gammac.txt")) {
        	file.WriteLine(string.Format("# {0, -8} {1, -10} {2, -10}", "x", "z", "Gamma(x+iz)"));
            double[] data = args.Select(x => double.Parse(x)).ToArray();
			double rstart = data[0], rend = data[1], rstep = data[2]; // real range
			double zstart = data[3], zend = data[4], zstep = data[5]; // complex range

            double[] a = new double[(int) Ceiling((rend - rstart)/rstep)];
			double[] b = new double[(int) Ceiling((zend - zstart)/zstep)];
            double[] res = new double[a.Length*b.Length];

			int k = 0; // result index
			complex q; // complex number to be evaluated for each iteration
            for (int i = 0; i < a.Length; i++) {
				a[i] = rstart + i*rstep;
				for (int j = 0; j < b.Length; j++) {
					b[j] = zstart + j*zstep;
					q = new complex(a[i], b[j]);
					q = Gamma(q); // q now stores the result
					k = i*a.Length + j; // updating the result index
					res[k] = Sqrt(Pow(q.Re, 2) + Pow(q.Im, 2)); // getting the magnitude
                	file.WriteLine($"{a[i], -10:F3} {b[j], -10:F3} {res[k], -10:F3}");
				}
            }
            return;
		}
	}

	static int g = 7;
	static double[] p = {0.99999999999980993, 676.5203681218851, -1259.1392167224028,
	     771.32342877765313, -176.61502916214059, 12.507343278686905,
	     -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7};
	private static complex Gamma(complex z) {
		// Reflection formula
    	if (z.Re < 0.5)
        	return PI/(sin(PI*z)*Gamma(1 - z));
    	else {
        	z -= 1;
	        complex x = p[0];
 	    	for (var i = 1; i < g + 2; i++)
            	x += p[i]/(z+i);
        	complex t = z + g + 0.5;
        	return sqrt(2*PI)*cmath.pow(t, z + 0.5)*exp(-t)*x;
		}
	}
}
