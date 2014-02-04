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
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Utilities;

namespace Knot3.Widgets
{
	/// <summary>
	/// Pausiert ein Spieler im Creative- oder Challenge-Modus das Spiel,
	/// wird dieser Dialog über anderen Spielkomponenten angezeigt.
	/// </summary>
	public sealed class ChallengePauseDialog : Dialog
	{
		#region Properties

		/// <summary>
		/// Das Menü, das verschiedene Schaltflächen enthält.
		/// </summary>
		private Menu pauseMenu;

		#endregion

		#region Constructors

		/// <summary>
		///
		/// </summary>
		public ChallengePauseDialog (IGameScreen screen, DisplayLayer drawOrder)
		: base(screen, drawOrder, "Pause", String.Empty)
		{
			// Der Titel-Text ist mittig ausgerichtet
			AlignX = HorizontalAlignment.Center;

			Bounds.Size = new ScreenPoint (screen, 0.3f, 0.31f);
			// Erstelle das Pause-Menü
			pauseMenu = new Menu (Screen, Index + DisplayLayer.Menu);
			pauseMenu.Bounds = ContentBounds;

			pauseMenu.ItemAlignX = HorizontalAlignment.Left;
			pauseMenu.ItemAlignY = VerticalAlignment.Center;

			MenuEntry settingsButton = new MenuEntry (
			    screen: Screen,
			    drawOrder: Index + DisplayLayer.MenuItem,
			    name: "Settings",
			onClick: (time) => {
				Close (time);
				Screen.NextScreen = new SettingsScreen (Screen.Game);
			}
			);
			MenuEntry backButton = new MenuEntry (
			    screen: Screen,
			    drawOrder: Index + DisplayLayer.MenuItem,
			    name: "Back to Game",
			onClick: (time) => {
				Close (time);
			}
			);

			backButton.AddKey (Keys.Escape);
			MenuEntry discardExitButton = new MenuEntry (
			    screen: Screen,
			    drawOrder: Index + DisplayLayer.MenuItem,
			    name: "Discard Changes and Exit",
			onClick: (time) => {
				Close (time);
				Screen.NextScreen = new StartScreen (Screen.Game);
			}
			);
			backButton.AddKey (Keys.Escape);

			pauseMenu.Add (settingsButton);
			pauseMenu.Add (backButton);
			pauseMenu.Add (discardExitButton);
		}

		#endregion

		#region Methods

		public override void Draw (GameTime time)
		{
			spriteBatch.Begin ();
			spriteBatch.DrawColoredRectangle (Color.Black * 0.8f, Screen.Bounds);
			spriteBatch.End ();

			base.Draw (time);
		}

		public override IEnumerable<IGameScreenComponent> SubComponents (GameTime time)
		{
			foreach (DrawableGameScreenComponent component in base.SubComponents(time)) {
				yield return component;
			}
			yield return pauseMenu;
		}

		#endregion
	}
}
