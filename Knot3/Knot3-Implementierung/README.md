#Knot3

Bei Knot3 handelt es sich um ein innovatives Spiel bei dem man Knoten im dreidimensionalem Raum entweder frei modifizieren oder nach Vorgabe auf Zeit ineinander überführen kann.

##Installation

###Debian

A debian repository is available. You need to include it in your sources.list file to install Knot3:

    echo deb http://www.knot3.de debian/ | sudo tee /etc/apt/sources.list.d/knot3
    apt-get update
    apt-get install knot3

###Other Linux

You need to have Mono (3.0+), MonoGame (3.1.X+) and xbuild installed. Once installed,
simply run:

    make
    make install

##Authors

* [Tobias Schulz](https://github.com/tobiasschulz)
* [Gerd Augsburg](https://github.com/Balduro)
* [Maximilian Reuter](https://github.com/Maximilian-Reuter)
* [Pascal Knodel](https://github.com/pse)
* [Christina Erler](https://github.com/Sakurachan4)
* [Daniel Warzel](https://github.com/wudi0910)

Knot3 is the work of students at [Karlsruhe Institute of Technology](http://www.kit.edu)
in collaborative work with M. Retzlaff, F. Kalka, G. Hoffmann, T. Schmidt.

