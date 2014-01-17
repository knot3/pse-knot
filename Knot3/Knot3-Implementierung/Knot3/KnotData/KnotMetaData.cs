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
	/// Enthält Metadaten eines Knotens, die aus einer Spielstand-Datei schneller eingelesen werden können,
	/// als der vollständige Knoten. Dieses Objekt enthält keine Datenstruktur zur Repräsentation der Kanten,
	/// sondern nur Informationen über den Namen des Knoten und die Anzahl seiner Kanten. Es kann ohne ein
	/// dazugehöriges Knoten-Objekt existieren, aber jedes Knoten-Objekt enthält genau ein Knoten-Metadaten-Objekt.
	/// </summary>
	public class KnotMetaData
	{
		#region Properties

		/// <summary>
		/// Der Anzeigename des Knotens, welcher auch leer sein kann.
		/// Beim Speichern muss der Spieler in diesem Fall zwingend
		/// einen nichtleeren Namen wählen. Wird ein neuer Anzeigename festgelegt,
		/// dann wird der Dateiname ebenfalls auf einen neuen Wert gesetzt, unabhängig davon
		/// ob er bereits einen Wert enthält oder \glqq null\grqq~ist.
		/// Diese Eigenschaft kann öffentlich gelesen und gesetzt werden.
		/// </summary>
		public string Name
		{
			get {
				return name;
			}
			set {
				name = value;
				if (Format == null) {
					Format = new KnotFileIO ();
				}

				string extension;
				if (Format.FileExtensions.Count () > 0) {
					extension = Format.FileExtensions.ElementAt (0);
				}
				else {
					throw new ArgumentException ("Every implementation of IKnotIO must have at least one file extension.");
				}
				Filename = FileUtility.SavegameDirectory + FileUtility.Separator
				           + FileUtility.ConvertToFileName (name) + extension;
			}
		}

		private string name;

		/// <summary>
		/// Das Format, aus dem die Metadaten geladen wurden.
		/// Es ist genau dann \glqq null\grqq, wenn die Metadaten nicht aus einer Datei gelesen wurden. Nur lesbar.
		/// </summary>
		public IKnotIO Format { get; private set; }

		/// <summary>
		/// Ein Delegate, das die Anzahl der Kanten zurückliefert.
		/// Falls dieses Metadaten-Objekt Teil eines Knotens ist, gibt es dynamisch die Anzahl der
		/// Kanten des Knoten-Objektes zurück. Anderenfalls gibt es eine statische Zahl zurück,
		/// die beim Einlesen der Metadaten vor dem Erstellen dieses Objektes gelesen wurde. Nur lesbar.
		/// </summary>
		public int CountEdges { get { return countEdges (); } }

		private Func<int> countEdges;

		/// <summary>
		/// Falls die Metadaten aus einer Datei eingelesen wurden, enthält dieses Attribut den Dateinamen,
		/// sonst \glqq null\grqq.
		/// </summary>
		public string Filename { get; private set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues Knoten-Metadaten-Objekt mit einem angegebenen Knotennamen
		/// und einer angegebenen Funktion, welche eine Kantenanzahl zurück gibt.
		/// Zusätzlich wird der Dateiname oder das Speicherformat angegeben, aus dem die Metadaten gelesen wurden.
		/// </summary>
		public KnotMetaData (string name, Func<int> countEdges, IKnotIO format, string filename)
		{
			this.name = name;
			this.countEdges = countEdges;
			Format = format;
			Filename = filename;
		}

		/// <summary>
		/// Erstellt ein neues Knoten-Metadaten-Objekt mit einem angegebenen Knotennamen
		/// und einer angegebenen Funktion, welche eine Kantenanzahl zurück gibt.
		/// </summary>
		public KnotMetaData (string name, Func<int> countEdges)
		{
			this.name = name;
			this.countEdges = countEdges;
			Format = null;
			Filename = null;
		}

		#endregion

	}
}

