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
	/// Der Startbildschirm.
	/// </summary>
	public class StartScreen : MenuScreen
	{

		#region Properties

		/// <summary>
		/// Die Schaltflächen des Startbildschirms.
		/// </summary>
		private Menu buttons;
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
			buttons = new Menu (this, DisplayLayer.Menu);

			// logo
			logo = TextureHelper.LoadTexture (Content, "logo");

			// create a new SpriteBatch, which can be used to draw textures
			spriteBatch = new SpriteBatch (Device);

			// menu
			buttons.ItemForegroundColor = base.MenuItemForegroundColor;
			buttons.ItemBackgroundColor = base.MenuItemBackgroundColor;
			buttons.ItemAlignX = HorizontalAlignment.Center;
			buttons.ItemAlignY = VerticalAlignment.Center;

			MenuButton creativeButton = new MenuButton (
				                            screen: this,
				                            drawOrder: DisplayLayer.MenuItem,
				                            name: "Creative",
				                            onClick: (time) => NextScreen = new CreativeMainScreen (Game)
			                            );
			creativeButton.SetCoordinates (left: 0.700f, top: 0.250f, right: 0.960f, bottom: 0.380f);

			MenuButton challengeButton = new MenuButton (
				                             screen: this,
				                             drawOrder: DisplayLayer.MenuItem,
				                             name: "Challenge",
				                             onClick: (time) => NextScreen = new ChallengeStartScreen (Game)
			                             );
			challengeButton.SetCoordinates (left: 0.000f, top: 0.050f, right: 0.380f, bottom: 0.190f);

			MenuButton settingsButton = new MenuButton (
				                            screen: this,
				                            drawOrder: DisplayLayer.MenuItem,
				                            name: "Settings",
				                            onClick: (time) => NextScreen = new SettingsScreen (Game)
			                            );
			settingsButton.SetCoordinates (left: 0.260f, top: 0.840f, right: 0.480f, bottom: 0.950f);

			MenuButton exitButton = new MenuButton (
				                        screen: this,
				                        drawOrder: DisplayLayer.MenuItem,
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
		}

		/// <summary>
		/// Fügt die das Menü in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (GameScreen previousScreen, GameTime time)
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

