@echo off
echo %cd%
dotnet build -r win8-x64
cd bin
cd Debug 
cd netcoreapp2.0
cd win8-x64
start ECP.B2b.Main.GrpcServer.exe
cd..
cd..
cd..
cd..
echo %cd%
cmd /k echo
pause
