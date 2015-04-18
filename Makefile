all: build/ListExtensions.exe

build/ListExtensions.exe:
	mkdir -p build
	mcs -out:build/ListExtensions.exe Program.cs Properties/AssemblyInfo.cs

clean:
	rm -rf build
