using static System.Console;
using static System.Math;
using System;

class main {
	static void Main(string[] args) {
		// PLAIN SAMPLING
		writetitle("\nPlain sampling");
		Func<vector, double> f = (x) => Sin(x[0])/x[0];
		vector a = new vector(0.0), b = new vector(PI);
		int N = (int) 1e4;
		double exp = 1.852; // expected 
		(double res, _) = mcint.plainmc(f, a, b, N); // result

		WriteLine(string.Format("{0, -28} {1, -8} {2, -8} {3, -8} {4, -8}", "function", "a", "b", "expected", "result"));
		WriteLine($"{"Sin(x)/x", -28} {0, -8} {PI, -8:F3} {exp, -8:F3} {res, -8:F3}");
		
		f = (x) => Pow(PI*PI*PI*(1 - Cos(x[0])*Cos(x[1])*Cos(x[2])), -1);
		a = new vector(0, 0, 0); b = new vector(PI, PI, PI);
		N = (int) 1e5;
		exp = 1.392;
		(res, _) = mcint.plainmc(f, a, b, N);
		WriteLine($"{"(pi³(1-cosx*cosy*cosz))⁻¹", -28} {0, -8} {PI, -8:F3} {exp, -8:F3} {res, -8:F3}");


		// STRATIFIED SAMPLING
		writetitle("\nRecursive stratified sampling");
		N = (int) 1e4;
		exp = 1.852; // expected 
		(res, _) = mcint.stratified(f, a, b, N); // result
		WriteLine(string.Format("{0, -28} {1, -8} {2, -8} {3, -8} {4, -8}", "function", "a", "b", "expected", "result"));
		WriteLine($"{"Sin(x)/x", -28} {0, -8} {PI, -8:F3} {exp, -8:F3} {res, -8:F3}");
		
		f = (x) => Pow(PI*PI*PI*(1 - Cos(x[0])*Cos(x[1])*Cos(x[2])), -1);
		a = new vector(0, 0, 0); b = new vector(PI, PI, PI);
		N = (int) 1e5;
		exp = 1.392;
		(res, _) = mcint.stratified(f, a, b, N);
		WriteLine($"{"(pi³(1-cosx*cosy*cosz))⁻¹", -28} {0, -8} {PI, -8:F3} {exp, -8:F3} {res, -8:F3}");
	}
	
	private static void writetitle(string title) {
		Console.ForegroundColor = ConsoleColor.DarkBlue;
		WriteLine(title);
		Console.ResetColor();
	}
}
