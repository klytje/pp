libs=\
	 ../libs/vector.cs 

all: sincos.svg sirmodel.svg break-the-curve.svg three-body.gif 

three-body.gif: ../three-body.exe three-body-gif.gpi 
	mono $< > three-body.txt 
	gnuplot three-body-gif.gpi 

# Broken due to the gif requiring a different data format (2 spaces between each data line, can quickly be changed in three-body.cs). I think the gif is nicer anyway. 
three-body.svg: ../three-body.exe three-body.gpi  
	mono $< > three-body.txt 
	gnuplot three-body.gpi 

sirmodel.svg: ../sirmodel.exe sirmodel.gpi 
	mono $< N=6e6 Tc=2 Tr=14 dur=60 > sirmodel.txt 
	gnuplot sirmodel.gpi 

break-the-curve.svg: ../sirmodel.exe btc.gpi 
	mono $< N=6e6 Tc=2 Tr=14 dur=60 > btc.txt
	mono $< N=6e6 Tc=0.1 Tr=14 dur=60 >> btc.txt 
	gnuplot btc.gpi 

sincos.svg: ../sincos.exe sincos.gpi 
	mono $< > sincos.txt
	gnuplot sincos.gpi 

clean: 
	rm -f *.dll *.svg *.exe *.txt *.gif 
