using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

namespace Knot3.Widgets
{
	/// <summary>
	///
	/// </summary>
	public sealed class HighscoreDialog : Dialog
	{
		#region Constructors

		/// <summary>
		///
		/// </summary>
		public HighscoreDialog (GameScreen screen, DisplayLayer drawOrder, Challenge challenge)
		: base(screen, drawOrder, "Highscores", "fuck you")
		{
			throw new System.NotImplementedException ();
		}

		#endregion

	}
}

