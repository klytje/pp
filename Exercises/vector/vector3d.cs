using static System.Math;
using static ivector3d;

public struct vector3d : ivector3d {
	public double x, y, z;

	// constructor
	public vector3d(double x, double y, double z) {
		this.x=x;
		this.y=y;
		this.z=z;
	}

	// debug print
	public void print(string s="") {
		System.Console.Write($"({x}, {y}, {z})\n");
	}

	// operators
	public static vector3d operator+ (vector3d v, vector3d u) {
		return new vector3d(v.x+u.x, v.y+u.y, v.z+u.z);
	}

	public static vector3d operator- (vector3d v, vector3d u) {
		return new vector3d(v.x-u.x, v.y-u.y, v.z-u.z);
	}

	public static vector3d operator* (vector3d v, double a) {
		return new vector3d(v.x*a, v.y*a, v.z*a);
	}

	public static vector3d operator* (double a, vector3d v) {
		return new vector3d(a*v.x, a*v.y, a*v.z);
	}

	public static vector3d operator* (vector3d v, vector3d u) {
        return new vector3d(v.x*u.x, v.y*u.y, v.z*u.z);
    }


	// methods
    public double vprod(vector3d v) {
        return v.x*x + v.y*y + v.z*z;
    }

	public double magnitude() {
		return Sqrt(Pow(x, 2) + Pow(y, 2) + Pow(z, 2));
	}

	public override string ToString() {
		return "(" + x + ", " + y + ", " + z + ")";
	}
}
