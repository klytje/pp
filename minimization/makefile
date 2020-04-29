dir=$(HOME)/git/practical-programming/
libs=\
	$(dir)/ode/ode.cs\
	$(dir)/lineareq/qrdecomp.cs\
	$(dir)/libs/vector.cs\
	$(dir)/libs/matrix.cs 

all: test.exe 
	mono test.exe 

test.exe: test.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) mini.cs 
	mcs -target:library -out:$@ $^
