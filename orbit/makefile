all: out.txt i.svg ii.svg iii.svg

i.svg: i.gpi i.txt
	gnuplot $<
ii.svg: ii.gpi ii.txt
	gnuplot $<
iii.svg: iii.gpi iii.txt
	gnuplot $<

i.txt: b.exe
	# this is messed up for large ranges of x; xb-xa =~ 6 roughly corresponds to a single orbit
	mono $< xa=-3 xb=3 y0=1 y1=0 eps=0 > $@
ii.txt: b.exe
	mono $< xa=-30 xb=30 y0=1 y1=-0.5 eps=0 > $@
iii.txt: b.exe
	mono $< xa=-30 xb=30 y0=1 y1=-0.5 eps=0.01 > $@

out.txt: a.exe
	mono a.exe

b.exe: b.cs math.dll
	mcs -reference:math.dll $<

a.exe: a.cs math.dll
	mcs -reference:math.dll $<

math.dll: ode.cs vector.cs
	mcs -target:library -out:$@ $^

clean: 
	rm -f *.txt *.dll *.exe *.svg
