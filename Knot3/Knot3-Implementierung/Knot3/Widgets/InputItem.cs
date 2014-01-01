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
	/// Ein Menüeintrag, der Texteingaben vom Spieler annimmt.
	/// </summary>
	public class InputItem : MenuItem
	{
        #region Properties

		/// <summary>
		/// Beinhaltet den vom Spieler eingegebenen Text.
		/// </summary>
		public string InputText { get; set; }

		/// <summary>
		/// Wie viel Prozent der Name des Eintrags (auf der linken Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		protected virtual float NameWidth { get { return 0.5f; } }

		/// <summary>
		/// Wie viel Prozent der Wert des Eintrags (auf der rechten Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		protected virtual float ValueWidth { get { return 0.5f; } }

        #endregion

        #region Constructors

		/// <summary>
		/// Erzeugt ein neues InputItem-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
		/// Zudem sind Angaben zur Zeichenreihenfolge und für evtl. bereits vor-eingetragenen Text Pflicht.
		/// </summary>
		public InputItem (GameScreen screen, DisplayLayer drawOrder, string text, string inputText)
			: base(screen, drawOrder, text)
		{
			InputText = inputText;
		}

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			spriteBatch.Begin ();

			// berechne die Ausmaße des Eingabefelds
			Rectangle bounds = ValueBounds();

			// zeichne den Hintergrund des Eingabefelds
			spriteBatch.DrawColoredRectangle (Color.White * 0.9f, bounds);

			// lade die Schrift
			SpriteFont font = HfGDesign.MenuFont (Screen);

			// zeichne die Schrift
			spriteBatch.DrawStringInRectangle (font, InputText, Color.Black, bounds, HorizontalAlignment.Left, AlignY);

			spriteBatch.End ();
		}

		/// <summary>
		/// Berechnet die Ausmaße des Namens des Menüeintrags.
		/// </summary>
		protected override Rectangle NameBounds ()
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

