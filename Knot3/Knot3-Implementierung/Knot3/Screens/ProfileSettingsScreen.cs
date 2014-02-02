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

namespace Knot3.Screens
{
	/// <summary>
	/// Der Spielzustand, der die Profil-Einstellungen darstellt.
	/// </summary>
	public class ProfileSettingsScreen : SettingsScreen
	{
		#region Properties

		/// <summary>
		/// Das vertikale Menü wo die Einstellungen anzeigt. Hier nimmt der Spieler Einstellungen vor.
		/// </summary>
		private Menu settingsMenu { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines ProfileSettingsScreen-Objekts und initialisiert dieses mit einem Knot3Game-Objekt.
		/// </summary>
		public ProfileSettingsScreen (Knot3Game game)
		: base(game)
		{
			MenuName = "Profile";

			settingsMenu = new Menu(this, DisplayLayer.ScreenUI + DisplayLayer.Menu);
			settingsMenu.Bounds.Position = new ScreenPoint (this, 0.400f, 0.180f);
			settingsMenu.Bounds.Size = new ScreenPoint (this, 0.500f, 0.720f);
			settingsMenu.Bounds.Padding = new ScreenPoint (this, 0.010f, 0.010f);
			settingsMenu.ItemForegroundColor = base.MenuItemForegroundColor;
			settingsMenu.ItemBackgroundColor = base.MenuItemBackgroundColor;
			settingsMenu.ItemAlignX = HorizontalAlignment.Left;
			settingsMenu.ItemAlignY = VerticalAlignment.Center;

            
			InputItem playerNameInput = new InputItem(
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    text: "Player Name:",
                inputText: Options.Default["profile", "name", "Player"]
			);
			playerNameInput.OnValueSubmitted += () => {
				Options.Default["profile", "name", ""] = playerNameInput.InputText;
			};

			settingsMenu.Add(playerNameInput);
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
		/// Fügt das Menü mit den Einstellungen in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime time)
		{
			base.Entered(previousScreen, time);
			AddGameComponents(time, settingsMenu);
		}

		#endregion
	}
}
