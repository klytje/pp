dir=..
libs=\
	$(dir)/minimization/mini.cs\
	$(dir)/libs/vector.cs\
	$(dir)/libs/matrix.cs 

all: main.exe nn.svg  

nn.svg: nn.gpi main.exe 
	mono main.exe > data.txt 
	gnuplot $<

main.exe: main.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) nn.cs 
	mcs -target:library -out:$@ $^

clean: 
	rm -f *.dll *.svg *.txt *.exe 
