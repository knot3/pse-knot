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
	/// Eine Schaltfläche, der eine Zeichenkette anzeigt und auf einen Linksklick reagiert.
	/// </summary>
	public class MenuButton : MenuItem
	{
        #region Properties

		/// <summary>
		/// Die Aktion, die ausgeführt wird, wenn der Spieler auf die Schaltfläche klickt.
		/// </summary>
		public Action OnClick { get; set; }

        #endregion

        #region Constructors

		/// <summary>
		/// Erzeugt ein neues MenuButton-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
		/// Zudem sind Angabe der Zeichenreihenfolge, einer Zeichenkette für den Namen der Schaltfläche
		/// und der Aktion, welche bei einem Klick ausgeführt wird Pflicht.
		/// </summary>
		public MenuButton (GameScreen screen, DisplayLayer drawOrder, string name, Action onClick)
			: base(screen, drawOrder, name)
		{
			OnClick = onClick;
		}

        #endregion

		#region Methods

		/// <summary>
		/// Reaktionen auf einen Linksklick.
		/// </summary>
		public override void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			OnClick ();
		}

		/// <summary>
		/// Reaktionen auf Tasteneingaben.
		/// </summary>
		public override void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			if (keyEvent == KeyEvent.KeyDown) {
				OnClick ();
			}
		}

		public void AddKey (Keys key)
		{
			if (!ValidKeys.Contains (key)) {
				ValidKeys.Add (key);
			}
		}

		#endregion
	}
}

