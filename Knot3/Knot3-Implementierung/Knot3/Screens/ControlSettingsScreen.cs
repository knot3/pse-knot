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
	/// Der Spielzustand, der die Steuerungs-Einstellungen darstellt.
	/// </summary>
	public class ControlSettingsScreen : SettingsScreen
	{
		#region Properties

		/// <summary>
		/// Das Menü, das die Einstellungen enthält.
		/// </summary>
		private VerticalMenu settingsMenu;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues ControlSettingsScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
		/// </summary>
		public ControlSettingsScreen (Knot3Game game)
		: base(game)
		{
			MenuName = "Controls";

			settingsMenu = new VerticalMenu (this, DisplayLayer.Menu);
			settingsMenu.RelativePosition = () => new Vector2 (0.400f, 0.180f);
			settingsMenu.RelativeSize = () => new Vector2 (0.500f, 0.770f);
			settingsMenu.RelativePadding = () => new Vector2 (0.010f, 0.010f);
			settingsMenu.ItemForegroundColor = base.MenuItemForegroundColor;
			settingsMenu.ItemBackgroundColor = base.MenuItemBackgroundColor;
			settingsMenu.ItemAlignX = HorizontalAlignment.Left;
			settingsMenu.ItemAlignY = VerticalAlignment.Center;


			CheckBoxItem showOverlay = new CheckBoxItem (
			    screen: this,
			    drawOrder: DisplayLayer.MenuItem,
			    text: "Move Rotation Targets to Center",
			    option: new BooleanOptionInfo ("video", "arcball-around-center", true, Options.Default)
			);
			settingsMenu.Add (showOverlay);


			// Lade die Standardbelegung
			Dictionary<PlayerActions, Keys> defaultReversed = KnotInputHandler.DefaultKeyAssignment.ReverseDictionary();

			// Iteriere dazu über alle gültigen PlayerActions...
			foreach (PlayerActions action in typeof(PlayerActions).ToEnumValues<PlayerActions>()) {
				string actionName = action.ToEnumDescription ();

				// Erstelle das dazugehörige Options-Objekt...
				KeyOptionInfo option = new KeyOptionInfo (
				    section: "controls",
				    name: actionName,
				    defaultValue: defaultReversed [action],
				    configFile: Options.Default
				);

				// Erstelle ein KeyInputItem zum Festlegen der Tastenbelegung
				KeyInputItem item = new KeyInputItem (
				    screen: this,
				    drawOrder: DisplayLayer.MenuItem,
				    text: actionName,
				    option: option
				);

				// Füge es in das Menü ein
				settingsMenu.Add(item);
			}
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
		public override void Entered (GameScreen previousScreen, GameTime time)
		{
			base.Entered (previousScreen, time);
			AddGameComponents (time, settingsMenu);
		}

		#endregion

	}
}

