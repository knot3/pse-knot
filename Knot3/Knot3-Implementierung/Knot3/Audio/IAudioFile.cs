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
	/// <summary>
	/// Repräsentiert eine Audiodatei.
	/// </summary>
	public interface IAudioFile
	{
		/// <summary>
		/// Der Anzeigename der Audiodatei.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gibt an, ob die Wiedergabe läuft oder gestoppt bzw. pausiert ist.
		/// </summary>
		SoundState State { get; }

		/// <summary>
		/// Starte die Wiedergabe.
		/// </summary>
		void Play ();

		/// <summary>
		/// Stoppe die Wiedergabe.
		/// </summary>
		void Stop ();
	}

}
