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
	/// Ein abstrakte Klasse für Menüeinträge.
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
		/// Der Name des Menüeintrags, der auf der linken Seite angezeigt wird.
		/// </summary>
		public string Text { get; private set; }

		/// <summary>
		/// Eine Referenz auf das Menu, in dem sich der Eintrag befindet.
		/// </summary>
		public Menu Menu { get; set; }

		/// <summary>
		/// Wie viel Prozent der Name des Eintrags (auf der linken Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		protected virtual float NameWidth { get { return 0.5f; } }

		/// <summary>
		/// Wie viel Prozent der Wert des Eintrags (auf der rechten Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		protected virtual float ValueWidth { get { return 0.5f; } }
		
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
			if (Menu != null) {
				Menu.OnScroll (scrollValue);
			} else {
				Console.WriteLine ("Warning: MenuItem is not assigned to a menu: " + this);
			}
		}

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			if (IsVisible) {
				spriteBatch.Begin ();
				
				// zeichne den Hintergrund
				spriteBatch.DrawColoredRectangle (BackgroundColor (), Bounds ());

				// lade die Schrift
				SpriteFont font = HfGDesign.MenuFont (Screen);

				// zeichne die Schrift
				spriteBatch.DrawStringInRectangle (font, Text, ForegroundColor (), Bounds (), AlignX, AlignY);

				spriteBatch.End ();
			}
		}

		/// <summary>
		/// Berechnet die Ausmaße des Namens des Menüeintrags.
		/// </summary>
		protected virtual Rectangle NameBounds ()
		{
			Vector2 size = new Vector2 (ScaledSize.X * NameWidth, ScaledSize.Y);
			Vector2 topLeft = ScaledPosition + ScaledSize - size;
			return topLeft.CreateRectangle (size);
		}

		/// <summary>
		/// Berechnet die Ausmaße des Wertes des Menüeintrags.
		/// </summary>
		protected virtual Rectangle ValueBounds ()
		{
			Vector2 size = new Vector2 (ScaledSize.X * ValueWidth, ScaledSize.Y);
			Vector2 topLeft = ScaledPosition + ScaledSize - size;
			return topLeft.CreateRectangle (size);
		}

        #endregion

	}
}

