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
using Knot3.Utilities;

namespace Knot3.KnotData
{
	/// <summary>
	/// Enthält Metadaten zu einer Challenge.
	/// </summary>
	public class ChallengeMetaData
	{
		#region Properties

		/// <summary>
		/// Der Name der Challenge.
		/// </summary>
		public string Name
		{
			get {
				return name;
			}
			set {
				name = value;
				if (Format == null) {
					Format = new ChallengeFileIO ();
				}
				string extension;
				if (Format.FileExtensions.Count () > 0) {
					extension = Format.FileExtensions.ElementAt (0);
				}
				else {
					throw new ArgumentException ("Every implementation of IChallengeIO must have at least one file extension.");
				}
				Filename = FileUtility.SavegameDirectory + FileUtility.Separator
				           + FileUtility.ConvertToFileName (name) + extension;
			}
		}

		private string name;

		/// <summary>
		/// Der Ausgangsknoten, den der Spieler in den Referenzknoten transformiert.
		/// </summary>
		public KnotMetaData Start { get; private set; }

		/// <summary>
		/// Der Referenzknoten, in den der Spieler den Ausgangsknoten transformiert.
		/// </summary>
		public KnotMetaData Target { get; private set; }

		/// <summary>
		/// Das Format, aus dem die Metadaten der Challenge gelesen wurden oder null.
		/// </summary>
		public IChallengeIO Format { get; private set; }

		/// <summary>
		/// Der Dateiname, aus dem die Metadaten der Challenge gelesen wurden oder in den sie abgespeichert werden.
		/// </summary>
		public string Filename { get; private set; }

		/// <summary>
		/// Ein öffentlicher Enumerator, der die Bestenliste unabhängig von der darunterliegenden Datenstruktur zugänglich macht.
		/// </summary>
		public IEnumerable<KeyValuePair<string, int>> Highscore { get { return highscore; } }

		private List<KeyValuePair<string, int>> highscore;

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein Challenge-Metadaten-Objekt mit einem gegebenen Namen und den Metadaten des Ausgangs- und Referenzknotens.
		/// </summary>
		public ChallengeMetaData (string name, KnotMetaData start, KnotMetaData target,
		                          string filename, IChallengeIO format,
		                          IEnumerable<KeyValuePair<string, int>> highscore)
		{
			Name = name;
			Start = start;
			Target = target;
			Format = format ?? Format;
			Filename = filename ?? Filename;

			this.highscore = new List<KeyValuePair<string, int>> ();
			if (highscore != null) {
				foreach (KeyValuePair<string, int> entry in highscore) {
					this.highscore.Add (entry);
				}
			}
		}

		/// <summary>
		/// Fügt eine neue Bestzeit eines bestimmten Spielers in die Bestenliste ein.
		/// </summary>
		public void AddToHighscore (string name, int time)
		{
			KeyValuePair<string, int> entry = new KeyValuePair<string, int> (name, time);
			if (!highscore.Contains (entry)) {
				highscore.Add (entry);
			}
		}

		#endregion
	}
}

