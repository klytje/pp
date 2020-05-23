dir=../
libs=\
	 $(dir)/libs/ode.cs\
	 $(dir)/libs/vector.cs 

all: sin.exe 
	$(MAKE) -C sin
	echo SVG files produced in sin/ 
	echo Ignore the old folder, I cleaned the hand-in up and replaced it with this. 

sin.exe: sin.cs math.dll 
	mcs -reference:math.dll $< 

math.dll: $(libs) interpolate.cs 
	mcs -target:library -out:$@ $^

clean: 
	$(MAKE) -C sin clean 
	rm -f *.txt *.dll *.exe *.svg
