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

		/// <summary>
		///
		/// </summary>
		public Action KeyEvent { get; set; }

		private VerticalMenu menu;
		private InputItem textInput;

		#endregion

		#region Constructors

		/// <summary>
		///
		/// </summary>
		public TextInputDialog (GameScreen screen, DisplayLayer drawOrder, string title, string text, string inputText)
		: base (screen, drawOrder, title, text)
		{
			// Der Titel-Text ist mittig ausgerichtet
			AlignX = HorizontalAlignment.Center;
			menu = new VerticalMenu (Screen, Index + DisplayLayer.Menu);
			menu.RelativePosition = () => RelativeContentPosition;
			menu.RelativeSize = () => RelativeContentSize;
			menu.RelativePadding = () => RelativePadding ();
			menu.ItemForegroundColor = (s) => Color.White;
			menu.ItemBackgroundColor = (s) => Color.Transparent;
			menu.ItemAlignX = HorizontalAlignment.Left;
			menu.ItemAlignY = VerticalAlignment.Center;

			//die Texteingabe
			textInput = new InputItem (Screen, Index + DisplayLayer.MenuItem, text, inputText);
			menu.Add (textInput);
			textInput.IsEnabled = true;

			ValidKeys.AddRange (new Keys[] { Keys.Enter, Keys.Escape });
		}

		#endregion

		#region Methods

		/// <summary>
		///
		/// </summary>
		public override void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			if (key.Contains (Keys.Enter) || key.Contains (Keys.Escape)) {
				Close (time);
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

