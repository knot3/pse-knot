#!/usr/bin/perl

use strict;
use warnings;

sub author {
	my ($str) = @_;
	$str =~ s/^\s+//gm;
	$str =~ s/\s+$//gm;
	$str =~ s/[^a-zA-Z\s]//gm;
	return $str;
}

sub pad {
	my ($num, $len) = @_;
	return ' ' x ($len - length $num) . $num;
}

my @files = split(/[\r\n]+/, `git ls-files Knot3 Content/Shader/*.fx Content/models.ini`);

my @stat_files_authors_percent = ();
my %stat_authors_files_percent = ();
my %authors_allfiles_percent = ();
my $allfiles_loc = 0;

foreach my $file (@files) {
	my @blame = split(/[\r\n]+/, `git blame -w $file`);
	my %authors = ();
	foreach my $line (@blame) {
		if ($line =~ /^.*?\((.*?)\s[\d]{4}/) {
			$authors{author($1)} += 1;
		}
	}

	delete $authors{"PSE Knot"} if defined $authors{"PSE Knot"};
	delete $authors{"Not Committed Yet"} if defined $authors{"Not Committed Yet"};
	my $count = 0;
	$count += $_ foreach (values %authors);
	$authors_allfiles_percent{$_} += $authors{$_} foreach (keys %authors);
	$allfiles_loc += $count;
	do { $authors{$_} /= 5 foreach (keys %authors) } if $file =~ /.ini$/;

	push @stat_files_authors_percent, $file."\t".join(", ", map { $_.": ".int($authors{$_}/$count*100)."%" } keys %authors);
	foreach my $author (keys %authors) {
		$stat_authors_files_percent{$author} ||= [];
		push @{$stat_authors_files_percent{$author}}, [$authors{$author}, ($authors{$author}/$count), $file];
	}
}

#print join("\n", @stat_files_authors_percent)."\n";
my $authors_files = "";
foreach my $author (keys %stat_authors_files_percent) {
	my $author_percent = sprintf("%.1f", $authors_allfiles_percent{$author}/$allfiles_loc*100);
	$authors_files .= "  $author (".$author_percent."%):\n\n";
	foreach my $entry (sort {int(100*$b->[1]) <=> int(100*$a->[1]) or $a->[2] cmp $b->[2]} @{$stat_authors_files_percent{$author}}) {
		my ($total_lines, $percent_lines, $file) = @$entry;
		$percent_lines = sprintf("%.1f", $percent_lines*100);
		$authors_files .= "    ".pad($percent_lines, 5)."%    ".pad($total_lines, 5)." lines    ".$file."\n";
	}
	$authors_files .= "\n";
}

open my $fh, ">", "AUTHORS";
print $fh <<END;
  Copyright (C) 2013-2014 Tobias Schulz, Maximilian Reuter, Pascal Knodel,
                          Gerd Augsburg, Christina Erler, Daniel Warzel

  Copying, redistribution and use of code written by Tobias Schulz, in
  source and binary forms, with or without modification, are permitted
  provided that the conditions of the 2-Clause BSD license are met:
  http://choosealicense.com/licenses/bsd/

  The license of code written by Maximilian Reuter, Pascal Knodel,
  Gerd Augsburg, Christina Erler and Daniel Warzel is unknown.

  The license of the 3D models is unknown because of the creator's
  unknown identity.

  The sound effects are licensed under the CC BY 3.0 license:
  http://creativecommons.org/licenses/by/3.0/

  The music is licensed under the CC BY-NC-SA 3.0 license:
  http://creativecommons.org/licenses/by-nc-sa/3.0/

  Authors by lines of code:

END
print $fh $authors_files;
close $fh;



