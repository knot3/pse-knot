# make Datei fuer LaTeX
# Time-stamp: <2009-02-14 11:57:27 ewi>
# Aufruf: make macht das, was hinter all: steht, bei Bedarf anpassen.
#         make psA5 macht eine ps-Ausgabe in Din A5 Format.
#         make clean loescht alles, was neu erzeugt werden kann.
#         make pdf macht eine pdf-Ausgabe in Din A4 Format.
#         make view zeigt die dvi-Version auf dem Monitor an.
# ganz wichtig: die Befehlszeilen muessen mit einem TAB beginnen!!!

MAINFILE = pflichtenheft
INCLUDEFILES = zielbestimmung.tex produkteinsatz.tex funktionale_anforderungen.tex produktdaten.tex nichtfunktionale_anforderungen.tex systemmodelle.tex glossar.tex
LATEXVIEW = xdvi
PDFVIEW = evince # acroread xpdf

all: ${MAINFILE}.dvi

psA5: ${MAINFILE}.dvi
	dvips -o ${MAINFILE}.ps ${MAINFILE}.dvi
	psnup -2 ${MAINFILE}.ps > ${MAINFILE}A5.ps
	rm ${MAINFILE}.ps

pdf: ${MAINFILE}.tex ${INCLUDEFILES}
	pdflatex ${MAINFILE}.tex
	pdflatex ${MAINFILE}.tex
	$(PDFVIEW) ${MAINFILE}.pdf

view: ${MAINFILE}.dvi
	${LATEXVIEW} ${MAINFILE}.dvi

tgz: distclean
	cd ..; tgz ${MAINFILE}.tgz ${MAINFILE}; \
        mv ${MAINFILE}.tgz ${MAINFILE}

${MAINFILE}.dvi: ${MAINFILE}.tex ${INCLUDEFILES}
	latex ${MAINFILE}.tex
	latex ${MAINFILE}.tex

clean: distclean
	rm -f $(MAINFILE).{dvi,ps,pdf}	
distclean:
	rm -f *.aux
	rm -f *.log
	rm -f *.toc
	rm -f *.pdf
