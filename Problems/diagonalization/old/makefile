all: diatest.exe qbox.svg timecyclic.svg timerow.svg timeallrows.svg 
	mono diatest.exe > diatest.txt 

timeallrows.svg: timeallrows.gpi timeallrows.exe 
	mono timeallrows.exe > timeallrows.txt 
	gnuplot $<
qbox.svg: qbox.gpi qbox.exe
	mono qbox.exe > qbox.txt 
	gnuplot $<
timecyclic.svg: timecyclic.gpi timecyclic.exe 
	mono timecyclic.exe > timecyclic.txt 
	gnuplot $<
timerow.svg: timerow.gpi timerow.exe
	mono timerow.exe > timerow.txt 
	gnuplot $<

timeallrows.exe: timeallrows.cs math.dll 
	mcs -reference:math.dll $<
timerow.exe: timerow.cs math.dll
	mcs -reference:math.dll $<
timecyclic.exe: timecyclic.cs math.dll 
	mcs -reference:math.dll $< 
qbox.exe: qbox.cs math.dll 
	mcs -reference:math.dll $<
diatest.exe: diatest.cs math.dll
	mcs -reference:math.dll $< 

math.dll: vector.cs matrix.cs jacobi.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.svg *.exe
