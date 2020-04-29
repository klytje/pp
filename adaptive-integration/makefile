all: main.exe
	mono main.exe

main.exe: main.cs math.dll
	mcs -reference:math.dll $<

math.dll: quad.cs quadlib.cs
	mcs -target:library -out:$@ $^
