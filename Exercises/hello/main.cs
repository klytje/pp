using static System.Console;
using static System.Environment;

class main {
	static string user = UserName;
	static int Main(){
		Write($"Hello, {user}!\n");
		return 0;
	}
}
