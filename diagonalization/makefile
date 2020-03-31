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

timeallrows.exe: timeallrows.cs matrix.dll 
	mcs -reference:matrix.dll $<
timerow.exe: timerow.cs matrix.dll
	mcs -reference:matrix.dll $<
timecyclic.exe: timecyclic.cs matrix.dll 
	mcs -reference:matrix.dll $< 
qbox.exe: qbox.cs matrix.dll 
	mcs -reference:matrix.dll $<
diatest.exe: diatest.cs matrix.dll
	mcs -reference:matrix.dll $< 

matrix.dll: vector.cs matrix.cs jacobi.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.svg *.exe
