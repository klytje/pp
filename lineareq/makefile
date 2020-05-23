 libs=\
	  ../libs/vector.cs\
	  ../libs/matrix.cs 

all: out.txt

out.txt: main.exe
	mono $< > out.txt

main.exe: main.cs math.dll 
	mcs -reference:math.dll $<

math.dll: $(libs) qrdecomp.cs
	mcs -target:library -out:$@ $^

clean: 
	rm -f *.txt *.dll *.exe *.svg
