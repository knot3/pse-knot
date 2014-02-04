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
	/// Ein Dialog, der Schaltflächen zum Bestätigen einer Aktion anzeigt.
	/// </summary>
	public abstract class ConfirmDialog : Dialog
	{
		#region Properties

		/// <summary>
		/// Das Menü, das Schaltflächen enthält.
		/// </summary>
		private Container buttons { get; set; }

		private Menu menu;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues ConfirmDialog-Objekt und initialisiert dieses mit dem zugehörigen IGameScreen-Objekt.
		/// Zudem sind Angaben zur Zeichenreihenfolge, einer Zeichenkette für den Titel und für den eingeblendeten Text Pflicht.
		/// [base=screen, drawOrder, title, text]
		/// </summary>
		public ConfirmDialog (IGameScreen screen, DisplayLayer drawOrder, string title, string text)
		: base(screen, drawOrder, title, text)
		{
			// Der Titel-Text ist mittig ausgerichtet
			AlignX = HorizontalAlignment.Center;

			// Menü, in dem die Textanzeige angezeigt wird
			menu = new Menu (Screen, Index + DisplayLayer.Menu);
			menu.Bounds = ContentBounds;
			menu.ItemForegroundColor = (s) => Color.White;
			menu.ItemBackgroundColor = (s) => Color.Transparent;
			menu.ItemAlignX = HorizontalAlignment.Left;
			menu.ItemAlignY = VerticalAlignment.Center;

			// Die Textanzeige
			TextItem textInput = new TextItem (Screen, Index + DisplayLayer.MenuItem, text);
			menu.Add (textInput);

			ValidKeys.AddRange (new Keys[] { Keys.Enter, Keys.Escape });
		}

		#endregion

		#region Methods

		/// <summary>
		///
		/// </summary>
		public override void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			if (keyEvent == KeyEvent.KeyDown) {
				if (key.Contains (Keys.Enter) || key.Contains (Keys.Escape)) {
					Close (time);
				}
			}
			base.OnKeyEvent (key, keyEvent, time);
		}

		public override IEnumerable<IGameScreenComponent> SubComponents (GameTime time)
		{
			foreach (DrawableGameScreenComponent component in base.SubComponents(time)) {
				yield return component;
			}
			yield return menu;
		}

		#endregion
	}
}
