dir=$(HOME)/git/practical-programming/
libs=\
	$(dir)/libs/vector.cs\
	$(dir)/libs/matrix.cs 

all: dep.svg 	

dep.svg: dep.gpi dep.txt  
	gnuplot $<

dep.txt: main.exe 
	grep -A1000 'Error dependence' temp > dep.txt
	rm temp 
	gnuplot $<

main.exe: main.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) mcint.cs 
	mcs -target:library -out:$@ $^
