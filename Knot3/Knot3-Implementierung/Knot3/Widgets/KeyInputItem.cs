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
	/// Ein Menüeintrag, der einen Tastendruck entgegennimmt und in der enthaltenen Option als Zeichenkette speichert.
	/// </summary>
	public sealed class KeyInputItem : InputItem
	{
        #region Properties

		/// <summary>
		/// Die Option in einer Einstellungsdatei.
		/// </summary>
		private KeyOptionInfo option { get; set; }

		/// <summary>
		/// Gibt an, ob gerade auf einen Tastendruck gewartet wird.
		/// </summary>
		private bool isCapturing;

		/// <summary>
		/// Wie viel Prozent der Name des Eintrags (auf der linken Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		protected override float NameWidth { get { return Math.Min (0.70f, 1.0f - ValueWidth); } }

		/// <summary>
		/// Wie viel Prozent der Wert des Eintrags (auf der rechten Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		protected override float ValueWidth { get { return 3 * ScaledSize.Y / ScaledSize.X; } }

        #endregion

        #region Constructors

		/// <summary>
		/// Erzeugt ein neues CheckBoxItem-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
		/// Zudem sind Angaben zur Zeichenreihenfolge und der Eingabeoption Pflicht.
		/// </summary>
		public KeyInputItem (GameScreen screen, DisplayLayer drawOrder, string text, KeyOptionInfo option)
			: base(screen, drawOrder, text, (option as DistinctOptionInfo).Value)
		{
			this.option = option;
		}

        #endregion

        #region Methods

		/// <summary>
		/// Speichert die aktuell gedrückte Taste in der Option.
		/// </summary>
		public override void OnKeyEvent (List<Keys> keys, KeyEvent keyEvent, GameTime time)
		{
			if (keys.Count > 0) {
				option.Value = keys [0];
				InputText = (option as DistinctOptionInfo).Value;
			}
		}
		
		/// <summary>
		/// Reaktionen auf einen Linksklick.
		/// </summary>
		public override void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			if (isCapturing) {
				ValidKeys.Clear ();
				isCapturing = false;
			} else {
				ValidKeys.AddRange (typeof(Keys).ToEnumValues<Keys> ());
				isCapturing = true;
				InputText = "";
			}
		}

        #endregion
	}
}

