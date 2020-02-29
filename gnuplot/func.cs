using static System.Console;
using static System.Math;
using System.IO;

delegate double Func(double x);
class func {
	public static void Main(string[] args) {
		string func = args[0];
		double[] x;
		double[] res;
		Func fun = identity;

		if (func == "erf")
			fun = erf;
		else if (func == "gamma")
			fun = gamma;
		else if (func == "lngamma")
			fun = lngamma;
		
		using (StreamWriter writer = new StreamWriter($"{func}.txt")) {
        	writer.WriteLine(string.Format("#{0, -9:F3} {1, 10:F3}", "x", $"{func}(x)"));
			if (args[1] == "-range") {
				double curr = double.Parse(args[2]);
				double end = double.Parse(args[3]);
				double step = double.Parse(args[4]);
	            x = new double[(int) Ceiling((end - curr)/step)];
   		        res = new double[x.Length];
				
				int i = 0; // index
				while (curr < end) {
					x[i] = curr;
					res[i] = fun(x[i]);
					writer.WriteLine(string.Format("{0, -10:F3} {1, 10:F3}", x[i], res[i]));
					curr += step;
					i++;
				}
				return;
			}

			x = new double[args.Length - 1];
    	    res = new double[args.Length - 1];
			for (int i = 0; i < args.Length-1; i++) {
				x[i] = double.Parse(args[i+1]);
				res[i] = fun(x[i]);
				writer.WriteLine(string.Format("{0, -10:F3} {1, 10:F3}", x[i], res[i]));
			}
		}
	}
	
	public static double identity(double x) {
		return x;
	}

	public static double erf(double x) {
		if(x<0) return -erf(-x);
		double[] a={0.254829592,-0.284496736,1.421413741,-1.453152027,1.061405429};
		double t=1/(1+0.3275911*x);
		double sum=t*(a[0]+t*(a[1]+t*(a[2]+t*(a[3]+t*a[4]))));
		return 1-sum*Exp(-x*x);
	} 

	public static double gamma(double x){
		if(x<0)return PI/Sin(PI*x)/gamma(1-x);
        if(x<9)return gamma(x+1)/x;
        double lngamma = x*Log(x+1/(12*x-1/x/10))-x+Log(2*PI/x)/2;
		return Exp(lngamma);
	}

	public static double lngamma(double x) {
		return Log(gamma(x));
	}
}
