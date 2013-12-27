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
	/// Ein Menüeintrag, der einen Tastendruck entgegennimmt und in der enthaltenen Option als Zeichenkette speichert.
	/// </summary>
	public sealed class KeyInputItem : InputItem
	{

        #region Properties

		/// <summary>
		/// Die Option in einer Einstellungsdatei.
		/// </summary>
		private OptionInfo option { get; set; }

        #endregion

        #region Constructors

		/// <summary>
		/// Erzeugt ein neues CheckBoxItem-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
		/// Zudem sind Angaben zur Zeichenreihenfolge und der Eingabeoption Pflicht.
		/// </summary>
		public KeyInputItem (GameScreen screen, DisplayLayer drawOrder, string text, OptionInfo option)
			: base(screen, drawOrder, text, option.Value)
		{
			this.option = option;
		}

        #endregion

        #region Methods

		/// <summary>
		/// Speichert die aktuell gedrückte Taste in der Option.
		/// </summary>
		public override void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			option.Value = key.ToString ();
		}

        #endregion

	}
}

