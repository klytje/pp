dir=..
libs=\
	$(dir)/ode/ode.cs\
	$(dir)/lineareq/qrdecomp.cs\
	$(dir)/libs/vector.cs\
	$(dir)/libs/matrix.cs 

all: main.exe 
	mono $< 

main.exe: main.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) mini.cs 
	mcs -target:library -out:$@ $^

clean:
	rm -f *.dll *.exe 
