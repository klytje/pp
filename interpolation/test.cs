using static System.Console;

class test {
	public static void Main(string[] args) {
		double z = double.Parse(args[0]);
		vector x = new vector(0, 10, 20, 30);
		vector y = new vector(10, 10, 10, 10);
		WriteLine(interpolate.search(x, 0, x.size, z));
	}
}
