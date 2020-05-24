libs=\
	 ../libs/vector.cs 

all: sirmodel.exe sincos.exe 
	$(MAKE) -C images

sirmodel.exe: sirmodel.cs math.dll
	mcs -reference:math.dll $<
sincos.exe: sincos.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) ode.cs 
	mcs -target:library -out:$@ $^

clean: 
	$(MAKE) -C images clean 
	rm -f *.dll *.svg *.exe 
