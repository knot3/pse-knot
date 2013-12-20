CSharpUML.exe -vs2tex
CSharpUML.exe -uml2code -target ../../Knot3/Knot3-Implementierung/src/

pdflatex.exe -synctex=1 -interaction=nonstopmode Entwurfsdokument.tex
makeindex.exe Entwurfsdokument.idx
pdflatex.exe -synctex=1 -interaction=nonstopmode Entwurfsdokument.tex
