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
	/// Ein abstrakte Klasse für Menüeinträge, die
	/// </summary>
	public class MenuItem : Widget, IKeyEventListener, IMouseEventListener
	{

        #region Properties

		/// <summary>
		/// Gibt an, ob die Maus sich über dem Eintrag befindet, ohne ihn anzuklicken, ob er ausgewählt ist
		/// oder nichts von beidem.
		/// </summary>
		public ItemState ItemState { get; set; }

		/// <summary>
		/// Die Zeichenreihenfolge.
		/// </summary>
		public int ItemOrder { get; set; }

		/// <summary>
		/// Der Anzeigetext der Schaltfläche.
		/// </summary>
		public string Text { get; set; }

        #endregion
		
        #region Constructors

		public MenuItem (GameScreen screen, DisplayLayer drawOrder, string text)
			: base(screen, drawOrder)
		{
			Text = text;
			ItemOrder = -1;
			ItemState = Widgets.ItemState.None;
		}

        #endregion

        #region Methods

		/// <summary>
		/// Reaktionen auf einen Linksklick.
		/// </summary>
		public virtual void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Reaktionen auf einen Rechtsklick.
		/// </summary>
		public virtual void OnRightClick (Vector2 position, ClickState state, GameTime time)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Reaktionen auf Tasteneingaben.
		/// </summary>
		public virtual void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
		}

		public virtual void SetHovered (bool hovered)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public virtual void OnScroll (int scrollValue)
		{
			throw new System.NotImplementedException ();
		}

        #endregion

	}
}

