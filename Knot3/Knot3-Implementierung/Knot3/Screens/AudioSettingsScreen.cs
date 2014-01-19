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
	/// Die Klasse AudioSettingsScreen steht für den Spielzustand, der die Audio-Einstellungen repräsentiert.
	/// </summary>
	public class AudioSettingsScreen : SettingsScreen
	{

		#region Properties

		/// <summary>
		/// Das Menü, das die Einstellungen enthält.
		/// </summary>
		private VerticalMenu settingsMenu { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues AudioSettingsScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
		/// </summary>
		public AudioSettingsScreen (Knot3Game game)
		: base(game)
		{
			MenuName = "Audio";

			settingsMenu = new VerticalMenu(this, DisplayLayer.Menu);
			settingsMenu.RelativePosition = () => new Vector2(0.400f, 0.180f);
			settingsMenu.RelativeSize = () => new Vector2(0.500f, 0.770f);
			settingsMenu.RelativePadding = () => new Vector2(0.010f, 0.010f);
			settingsMenu.ItemForegroundColor = base.MenuItemForegroundColor;
			settingsMenu.ItemBackgroundColor = base.MenuItemBackgroundColor;
			settingsMenu.ItemAlignX = HorizontalAlignment.Left;
			settingsMenu.ItemAlignY = VerticalAlignment.Center;

           /* SliderItem musicslider = new SliderItem(
                screen: this,
                drawOrder: DisplayLayer.MenuItem,
                text: "Music",
                max: 100,
                min: 0,
                step: 10,
                value: 50
            );

             SliderItem soundslider = new SliderItem(
                screen: this,
                drawOrder: DisplayLayer.MenuItem,
                text: "Sound",
                max: 100,
                min: 0,
                step: 10,
                value: 50
            ); */
		}

		#endregion

		#region Methods

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			//throw new System.NotImplementedException();
		}

		/// <summary>
		/// Fügt das Menü mit den Einstellungen in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (GameScreen previousScreen, GameTime GameTime)
		{
			base.Entered(previousScreen, GameTime);
			AddGameComponents(GameTime, settingsMenu);
			//throw new System.NotImplementedException();
		}

		#endregion

	}
}

