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
	/// Ein Wrapper um die SoundEffect-Klasse des XNA-Frameworks.
	/// </summary>
	public class SoundEffectFile : IAudioFile
	{
		/// <summary>
		/// Der Anzeigename des SoundEffects.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gibt an, ob die Wiedergabe läuft oder gestoppt bzw. pausiert ist.
		/// </summary>
		public SoundState State { get { return Instance.State; } }

		public SoundEffect SoundEffect { get; private set; }

		private SoundEffectInstance Instance;

		private Sound SoundType;
		private float volume;

		/// <summary>
		/// Erstellt eine neue SoundEffect-Datei mit dem angegebenen Anzeigenamen und des angegebenen SoundEffect-Objekts.
		/// </summary>
		public SoundEffectFile (string name, SoundEffect soundEffect, Sound soundType)
		{
			Name = name;
			SoundEffect = soundEffect;
			Instance = soundEffect.CreateInstance ();
			SoundType = soundType;
		}

		public void Play ()
		{
			Log.Debug ("Play: " + Name);
			Instance.Volume = volume = AudioManager.Volume(SoundType);
			Instance.Play ();
		}

		public void Stop ()
		{
			Log.Debug ("Stop: " + Name);
			Instance.Stop ();
		}

		public void Update (GameTime time)
		{
			if (volume != AudioManager.Volume (SoundType)) {
				Instance.Volume = volume = AudioManager.Volume(SoundType);
			}
		}
	}
}
