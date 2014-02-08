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
	/// Eine Schaltfläche, der eine Zeichenkette anzeigt und auf einen Linksklick reagiert.
	/// </summary>
	public sealed class Button : Widget, IKeyEventListener, IMouseClickEventListener
	{
		#region Properties

		/// <summary>
		/// Die Aktion, die ausgeführt wird, wenn der Spieler auf die Schaltfläche klickt.
		/// </summary>
		public Action<GameTime> OnClick { get; set; }

		private string name;
		private SpriteBatch spriteBatch;

		public Bounds MouseClickBounds { get { return Bounds; } }

		public Action<bool, GameTime> Hovered = (isHovered, time) => {};

		public Texture2D BackgroundTexture { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues MenuButton-Objekt und initialisiert dieses mit dem zugehörigen IGameScreen-Objekt.
		/// Zudem sind Angabe der Zeichenreihenfolge, einer Zeichenkette für den Namen der Schaltfläche
		/// und der Aktion, welche bei einem Klick ausgeführt wird Pflicht.
		/// </summary>
		public Button (IGameScreen screen, DisplayLayer drawOrder, string name, Action<GameTime> onClick)
		: base (screen, drawOrder)
		{
			this.name = name;
			OnClick = onClick;
			spriteBatch = new SpriteBatch (screen.Device);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Reaktionen auf einen Linksklick.
		/// </summary>
		public void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			OnClick (time);
		}

		public void OnRightClick (Vector2 position, ClickState state, GameTime time)
		{
		}

		public void SetHovered (bool isHovered, GameTime time)
		{
			State = isHovered ? State.Hovered : State.None;
			Hovered (isHovered, time);
		}

		/// <summary>
		/// Reaktionen auf Tasteneingaben.
		/// </summary>
		public void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			Log.Debug ("OnKeyEvent: ", key [0]);
			if (keyEvent == KeyEvent.KeyDown) {
				OnClick (time);
			}
		}

		public void AddKey (Keys key)
		{
			if (!ValidKeys.Contains (key)) {
				ValidKeys.Add (key);
			}
		}

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			if (IsVisible) {
				spriteBatch.Begin ();

				// zeichne den Hintergrund
				spriteBatch.DrawColoredRectangle (BackgroundColorFunc (), Bounds);

				if (BackgroundTexture != null) {
					spriteBatch.Draw (BackgroundTexture, Bounds, Color.White);
				}

				// lade die Schrift
				SpriteFont font = Design.MenuFont (Screen);

				// zeichne die Schrift
				Color foreground = ForegroundColorFunc () * (IsEnabled ? 1f : 0.5f);
				spriteBatch.DrawStringInRectangle (font, name, foreground, Bounds, AlignX, AlignY);

				spriteBatch.End ();
			}
		}

		#endregion
	}
}
