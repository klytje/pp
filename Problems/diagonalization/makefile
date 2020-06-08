libs=\
	 ../libs/vector.cs\
	 ../libs/matrix.cs 

all: main.exe qbox.svg
	$(MAKE) -C timed 
	mono $<
	echo As the results are based on a random generator, repeatedly issuing the "make" command may be instructive. 
	echo See the directory "timed" for the generated image files. 

qbox.svg: qbox.gpi qbox.exe
	mono qbox.exe > qbox.txt 
	gnuplot $<

timed.exe: timed.cs math.dll 
	mcs -reference:math.dll $<
qbox.exe: qbox.cs math.dll 
	mcs -reference:math.dll $<
main.exe: main.cs math.dll
	mcs -reference:math.dll $< 

math.dll: $(libs) jacobi.cs
	mcs -target:library -out:$@ $^

clean:
	$(MAKE) -C timed clean 
	rm *.dll *.svg *.exe
