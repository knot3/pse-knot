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

		private Menu highscoreList;

		#endregion

		#region Constructors

		/// <summary>
		///
		/// </summary>
		public HighscoreDialog (IGameScreen screen, DisplayLayer drawOrder, Challenge challenge)
		: base(screen, drawOrder, "Highscores", String.Empty)
		{
			// Der Titel-Text ist mittig ausgerichtet
			AlignX = HorizontalAlignment.Center;
			highscoreList = new Menu (Screen, Index + DisplayLayer.Menu);
			highscoreList.Bounds = ContentBounds;
			highscoreList.ItemForegroundColor = MenuItemForegroundColor;
			highscoreList.ItemBackgroundColor = MenuItemBackgroundColor;
			highscoreList.ItemAlignX = HorizontalAlignment.Left;
			highscoreList.ItemAlignY = VerticalAlignment.Center;

			if (challenge.Highscore != null) {
				//sotiert die Highscoreliste wird nach der Zeit sotiert
				int highscoreCounter = 0;
				foreach (KeyValuePair<string, int> entry in challenge.Highscore.OrderBy(key => key.Value)) {
					TextItem firstScore = new TextItem (screen, drawOrder, entry.Value + " " + entry.Key);
					highscoreList.Add (firstScore);
					highscoreCounter++;
					if (highscoreCounter >8) {
						break;
					}
				}
			}

			//Button f�rs Neustarten
			MenuEntry restartButton = new MenuEntry (
			    screen: Screen,
			    drawOrder: Index + DisplayLayer.MenuItem,
			    name: "Restart challenge",
			onClick: (time) => {
				Close (time);
				Screen.NextScreen = new ChallengeModeScreen (Screen.Game, challenge);
			}
			);

			highscoreList.Add (restartButton);

			//Button f�r die R�ckkehr zum StartScreen
			MenuEntry returnButton = new MenuEntry (
			    screen: Screen,
			    drawOrder: Index + DisplayLayer.MenuItem,
			    name: "Return to menu",
			onClick: (time) => {
				Close (time);
				Screen.NextScreen = new StartScreen (Screen.Game);
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
