all: erf.svg gamma.svg lngamma.svg gammac.svg

erf.svg: erf.gpi erf.txt erf_table.txt
	gnuplot $<
gamma.svg: gamma.gpi gamma.txt gamma_table.txt
	gnuplot $<
lngamma.svg: lngamma.gpi lngamma.txt
	gnuplot $<
gammac.svg: gammac.gpi gammac.txt
	gnuplot $<

erf.txt: func.exe
	mono $< erf -range -4 4 0.1
gamma.txt: func.exe
	mono $< gamma -range -4.05 4.05 0.1
lngamma.txt: func.exe
	mono $< lngamma -range -4.05 4.05 0.1
gammac.txt: gammac.exe
	mono $< -4.05 4.05 0.1 -4.05 4.05 0.1

lngammac.exe: lngammac.cs cmath.dll
	mcs -reference:cmath.dll $<
func.exe: func.cs
	mcs func.cs
gammac.exe: gammac.cs cmath.dll
	mcs -reference:cmath.dll $<

cmath.dll: cmath.cs complex.cs
	mcs $^ -target:library -out:$@

erf.gpi: 
	echo 'set encoding utf8' >> $@
	echo 'set title "Error function"' >> $@
	echo 'set term svg' >> $@
	echo 'set out "erf.svg"' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key bottom right' >> $@
	echo 'plot "erf.txt" with lines title "erf(x)", \' >> $@
	echo '"erf_table.txt" with points pt 7 title "tabulated"' >> $@

gamma.gpi:
	echo 'set encoding utf8' >> $@
	echo 'set title "Gamma function"' >> $@
	echo 'set term svg' >> $@
	echo 'set out "gamma.svg"' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key bottom right' >> $@
	echo 'plot "gamma.txt" with lines title "gamma(x)", \' >> $@
	echo '"gamma_table.txt" with points pt 7 title "tabulated"' >> $@

lngamma.gpi:
	echo 'set encoding utf8' >> $@
	echo 'set title "Log gamma function"' >> $@
	echo 'set term svg' >> $@
	echo 'set out "lngamma.svg"' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key bottom right' >> $@
	echo 'plot "lngamma.txt" with lines title "gamma(x)"' >> $@

gammac.gpi:
	echo 'set encoding utf8' >> $@
	echo 'set title "Complex gamma function"' >> $@
	echo 'set term svg' >> $@
	echo 'set out "gammac.svg"' >> $@
	echo 'set xlabel "real axis"' >> $@
	echo 'set ylabel "imaginary axis"' >> $@
	echo 'set zlabel "Gamma(x + iy)" rotate by 90' >> $@
	echo 'set tics out' >> $@
	echo 'splot "gammac.txt" with lines' >> $@

clean: 
	rm -f *.exe *.svg *.gpi
