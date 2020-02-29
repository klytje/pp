using static System.Console;
using static System.Math;
using static System.Double;
using static quad;
using System;

class main {
	public static void Main(string[] args) {
		Func<double, double> f = (x) => Log(x)/Sqrt(x);
		double a=0, b=1;
		double res = o8av(f, a, b), exp;
		WriteLine("{0, -20} | {1, -10} | {2, -10} | {3, -10} | {4, -10}", "Integral function", "a", "b", "result", "expected");
		Format("log(x)/sqrt(x)", a, b, res, -4);		

		f = (x) => Exp(-Pow(x, 2));
		a=NegativeInfinity; b=PositiveInfinity;
		res = o8av(f, a, b); exp = Sqrt(PI);
		Format("exp(-x²)", a, b, res, exp);

		Func<double, double, double> g = (x, y) => Pow(Log(1/x), y);
		double p = 4;
		f = (x) => g(x, p);
		a = 0; b = 1;
		res = o8av(f, a, b); exp = Gamma(p+1);
		Format($"ln(1/x)^{p}", a, b, res, exp);

		f = (x) => Sqrt(x)*Exp(-x);
		a = 0; b = PositiveInfinity;
		res = o8av(f, a, b); exp = Sqrt(PI)/2;
		Format("sqrt(x)exp(-x))", a, b, res, exp);

		f = (x) => Pow(x, 3)/(Exp(x)-1);
		a = 0; b = PositiveInfinity;
		res = o8av(f, a, b); exp = Pow(PI, 4)/15;
		Format("x³/(exp(x) - 1)", a, b, res, exp);

		f = (x) => Pow(Sin(x), 2)/Pow(x, 2);
		a = 0; b = PositiveInfinity;
		res = o8av(f, a, b); exp = PI/2;
		Format("sin²(x)/x²", a, b, res, exp);
	}

	private static void Format(string func, double a, double b, double res, double exp) {
		WriteLine("{0, -20} | {1, -10:F3} | {2, -10:F3} | {3, -10:F3} | {4, -10:F3}", func, a, b, res, exp);
	}

	// I didn't define gamma in a separate file last week, so I have to define it again here.
	private static double Gamma(double x){
        if(x<0)return PI/Sin(PI*x)/Gamma(1-x);
        if(x<9)return Gamma(x+1)/x;
        double lngamma=x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
        return Exp(lngamma);
    }
}
