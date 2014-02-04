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
using Knot3.Development;

namespace Knot3.Widgets
{
	/// <summary>
	/// Ein abstrakte Klasse für Menüeinträge.
	/// </summary>
	public abstract class MenuItem : Widget, IKeyEventListener, IMouseClickEventListener, IMouseScrollEventListener
	{
		#region Properties

		/// <summary>
		/// Die Zeichenreihenfolge.
		/// </summary>
		public int ItemOrder { get; set; }

		/// <summary>
		/// Der Name des Menüeintrags, der auf der linken Seite angezeigt wird.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Eine Referenz auf das Menu, in dem sich der Eintrag befindet.
		/// </summary>
		public Container Menu { get; set; }

		/// <summary>
		/// Wie viel Prozent der Name des Eintrags (auf der linken Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		public virtual float NameWidth { get { return _nameWidth; } set { _nameWidth = value; } }

		private float _nameWidth = 0.5f;

		/// <summary>
		/// Wie viel Prozent der Wert des Eintrags (auf der rechten Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		public virtual float ValueWidth { get { return _valueWidth; } set { _valueWidth = value; } }

		private float _valueWidth = 0.5f;
		// ein Spritebatch
		protected SpriteBatch spriteBatch;

		public virtual Bounds MouseClickBounds { get { return Bounds; } }

		public Bounds MouseScrollBounds { get { return Bounds; } }

		public Action<bool, GameTime> Hovered = (isHovered, time) => {};
		#endregion

		#region Constructors

		public MenuItem (IGameScreen screen, DisplayLayer drawOrder, string text)
		: base (screen, drawOrder)
		{
			Text = text;
			ItemOrder = -1;
			State = State.None;
			spriteBatch = new SpriteBatch (screen.Device);
			SelectedColorBackground = Color.Transparent;
			SelectedColorForeground = Color.White;
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

		public virtual void SetHovered (bool isHovered, GameTime time)
		{
			State = isHovered ? State.Hovered : State.None;
			Hovered (isHovered, time);
		}

		/// <summary>
		/// Die Reaktion auf eine Bewegung des Mausrads.
		/// </summary>
		public void OnScroll (int scrollValue)
		{
			if (Menu != null) {
				Menu.OnScroll (scrollValue);
			}
			else {
				Log.Debug ("Warning: MenuItem is not assigned to a menu: ", this);
			}
		}

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			if (IsVisible) {
				spriteBatch.Begin ();

				// zeichne den Hintergrund
				spriteBatch.DrawColoredRectangle (BackgroundColorFunc (), Bounds);

				// lade die Schrift
				SpriteFont font = HfGDesign.MenuFont (Screen);

				// zeichne die Schrift
				Color foreground = ForegroundColorFunc () * (IsEnabled ? 1f : 0.5f);
				spriteBatch.DrawStringInRectangle (font, Text, foreground, Bounds, AlignX, AlignY);

				spriteBatch.End ();
			}
		}

		/// <summary>
		/// Berechnet die Ausmaße des Namens des Menüeintrags.
		/// </summary>
		protected virtual Bounds NameBounds
		{
			get {
				return Bounds.FromLeft (() => NameWidth);
			}
		}

		/// <summary>
		/// Berechnet die Ausmaße des Wertes des Menüeintrags.
		/// </summary>
		protected virtual Bounds ValueBounds
		{
			get {
				return Bounds.FromRight (() => ValueWidth);
			}
		}

		public virtual void Collapse ()
		{
		}

		#endregion
	}
}
