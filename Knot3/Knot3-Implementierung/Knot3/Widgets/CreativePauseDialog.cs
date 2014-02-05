using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
	public sealed class CreativePauseDialog : Dialog
	{
		#region Properties

		/// <summary>
		/// Das Menü, das verschiedene Schaltflächen enthält.
		/// </summary>
		private Menu pauseMenu;
		private Knot knot;

		#endregion

		#region Constructors

		/// <summary>
		///
		/// </summary>
		public CreativePauseDialog (IGameScreen screen, DisplayLayer drawOrder, Knot knot)
		: base(screen, drawOrder, "Pause", String.Empty)
		{
			this.knot = knot;

			// Der Titel-Text ist mittig ausgerichtet
			AlignX = HorizontalAlignment.Center;
			Bounds.Size = new ScreenPoint(screen,0.3f,0.31f);
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
			MenuEntry saveButton = new MenuEntry (
			    screen: Screen,
			    drawOrder: Index + DisplayLayer.MenuItem,
			    name: "Save",
			onClick: (time) => {
				Close (time);
				KnotSave (time);
			}
			);
			MenuEntry saveAsButton = new MenuEntry (
			    screen: Screen,
			    drawOrder: Index + DisplayLayer.MenuItem,
			    name: "Save As",
			onClick: (time) => {
				Close (time);
				KnotSaveAs (time);
			}
			);
			MenuEntry saveExitButton = new MenuEntry (
			    screen: Screen,
			    drawOrder: Index + DisplayLayer.MenuItem,
			    name: "Save and Exit",
			onClick: (time) => {
				Close (time);
				KnotSave (time);
				Screen.NextScreen = new StartScreen (Screen.Game);
			}
			);
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
			pauseMenu.Add (saveButton);
			pauseMenu.Add (saveAsButton);
			pauseMenu.Add (saveExitButton);
			pauseMenu.Add (discardExitButton);
		}

		public override IEnumerable<IGameScreenComponent> SubComponents (GameTime time)
		{
			foreach (DrawableGameScreenComponent component in base.SubComponents(time)) {
				yield return component;
			}
			yield return pauseMenu;
		}

		private void KnotSave (GameTime time)
		{
			try {
				knot.Save ();
			}
			catch (IOException) {
				KnotSaveAs (time);
			}
		}

		private void KnotSaveAs (GameTime time)
		{
			TextInputDialog saveDialog = new TextInputDialog (
			    screen: Screen,
			    drawOrder: DisplayLayer.Dialog,
			    title: "Save Knot",
			    text: "Name:",
			    inputText: knot.Name != null ? knot.Name : String.Empty
			);
			saveDialog.NoCloseEmpty = true;
			saveDialog.NoWhiteSpace = true;
			saveDialog.Text = "Press Enter to save the Knot.";
			Screen.AddGameComponents (null, saveDialog);
			saveDialog.Close += (t) => {
				try {
					knot.Name = saveDialog.InputText;
					knot.Save ();
				}
				catch (IOException ex) {
					ErrorDialog errorDialog = new ErrorDialog (
					    screen: Screen,
					    drawIndex: DisplayLayer.Dialog * 2,
					    message: "Error in Knot.Save(): " + ex.ToString ()
					);
					Screen.AddGameComponents (null, errorDialog);
				}
			};
		}

		#endregion
	}
}
