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
	/// Der Spielzustand, der den Ladebildschirm für Challenges darstellt.
	/// </summary>
	public sealed class ChallengeStartScreen : MenuScreen
	{
		#region Properties

		/// <summary>
		/// Das Menü, das die Spielstände enthält.
		/// </summary>
		private Menu savegameMenu;

		private Button backButton;

		// Der Titel des Screens
		private TextItem title;

		// Spielstand-Loader
		private SavegameLoader<Challenge, ChallengeMetaData> loader;

		// Preview
		private World previewWorld;
		private KnotRenderer previewRenderer;
		private KnotMetaData previewKnotMetaData;
		private Border previewBorder;
		private KnotInputHandler previewInput;
		private ModelMouseHandler previewMouseHandler;

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt eine neue Instanz eines ChallengeStartScreen-Objekts und
		/// initialisiert diese mit einem Knot3Game-Objekt.
		/// </summary>
		public ChallengeStartScreen (Knot3Game game)
		: base (game)
		{
			savegameMenu = new Menu (this, DisplayLayer.ScreenUI + DisplayLayer.Menu);
			savegameMenu.Bounds.Position = new ScreenPoint (this, 0.100f, 0.180f);
			savegameMenu.Bounds.Size = new ScreenPoint (this, 0.300f, 0.720f);
			savegameMenu.Bounds.Padding = new ScreenPoint (this, 0.010f, 0.010f);
			savegameMenu.ItemForegroundColor = base.MenuItemForegroundColor;
			savegameMenu.ItemBackgroundColor = base.MenuItemBackgroundColor;
			savegameMenu.ItemAlignX = HorizontalAlignment.Left;
			savegameMenu.ItemAlignY = VerticalAlignment.Center;

			/*			lines.AddPoints (
						   0, 50,

						    30, 970,
						    170, 895,
						    270, 970,
						    970, 50,
						    1000
						);*/

			lines.AddPoints(0, 50,
			                30, 970,
			                770, 895,
			                870, 970,
			                970, 50, 1000
			               );

			title = new TextItem (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem, name: "Load Challenge");
			title.Bounds.Position = new ScreenPoint (this, 0.100f, 0.050f);
			title.Bounds.Size = new ScreenPoint (this, 0.900f, 0.050f);
			title.ForegroundColor = () => Color.White;

			// Erstelle einen Parser für das Dateiformat
			ChallengeFileIO fileFormat = new ChallengeFileIO ();
			// Erstelle einen Spielstand-Loader
			loader = new SavegameLoader<Challenge, ChallengeMetaData> (fileFormat, "index-challenges");

			// Preview
			Bounds previewBounds = new Bounds (this, 0.45f, 0.1f, 0.48f, 0.7f);
			previewWorld = new World (
			    screen: this,
			    drawIndex: DisplayLayer.ScreenUI + DisplayLayer.GameWorld,
			    bounds: previewBounds
			);
			previewRenderer = new KnotRenderer (screen: this, position: Vector3.Zero);
			previewWorld.Add (previewRenderer);
			previewBorder = new Border (
			    screen: this,
			    drawOrder: DisplayLayer.GameWorld,
			    bounds: previewBounds,
			    lineWidth: 2,
			    padding: 0
			);
			previewInput = new KnotInputHandler (screen: this, world: previewWorld);
			previewMouseHandler = new ModelMouseHandler (screen: this, world: previewWorld);

			backButton = new Button(
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Back",
			    onClick: (time) => NextScreen = Game.Screens.Where((s) => !(s is ChallengeStartScreen)).ElementAt(0)
			);
			backButton.AddKey(Keys.Escape);
			backButton.SetCoordinates(left: 0.770f, top: 0.910f, right: 0.870f, bottom: 0.960f);
			backButton.AlignX = HorizontalAlignment.Center;
		}

		#endregion

		#region Methods

		private void UpdateFiles ()
		{
			// Leere das Spielstand-Menü
			savegameMenu.Clear ();

			// Suche nach Spielständen
			loader.FindSavegames (AddSavegameToList);
		}

		/// <summary>
		/// Diese Methode wird für jede gefundene Spielstanddatei aufgerufen
		/// </summary>
		private void AddSavegameToList (string filename, ChallengeMetaData meta)
		{
			// Erstelle eine Lamdafunktion, die beim Auswählen des Menüeintrags ausgeführt wird
			Action<GameTime> LoadFile = (time) => {
				NextScreen = new ChallengeModeScreen (game: Game, challenge: loader.FileFormat.Load (filename));
			};

			// Finde den Namen der Challenge
			string name = meta.Name.Length > 0 ? meta.Name : filename;

			// Erstelle den Menüeintrag
			MenuEntry button = new MenuEntry (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: name,
			    onClick: LoadFile
			);
			button.Hovered += (isHovered, time) => {
				if (isHovered) {
					if (previewKnotMetaData != meta.Target) {
						previewRenderer.Knot = loader.FileFormat.Load (filename).Target;
						previewKnotMetaData = meta.Target;
					}
				}
			};
			savegameMenu.Add (button);
		}

		/// <summary>
		/// Fügt das Menü mit den Spielständen in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime time)
		{
			UpdateFiles ();
			base.Entered (previousScreen, time);
			AddGameComponents(time, savegameMenu, title, previewWorld, previewBorder, previewInput, previewMouseHandler, backButton);
		}

		#endregion
	}
}
