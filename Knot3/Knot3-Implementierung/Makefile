X = xbuild
CP = cp -v
CPR = cp -rvf
RM = rm -v
RMR = rm -rvf
MKDIR = mkdir -p
BINDIR = $(DESTDIR)/usr/games
GAMEDIR = $(DESTDIR)/usr/share/knot3
NAME = knot3
SOLUTION=Knot3-Linux.csproj

all: build

build:
	xbuild $(SOLUTION)

install: build
	install --mode=755 $(NAME) $(BINDIR)/
	$(MKDIR) $(GAMEDIR)
	$(CPR) bin/Debug/* $(GAMEDIR)/

clean: distclean

distclean:
	$(RMR) Knot3-Implementierung/bin Knot3-Unit-Tests/bin

uninstall:
	$(RM) $(BINDIR)/$(NAME)
	$(RMR) $(GAMEDIR)/

.PHONY: clean distclean uninstall build install all
