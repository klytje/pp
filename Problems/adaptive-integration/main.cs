using static System.Console;
using static System.Math;
using static quadrature;
using System;

public class main {
	private static double sig = 1e-3, eta = 1e-3;
	public static void Main(string[] args) {
		Console.ForegroundColor = ConsoleColor.Red;
		WriteLine("\nPlease enlarge the window size, such that all text fits on a single line.");
		Console.ResetColor();
		double a = 0, b = 1, res, exact;
		double pinf = double.PositiveInfinity, ninf = double.NegativeInfinity;
		
		// BASIC INTEGRAL TESTING
		Func<double, double> f;	
		writetitle("Basic integration testing");
		WriteLine($"{"function", -20} {"a", -10} {"b", -10} {"result", -8} {"exact", -8}");
		
		f = (x) => Sqrt(x);
		res = recAdapt.integrate(f, a, b, sig, eta); exact = 2.0/3;
		printres("sqrt(x)", a, b, res, exact);

		f = (x) => 4*Sqrt(1-x*x);
		res = recAdapt.integrate(f, a, b, sig, eta); exact = PI;
		printres("4*sqrt(1 - x*x)", a, b, res, exact);


		// CLENSHAW-CURTIS VARIABLE TRANSFORMS
		writetitle("\nClenshaw-Curtis variable transform");
		a = 0; b = 1;
		f = (x) => 1/Sqrt(x);
		res = varTrans.integrate(f, a, b, sig, eta); exact = 2;
		printres("1/sqrt(x)", a, b, res, exact);

		f = (x) => Log(x)/Sqrt(x);
		res = varTrans.integrate(f, a, b, sig, eta); exact = -4;
		printres("ln(x)/sqrt(x)", a, b, res, exact);

		// COMPARISON BETWEEN THE DIFFERENT IMPLEMENTATIONS
		writetitle("\nComparing accuracy and number of evaluations");
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


		// TESTING INFINITE LIMITS
		writetitle("\nTesting integrals with infinite limits");
		WriteLine("using custom implementation:");
		WriteLine($"{"function", -20} {"a", -10} {"b", -10} {"result", -8} {"exact", -8} {"calls", -8} {"deviation", -10} {"tolerance", -10}");
		calls = 0;
		f = delegate(double x) {calls++; return 1/x/x;};
		res = varTrans.integrate(f, 1, pinf, sig, eta); 
		exact = 1;
		printres("1/x^2", 1, pinf, res, exact, calls);

		calls = 0;
		f = delegate(double x) {calls++; return 1/(x + 1)/Sqrt(x);};
		res = varTrans.integrate(f, 0, pinf, sig, eta); 
		exact = PI;
		printres("1/((x+1)*sqrt(x))", 0, pinf, res, exact, calls);

		calls = 0;
		f = delegate(double x) {calls++; return Exp(-x*x);};
		res = varTrans.integrate(f, ninf, pinf, sig, eta); 
		exact = Sqrt(PI);
		printres("exp(-x^2)", ninf, pinf, res, exact, calls);

		// TESTING INFINITE LIMITS USING o8av
		WriteLine("\nusing o8av:");
		calls = 0;
		f = delegate(double x) {calls++; return 1/x/x;};
		res = quad.o8av(f, 1, pinf, sig, eta);
		exact = 1;
		printres("1/x^2", 1, pinf, res, exact, calls);

		calls = 0;
		f = delegate(double x) {calls++; return 1/(x + 1)/Sqrt(x);};
		res = quad.o8av(f, 0, pinf, sig, eta);
		exact = PI;
		printres("1/((x+1)*sqrt(x))", 0, pinf, res, exact, calls);

		calls = 0;
		f = delegate(double x) {calls++; return Exp(-x*x);};
		res = quad.o8av(f, ninf, pinf, sig, eta);
		exact = Sqrt(PI);
		printres("exp(-x^2)", ninf, pinf, res, exact, calls);
	}

	private static void writetitle(string title) {
		Console.ForegroundColor = ConsoleColor.DarkBlue;
		WriteLine(title);
		Console.ResetColor();
	}

	private static void printres(string func, double a, double b, double res, double exact) {
		WriteLine($"{func, -20} {a,- 10:F3} {b, -10:F3} {res, -8:F3} {exact, -8:F3}");
	}

	private static void printres(string func, double a, double b, double res, double exact, double calls) {
		WriteLine($"{func, -20} {a,- 10:F3} {b, -10:F3} {res, -8:F3} {exact, -8:F3} {calls, -8:F3} {Abs(res - exact).ToString("0.000E0"), -10} {(sig + Abs(res)*eta).ToString("0.000E0"), -10}");
	}
}
