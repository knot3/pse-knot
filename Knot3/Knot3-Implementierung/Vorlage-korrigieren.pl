#!/usr/bin/perl

use strict;
use warnings;

my $header = q[using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace];

while (my $filename = <STDIN>) {
	chomp $filename;
	open my $f, "<", $filename;
	my $content = join("", grep {!/^\tusing System/} <$f>);
	close $f;

	$content =~ s/^.*?namespace/$header/igms;
	
	open my $f, ">", $filename;
	print $f $content;
	close $f;
}
