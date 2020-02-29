using static System.Console;

class Harmonic {
	public static int Main(string[] args) {
		int num = 2;
		if (args.Length != 0) {num = int.Parse(args[0]);}

		int max = int.MaxValue/num;
		float sum_up = 0;
		float sum_down = 0;
		for (int i = 1; i <= max; i++) {
			sum_up += 1f/i;
		}
		for (int i = max; i >= 1; i--) {
			sum_down += 1f/i;
		}
		Write("Harmonic sum for floats:\n" +
			$"Summing up: {sum_up}\n" +
			$"Summing down: {sum_down}\n");
		return 0;
	}
}
