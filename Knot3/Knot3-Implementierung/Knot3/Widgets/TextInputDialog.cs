using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
	/// Ein Dialog, der eine Texteingabe des Spielers entgegennimmt.
	/// </summary>
	public class TextInputDialog : Dialog, IKeyEventListener
	{
		#region Properties

		/// <summary>
		/// Der Text, der durch den Spieler eingegeben wurde.
		/// </summary>
		public string InputText
		{
			get {
				return textInput.InputText;
			}
			set {
				textInput.InputText = value;
			}
		}
		public bool NoCloseEmpty
		{
			get;
			set;
		}

		public bool NoWhiteSpace
		{
			get;
			set;
		}

		private static readonly Regex Whitespace = new Regex("^\\s*$"); // Todo: global besser!?

		public string Text
		{
			get {
				return textItem.Text;
			}
			set {
				textItem.Text = value;
			}
		}
		/// <summary>
		///
		/// </summary>
		public Action KeyEvent { get; set; }

		private Menu menu;
		private InputItem textInput;
		private TextItem textItem;

		#endregion

		#region Constructors

		/// <summary>
		///
		/// </summary>
		public TextInputDialog (IGameScreen screen, DisplayLayer drawOrder, string title, string text, string inputText)
		: base (screen, drawOrder, title, text)
		{
			textItem = new TextItem(screen, drawOrder, String.Empty);

			Bounds.Size = new ScreenPoint(screen,0.5f,0.3f);
			// Der Titel-Text ist mittig ausgerichtet
			AlignX = HorizontalAlignment.Center;
			menu = new Menu (Screen, Index + DisplayLayer.Menu);
			menu.Bounds = ContentBounds;
			menu.Bounds.Padding = new ScreenPoint(screen,0.010f,0.019f);
			menu.ItemAlignX = HorizontalAlignment.Left;
			menu.ItemAlignY = VerticalAlignment.Center;

			//die Texteingabe
			textInput = new InputItem (Screen, Index + DisplayLayer.MenuItem, text, inputText);
			menu.Add (textInput);
			menu.Add (textItem);
			textInput.IsEnabled = true;
			textInput.IsInputEnabled = true;
			textInput.ValueWidth = 0.75f;

			ValidKeys.AddRange (new Keys[] { Keys.Enter, Keys.Escape });
		}

		#endregion

		#region Methods

		/// <summary>
		///
		/// </summary>
		public override void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			if (key.Contains (Keys.Enter)) {
				bool canClose = true;

				if (NoCloseEmpty) {
					if (textInput.InputText == null || textInput.InputText.Length == 0) {
						canClose = false;
						textInput.InputText = String.Empty;
						textInput.IsInputEnabled = true; // Fokus
						// FIX: bekommt bei schnellem ENTER drücken nicht wieder den Fokus.
					}
				}

				if (NoWhiteSpace) {
					if (Whitespace.IsMatch(textInput.InputText)) {
						canClose = false;
						textInput.InputText = String.Empty;
						textInput.IsInputEnabled = true; // Fokus
						// FIX: bekommt bei schnellem ENTER drücken nicht wieder den Fokus.
					}
				}

				if (canClose) {
					Close(time);
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
