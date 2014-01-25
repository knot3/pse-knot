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
	/// Ein Dialog ist ein im Vordergrund erscheinendes Fenster, das auf Nutzerinteraktionen wartet.
	/// </summary>
	public abstract class Dialog : Widget, IKeyEventListener, IMouseEventListener
	{
		#region Properties

		/// <summary>
		/// Der Fenstertitel.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Der angezeigte Text.
		/// </summary>
		public string Text { get; set; }

		protected SpriteBatch spriteBatch;
		public Action<GameTime> Close;

		protected Func<Color> TitleBackgroundColor { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues Dialog-Objekt und initialisiert dieses mit dem zugehörigen IGameScreen-Objekt.
		/// Zudem sind Angaben zur Zeichenreihenfolge, einer Zeichenkette für den Titel und für den eingeblendeten Text Pflicht.
		/// [base=screen, drawOrder]
		/// </summary>
		public Dialog (IGameScreen screen, DisplayLayer drawOrder, string title, string text)
		: base(screen, drawOrder)
		{
			// Setzte Titel und Text
			Title = title;
			Text = text;

			// erstelle einen Spritebatch zum Zeichnen der Titelleiste
			spriteBatch = new SpriteBatch (screen.Device);

			// Dialoge sind nach dem erstellen sichtbar, und das Delegate zum schließen macht sie unsichtbar
			IsVisible = true;
			Close = (time) => {
				IsVisible = false;
				Screen.RemoveGameComponents (time, this);
			};

			// Die Standardposition ist in der Mitte des Bildschirms
			RelativePosition = () => (Vector2.One - RelativeSize ()) / 2;
			// Die Standardgröße
			RelativeSize = () => new Vector2 (0.500f, 0.500f);
			// Der Standardabstand
			RelativePadding = () => new Vector2 (0.010f, 0.010f);
			// Die Standardfarben
			BackgroundColor = () => screen.BackgroundColor.Mix (Color.White, 0.15f);
			ForegroundColor = () => Color.Black;
			TitleBackgroundColor = () => new Color (0xb4, 0xff, 0x00) * 0.75f;

			// ...
			ValidKeys = new List<Keys> ();
		}

		#endregion

		#region Methods

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			spriteBatch.Begin ();

			// zeichne den Hintergrund
			spriteBatch.DrawColoredRectangle (BackgroundColor (), Bounds ());

			// lade die Schrift
			SpriteFont font = HfGDesign.MenuFont (Screen);

			// zeichne den Titel des Dialogs
			spriteBatch.DrawColoredRectangle (TitleBackgroundColor (), TitleBounds ());
			spriteBatch.DrawStringInRectangle (font, Title, ForegroundColor (), TitleBounds (), AlignX, AlignY);

			spriteBatch.End ();
		}

		protected Vector2 RelativeTitlePosition
		{
			get {
				Vector2 pos = RelativePosition ();
				pos.Y += 0.000f;
				return pos;
			}
		}

		protected Vector2 RelativeTitleSize
		{
			get {
				Vector2 size = RelativeSize ();
				size.Y = 0.075f;
				return size;
			}
		}

		protected Vector2 RelativeContentPosition
		{
			get {
				Vector2 pos = RelativePosition ();
				pos.Y += RelativeTitleSize.Y;
				pos += RelativePadding ();
				return pos;
			}
		}

		protected Vector2 RelativeContentSize
		{
			get {
				Vector2 size = RelativeSize ();
				size.Y -= RelativeTitleSize.Y;
				size -= RelativePadding () * 2;
				return size;
			}
		}

		protected Rectangle TitleBounds ()
		{
			return RelativeTitlePosition.Scale (Screen.Viewport)
			       .CreateRectangle (RelativeTitleSize.Scale (Screen.Viewport));
		}

		protected Rectangle ContentBounds ()
		{
			return RelativeContentPosition.Scale (Screen.Viewport)
			       .CreateRectangle (RelativeContentSize.Scale (Screen.Viewport));
		}

		/// <summary>
		/// Durch Drücken der Entertaste wird die ausgewählte Aktion ausgeführt. Durch Drücken der Escape-Taste wird der Dialog abgebrochen.
		/// Mit Hilfe der Pfeiltasten kann zwischen den Aktionen gewechselt werden.
		/// </summary>
		public virtual void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
		}

		public virtual void SetHovered (bool hovered)
		{
		}

		/// <summary>
		/// Bei einem Linksklick geschieht nichts.
		/// </summary>
		public virtual void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
		}

		/// <summary>
		/// Bei einem Rechtsklick geschieht nichts.
		/// </summary>
		public virtual void OnRightClick (Vector2 position, ClickState state, GameTime time)
		{
		}

		/// <summary>
		///
		/// </summary>
		public virtual void OnScroll (int scrollValue)
		{
		}

		#endregion
	}
}

