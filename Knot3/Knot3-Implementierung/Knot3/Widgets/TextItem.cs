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
	/// Ein Widget, der eine Zeichenkette anzeigt.
	/// </summary>
	public class TextItem : MenuItem
	{
		#region Properties

		/// <summary>
		/// Wie viel Prozent der Name des Eintrags (auf der linken Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		protected override float NameWidth { get { return 1.00f; } }

		/// <summary>
		/// Wie viel Prozent der Wert des Eintrags (auf der rechten Seite) von der Breite des Eintrags einnehmen darf.
		/// </summary>
		protected override float ValueWidth { get { return 0.00f; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues TextItem-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
		/// Zudem sind Angabe der Zeichenreihenfolge und der Zeichenkette, die angezeigt wird, für Pflicht.
		/// </summary>
		public TextItem (GameScreen screen, DisplayLayer drawOrder, string name, Action onClick)
		: base(screen, drawOrder, name)
		{
		}

		#endregion
	}
}

