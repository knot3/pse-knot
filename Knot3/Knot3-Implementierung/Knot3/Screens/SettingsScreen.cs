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
using Knot3.Debug;

namespace Knot3.Screens
{
	/// <summary>
	/// Ein Spielzustand, der das Haupt-Einstellungsmenü zeichnet.
	/// </summary>
	public class SettingsScreen : MenuScreen
	{
		#region Properties

		protected string MenuName;
		private SpriteBatch spriteBatch;

		/// <summary>
		/// Das Menu, in dem man die Einstellungs-Kategorie auswählen kann.
		/// </summary>
		private VerticalMenu navigationMenu;

		#endregion

		#region Constructors

		public SettingsScreen (Knot3Game game)
		: base(game)
		{
			MenuName = "Settings";

			spriteBatch = new SpriteBatch (Device);

			navigationMenu = new VerticalMenu (this, DisplayLayer.Menu);
			navigationMenu.RelativePosition = () => new Vector2 (0.100f, 0.180f);
			navigationMenu.RelativeSize = () => new Vector2 (0.300f, 0.770f);
			navigationMenu.RelativePadding = () => new Vector2 (0.010f, 0.010f);
			navigationMenu.ItemForegroundColor = base.MenuItemForegroundColor;
			navigationMenu.ItemBackgroundColor = base.MenuItemBackgroundColor;
			navigationMenu.ItemAlignX = HorizontalAlignment.Left;
			navigationMenu.ItemAlignY = VerticalAlignment.Center;

			MenuButton debugButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.MenuItem,
			    name: "Debug",
			    onClick: (time) => NextScreen = new DebugSettingsScreen (Game)
			);
			MenuButton graphicsButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.MenuItem,
			    name: "Video",
			    onClick: (time) => NextScreen = new GraphicsSettingsScreen (Game)
			);
			MenuButton audioButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.MenuItem,
			    name: "Audio",
			    onClick: (time) => NextScreen = new AudioSettingsScreen (Game)
			);
			MenuButton controlsButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.MenuItem,
			    name: "Controls",
			    onClick: (time) => NextScreen = new ControlSettingsScreen (Game)
			);
			MenuButton profileButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.MenuItem,
			    name: "Profile",
			    onClick: (time) => NextScreen = new ProfileSettingsScreen (Game)
			);
			MenuButton backButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.MenuItem,
			    name: "Back",
			    onClick: (time) => NextScreen = Game.Screens.Where ((s) => !(s is SettingsScreen)).ElementAt (0)
			);
			backButton.AddKey (Keys.Escape);

			navigationMenu.Add (debugButton);
			navigationMenu.Add (graphicsButton);
			navigationMenu.Add (audioButton);
			navigationMenu.Add (controlsButton);
			navigationMenu.Add (profileButton);
			navigationMenu.Add (backButton);

			lines.AddPoints (0, 50,
			                 30, 970, 970, 50, 1000
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
		/// Fügt das Haupt-Einstellungsmenü in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (GameScreen previousScreen, GameTime time)
		{
			base.Entered (previousScreen, time);
			AddGameComponents (time, navigationMenu);
		}

		public override void Draw (GameTime time)
		{
			spriteBatch.Begin ();
			// text
			spriteBatch.DrawStringInRectangle (
			    font: HfGDesign.MenuFont (this),
			    text: MenuName,
			    color: Color.White,
			    bounds: new Rectangle (50, 50, 900, 50).Scale (Viewport),
			    alignX: HorizontalAlignment.Left,
			    alignY: VerticalAlignment.Center
			);
			spriteBatch.End ();
		}

		#endregion

	}
}

