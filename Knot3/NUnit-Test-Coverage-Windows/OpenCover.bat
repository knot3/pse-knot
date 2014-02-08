echo.
echo --- OpenCover.bat ---
echo.
::
:: Visual Studio: 
::
:: 1. Einstellungen treffen: (Projektmappe)
::
:: NUnit-Test-Coverage-Windows -(Rechtsklick)-> Eigenschaften ->
::
:: Befehlszeile für Postbuildereignis : "$(SolutionDir)NUnit-Test-Coverage-Windows\OpenCover.bat"
:: Postbuildereignis ausführen        : Immer
::
:: 2. Erstellen: (Visual Studio Fenster, Menüzeile oben)
::
:: Erstellen -> NUnit-Test-Coverage-Windows neu erstellen
::
:: ...
::
set PATH_TO_NUNIT="%ProgramFiles(x86)%\NUnit 2.6.3\bin"
set PATH_TO_OPENCOVER="%USERPROFILE%\AppData\Local\Apps\OpenCover"
cd ..
cd ..
cd ..
set PATH_TO_PROJECT="%CD%\Knot3-Unit-Tests\bin\Debug"
echo.
echo.
echo Settings:
echo.
echo Current path: %CD%
echo.
echo Paths to ...
echo  NUnit      : %PATH_TO_NUNIT%
echo  OpenCover  : %PATH_TO_OPENCOVER%
echo  VS-Project : %PATH_TO_PROJECT%
echo.
%PATH_TO_OPENCOVER%\OpenCover.Console.exe -target:%PATH_TO_NUNIT%\nunit-console.exe -targetargs:"/noshadow %CD%\Assembly.exe" -filter:"+[*]*" -output:NUnit_test_coverage.xml