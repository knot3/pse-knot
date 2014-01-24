using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using Ionic.Zip;

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
	/// Implementiert das Speicherformat für Challenges.
	/// </summary>
	public sealed class ChallengeFileIO : IChallengeIO
	{
		#region Properties

		/// <summary>
		/// Die für eine Knoten-Datei gültigen Dateiendungen.
		/// </summary>
		public IEnumerable<string> FileExtensions
		{
			get {
				yield return ".challenge";
				yield return ".chl";
				yield return ".chn";
				yield return ".chg";
				yield return ".chlng";
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein ChallengeFileIO-Objekt.
		/// </summary>
		public ChallengeFileIO ()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Speichert eine Challenge in dem Dateinamen, der in dem Challenge-Objekt enthalten ist.
		/// </summary>
		public void Save (Challenge challenge)
		{
			using (ZipFile zip = new ZipFile()) {
				// Namen
				zip.AddEntry ("name.txt", challenge.Name);
				// Startknoten
				KnotStringIO parser = new KnotStringIO (challenge.Start);
				zip.AddEntry ("start.knot", parser.Content);
				// Zielknoten
				parser = new KnotStringIO (challenge.Target);
				zip.AddEntry ("target.knot", parser.Content);
				// Highscore
				zip.AddEntry ("highscore.txt", string.Join ("\n", printHighscore (challenge.Highscore)));
				// ZIP-Datei speichern
				zip.Save (challenge.MetaData.Filename);
			}
		}

		/// <summary>
		/// Lädt eine Challenge aus einer angegebenen Datei.
		/// </summary>
		public Challenge Load (string filename)
		{
			ChallengeMetaData meta = LoadMetaData (filename: filename);
			Knot start = null;
			Knot target = null;

			using (ZipFile zip = ZipFile.Read(filename)) {
				foreach (ZipEntry entry in zip) {
					string content = entry.ReadContent ();

					// für die Datei mit dem Startknoten
					if (entry.FileName.ToLower ().Contains ("start")) {
						KnotStringIO parser = new KnotStringIO (content: content);
						start = new Knot (
						    new KnotMetaData (parser.Name, () => parser.CountEdges, null, null),
						    parser.Edges
						);
					}

					// für die Datei mit dem Zielknoten
					else if (entry.FileName.ToLower ().Contains ("target")) {
						KnotStringIO parser = new KnotStringIO (content: content);
						target = new Knot (
						    new KnotMetaData (parser.Name, () => parser.CountEdges, null, null),
						    parser.Edges
						);
					}
				}
			}

			if (meta != null && start != null && target != null) {
				return new Challenge (meta, start, target);
			}
			else {
				throw new IOException (
				    "Error! Invalid challenge file: " + filename
				    + " (meta=" + meta + ",start=" + start + ",target=" + target + ")"
				);
			}
		}

		/// <summary>
		/// Lädt die Metadaten einer Challenge aus einer angegebenen Datei.
		/// </summary>
		public ChallengeMetaData LoadMetaData (string filename)
		{
			string name = null;
			KnotMetaData start = null;
			KnotMetaData target = null;
			IEnumerable<KeyValuePair<string, int>> highscore = null;
			using (ZipFile zip = ZipFile.Read(filename)) {
				foreach (ZipEntry entry in zip) {
					string content = entry.ReadContent ();

					// für die Datei mit dem Startknoten
					if (entry.FileName.ToLower ().Contains ("start")) {
						KnotStringIO parser = new KnotStringIO (content: content);
						start = new KnotMetaData (parser.Name, () => parser.CountEdges, null, null);
					}

					// für die Datei mit dem Zielknoten
					else if (entry.FileName.ToLower ().Contains ("target")) {
						KnotStringIO parser = new KnotStringIO (content: content);
						target = new KnotMetaData (parser.Name, () => parser.CountEdges, null, null);
					}

					// für die Datei mit dem Namen
					else if (entry.FileName.ToLower ().Contains ("name")) {
						name = content.Trim ();
					}

					// für die Datei mit den Highscores
					else if (entry.FileName.ToLower ().Contains ("highscore")) {
						highscore = parseHighscore (content.Split (new char[] {'\r','\n'}, StringSplitOptions.RemoveEmptyEntries));
					}
				}
			}
			if (name != null && start != null && target != null) {
				Console.WriteLine("Load challenge file: " + filename
				                  + " (name=" + name + ",start=" + start + ",target=" + target + ",highscore=" + highscore + ")"
				                 );
				return new ChallengeMetaData (
				           name: name,
				           start: start,
				           target: target,
				           filename: filename,
				           format: this,
				           highscore: highscore
				       );
			}
			else {
				throw new IOException (
				    "Error! Invalid challenge file: " + filename
				    + " (name=" + name + ",start=" + start + ",target=" + target + ",highscore=" + highscore + ")"
				);
			}
		}

		IEnumerable<string> printHighscore (IEnumerable<KeyValuePair<string, int>> highscore)
		{
			foreach (KeyValuePair<string, int> entry in highscore) {
				Console.WriteLine("Save Highscore: "+entry.Value + ":" + entry.Key);
				yield return entry.Value + ":" + entry.Key;
			}
		}

		IEnumerable<KeyValuePair<string, int>> parseHighscore (IEnumerable<string> highscore)
		{
			foreach (string line in highscore) {
				Console.WriteLine("Load Highscore: "+line);
				if (line.Contains (":")) {
					string[] entry = line.Split (new char[] {':'}, 2, StringSplitOptions.None);
					string name = entry [1].Trim ();
					int time;
					if (Int32.TryParse (entry [0], out time)) {
						Console.WriteLine("=> "+name+":"+time);
						yield return new KeyValuePair<string, int> (name, time);
					}
				}
			}
		}

		#endregion
	}

	static class ZipHelper
	{
		public static string ReadContent (this ZipEntry entry)
		{
			MemoryStream memory = new MemoryStream ();
			entry.Extract (memory);
			memory.Position = 0;
			var sr = new StreamReader(memory);
			return sr.ReadToEnd();
		}
	}
}

