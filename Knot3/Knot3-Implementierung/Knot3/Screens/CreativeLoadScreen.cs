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

		/// <summary>
		/// Das Menü, das die Spielstände enthält.
		/// </summary>
		private Menu savegameMenu;
		// Der Titel des Screens
		private TextItem title;
		// Spielstand-Loader
		private SavegameLoader<Knot, KnotMetaData> loader;
		// Zurück-Button
		private Button backButton;
		private Button startButton;
		// Preview
		private TextItem infoTitle;
		private World previewWorld;
		private KnotRenderer previewRenderer;
		private KnotMetaData previewKnotMetaData;
		private Border previewBorder;
		private KnotInputHandler previewInput;
		private ModelMouseHandler previewMouseHandler;
		private Menu knotInfo;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues CreativeLoadScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
		/// </summary>
		public CreativeLoadScreen (Knot3Game game)
		: base (game)
		{
			savegameMenu = new Menu (this, DisplayLayer.ScreenUI + DisplayLayer.Menu);
			savegameMenu.Bounds.Position = new ScreenPoint (this, 0.100f, 0.180f);
			savegameMenu.Bounds.Size = new ScreenPoint (this, 0.300f, 0.620f);
			savegameMenu.Bounds.Padding = new ScreenPoint (this, 0.010f, 0.010f);
			savegameMenu.ItemAlignX = HorizontalAlignment.Left;
			savegameMenu.ItemAlignY = VerticalAlignment.Center;

			lines.AddPoints (.000f, .050f, .030f, .970f, .630f, .895f, .730f, .970f, .770f, .895f, .870f, .970f, .970f, .050f, 1.000f);

			title = new TextItem (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem, name: "Load Knot");
			title.Bounds.Position = new ScreenPoint (this, 0.100f, 0.050f);
			title.Bounds.Size = new ScreenPoint (this, 0.900f, 0.050f);
			title.ForegroundColorFunc = () => Color.White;

			infoTitle = new TextItem (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem, name: "Knot Info:");
			infoTitle.Bounds.Position = new ScreenPoint (this, 0.45f, 0.62f);
			infoTitle.Bounds.Size = new ScreenPoint (this, 0.900f, 0.050f);
			infoTitle.ForegroundColorFunc = () => Color.White;

			knotInfo = new Menu (this, DisplayLayer.ScreenUI + DisplayLayer.Menu);
			knotInfo.Bounds.Position = new ScreenPoint (this, 0.47f, 0.70f);
			knotInfo.Bounds.Size = new ScreenPoint (this, 0.300f, 0.500f);
			knotInfo.Bounds.Padding = new ScreenPoint (this, 0.010f, 0.010f);
			knotInfo.ItemAlignX = HorizontalAlignment.Left;
			knotInfo.ItemAlignY = VerticalAlignment.Center;

			// Erstelle einen Parser für das Dateiformat
			KnotFileIO fileFormat = new KnotFileIO ();
			// Erstelle einen Spielstand-Loader
			loader = new SavegameLoader<Knot, KnotMetaData> (fileFormat, "index-knots");

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
			    onClick: (time) => NextScreen = Game.Screens.Where ((s) => !(s is CreativeLoadScreen)).ElementAt (0)
			);
			backButton.AddKey (Keys.Escape);
			backButton.SetCoordinates (left: 0.770f, top: 0.910f, right: 0.870f, bottom: 0.960f);
			backButton.AlignX = HorizontalAlignment.Center;

			startButton = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Load",
			    onClick: (time) => NextScreen = new CreativeModeScreen (game: Game, knot: loader.FileFormat.Load (previewKnotMetaData.Filename))
			);
			startButton.IsVisible = false;
			startButton.AddKey (Keys.Enter);
			startButton.SetCoordinates (left: 0.630f, top: 0.910f, right: 0.730f, bottom: 0.960f);
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

			//throw new Exception("test error");
		}

		/// <summary>
		/// Diese Methode wird für jede gefundene Spielstanddatei aufgerufen
		/// </summary>
		private void AddSavegameToList (string filename, KnotMetaData meta)
		{
			// Finde den Namen des Knotens
			string name = meta.Name.Length > 0 ? meta.Name : filename;
			Action<GameTime> nullAction = (time) => {
			};
			// Erstelle eine Lamdafunktion, die beim Auswählen des Menüeintrags ausgeführt wird
			Action<GameTime> preview = (time) => {
				if (previewKnotMetaData != meta) {
					RemoveGameComponents (time, knotInfo);
					knotInfo.Clear ();

					previewRenderer.Knot = loader.FileFormat.Load (filename);
					previewWorld.Camera.ResetCamera ();
					previewKnotMetaData = meta;
					startButton.IsVisible = true;

					MenuEntry count = new MenuEntry (
					    screen: this,
					    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
					    name: "Knot Count: " + previewKnotMetaData.CountEdges,
					    onClick: nullAction
					);

					count.Selectable = false;
					count.Enabled = false;
					knotInfo.Add (count);
					AddGameComponents (time, knotInfo);
				}
			};

			// Erstelle den Menüeintrag
			MenuEntry button = new MenuEntry (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: name,
			    onClick: preview
			);

			button.SelectedColorBackground = Design.WidgetForeground;
			button.SelectedColorForeground = Design.WidgetBackground;

			savegameMenu.Add (button);
		}

		/// <summary>
		/// Fügt das Menü mit den Spielständen in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime time)
		{
			UpdateFiles ();
			base.Entered (previousScreen, time);
			AddGameComponents (time, title, savegameMenu, previewBorder, previewWorld, previewInput, previewMouseHandler, backButton, startButton, infoTitle);
		}

		#endregion
	}
}
