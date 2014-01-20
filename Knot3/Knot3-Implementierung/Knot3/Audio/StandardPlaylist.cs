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

namespace Knot3.Audio
{
	public class StandardPlaylist : IPlaylist
	{
		private List<IAudioFile> Sounds;
		private int index;

		public SoundState State { get; private set; }

		public StandardPlaylist (IEnumerable<IAudioFile> sounds)
		{
			Sounds = sounds.ToList ();
			index = 0;
			State = SoundState.Stopped;
			
			Console.WriteLine ("Created new playlist (" + Sounds.Count + " songs)");
			foreach (IAudioFile sound in Sounds) {
				Console.WriteLine ("  - " + sound.Name);
			}
		}

		public void Start ()
		{
			State = SoundState.Playing;
			Sounds [index].Play ();
		}

		public void Stop ()
		{
			State = SoundState.Stopped;
			Sounds [index].Stop ();
		}

		public void Update (GameTime time)
		{
			if (State == SoundState.Playing && Sounds.At (index).State != SoundState.Playing) {
				++index;
				Sounds.At (index).Play ();
			}
		}
	}
}
