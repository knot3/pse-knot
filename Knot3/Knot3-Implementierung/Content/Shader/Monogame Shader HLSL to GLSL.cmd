@echo off

setlocal enabledelayedexpansion

for %%i in (*.fx) do (
	set file=%%~ni
	echo !file!
	"C:\Program Files (x86)\MSBuild\Monogame\v3.0\2mgfx.exe" !file!.fx !file!_3.0.mgfx
	"..\..\Tools\2MGFX\2mgfx.exe" !file!.fx !file!_3.1.mgfx
)
pause

