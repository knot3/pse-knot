using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
using Knot3.Development;


namespace Knot3.Screens
{
	/// <summary>
	/// Der Startbildschirm.
	/// </summary>
	public class StartScreen : MenuScreen
	{
		#region Properties

		/// <summary>
		/// Die Schaltflächen des Startbildschirms.
		/// </summary>
		private Container buttons;
		// das Logo
		private Texture2D logo;
		private SpriteBatch spriteBatch;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines StartScreen-Objekts und initialisiert diese mit einem Knot3Game-Objekt.
		/// </summary>
		public StartScreen (Knot3Game game)
		: base (game)
		{
			buttons = new Container (this, DisplayLayer.ScreenUI + DisplayLayer.Menu);

			// logo
			logo = this.LoadTexture ("logo");

			// create a new SpriteBatch, which can be used to draw textures
			spriteBatch = new SpriteBatch (Device);

			// menu
			buttons.ItemAlignX = HorizontalAlignment.Center;
			buttons.ItemAlignY = VerticalAlignment.Center;

			Button creativeButton = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Creative",
			    onClick: (time) => NextScreen = new CreativeMainScreen (Game)
			);
			creativeButton.SetCoordinates (left: 0.700f, top: 0.250f, right: 0.960f, bottom: 0.380f);

			Button challengeButton = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Challenge",
			    onClick: (time) => NextScreen = new ChallengeStartScreen (Game)
			);
			challengeButton.SetCoordinates (left: 0.000f, top: 0.050f, right: 0.380f, bottom: 0.190f);

			Button settingsButton = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Settings",
			    onClick: (time) => NextScreen = new SettingsScreen (Game)
			);
			settingsButton.SetCoordinates (left: 0.260f, top: 0.840f, right: 0.480f, bottom: 0.950f);

			Button exitButton = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Exit",
			    onClick: (time) => Game.Exit ()
			);
			exitButton.AddKey (Keys.Escape);
			exitButton.SetCoordinates (left: 0.800f, top: 0.535f, right: 0.980f, bottom: 0.790f);

			buttons.Add (creativeButton);
			buttons.Add (challengeButton);
			buttons.Add (settingsButton);
			buttons.Add (exitButton);

			// die Linien
			lines.AddPoints (0.000f, 0.050f,
			                 0.380f, 0.250f, 0.960f, 0.380f, 0.700f, 0.160f, 1.000f
			                );
			lines.AddPoints (0.000f, 0.190f,
			                 0.620f, 0.855f, 0.800f, 0.535f, 0.980f, 0.790f,
			                 0.480f, 0.950f, 0.260f, 0.840f, 0.520f, 1.000f
			                );
		}

		#endregion

		#region Methods

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			// Versteckte Funktionen
			if (Keys.F1.IsDown ()) {
				Button debugButton = new Button (
				    screen: this,
				    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
				    name: "Junction Editor",
				    onClick: (t) => NextScreen = new JunctionEditorScreen (Game)
				);

				debugButton.AlignX = HorizontalAlignment.Center;
				debugButton.AlignY = VerticalAlignment.Center;

				debugButton.AddKey (Keys.D);
				debugButton.SetCoordinates (left: 0.800f, top: 0.030f, right: 0.950f, bottom: 0.100f);
				AddGameComponents (time, debugButton);
				Border border = new Border (this, DisplayLayer.ScreenUI, debugButton);
				AddGameComponents (time, border);
			}
		}

		/// <summary>
		/// Fügt die das Menü in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime time)
		{
			base.Entered (previousScreen, time);
			AddGameComponents (time, buttons);
		}

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			// Zeichne das Logo
			spriteBatch.Begin ();
			spriteBatch.Draw (logo, new Rectangle (50, 380, 500, 300).Scale (Viewport), Color.White);
			spriteBatch.End ();
		}

		#endregion
	}
}
