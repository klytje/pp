all: sincos.svg sirmodel.svg break-the-curve.svg

break-the-curve.svg: btc.gpi sirmodel.exe
	mono sirmodel.exe N=6e6 Tc=2 Tr=14 dur=60 > btc.txt
	mono sirmodel.exe N=6e6 Tc=0.1 Tr=14 dur=60 >> btc.txt 
	gnuplot $<

sirmodel.svg: sirmodel.gpi sirmodel.exe
	mono sirmodel.exe N=6e6 Tc=2 Tr=14 dur=60 > sirmodel.txt
	gnuplot $<
sincos.svg: sincos.gpi sincos.exe 
	mono sincos.exe > sincos.txt
	gnuplot $<

sirmodel.exe: sirmodel.cs math.dll
	mcs -reference:math.dll $<
sincos.exe: sincos.cs math.dll
	mcs -reference:math.dll $<

math.dll: vector.cs ode.cs 
	mcs -target:library -out:$@ $^

clean: 
	rm *.dll *.svg *.exe 
