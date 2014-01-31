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
using Knot3.KnotData;

namespace Knot3.Utilities
{
	public class SavegameLoader<Savegame, SavegameMetaData>
	{
		public ISavegameIO<Savegame, SavegameMetaData> FileFormat { get; set; }

		public FileIndex fileIndex { get; private set; }

		public string IndexName;
		private Action<string, SavegameMetaData> OnSavegameFound;

		public SavegameLoader (ISavegameIO<Savegame, SavegameMetaData> fileFormat, string indexName)
		{
			FileFormat = fileFormat;
			IndexName = indexName;
		}

		public void FindSavegames (Action<string, SavegameMetaData> onSavegameFound)
		{
			// Erstelle einen neuen Index, der eine Datei mit dem angegeben Indexnamen im Spielstandverzeichnis einliest
			fileIndex = new FileIndex (FileUtility.SavegameDirectory + FileUtility.Separator + IndexName + ".txt");

			// Diese Verzeichnisse werden nach Spielständen durchsucht
			string[] searchDirectories = new string[] {
				FileUtility.BaseDirectory,
				FileUtility.SavegameDirectory
			};
			Console.WriteLine ("Search for Savegames: " + string.Join (", ", searchDirectories));

			// Suche nach Spielstanddateien und fülle das Menü auf
			OnSavegameFound = onSavegameFound;
			FileUtility.SearchFiles (searchDirectories, FileFormat.FileExtensions, AddFileToList);
		}

		/// <summary>
		/// Diese Methode wird für jede gefundene Spielstanddatei aufgerufen
		/// </summary>
		private void AddFileToList (string filename)
		{
			// Lese die Datei ein und erstelle einen Hashcode
			string hashcode = FileUtility.GetHash (filename);

			// Ist dieser Hashcode im Index enthalten?
			// Dann wäre der Spielstand gültig, sonst ungültig oder unbekannt.
			bool isValid = fileIndex.Contains (hashcode);

			// Wenn der Spielstand ungültig oder unbekannt ist...
			if (!isValid) {
				try {
					// Lade den Knoten und prüfe, ob Exceptions auftreten
					FileFormat.Load (filename);
					// Keine Exceptions? Dann ist enthält die Datei einen gültigen Knoten!
					isValid = true;
					fileIndex.Add (hashcode);

				}
				catch (Exception ex) {
					// Es ist eine Exception aufgetreten, der Knoten ist offenbar ungültig.
					Console.WriteLine (ex);
					isValid = false;
				}
			}

			// Falls der Knoten gültig ist, entweder laut Index oder nach Überprüfung, dann...
			if (isValid) {
				// Lade die Metadaten
				SavegameMetaData meta = FileFormat.LoadMetaData (filename);

				// Rufe die Callback-Funktion auf
				OnSavegameFound (filename, meta);
			}
		}
	}
}
