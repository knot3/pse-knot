using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
	/// Eine Schnittstelle, die von Klassen implementiert wird, welche auf Tastatureingaben reagieren.
	/// </summary>
	public interface IKeyEventListener
	{
		#region Properties

		/// <summary>
		/// Die Eingabepriorität.
		/// </summary>
		DisplayLayer Index { get; }

		/// <summary>
		/// Zeigt an, ob die Klasse zur Zeit auf Tastatureingaben reagiert.
		/// </summary>
		bool IsKeyEventEnabled { get; }

		bool IsModal { get; }

		/// <summary>
		/// Die Tasten, auf die die Klasse reagiert.
		/// </summary>
		List<Keys> ValidKeys { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Die Reaktion auf eine Tasteneingabe.
		/// </summary>
		void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time);

		#endregion
	}
}
