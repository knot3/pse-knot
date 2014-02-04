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

using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Core
{
	/// <summary>
	/// Eine Schnittstelle, die von Klassen implementiert wird, die auf Maus-Klicks reagieren.
	/// </summary>
	public interface IMouseMoveEventListener
	{
		#region Properties

		/// <summary>
		/// Die Eingabepriorität.
		/// </summary>
		DisplayLayer Index { get; }

		/// <summary>
		/// Ob die Klasse zur Zeit auf Mausbewegungen reagiert.
		/// </summary>
		bool IsMouseMoveEventEnabled { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Die Ausmaße des von der Klasse repräsentierten Objektes.
		/// </summary>
		Bounds MouseMoveBounds { get; }

		void OnLeftMove (ScreenPoint previousPosition, ScreenPoint currentPosition, ScreenPoint move, GameTime time);

		void OnRightMove (ScreenPoint previousPosition, ScreenPoint currentPosition, ScreenPoint move, GameTime time);

		void OnMove (ScreenPoint previousPosition, ScreenPoint currentPosition, ScreenPoint move, GameTime time);

		#endregion
	}
}
