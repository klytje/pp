using static System.Console;

class main {
	public static int Main() {
		int i = 1;
		while(i+1 > i) {
			i++;
		}
		int j = -1;
		while (j-1 < j) {
			j--;
		}
		Write($"My max int is {i}!\nMy min int is {j}!\n");
		Write($"int.MaxValue: {int.MaxValue}, " +
				$"int.minvalue: {int.MinValue}\n");
		return 0;
	}
}
