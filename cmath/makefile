CSFLAGS = -optimize+ -platform:anycpu

.PHONEY: all

all: out.txt
	cat out.txt

out.txt: main.exe
	mono main.exe > out.txt

main.exe: main.cs cmath.dll
	mcs $(CSFLAGS) -reference:cmath.dll -out:main.exe main.cs

cmath.dll: cmath.cs complex.cs
	mcs $(CSFLAGS) -target:library -out:$@ $^

.PHONEY: clean

clean:
	rm --force out* *.dll *.exe
