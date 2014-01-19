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
	///Dieser Dialog Zeigt den Highscore f�r die Gegebene Challenge an und bietet die Option
	///zum Neustarten oder R�ckkehr zum Hauptmen�
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

			//F�r Reine Textfelder oder Listen besitzen wir kein Widget. Also habe ich Buttons ohne Funktion verwendet.
			if (challenge.Highscore != null) {
				foreach (KeyValuePair<string, int> entry in challenge.Highscore) {
					TextItem firstScore = new TextItem (screen, drawOrder, entry.Value.ToString () + " " + entry.Key);
					highscoreList.Add (firstScore);
				}
			}

			//Startet die Challenge erneut
			Action<GameTime> restartChallenge = (time) => {
				Screen.NextScreen = new ChallengeModeScreen (Screen.Game, challenge);
			};
			//Button f�rs Neustarten
			MenuButton restartButton = new MenuButton (screen, drawOrder, "Restart Challenge", restartChallenge);
			highscoreList.Add (restartButton);

			//Kehrt zum Startscreen zur�ck
			Action<GameTime> returnToMenu = (time) => {
				Screen.NextScreen = new StartScreen (Screen.Game);
			};
			//Button f�r die R�ckkehr zum StartScreen
			MenuButton returnButton = new MenuButton (screen, drawOrder, "Return to menu", returnToMenu);
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

