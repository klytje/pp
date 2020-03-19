all: ls.svg

ls.svg: ls.gpi fit.txt
	gnuplot $<

fit.txt: lstest.exe 
	mono $< > $@

lstest.exe: lstest.cs matrix.dll
	mcs -reference:matrix.dll lstest.cs 

matrix.dll: vector.cs matrix.cs qrdecomp.cs ls.cs
	mcs -target:library -out:$@ $^

clean:
	rm matrix.dll lstest.exe
