echo.
echo --- OpenCover.bat ---
echo.
::
:: Folgende Tools müssen installiert sein:
::
:: !!!!!!!!!!!!!!!!!!!!!!!!!
::
:: NUnit (2.6.3)
:: OpenCover (4.5.1923)
:: ReportGenerator (dort hin tun: %ProgramFiles(x86)%\ReportGenerator)
::
:: !!!!!!!!!!!!!!!!!!!!!!!!!
::
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
set PATH_TO_OPENCOVER="%USERPROFILE%\AppData\Local\Apps\OpenCover"
set PATH_TO_NUNIT="%ProgramFiles(x86)%\NUnit 2.6.3\bin"
cd ..
cd ..
cd ..
set PATH_TO_PROJECT="%CD%\Knot3-Unit-Tests\bin\Debug\Knot3.UnitTests.dll"
set PATH_TO_REPORTGENERATOR="%ProgramFiles(x86)%\ReportGenerator\bin"
echo.
echo.
echo Settings:
echo.
echo Current path: %CD%
echo.
echo Paths to ...
echo  NUnit           : %PATH_TO_NUNIT%
echo  OpenCover       : %PATH_TO_OPENCOVER%
echo  VS-Project      : %PATH_TO_PROJECT%
echo  ReportGenerator : %PATH_TO_REPORTGENERATOR%
echo.
::
:: Hinweis: Auf einem 64-Bit-System ist nunit-console-x86.exe für 32-Bit-Projekte zu verwenden! (sonst tritt ein Fehler auf)
::
%PATH_TO_OPENCOVER%\OpenCover.Console.exe -target:%PATH_TO_NUNIT%\nunit-console-x86.exe -targetargs:"/noshadow %PATH_TO_PROJECT%" -filter:"+[Tests*]*" -output:NUnit_test_coverage.xml
echo.
echo Current path: %CD%
echo.
set PATH_TO_REPORT="%CD%\NUnit-Test-Coverage-Windows\bin\Debug"
echo Path to report: %PATH_TO_REPORT%
echo.
%PATH_TO_REPORTGENERATOR%\ReportGenerator.exe %PATH_TO_REPORT%\NUnit_test_coverage.xml %PATH_TO_REPORT%\coverage_report