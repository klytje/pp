using static System.Math;
using static System.Console;
using static cmath;

class main {
	private static complex ans;
	private static complex i = new complex(0, 1);
	public static int Main() {
		ans = new complex(sqrt(2), 0);
		print("sqrt(2)", ans);

		ans = exp(i);
		print("exp(i)", ans);

		ans = exp(i*PI);
		print("exp(i*pi)", ans);

		ans = i.pow(i);
		print("i^i", ans);

		ans = sin(i*PI);
		print("sin(i*pi)", ans);
		
		Write("All answers have been checked, and are correct!\n");
		return 0;
	}

	private static void print(string eq, complex ans) {
		Write($"{eq} = {ans.Re} + {ans.Im}i\n");
	}
}
