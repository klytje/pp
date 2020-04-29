dir=$(HOME)/git/practical-programming/
libs=\
	$(dir)/ode/ode.cs\
	$(dir)/lineareq/qrdecomp.cs\
	$(dir)/libs/vector.cs\
	$(dir)/libs/matrix.cs 

all: test.exe hshoot.svg
	mono test.exe > temp 
	grep -A1000 'Hydrogen' temp > hshoot.txt
	rm temp 

hshoot.svg: hshoot.gpi hshoot.txt 
	gnuplot $<

test.exe: test.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) root.cs 
	mcs -target:library -out:$@ $^
