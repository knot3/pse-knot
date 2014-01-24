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
		private VerticalMenu pauseMenu;

		#endregion

		#region Constructors

		/// <summary>
		///
		/// </summary>
		public ChallengePauseDialog (GameScreen screen, DisplayLayer drawOrder)
		: base (screen, drawOrder, "Pause", "")
		{
			// Der Titel-Text ist mittig ausgerichtet
			AlignX = HorizontalAlignment.Center;

			// Erstelle das Pause-Menü
			pauseMenu = new VerticalMenu (Screen, DisplayLayer.Menu);
			pauseMenu.RelativePosition = () => RelativeContentPosition;
			pauseMenu.RelativeSize = () => RelativeContentSize;
			pauseMenu.RelativePadding = () => RelativePadding ();
			pauseMenu.ItemForegroundColor = (s) => Color.White;
			pauseMenu.ItemBackgroundColor = (s) => (s == ItemState.Hovered) ? Color.White * 0.3f : Color.White * 0.1f;
			pauseMenu.ItemAlignX = HorizontalAlignment.Left;
			pauseMenu.ItemAlignY = VerticalAlignment.Center;

			MenuButton settingsButton = new MenuButton (
			    screen: Screen,
			    drawOrder: DisplayLayer.MenuItem,
			    name: "Settings",
			onClick: (time) => {
				Close (time);
				Screen.NextScreen = new SettingsScreen (Screen.Game);
			}
			);
			MenuButton backButton = new MenuButton (
			    screen: Screen,
			    drawOrder: DisplayLayer.MenuItem,
			    name: "Back to Game",
			onClick: (time) => {
				Close (time);
			}
			);

			backButton.AddKey (Keys.Escape);
			MenuButton discardExitButton = new MenuButton (
			    screen: Screen,
			    drawOrder: DisplayLayer.MenuItem,
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

