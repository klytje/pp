dir=..
libs=\
	$(dir)/libs/vector.cs\
	$(dir)/libs/matrix.cs 

all: main.exe samplemap.png dep.svg
	mono main.exe 

samplemap.png: samplemap.gpi data.txt 
	gnuplot $<

dep.svg: dep.gpi data.txt 
	gnuplot $<

data.txt: data.cs math.dll 
	mcs -reference:math.dll $<
	mono data.exe > data.txt 
	
main.exe: main.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) mcint.cs 
	mcs -target:library -out:$@ $^

clean:
	rm -f *.dll *.exe *.svg *.txt *.png
