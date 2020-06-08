using static System.Console;
using static System.Math;
using System.Linq;
using System;

// no input exceptions are not handled - I couldn't be bothered to include it this time..
class pipe {
	public static int Main(string[] args) {
		var stdin = In;
		double i;
		string line = stdin.ReadLine(); // we expect at *least* one input
		bool horizontal = line.Contains(" "); // check if the input is horizontal or vertical
		WriteLine("{0, -8:N3} {1, -8:N3} {2, -8:N3}", "i", "cos(i)", "sin(i)");

		if (horizontal) {
			string[] input = line.Split(' '); // split the input into sections
			var data = input.Select(x => double.Parse(x)); // map the strings to doubles
			foreach (double d in data) {
				WriteLine("{0, -8:N3} {1, -8:N3} {2, -8:N3}", d, Cos(d), Sin(d));
			}
			return 0;
		}
		do {
	        i = double.Parse(line);
            WriteLine("{0, -8:N3} {1, -8:N3} {2, -8:N3}", line, Cos(i), Sin(i));
		} while ((line = stdin.ReadLine()) != null);

		return 0;
	}
}
