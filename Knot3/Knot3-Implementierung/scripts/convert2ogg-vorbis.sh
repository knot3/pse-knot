find -name "*.mp3" -type f | while read mp3
do
	ogg=$(echo $mp3 | sed 's@mp3@ogg@igm')
	ffmpeg -i $mp3 -acodec libvorbis $ogg
done
