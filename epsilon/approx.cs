using static System.Math;

// not linked as it is never used
class Equality {
	bool Approx(double a, double b, double tau=1e-9, double epsilon=1e-9) {
		double abs_diff = Abs(a - b);
		double sum = Abs(a) + Abs(b);

		if (abs_diff < tau || abs_diff/sum < epsilon/2)
			return true;
		return false;
	}
}
