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
	/// Dieses Interface repräsentiert eine Playlist.
	/// </summary>
	public interface IPlaylist
	{
		/// <summary>
		/// Starte die Wiedergabe.
		/// </summary>
		void Start ();

		/// <summary>
		/// Stoppe die Wiedergabe.
		/// </summary>
		void Stop ();

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		void Update (GameTime time);

		void Shuffle ();
	}
}
