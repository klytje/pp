CSFLAGS = -optimize+ -platform:anycpu

.PHONEY: all

all: out.txt
	cat out.txt

out.txt: main.exe
	mono $< > $@

main.exe: main.cs vector3d.dll
	mcs $(CSFLAGS) -reference:vector3d.dll -reference:ivector3d.dll -out:main.exe $< 

vector3d.dll: vector3d.cs ivector3d.dll
	mcs $(CSFLAGS) -target:library -reference:ivector3d.dll -out:$@ $<

ivector3d.dll: ivector3d.cs
	mcs $(CSFLAGS) -target:library -out:$@ $<

.PHONEY: clean
clean: rm --force *.dll *.exe out.txt
