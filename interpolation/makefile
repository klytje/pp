all: linear.svg quadratic.svg cubic.svg 

linear.svg: linear.gpi linearsin.gpi linear.txt
	# gnuplot $<
	gnuplot linearsin.gpi 
quadratic.svg: quadratic.gpi quadraticsin.gpi quadratic.txt
	# gnuplot $<
	gnuplot quadraticsin.gpi 
cubic.svg: cubic.gpi cubicsin.gpi cubic.txt pusspline.txt 
	# gnuplot $<
	gnuplot cubicsin.gpi 

pusspline.txt: cubic.txt
	awk '{print $$1, $$5}' cubic.txt | spline > $@

linear.txt: linear.exe
	mono $< > $@
quadratic.txt: quadratic.exe 
	mono $< > $@
cubic.txt: cubic.exe
	mono $< > $@

linear.exe: linear.cs math.dll
	mcs -reference:math.dll $<
quadratic.exe: quadratic.cs math.dll
	mcs -reference:math.dll $<
cubic.exe: cubic.cs math.dll 
	mcs -reference:math.dll $<

math.dll: ode.cs vector.cs interpolate.cs 
	mcs -target:library -out:$@ $^

clean: 
	rm -f *.txt *.dll *.exe *.svg
