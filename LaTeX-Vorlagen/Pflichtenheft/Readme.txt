Texstudio: Aufruf von pdflatex, damit ein Glossar erstellt wird.

pdflatex.exe -synctex=1 -interaction=nonstopmode --shell-escape %.tex | makeglossaries %