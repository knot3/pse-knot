Es muss Inkscape installiert sein.
Es muss Perl installiert sein (Empf. f. Windows: ActivePerl)


Texstudio/Etc.: Aufruf von pdflatex, damit ein Glossar erstellt wird.


pdflatex.exe -synctex=1 -interaction=nonstopmode --shell-escape %.tex | makeglossaries %


"makeglossaries Pflichtenheft" kann im obigen Aufruf auch weggelassen und
manuell über die Shell ausgeführt werden.