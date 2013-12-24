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
	/// Ein Menü, das alle Einträge vertikal anordnet.
	/// </summary>
	public class VerticalMenu : Menu
	{
        #region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines VerticalMenu-Objekts und initialisiert diese mit dem zugehörigen GameScreen-Objekt.
		/// Zudem ist die Angaben der Zeichenreihenfolge Pflicht.
		/// </summary>
		public VerticalMenu (GameScreen screen, DisplayLayer drawOrder)
			: base(screen, drawOrder)
		{
		}

        #endregion

        #region Methods

		/// <summary>
		/// Ordnet die Einträge vertikal an.
		/// </summary>
		public virtual void AlignItems ()
		{
			throw new System.NotImplementedException ();
		}

        #endregion

	}
}

