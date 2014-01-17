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
            highscoreList = new VerticalMenu(Screen, DisplayLayer.Menu);
            highscoreList.RelativePosition = () => RelativeContentPosition;
            highscoreList.RelativeSize = () => RelativeContentSize;
            highscoreList.RelativePadding = () => RelativePadding();
            highscoreList.ItemForegroundColor = (s) => Color.White;
            highscoreList.ItemBackgroundColor = (s) => (s == ItemState.Hovered) ? Color.White * 0.3f : Color.White * 0.1f;
            highscoreList.ItemAlignX = HorizontalAlignment.Left;
            highscoreList.ItemAlignY = VerticalAlignment.Center;
            
            //Für Reine Textfelder oder Listen besitzen wir kein Widget. Also habe ich Buttons ohne Funktion verwendet.
            if (!challenge.Highscore.Equals(null)) {
                MenuButton firstScore = new MenuButton(screen, drawOrder, challenge.Highscore.Current.Value.ToString() + " " + challenge.Highscore.Current.Value,  null);
                highscoreList.Add(firstScore);
                while(challenge.Highscore.MoveNext()) {
                    MenuButton nextScore = new MenuButton(screen, drawOrder, challenge.Highscore.Current.Value.ToString() + " " + challenge.Highscore.Current.Value,  null);
                    highscoreList.Add(nextScore);
                }
            }

           //Startet die Challenge erneut
           Action restartChallenge = () => {
					Screen.NextScreen = new ChallengeModeScreen(Screen.Game,challenge);
				};
            //Button fürs Neustarten
            MenuButton restartButton = new MenuButton(screen, drawOrder, "Restart Challenge", restartChallenge);
            highscoreList.Add(restartButton);

           //Kehrt zum Startscreen zurück
           Action returnToMenu = () => {
					Screen.NextScreen = new StartScreen(Screen.Game);
				};
            //Button für die Rückkehr zum StartScreen
            MenuButton returnButton = new MenuButton(screen, drawOrder, "Return to menu", returnToMenu);
            highscoreList.Add(returnButton);    

          }
		}

		#endregion

	}
}

