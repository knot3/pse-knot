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
using Knot3.Widgets;

namespace Knot3.KnotData
{
	/// <summary>
	/// Ein Objekt dieser Klasse repräsentiert eine Challenge.
	/// </summary>
	public sealed class Challenge
	{
		#region Properties

		/// <summary>
		/// Der Ausgangsknoten, den der Spieler in den Referenzknoten transformiert.
		/// </summary>
		public Knot Start { get; private set; }

		/// <summary>
		/// Der Referenzknoten, in den der Spieler den Ausgangsknoten transformiert.
		/// </summary>
		public Knot Target { get; private set; }

		/// <summary>
		/// Eine sortierte Bestenliste.
		/// </summary>
		private SortedList<int, string> highscore { get; set; }

		/// <summary>
		/// Ein öffentlicher Enumerator, der die Bestenliste unabhängig von der darunterliegenden Datenstruktur zugänglich macht.
		/// </summary>
		public IEnumerable<KeyValuePair<string, int>> Highscore { get; set; }

		/// <summary>
		/// Die Metadaten der Challenge.
		/// </summary>
		public ChallengeMetaData MetaData { get; private set; }

		/// <summary>
		/// Der Name der Challenge.
		/// </summary>
		public string Name
		{
			get { return MetaData.Name; }
			set { MetaData.Name = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein Challenge-Objekt aus einem gegebenen Challenge-Metadaten-Objekt.
		/// Erstellt ein Challenge-Objekt aus einer gegebenen Challenge-Datei.
		/// </summary>
		public Challenge (ChallengeMetaData meta, Knot start, Knot target)
		{
			MetaData = meta;
			Start = start;
			Target = target;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Fügt eine neue Bestzeit eines bestimmten Spielers in die Bestenliste ein.
		/// </summary>
		public void AddToHighscore (string name, int time)
		{
			throw new System.NotImplementedException ();
		}

		#endregion
	}
}

