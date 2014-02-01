#!/bin/bash

# cd /usr/share/knot3/
# mono Knot3.exe

rsync -zavP /usr/share/knot3/ $HOME/.knot3/base/
cd $HOME/.knot3/base/
mono Knot3.exe

