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
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;

namespace Knot3.Screens
{
	/// <summary>
	/// Der Spielzustand, der den Ladebildschirm für Knoten darstellt.
	/// </summary>
	public sealed class CreativeLoadScreen : MenuScreen
	{
		#region Properties

		private SpriteBatch spriteBatch;

		/// <summary>
		/// Das Menü, das die Spielstände enthält.
		/// </summary>
		private VerticalMenu savegameMenu;

		// files
		private FileIndex fileIndex;
		private IKnotIO fileFormat;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues CreativeLoadScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
		/// </summary>
		public CreativeLoadScreen (Knot3Game game)
		: base(game)
		{
			spriteBatch = new SpriteBatch (Device);

			savegameMenu = new VerticalMenu (this, DisplayLayer.Menu);
			savegameMenu.RelativePosition = () => new Vector2 (0.100f, 0.180f);
			savegameMenu.RelativeSize = () => new Vector2 (0.800f, 0.720f);
			savegameMenu.RelativePadding = () => new Vector2 (0.010f, 0.010f);
			savegameMenu.ItemForegroundColor = base.MenuItemForegroundColor;
			savegameMenu.ItemBackgroundColor = base.MenuItemBackgroundColor;
			savegameMenu.ItemAlignX = HorizontalAlignment.Left;
			savegameMenu.ItemAlignY = VerticalAlignment.Center;

			lines.AddPoints (0, 50,
			                 30, 970, 970, 50, 1000
			                );
		}

		#endregion

		#region Methods

		private void UpdateFiles ()
		{
			// Erstelle einen neuen Parser
			fileFormat = new KnotFileIO ();
			// Erstelle einen neuen Index, der die Datei "index.txt" im Spielstandverzeichnis einliest
			fileIndex = new FileIndex (FileUtility.SavegameDirectory + FileUtility.Separator + "index-knots.txt");

			// Diese Verzeichnisse werden nach Spielständen durchsucht
			string[] searchDirectories = new string[] {
				FileUtility.BaseDirectory,
				FileUtility.SavegameDirectory
			};
			Console.WriteLine ("Search for Savegames: " + string.Join (", ", searchDirectories));

			// Leere das Spielstand-Menü
			savegameMenu.Clear ();

			// Suche nach Spielstanddateien und fülle das Menü auf
			FileUtility.SearchFiles (searchDirectories, fileFormat.FileExtensions, AddFileToList);
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
					fileFormat.Load (filename);
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
				KnotMetaData meta = fileFormat.LoadMetaData (filename);

				// Erstelle eine Lamdafunktion, die beim Auswählen des Menüeintrags ausgeführt wird
				Action<GameTime> LoadFile = (time) => {
					NextScreen = new CreativeModeScreen (game: Game, knot: fileFormat.Load (filename));
				};

				// Finde den Namen des Knotens
				string name = meta.Name.Length > 0 ? meta.Name : filename;

				// Erstelle den Menüeintrag
				MenuButton button = new MenuButton (
				    screen: this,
				    drawOrder: DisplayLayer.MenuItem,
				    name: name,
				    onClick: LoadFile
				);
				savegameMenu.Add (button);
			}
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
		}

		public override void Draw (GameTime time)
		{
			spriteBatch.Begin ();
			// text
			spriteBatch.DrawStringInRectangle (
			    font: HfGDesign.MenuFont (this),
			    text: "Load Knot",
			    color: Color.White,
			    bounds: new Rectangle (100, 50, 900, 50).Scale (Viewport),
			    alignX: HorizontalAlignment.Left,
			    alignY: VerticalAlignment.Center
			);
			spriteBatch.End ();
		}

		/// <summary>
		/// Fügt das Menü mit den Spielständen in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (GameScreen previousScreen, GameTime time)
		{
			UpdateFiles ();
			base.Entered (previousScreen, time);
			AddGameComponents (time, savegameMenu);
		}

		#endregion

	}
}

