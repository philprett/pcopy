@echo off

if not exist "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\vsdevcmd\core\vsdevcmd_start.bat" goto novisualstudio
call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\vsdevcmd\core\vsdevcmd_start.bat"
call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\vsdevcmd\core\msbuild.bat"
call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\vsdevcmd\core\vsdevcmd_end.bat"

CD /D "%~dp0"

rmdir PCopy\PCopy\bin /s /q

msbuild PCopy\PCopy\PCopy.csproj /p:Configuration=Debug /t:Rebuild
if errorlevel 1 goto builderror

msbuild PCopy\PCopy\PCopy.csproj /p:Configuration=Release /t:Rebuild
if errorlevel 1 goto builderror
copy PCopy\PCopy\bin\Release\PCopy.exe .

rmdir PCopy\PDelete\bin /s /q

msbuild PCopy\PDelete\PDelete.csproj /p:Configuration=Debug /t:Rebuild
if errorlevel 1 goto builderror

msbuild PCopy\PDelete\PDelete.csproj /p:Configuration=Release /t:Rebuild
if errorlevel 1 goto builderror
copy PCopy\PDelete\bin\Release\PDelete.exe .

goto :EOF


:novisualstudio
echo We could not find the Visual Studio 2017 command line tools.
pause
goto :EOF

:builderror
echo There were some errors while building
pause
goto :EOF
