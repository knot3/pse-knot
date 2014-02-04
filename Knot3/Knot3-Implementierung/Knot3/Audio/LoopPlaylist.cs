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
using Knot3.Development;

namespace Knot3.Audio
{
	/// <summary>
	/// Diese Klasse repräsentiert eine Playlist, deren Audiodateien der reihe nach in einer
	/// Endlosschleife abgespielt werden.
	/// </summary>
	public class LoopPlaylist : IPlaylist
	{
		private List<IAudioFile> Sounds;
		private int index;

		public SoundState State { get; private set; }

		/// <summary>
		/// Erstellt eine neue Playlist.
		/// </summary>
		/// <param name='sounds'>
		/// Die abzuspielenden Audiodateien.
		/// </param>
		public LoopPlaylist (IEnumerable<IAudioFile> sounds)
		{
			Sounds = sounds.ToList ();
			index = 0;
			State = SoundState.Stopped;

			Log.Debug ("Created new playlist (" + Sounds.Count + " songs)");
			foreach (IAudioFile sound in Sounds) {
				Log.Debug ("  - " + sound.Name);
			}
		}

		public void Shuffle ()
		{
			Sounds = Sounds.Shuffle ().ToList ();
		}

		/// <summary>
		/// Starte die Wiedergabe.
		/// </summary>
		public void Start ()
		{
			if (Sounds.Count > 0) {
				State = SoundState.Playing;
				Sounds .At (index).Play ();
			}
		}

		/// <summary>
		/// Stoppe die Wiedergabe.
		/// </summary>
		public void Stop ()
		{
			if (Sounds.Count > 0) {
				State = SoundState.Stopped;
				Sounds.At (index).Stop ();
			}
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public void Update (GameTime time)
		{
			if (Sounds.Count > 0) {
				if (State == SoundState.Playing && Sounds.At (index).State != SoundState.Playing) {
					++index;
					Sounds.At (index).Play ();
				}
			}
			Sounds.At (index).Update (time);
		}
	}
}
