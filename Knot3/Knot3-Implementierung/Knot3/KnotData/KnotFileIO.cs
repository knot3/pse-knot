using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
	/// Implementiert das Speicherformat für Knoten.
	/// </summary>
	public sealed class KnotFileIO : IKnotIO
	{
		#region Properties

		/// <summary>
		/// Die für eine Knoten-Datei gültigen Dateiendungen.
		/// </summary>
		public IEnumerable<string> FileExtensions
		{
			get {
				yield return ".knot";
				yield return ".knt";
			}
		}

		private Dictionary<string, Knot> KnotCache = new Dictionary<string, Knot> ();
		private Dictionary<string, KnotMetaData> KnotMetaDataCache = new Dictionary<string, KnotMetaData> ();

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein KnotFileIO-Objekt.
		/// </summary>
		public KnotFileIO ()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Speichert einen Knoten in dem Dateinamen, der in dem Knot-Objekt enthalten ist.
		/// </summary>
		public void Save (Knot knot)
		{
			KnotStringIO parser = new KnotStringIO (knot);
			//Console.WriteLine ("KnotFileIO.Save(" + knot + ") = #" + parser.Content.Length);
			if (knot.MetaData.Filename == null) {
				throw new IOException ("Error! knot has no filename: " + knot);
			}
			else {
				File.WriteAllText (knot.MetaData.Filename, parser.Content);
			}
		}

		/// <summary>
		/// Lädt eines Knotens aus einer angegebenen Datei.
		/// </summary>
		public Knot Load (string filename)
		{
			if (KnotCache.ContainsKey (filename)) {
				return KnotCache [filename];
			}
			else {
				//Console.WriteLine ("Load knot from " + filename);
				KnotStringIO parser = new KnotStringIO (content: string.Join ("\n", FileUtility.ReadFrom (filename)));
				return KnotCache [filename] = new Knot (
				    new KnotMetaData (parser.Name, () => parser.CountEdges, this, filename),
				    parser.Edges
				);
			}
		}

		/// <summary>
		/// Lädt die Metadaten eines Knotens aus einer angegebenen Datei.
		/// </summary>
		public KnotMetaData LoadMetaData (string filename)
		{
			if (KnotMetaDataCache.ContainsKey (filename)) {
				return KnotMetaDataCache [filename];
			}
			else {
				KnotStringIO parser = new KnotStringIO (content: string.Join ("\n", FileUtility.ReadFrom (filename)));
				return KnotMetaDataCache [filename] = new KnotMetaData (
				    name: parser.Name,
				    countEdges: () => parser.CountEdges,
				    format: this,
				    filename: filename
				);
			}
		}

		public override string ToString ()
		{
			return "KnotFileIO";
		}

		#endregion
	}
}
