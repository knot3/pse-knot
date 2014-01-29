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
	/// In diesem Menü trifft der Spieler die Wahl, ob er im Creative-Modus einen neuen Knoten erstellen, einen Knoten laden oder
	/// eine neue Challenge erstellen möchte.
	/// </summary>
	public class CreativeMainScreen : MenuScreen
	{
		#region Properties

		/// <summary>
		/// Ein Menü aus Schaltflächen, welche den Spielwunsch des Spielers weiterleiten.
		/// </summary>
		private Menu buttons;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues CreativeMainScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
		/// </summary>
		public CreativeMainScreen (Knot3Game game)
		: base(game)
		{
			buttons = new Menu (this, DisplayLayer.ScreenUI + DisplayLayer.Menu);

			// menu
			buttons.ItemForegroundColor = base.MenuItemForegroundColor;
			buttons.ItemBackgroundColor = base.MenuItemBackgroundColor;
			buttons.ItemAlignX = HorizontalAlignment.Center;
			buttons.ItemAlignY = VerticalAlignment.Center;

			MenuButton newKnotButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "NEW\nKnot",
			    onClick: (time) => NextScreen = new CreativeModeScreen (Game, new Knot ())
			);
			newKnotButton.SetCoordinates (left: 0.100f, top: 0.150f, right: 0.300f, bottom: 0.350f);

			MenuButton loadKnotButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "LOAD\nKnot",
			    onClick: (time) => NextScreen = new CreativeLoadScreen (Game)
			);
			loadKnotButton.SetCoordinates (left: 0.675f, top: 0.300f, right: 0.875f, bottom: 0.475f);

			MenuButton newChallengeButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "NEW\nChallenge",
			    onClick: (time) => NextScreen = new ChallengeCreateScreen (Game)
			);
			newChallengeButton.SetCoordinates (left: 0.250f, top: 0.525f, right: 0.600f, bottom: 0.750f);

			MenuButton backButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Back",
			    onClick: (time) => NextScreen = Game.Screens.Where ((s) => !(s is CreativeMainScreen)).ElementAt (0)
			);
			backButton.AddKey (Keys.Escape);

			MenuButton returnButton = new MenuButton (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Back",
			    onClick: (time) => NextScreen = new StartScreen (Game)
			);
			returnButton.SetCoordinates (left: 0.825f, top: 0.850f, right: 0.975f, bottom: 0.950f);

			buttons.Add (newKnotButton);
			buttons.Add (loadKnotButton);
			buttons.Add (newChallengeButton);
			buttons.Add (backButton);
			buttons.Add (returnButton);

			// die Linien
			lines.AddPoints (0.000f, 0.150f,
			                 0.300f, 0.350f, 0.100f, 0.070f, 0.600f, 0.750f, 0.250f,
			                 0.525f, 0.875f, 0.300f, 0.675f, 0.475f, 0.950f, 0.000f
			                );

			lines.AddPoints (0.975f, 0.850f, 0.825f, 0.950f, 0.975f, 0.850f);
		}

		#endregion

		#region Methods

		/// <summary>
		///
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime time)
		{
			base.Entered (previousScreen, time);
			AddGameComponents (time, buttons);
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
		}

		#endregion
	}
}

