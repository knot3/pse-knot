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
		private TextItem infoTitle;
		private Menu challengeInfo;
		private World previewWorld;
		private KnotRenderer previewRenderer;
		private KnotMetaData previewKnotMetaData;
		private Border previewBorder;
		private KnotInputHandler previewInput;
		private ModelMouseHandler previewMouseHandler;
		private Button startButton;

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

			lines.AddPoints (0, 50,
			                 30, 970,
			                 770, 895,
			                 870, 970,
			                 970, 50, 1000
			                );

			title = new TextItem (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem, name: "Load Challenge");
			title.Bounds.Position = new ScreenPoint (this, 0.100f, 0.050f);
			title.Bounds.Size = new ScreenPoint (this, 0.900f, 0.050f);
			title.ForegroundColorFunc = () => Color.White;

			infoTitle = new TextItem (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem, name: "Knot Info:");
			infoTitle.Bounds.Position = new ScreenPoint (this, 0.45f, 0.62f);
			infoTitle.Bounds.Size = new ScreenPoint (this, 0.900f, 0.050f);
			infoTitle.ForegroundColorFunc = () => Color.White;

			challengeInfo = new Menu(this, DisplayLayer.ScreenUI + DisplayLayer.Menu);
			challengeInfo.Bounds.Position = new ScreenPoint (this,0.47f, 0.70f);
			challengeInfo.Bounds.Size = new ScreenPoint (this, 0.300f, 0.500f);
			challengeInfo.Bounds.Padding = new ScreenPoint (this, 0.010f, 0.010f);
			challengeInfo.ItemAlignX = HorizontalAlignment.Left;
			challengeInfo.ItemAlignY = VerticalAlignment.Center;

			// Erstelle einen Parser für das Dateiformat
			ChallengeFileIO fileFormat = new ChallengeFileIO ();
			// Erstelle einen Spielstand-Loader
			loader = new SavegameLoader<Challenge, ChallengeMetaData> (fileFormat, "index-challenges");

			// Preview
			Bounds previewBounds = new Bounds (this, 0.45f, 0.1f, 0.48f, 0.5f);
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

			backButton = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Back",
			    onClick: (time) => NextScreen = Game.Screens.Where ((s) => !(s is ChallengeStartScreen)).ElementAt (0)
			);
			backButton.AddKey (Keys.Escape);

			backButton.SetCoordinates (left: 0.770f, top: 0.910f, right: 0.870f, bottom: 0.960f);
			backButton.AlignX = HorizontalAlignment.Center;
			startButton = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Start",
			    onClick: (time) => NextScreen = NextScreen = new ChallengeModeScreen (game: Game, challenge: loader.FileFormat.Load (previewKnotMetaData.Filename))
			);
			startButton.IsVisible = false;
			startButton.AddKey (Keys.Enter);
			startButton.SetCoordinates (left: 0.660f, top: 0.910f, right: 0.770f, bottom: 0.960f);

			startButton.AlignX = HorizontalAlignment.Center;
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
			Action<GameTime> nullAction = (time) => {
			};
			Action<GameTime> LoadFile = (time) => {
				RemoveGameComponents (time,challengeInfo);
				challengeInfo.Clear();
				if (previewKnotMetaData != meta.Target) {
					previewRenderer.Knot = loader.FileFormat.Load (filename).Target;
					previewKnotMetaData = meta.Target;
					startButton.IsVisible = true;

					MenuEntry count = new MenuEntry (
					    screen: this,
					    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
					    name: "Knot Count: "+ previewKnotMetaData.CountEdges,
					    onClick: nullAction
					);
					count.Selectable =false;
					count.Enabled=false;
					challengeInfo.Add(count);
					AddGameComponents(time,challengeInfo);
				}
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
			button.SelectedColorBackground = Color.White;
			button.SelectedColorForeground = Color.Black;
			savegameMenu.Add (button);
		}

		/// <summary>
		/// Fügt das Menü mit den Spielständen in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime time)
		{
			UpdateFiles ();
			base.Entered (previousScreen, time);
			AddGameComponents (time, savegameMenu, title, previewWorld, previewBorder, previewInput, previewMouseHandler, backButton,startButton, infoTitle);
		}

		#endregion
	}
}
