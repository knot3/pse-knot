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
	///Dieser Dialog Zeigt den Highscore für die Gegebene Challenge an und bietet die Option
	///zum Neustarten oder Rückkehr zum Hauptmenü
	/// </summary>
	public sealed class HighscoreDialog : Dialog
	{
		#region Properties

		private VerticalMenu highscoreList;

		#endregion

		#region Constructors

		/// <summary>
		///
		/// </summary>
		public HighscoreDialog (GameScreen screen, DisplayLayer drawOrder, Challenge challenge)
		: base(screen, drawOrder, "Highscores", "")
		{
			// Der Titel-Text ist mittig ausgerichtet
			AlignX = HorizontalAlignment.Center;
			highscoreList = new VerticalMenu (Screen, DisplayLayer.Menu);
			highscoreList.RelativePosition = () => RelativeContentPosition;
			highscoreList.RelativeSize = () => RelativeContentSize;
			highscoreList.RelativePadding = () => RelativePadding ();
			highscoreList.ItemForegroundColor = (s) => Color.White;
			highscoreList.ItemBackgroundColor = (s) => (s == ItemState.Hovered) ? Color.White * 0.3f : Color.White * 0.1f;
			highscoreList.ItemAlignX = HorizontalAlignment.Left;
			highscoreList.ItemAlignY = VerticalAlignment.Center;

			if (challenge.Highscore != null) {
				//sotiert die Highscoreliste wird nach der Zeit sotiert
				foreach (KeyValuePair<string, int> entry in challenge.Highscore.OrderBy(key => key.Value)) {
					TextItem firstScore = new TextItem (screen, drawOrder, entry.Value.ToString () + " " + entry.Key);
					highscoreList.Add (firstScore);
				}
			}


			//Button fürs Neustarten
			MenuButton restartButton = new MenuButton(
			    screen: Screen,
			    drawOrder: DisplayLayer.MenuItem,
			    name: "Restart challenge",
			onClick: (time) => {
				Close(time);
				Screen.NextScreen = new ChallengeModeScreen(Screen.Game, challenge);
			}
			);

			highscoreList.Add (restartButton);

			//Kehrt zum Startscreen zurück
			Action<GameTime> returnToMenu = (time) => {
				Screen.NextScreen = new StartScreen (Screen.Game);
			};

			//Button für die Rückkehr zum StartScreen
			MenuButton returnButton = new MenuButton (
			    screen: Screen,
			    drawOrder: DisplayLayer.MenuItem,
			    name: "Return to menu",
			onClick: (time) => {
				Close(time);
				Screen.NextScreen = new StartScreen(Screen.Game);
			}
			);
			highscoreList.Add (returnButton);
		}

		#endregion

		#region Methods

		public override IEnumerable<IGameScreenComponent> SubComponents (GameTime time)
		{
			foreach (DrawableGameScreenComponent component in base.SubComponents(time)) {
				yield return component;
			}
			yield return highscoreList;
		}

		#endregion
	}
}

