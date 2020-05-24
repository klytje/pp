row.svg: time.gpi time.exe # named so it does not recompile every time make is called
	echo Timing the different algorithms. This may take a few minutes..
	mono time.exe > time.txt
	gnuplot $<

time.exe: time.cs ../math.dll
	cp ../math.dll math.dll # referencing it directly does not work
	mcs -reference:math.dll $<

clean:
	rm -f *.dll *.svg *.exe
