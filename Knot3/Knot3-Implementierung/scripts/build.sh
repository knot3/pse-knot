#!/bin/bash

export PATH=/home/pse-knot/perl5/bin:/home/pse-knot/bin:/usr/local/bin:/usr/bin:/bin:/usr/local/games:/usr/games:$PATH
cd /home/pse-knot

cd pse-knot
git reset --hard HEAD
git clean -dxf
git checkout HEAD --force
git pull origin master
git pull

find -name Thumbs.db -exec rm '{}' \;

(
	cd Knot3

#	cd Knot3-Implementierung/; mkdir Standard_Knots; find . -name "*.knot" -exec git mv '{}' Standard_Knots \; ; rmdir * ; cd ..
	cd Knot3-Unit-Tests/; mkdir Standard_Knots; find . -name "*.knot" -exec git mv '{}' Standard_Knots \; ; rmdir * ; cd ..

	cd ..
)

(
	cd Knot3/Knot3-Implementierung/

	astyle-csharp --suffix=none --lineend=linux Knot3/*/*.cs Knot3/*.cs ../*/*.cs
	~/bin/corrent-empty-lines
	scripts/copyright.pl

	cd ../..

	export LAST_AUTHOR=$(git log --stat | grep Author:  | cut -d" " -f2,3,4,5,6,7,8,9 | grep -vi "PSE Knot" | head -n 1)
	git commit -a -m "Code Format"  --author="$LAST_AUTHOR"
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
	export V=$(basename ../*.deb | perl -n -e 'print $1 if (/_(.*?)_/);')
	mv -f ../*.deb *.rpm /var/www/knot3.de/download/

	# export V=$(git rev-list HEAD --count)-$(git log --oneline | head -n 1 | cut -d' ' -f1)
	unzip -o ~/MonoGame-Windows-3.1.2.zip -d /usr/lib/mono/4.0/
	xbuild /p:Configuration=Release Knot3-Linux.csproj
	cp -rf Standard_Knots/ Content/ bin/Release/
	unzip -o ~/MonoGame-Windows-3.1.2.zip -d bin/Release/
	cp ~/oalinst.exe bin/Release/
	mv bin/Release/MonoGame*/* bin/Release/
	rmdir bin/Release/*
	find bin/Release/ -name "*.csproj" -exec rm '{}' \;
	cp debian/changelog bin/Release/CHANGELOG
	cp AUTHORS bin/Release/
	cd bin/Release/; zip -r ../../Knot3-git$V.zip *; cd ../..
	mv Knot3-*.zip /var/www/knot3.de/download/

	unzip -o ~/MonoGame-Linux-20130126.zip -d /usr/lib/mono/4.0/

	# Source tarballs
	rm -rf bin/
	cd ..
	cp -rf Knot3-Implementierung/ knot3_$V
	tar cplSzf knot3_$V.tar.gz knot3_$V
	rm -rf knot3_$V
	mv -f knot3_$V.tar.gz /var/www/knot3.de/download/
	cd Knot3-Implementierung/
)

rm -f /var/www/knot3.de/download/{knot3_.tar.gz,Knot3-git.zip}

