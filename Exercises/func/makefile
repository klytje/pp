CSFLAGS = -optimize+ -platform:anycpu

.PHONEY: all

all: out.txt variational.svg
	cat out.txt

out.txt: main.exe
	mono $< > $@

main.exe: main.cs quado8av.dll
	mcs $(CSFLAGS) -reference:quado8av.dll $<

variational.svg: variational.gpi variational.exe
	mono variational.exe 0 10 100
	gnuplot $<

variational.exe: variational.cs quado8av.dll
	mcs $(CSFLAGS) -reference:quado8av.dll $<

quado8av.dll: quado8av.cs
	mcs $(CSFLAGS) -target:library -out:$@ $<

variational.gpi:
	echo 'set encoding utf8' >> $@
	echo 'set title "Variational method"' >> $@
	echo 'set term svg' >> $@
	echo 'set out "variational.svg"' >> $@
	echo 'set xlabel "α"' >> $@
	echo 'set ylabel "Energy"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key bottom right' >> $@
	echo 'plot "results.txt" with lines' >> $@

.PHONEY: clean 
clean:
	rm --force out.* *.dll *.exe *.svg *.gpi
