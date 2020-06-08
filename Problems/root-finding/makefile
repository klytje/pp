dir=..
libs=\
	$(dir)/ode/ode.cs\
	$(dir)/lineareq/qrdecomp.cs\
	$(dir)/libs/vector.cs\
	$(dir)/libs/matrix.cs 

all: main.exe hshoot.svg
	echo See out.txt for the answers on problem a. 

hshoot.svg: hshoot.gpi main.exe 
	mono main.exe > out.txt  
	grep -A1000 'Hydrogen' out.txt > hshoot.txt 
	gnuplot $<

main.exe: main.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) root.cs 
	mcs -target:library -out:$@ $^

clean:
	rm -f *.dll *.txt *.svg 
