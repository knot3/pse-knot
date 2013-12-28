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
	/// Ein Menüeintrag, der einen Auswahlkasten darstellt.
	/// </summary>
	public sealed class CheckBoxItem : MenuItem
	{

        #region Properties

		/// <summary>
		/// Die Option, die mit dem Auswahlkasten verknüpft ist.
		/// </summary>
		private BooleanOptionInfo option { get; set; }

        #endregion

        #region Constructors

		/// <summary>
		/// Erzeugt ein neues CheckBoxItem-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
		/// Zudem sind Angaben zur Zeichenreihenfolge und der Auswahloption Pflicht.
		/// </summary>
		public CheckBoxItem (GameScreen screen, DisplayLayer drawOrder, string text, BooleanOptionInfo option)
			: base(screen, drawOrder, text)
		{
		}

        #endregion

	}
}

