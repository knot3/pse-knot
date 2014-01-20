using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;

namespace Knot3.Audio.XNA
{
	/// <summary>
	/// Ein Wrapper um die Song-Klasse des XNA-Frameworks.
	/// </summary>
	public class SongFile : IAudioFile
	{
		/// <summary>
		/// Der Anzeigename des Songs.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gibt an, ob die Wiedergabe läuft oder gestoppt bzw. pausiert ist.
		/// </summary>
		public SoundState State
		{
			get {
				//Console.WriteLine (MediaPlayer.State);
				return MediaPlayer.State == MediaState.Playing ? SoundState.Playing
					: MediaPlayer.State == MediaState.Stopped ? SoundState.Stopped
					: MediaPlayer.State == MediaState.Paused ? SoundState.Paused
					: SoundState.Stopped;
			}
		}

		private Song Song;
        private bool valid;

		/// <summary>
		/// Erstellt eine neue Song-Datei mit dem angegebenen Anzeigenamen und des angegebenen Song-Objekts.
		/// </summary>
		public SongFile (string name, Song song)
		{
			Name = name;
			Song = song;
            valid = true;
		}

		public void Play ()
		{
            if (valid)
            {
                Console.WriteLine("Play: " + Name);
                try
                {
                    MediaPlayer.Play(Song);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    valid = false;
                }
            }
		}

		public void Stop ()
		{
			Console.WriteLine ("Stop: " + Name);
			MediaPlayer.Stop ();
		}
	}	
}
