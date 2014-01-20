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
	public class SoundEffectFile : IAudioFile
	{
		public string Name { get; private set; }

		public SoundState State { get { return Instance.State; } }

		public SoundEffect SoundEffect { get; private set; }

		private SoundEffectInstance Instance;

		public SoundEffectFile (string name, SoundEffect soundEffect)
		{
			Name = name;
			SoundEffect = soundEffect;
			Instance = soundEffect.CreateInstance ();
		}

		public void Play ()
		{
			Console.WriteLine ("Play: " + Name);
			Instance.Play ();
		}

		public void Stop ()
		{
			Console.WriteLine ("Stop: " + Name);
			Instance.Stop ();
		}
	}
}
