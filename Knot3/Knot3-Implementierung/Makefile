X = xbuild
CP = cp -v
CPR = cp -rvf
RM = rm -v
RMR = rm -rvf
MKDIR = mkdir -p
UNZIP = unzip
BINDIR = $(DESTDIR)/usr/games
GAMEDIR = $(DESTDIR)/usr/share/knot3
NAME = knot3
SOLUTION=Knot3-Linux.csproj

all: build

build:
	xbuild $(SOLUTION)

install: build
	$(MKDIR) $(BINDIR)
	install --mode=755 knot3.sh $(BINDIR)/$(NAME)
	$(MKDIR) $(GAMEDIR)
	$(UNZIP) -o -d bin/Debug/ lib/MonoGame-Linux-3.1.2.zip
	$(CPR) bin/Debug/* $(GAMEDIR)/
	$(CPR) Standard_Knots/ $(GAMEDIR)/
	$(CP) AUTHORS LICENSE $(GAMEDIR)/
	$(CP) debian/changelog $(GAMEDIR)/CHANGELOG

clean: distclean

distclean:
	$(RMR) bin

uninstall:
	$(RM) $(BINDIR)/$(NAME)
	$(RMR) $(GAMEDIR)/

.PHONY: clean distclean uninstall build install all

