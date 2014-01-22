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

		public Action OnValueChanged = () => {};

		/// <summary>
		/// Gibt an, ob gerade auf einen Tastendruck gewartet wird.
		/// </summary>
		public override bool IsEnabled { get { return base.IsEnabled; } set { base.IsEnabled = value; } }

		public override bool IsKeyEventEnabled
		{
			get { return isKeyEventEnabled.HasValue ? isKeyEventEnabled.Value : IsVisible && IsEnabled; }
			set { isKeyEventEnabled = value; }
		}

		private bool? isKeyEventEnabled = null;

		public override bool IsMouseEventEnabled
		{
			get { return isMouseEventEnabled.HasValue ? isMouseEventEnabled.Value : base.IsMouseEventEnabled; }
			set { isMouseEventEnabled = value; }
		}

		private bool? isMouseEventEnabled = null;

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
			ValidKeys.AddRange (TextHelper.ValidKeys);
			ValidKeys.Add (Keys.Enter);
			IsEnabled = false;
		}

		public override void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			string temp = InputText;
			TextHelper.TryTextInput (ref temp, time);
			InputText = temp;
			if (key.Contains (Keys.Enter)) {
				IsEnabled = false;
				OnValueChanged ();
			}
		}

		/// <summary>
		/// Reaktionen auf einen Linksklick.
		/// </summary>
		public override void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			if (IsVisible) {
				IsEnabled = true;
			}
		}

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			spriteBatch.Begin ();

			// berechne die Ausmaße des Eingabefelds
			Rectangle bounds = ValueBounds ();

			// zeichne den Hintergrund des Eingabefelds
			spriteBatch.DrawColoredRectangle (ForegroundColor (), bounds);
			Color backgroundColor = IsEnabled ? Color.Black.Mix (Color.White, 0.25f) : Color.Black;
			spriteBatch.DrawColoredRectangle (backgroundColor, bounds.Shrink (xy: 2));

			// lade die Schrift
			SpriteFont font = HfGDesign.MenuFont (Screen);

			// zeichne die Schrift
			spriteBatch.DrawStringInRectangle (
			    font: font,
			    text: InputText,
			    color: ForegroundColor (),
			    bounds: bounds.Shrink (x: 4, y: 2),
			    alignX: HorizontalAlignment.Left,
			    alignY: AlignY
			);

			spriteBatch.End ();
		}

		#endregion
	}
}

