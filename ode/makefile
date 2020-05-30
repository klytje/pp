libs=\
	 ../libs/vector.cs 

all: sirmodel.exe sincos.exe three-body.exe 
	$(MAKE) -C images

three-body.exe: three-body.cs math.dll 
	mcs -reference:math.dll $<
sirmodel.exe: sirmodel.cs math.dll
	mcs -reference:math.dll $<
sincos.exe: sincos.cs math.dll
	mcs -reference:math.dll $<

math.dll: $(libs) ode.cs 
	mcs -target:library -out:$@ $^

clean: 
	$(MAKE) -C images clean 
	rm -f *.dll *.svg *.exe 
