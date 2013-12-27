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
using Knot3.Utilities;

namespace Knot3.Widgets
{
	/// <summary>
	/// Ein abstrakte Klasse für Menüeinträge, die
	/// </summary>
	public abstract class MenuItem : Widget, IKeyEventListener, IMouseEventListener
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
		public string Text { get; private set; }

		/// <summary>
		/// Eine Referenz auf das Menu, in dem sich der Eintrag befindet.
		/// </summary>
		public Menu Menu { get; set; }
		
		protected SpriteBatch spriteBatch;

        #endregion
		
        #region Constructors

		public MenuItem (GameScreen screen, DisplayLayer drawOrder, string text)
			: base(screen, drawOrder)
		{
			Text = text;
			ItemOrder = -1;
			ItemState = ItemState.None;
			spriteBatch = new SpriteBatch (screen.Device);
		}

        #endregion

        #region Methods

		/// <summary>
		/// Reaktionen auf einen Linksklick.
		/// </summary>
		public virtual void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
		}

		/// <summary>
		/// Reaktionen auf einen Rechtsklick.
		/// </summary>
		public virtual void OnRightClick (Vector2 position, ClickState state, GameTime time)
		{
		}

		/// <summary>
		/// Reaktionen auf Tasteneingaben.
		/// </summary>
		public virtual void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
		}

		public virtual void SetHovered (bool hovered)
		{
			ItemState = hovered ? ItemState.Hovered : ItemState.None;
		}

		/// <summary>
		/// Die Reaktion auf eine Bewegung des Mausrads.
		/// </summary>
		public virtual void OnScroll (int scrollValue)
		{
			Menu.OnScroll (scrollValue);
		}

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			Console.WriteLine ("...: " + this + ", position=" + ScaledPosition + ", size=" + ScaledSize);
			spriteBatch.Begin ();
				
			// zeichne den Hintergrund
			spriteBatch.DrawColoredRectangle (BackgroundColor (), Bounds ());

			// lade die Schrift
			SpriteFont font = HfGDesign.MenuFont (Screen);

			// zeichne die Schrift
			spriteBatch.DrawStringInRectangle (font, Text, ForegroundColor (), Bounds (), AlignX, AlignY);

			spriteBatch.End ();
		}

        #endregion

	}
}

