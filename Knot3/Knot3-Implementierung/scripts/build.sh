#!/bin/bash

export PATH=/home/pse-knot/perl5/bin:/home/pse-knot/bin:/usr/local/bin:/usr/bin:/bin:/usr/local/games:/usr/games:$PATH
cd /home/pse-knot

cd pse-knot
git reset --hard HEAD
git clean -dxf
git checkout HEAD --force
git pull origin master
git pull

(
	cd Knot3/Knot3-Implementierung/

	astyle-csharp --suffix=none --lineend=linux Knot3/*/*.cs Knot3/*.cs ../*/*.cs
	~/bin/corrent-empty-lines
	scripts/copyright.pl

	cd ../..

	git commit -a -m "Code Format"
	git push origin master
)

(
	cd Knot3

	doxygen Knot3.doxygen
	rsync -zavP doc/ /var/www/knot3.de/doc/

	cd ..
)

(
	cd Knot3/Knot3-Implementierung/

	unzip -o ~/MonoGame-Linux-20130126.zip -d /usr/lib/mono/4.0/
	# xbuild /p:Configuration=Release Knot3-Linux.sln
	gitlog-to-deblog.rb | sed 's@pse-knot@knot3@gm' > debian/changelog
	debuild -b -us -uc
	alien -r ../*.deb
	mv -f ../*.deb *.rpm /var/www/knot3.de/download/

	export V=$(git rev-list HEAD --count)-$(git log --oneline | head -n 1 | cut -d' ' -f1)
	unzip -o ~/MonoGame-Windows-3.1.2.zip -d /usr/lib/mono/4.0/
	xbuild /p:Configuration=Release Knot3-Linux.csproj
	cp -rf Savegames/ Content/ bin/Release/
	unzip -o ~/MonoGame-Windows-3.1.2.zip -d bin/Release/
	cp ~/oalinst.exe bin/Release/
	mv bin/Release/MonoGame*/* bin/Release/
	rmdir bin/Release/*
	find bin/Release/ -name "*.csproj" -exec rm '{}' \;
	cp debian/changelog bin/Release/Changelog.txt
	cd bin/Release/; zip -r ../../Knot3-git$V.zip *; cd ../..
	mv Knot3-*.zip /var/www/knot3.de/download/

	unzip -o ~/MonoGame-Linux-20130126.zip -d /usr/lib/mono/4.0/

	cd ../..
)
