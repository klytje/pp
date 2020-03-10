euler.pdf: euler.tex spiral.pdf 
	pdflatex $<

spiral.pdf: euler.gpi out.txt
	gnuplot $<

out.txt: euler.exe
	mono $< > $@ za=-10 zb=10 step=0.01

euler.exe: euler.cs quado8av.dll
	mcs -reference:quado8av.dll $<

quado8av.dll: quado8av.cs
	mcs -target:library -out:$@ $<

clean:
	rm -f *.txt *.svg *.dll
