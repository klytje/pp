dir=..
libs=\
	$(dir)/libs/vector.cs\
	$(dir)/libs/matrix.cs 

all: dep.svg 	

dep.svg: dep.gpi main.exe 
	mono main.exe > out.txt 
	gnuplot $<

main.exe: main.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) mcint.cs 
	mcs -target:library -out:$@ $^

clean:
	rm -f *.dll *.exe *.svg *.txt
