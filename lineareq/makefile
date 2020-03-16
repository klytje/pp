all: out.txt

out.txt: qrtest.exe
	mono $< > out.txt

qrtest.exe: qrtest.cs matrix.dll 
	mcs -reference:matrix.dll $<

matrix.dll: vector.cs matrix.cs qrdecomp.cs
	mcs -target:library -out:$@ $^
