@echo off

setlocal enabledelayedexpansion

for %%i in (*.fx) do (
	set file=%%~ni
	echo !file!
	"C:\Program Files (x86)\MSBuild\Monogame\v3.0\2mgfx.exe" !file!.fx !file!.mgfx
)
pause

