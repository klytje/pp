using static System.Console;
using static System.Math;
using static quadrature;
using System;

public class main {
	public static void Main(string[] args) {
		double a = 0, b = 1, sig = 1e-3, eta = 1e-3, res, exact;
		Func<double, double> f;
		WriteLine(string.Format("{0, -20} {1, -8} {2, -8} {3, -8} {4, -8}", "function", "a", "b", "result", "exact"));
		
		f = (x) => Sqrt(x);
		res = recAdapt.integrate(f, a, b, sig, eta); exact = 2.0/3;
		printres("sqrt(x)", a, b, res, exact);

		f = (x) => 4*Sqrt(1-x*x);
		res = recAdapt.integrate(f, a, b, sig, eta); exact = PI;
		printres("4*sqrt(1 - x*x)", a, b, res, exact);

		WriteLine("\nClenshaw-Curtis variable transformation");
		a = 0; b = 1;
		f = (x) => 1/Sqrt(x);
		res = varTrans.integrate(f, a, b, sig, eta); exact = 2;
		printres("1/sqrt(x)", a, b, res, exact);

		f = (x) => Log(x)/Sqrt(x);
		res = varTrans.integrate(f, a, b, sig, eta); exact = -4;
		printres("ln(x)/sqrt(x)", a, b, res, exact);

		WriteLine("\nComparing accuracy and number of evaluations");
		WriteLine(string.Format("{0, -20} {1, -8} {2}", "info", "evals", "result"));
		int calls = -1; // accounts for the variable transform
		f = delegate(double x) {calls++; return 4*Sqrt(1-x*x);};
		res = varTrans.integrate(f, a, b, sig, eta);
		WriteLine($"{"with CC transform", -20} {calls, -8:F3} {res}");

		calls = 0;
		res = recAdapt.integrate(f, a, b, sig, eta);
		WriteLine($"{"without CC transform", -20} {calls, -8:F3} {res}");
		
		calls = 0;
		res = quad.o8av(f, a, b, sig, eta);	
		WriteLine($"{"using o8av", -20} {calls, -8:F3} {res}");
		WriteLine($"{"exact result", -20} {"-", -8} {PI}");
	}

	private static void printres(string func, double a, double b, double res, double exact) {
		WriteLine($"{func, -20} {a,- 8:F3} {b, -8:F3} {res, -8:F3} {exact, -8:F3}");
	}
}
