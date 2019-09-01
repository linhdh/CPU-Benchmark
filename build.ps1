cd Benchmarker
Remove-Item -Path bin/Release -Recurse
mkdir bin/Release
dotnet restore Benchmarker.csproj
dotnet publish Benchmarker.csproj --framework netcoreapp3.0 -c Release -o bin/Release/Benchmarker
dotnet publish Benchmarker.csproj --framework netcoreapp3.0 -c Release -r win-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true -o bin/Release/Benchmarker-win64
dotnet publish Benchmarker.csproj --framework netcoreapp3.0 -c Release -r win-x86 /p:PublishSingleFile=true /p:PublishTrimmed=true -o bin/Release/Benchmarker-win32
dotnet publish Benchmarker.csproj --framework netcoreapp3.0 -c Release -r linux-arm /p:PublishSingleFile=true /p:PublishTrimmed=true -o bin/Release/Benchmarker-linux-arm32v7
dotnet publish Benchmarker.csproj --framework netcoreapp3.0 -c Release -r win-arm /p:PublishSingleFile=true /p:PublishTrimmed=true -o bin/Release/Benchmarker-win-arm32v7
dotnet publish Benchmarker.csproj --framework netcoreapp3.0 -c Release -r win-arm64 /p:PublishSingleFile=true /p:PublishTrimmed=true -o bin/Release/Benchmarker-win-arm64
dotnet publish Benchmarker.csproj --framework netcoreapp3.0 -c Release -r linux-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true -o bin/Release/Benchmarker-linux64
dotnet publish Benchmarker.csproj --framework netcoreapp3.0 -c Release -r debian-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true -o bin/Release/Benchmarker-debian64
dotnet publish Benchmarker.csproj --framework netcoreapp3.0 -c Release -r ubuntu-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true -o bin/Release/Benchmarker-ubuntu64
dotnet publish Benchmarker.csproj --framework netcoreapp3.0 -c Release -r osx-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true -o bin/Release/Benchmarker-osx64

Copy-Item ../README.md bin/Release/Benchmarker/README.md
Copy-Item ../README.md bin/Release/Benchmarker-win64/README.md
Copy-Item ../README.md bin/Release/Benchmarker-win32/README.md
Copy-Item ../README.md bin/Release/Benchmarker-linux-arm32v7/README.md
Copy-Item ../README.md bin/Release/Benchmarker-win-arm32v7/README.md
Copy-Item ../README.md bin/Release/Benchmarker-win-arm64/README.md
Copy-Item ../README.md bin/Release/Benchmarker-linux64/README.md
Copy-Item ../README.md bin/Release/Benchmarker-debian64/README.md
Copy-Item ../README.md bin/Release/Benchmarker-ubuntu64/README.md
Copy-Item ../README.md bin/Release/Benchmarker-osx64/README.md

Copy-Item ../LICENSE bin/Release/Benchmarker/LICENSE
Copy-Item ../LICENSE bin/Release/Benchmarker-win64/LICENSE
Copy-Item ../LICENSE bin/Release/Benchmarker-win32/LICENSE
Copy-Item ../LICENSE bin/Release/Benchmarker-linux-arm32v7/LICENSE
Copy-Item ../LICENSE bin/Release/Benchmarker-win-arm32v7/LICENSE
Copy-Item ../LICENSE bin/Release/Benchmarker-win-arm64/LICENSE
Copy-Item ../LICENSE bin/Release/Benchmarker-linux64/LICENSE
Copy-Item ../LICENSE bin/Release/Benchmarker-debian64/LICENSE
Copy-Item ../LICENSE bin/Release/Benchmarker-ubuntu64/LICENSE
Copy-Item ../LICENSE bin/Release/Benchmarker-osx64/LICENSE