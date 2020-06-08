libs=\
	../libs/quad.cs 

all: main.exe
	mono main.exe

main.exe: main.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) quad.cs
	mcs -target:library -out:$@ $^

clean: 
	rm -f *.exe *.dll 
