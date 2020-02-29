using static System.Console;
using static vector3d;

class main {
	public static void Main() {
	vector3d v = new vector3d(1, 1, 1);
	vector3d u = new vector3d(1, 2, 3);
		
	Write($"{v}*{u} = " + v*u + "\n");
	Write($" {v} - {u} = " + (v-u) + "\n");
	Write($"|{v}| = " + v.magnitude() + "\n");

	}
}
