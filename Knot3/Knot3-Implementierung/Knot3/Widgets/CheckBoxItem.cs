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
	/// Ein Menüeintrag, der einen Auswahlkasten darstellt.
	/// </summary>
	public sealed class CheckBoxItem : MenuItem
	{
		#region Properties

		/// <summary>
		/// Die Option, die mit dem Auswahlkasten verknüpft ist.
		/// </summary>
		private BooleanOptionInfo option { get; set; }

		/// <summary>
		/// Wie viel Prozent der Name des Eintrags (auf der linken Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		public override float NameWidth
		{
			get { return Math.Min (0.90f, 1.0f - ValueWidth); }
			set { throw new ArgumentException("You can't change the NameWidth of a CheckBoxItem!"); }
		}

		/// <summary>
		/// Wie viel Prozent der Wert des Eintrags (auf der rechten Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		public override float ValueWidth
		{
			get { return Bounds.Size.Relative.Y / Bounds.Size.Relative.X; }
			set { throw new ArgumentException("You can't change the ValueWidth of a CheckBoxItem!"); }
		}

		private bool currentValue;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues CheckBoxItem-Objekt und initialisiert dieses mit dem zugehörigen IGameScreen-Objekt.
		/// Zudem sind Angaben zur Zeichenreihenfolge und der Auswahloption Pflicht.
		/// </summary>
		public CheckBoxItem (IGameScreen screen, DisplayLayer drawOrder, string text, BooleanOptionInfo option)
		: base(screen, drawOrder, text)
		{
			this.option = option;
			currentValue = option.Value;
		}

		#endregion

		#region Methods

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			spriteBatch.Begin ();

			// berechne die Ausmaße des Wertefelds
			Rectangle bounds = ValueBounds.Rectangle;

			// zeichne den Hintergrund des Wertefelds
			spriteBatch.DrawColoredRectangle (ForegroundColor (), bounds);
			spriteBatch.DrawColoredRectangle (Color.Black, bounds.Shrink (2));

			// wenn der Wert wahr ist
			if (currentValue) {
				spriteBatch.DrawColoredRectangle (ForegroundColor (), bounds.Shrink (4));
			}

			spriteBatch.End ();
		}

		private void onClick ()
		{
			currentValue = option.Value = !option.Value;
			Console.WriteLine ("option: " + option.ToString () + " := " + currentValue);
		}

		/// <summary>
		/// Reaktionen auf einen Linksklick.
		/// </summary>
		public override void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			onClick ();
		}

		/// <summary>
		/// Reaktionen auf Tasteneingaben.
		/// </summary>
		public override void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			if (keyEvent == KeyEvent.KeyDown) {
				onClick ();
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
