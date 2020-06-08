libs=\
	 libs/matrix.cs\
	 libs/vector.cs\
	 libs/qrdecomp.cs

all: main.exe dep-mu.svg
	mono $<

dep-mu.svg: dep-mu.gpi dep-b.gpi data.exe math.dll 
	mono data.exe > data.txt
	gnuplot dep-mu.gpi 
	gnuplot dep-b.gpi

main.exe: main.cs math.dll
	mcs -reference:math.dll $<
data.exe: data.cs math.dll 
	mcs -reference:math.dll $<

math.dll: $(libs) eigens.cs 
	mcs -target:library -out:$@ $^

clean: 
	rm *.dll *.exe *.svg data.txt 
