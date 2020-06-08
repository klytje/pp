all: sin 

sin: sin.gpi sin.txt pusspline.txt 
	gnuplot $<

sin.txt: ../sin.exe 
	mono $< > $@ 

pusspline.txt: sin.txt 
	grep -A1000 'CUBIC' sin.txt > temp # get the cubic data only
	awk '{print $$1, $$5}' temp | spline > $@
	rm temp

clean: 
	rm -f *.txt *.dll *.exe *.svg
