using static System.Console;

class Harmonic {
	public static int Main(string[] args) {
		int num = 2;
		if (args.Length != 0) {num = int.Parse(args[0]);}

		int max = int.MaxValue/num;
		double sum_up = 0;
		double sum_down = 0;
		for (int i = 1; i <= max; i++) {
			sum_up += 1d/i;
		}
		for (int i = max; i >= 1; i--) {
			sum_down += 1d/i;
		}
		Write("Harmonic sum for doubles:\n" +
			$"Summing up: {sum_up}\n" +
			$"Summing down: {sum_down}\n");
		return 0;
	}
}
