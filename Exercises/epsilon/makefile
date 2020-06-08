CSFLAGS = -optimize+ -platform:anycpu 

.PHONEY: all

all: out.txt
	
out.txt: maxint.exe epsilon.exe harmonic.exe harmonic_double.exe 
	mono maxint.exe >> out.txt
	mono epsilon.exe >> out.txt 
	mono harmonic.exe >> out.txt
	mono harmonic_double.exe >> out.txt 
	cat out.txt	

epsilon.exe: epsilon.cs
	mcs $<

harmonic.exe: harmonic.cs
	mcs $<

harmonic_double.exe: harmonic_double.cs 
	mcs $<

maxint.exe: maxint.cs
	mcs $<

.PHONEY: clean

clean:
	rm --force out* *.dll *.exe out.txt
