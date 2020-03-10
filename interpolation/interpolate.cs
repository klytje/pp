using static System.Console;
using static System.Math;

public class interpolate {
	public class linear {
		public static double spline(vector x, vector y, double z) {
			int i = search(x, 0, x.size, z);
			return y[i] + (y[i+1] - y[i])/(x[i+1] - x[i])*(z - x[i]);
		}
		
		// I don't save the b list, so not sure how to implement this one analytically.
		// Right now it just connects the data points with straight lines, and calculates
		// the area underneath. 
		public static double integrate(vector x, vector y, double z) {
			int i = search(x, 0, x.size, z);
			double A = 0; // area
			for (int j = 0; j < i; j++) {
				A += (x[j+1] - x[j])*y[j] + (x[j+1] - x[j])*(y[j+1] - y[j])/2;
			}
			A += (z - x[i])*y[i] + (z - x[i])*(spline(x, y, z) - y[i])/2;
			return A;
		}
	}
	public class quadratic {
		private vector x, y, c, b;
		public quadratic(vector x, vector y) {
			this.x = x;
			this.y = y;
			setup();
		}
		public double spline(double z) {
			int i = search(x, 0, x.size, z);
			return y[i] + b[i]*(z - x[i]) + c[i]*Pow(z - x[i], 2);
		}
		public double integrate(double z) {
			int i = search(x, 0, x.size, z);
			double sum = 0, dx;
			for (int j = 0; j < i; j++) {
				dx = z - x[j];
				sum += y[j]*dx + b[j]*Pow(dx, 2)/2 + c[j]*Pow(dx, 3)/3;
			}
			dx = z - x[i];
			return sum + y[i]*dx + b[i]*Pow(dx, 2)/2 + c[i]*Pow(dx, 3)/3;
		}
		public double differentiate(double z) {
			int i = search(x, 0, x.size, z);
			return b[i] + 2*c[i]*(z - x[i]);
		}
		private void setup() {
			vector cseq = new vector(x.size); // front to back
			vector crev = new vector(x.size); // back to front
			vector p = new vector(x.size), dx = new vector(x.size);
            
			// setup initial values
			cseq[0] = 0; crev[crev.size-1] = 0;
			dx[0] = x[1] - x[0];
			p[0] = (y[1]-y[0])/dx[0];
			for (int i = 0; i < cseq.size-2; i++) {
				dx[i+1] = x[i+2]-x[i+1];
				p[i+1] = (y[i+2]-y[i+1])/dx[i+1];
				cseq[i+1] = (p[i+1] - p[i] - cseq[i]*dx[i])/dx[i+1];
			}
			for (int i = crev.size-2; 0 < i; i--) {
				crev[i] = (p[i+1] - p[i] - crev[i+1]*dx[i+1])/dx[i];
			}
			c = (cseq + crev)/2;
			b = p - c*dx;
		}
	}
	
	public class cubic {
		private vector x, y, b, c, d;
		public cubic(vector x, vector y) {
			this.x = x; 
			this.y = y;
			setup();
		}
		public double spline(double z) {
			int i = search(x, 0, x.size, z);
			return y[i] + b[i]*(z - x[i]) + c[i]*Pow(z - x[i], 2) + d[i]*Pow(z - x[i], 3);
		}
		public double integrate(double z) {
			int i = search(x, 0, x.size, z);
			double sum = 0, dx;
			for(int j = 0; j < i; j++) {
				dx = x[j+1] - x[j];
				sum += y[j]*dx + b[j]*Pow(dx, 2)/2 + c[j]*Pow(dx, 3)/3 + d[j]*Pow(dx, 4)/4;
			}
			dx = z - x[i];
			return sum + y[i]*dx + b[i]*Pow(dx, 2)/2 + c[i]*Pow(dx, 3)/3 + d[i]*Pow(dx, 4)/4;
		}
		public double differentiate(double z) {
			int i = search(x, 0, x.size, z);
			return b[i] + 2*c[i]*(z - x[i]) + 3*d[i]*Pow(z - x[i], 2);
		}
		private void setup() {
			int n = x.size;
			b = new vector(n);
			c = new vector(n-1);
			d = new vector(n-1);
			vector D = new vector(n);
			vector Q = new vector(n-1);
			vector B = new vector(n);
			vector h = new vector(n-1);
			vector p = new vector(n-1);
			for (int i = 0; i < n-1; i++) {
				h[i] = x[i+1]-x[i]; // definition of h (eq 15)
				p[i] = (y[i+1]-y[i])/h[i]; // definition of p (eq 6)
			}
			// setting up our known initial values (eq 21 - 23)
			D[0] = 2; Q[0] = 1; B[0] = 3*p[0]; D[n-1] = 2; B[n-1] = 3*p[n-2];
			// recursive relations described by the same set of equations
			for (int i = 0; i < n-2; i++) {
				D[i+1] = 2*h[i]/h[i+1] + 2;
				Q[i+1] = h[i]/h[i+1];
				B[i+1] = 3*(p[i] + p[i+1]*h[i]/h[i+1]);
			}
			for (int i = 1; i < n; i++) {
				D[i] -= Q[i-1]/D[i-1]; // converting D to Dtilde (eq 25)
				B[i] -= B[i-1]/D[i-1]; // converting B to Btilde (eq 26)
			}
			b[n-1] = B[n-1]/D[n-1]; // definition of b (eq 27)
			for (int i = n-2; 0 <= i; i--) {
				b[i] = (B[i] - Q[i]*b[i+1])/D[i]; // definition of b (eq 27)
			}
			for (int i = 0; i < n-1; i++) {
				c[i] = (-2*b[i] - b[i+1] + 3*p[i])/h[i]; // definition of c (eq 18)
				d[i] = (b[i] + b[i+1] - 2*p[i])/Pow(h[i], 2); // definition of d (eq 18)
			}
		}
	}

	// returns the index z is supposed to be at
    private static int search(vector x, int l, int r, double z) {
        int mid = l+(r-l)/2; // middle index to compare with
        if (l == r)
            return l-1;
        if (z < x[mid])
            return search(x, l, mid, z);
        return search(x, mid+1, r, z);
	}
}
