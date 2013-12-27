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
	/// Ein Menüeintrag, der den ausgewählten Wert anzeigt und bei einem Linksklick ein Dropdown-Menü zur Auswahl eines neuen Wertes ein- oder ausblendet.
	/// </summary>
	public sealed class DropDownMenuItem : MenuItem
	{
        #region Properties

		/// <summary>
		/// Das Dropdown-Menü, das ein- und ausgeblendet werden kann.
		/// </summary>
		private VerticalMenu dropdown { get; set; }

        #endregion

        #region Constructors

		/// <summary>
		/// Erzeugt ein neues ConfirmDialog-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
		/// Zudem ist die Angabe der Zeichenreihenfolge Pflicht.
		/// </summary>
		public DropDownMenuItem (GameScreen screen, DisplayLayer drawOrder, string text)
			: base(screen, drawOrder, text)
		{
		}

        #endregion

        #region Methods

		/// <summary>
		/// Fügt Einträge in das Dropdown-Menü ein, die auf Einstellungsoptionen basieren.
		/// Fügt Einträge in das Dropdown-Menü ein, die nicht auf Einstellungsoptionen basieren.
		/// </summary>
		public void AddEntries (DistinctOptionInfo option)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Fügt Einträge in das Dropdown-Menü ein, die auf Einstellungsoptionen basieren.
		/// Fügt Einträge in das Dropdown-Menü ein, die nicht auf Einstellungsoptionen basieren.
		/// </summary>
		public void AddEntries (DropDownEntry enties)
		{
			throw new System.NotImplementedException ();
		}

        #endregion

	}
}

