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
	/// Ein Menüeintrag, der Texteingaben vom Spieler annimmt.
	/// </summary>
	public class InputItem : MenuItem
	{
        #region Properties

		/// <summary>
		/// Beinhaltet den vom Spieler eingegebenen Text.
		/// </summary>
		public string InputText { get; set; }

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

        #endregion
	}
}

