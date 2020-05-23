libs=\
	 ../libs/matrix.cs\
	 ../libs/vector.cs\
	 ../lineareq/qrdecomp.cs 

all: ls.svg

ls.svg: ls.gpi main.exe 
	mono main.exe > fit.txt 
	gnuplot $<

main.exe: main.cs math.dll
	mcs -reference:math.dll $< 

math.dll: $(libs) ls.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe *.svg 
