libs=\
	 ../libs/matrix.cs\
	 ../libs/vector.cs\
	 ../lineareq/qrdecomp.cs

all: main.exe
	mono $<

main.exe: main.cs math.dll
	mcs -reference:math.dll $<

temp.svg: temp.gpi data.cs math.dll 
	mcs -reference:math.dll data.cs 
	mono data.exe > data.txt
	gnuplot temp.gpi 

math.dll: $(libs) eigens.cs 
	mcs -target:library -out:$@ $^

clean: 
	rm *.dll *.exe 
