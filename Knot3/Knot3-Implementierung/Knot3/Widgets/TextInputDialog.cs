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
		public string InputText { get; set; }

		/// <summary>
		///
		/// </summary>
		public Action KeyEvent { get; set; }

		private VerticalMenu inputMenu;
		#endregion

		#region Constructors

		/// <summary>
		///
		/// </summary>
		public TextInputDialog (GameScreen screen, DisplayLayer drawOrder, string title, string text, string inputText)
		: base(screen, drawOrder, title, text)
		{
			// Der Titel-Text ist mittig ausgerichtet
			AlignX = HorizontalAlignment.Center;
			inputMenu = new VerticalMenu(Screen, DisplayLayer.Menu);
			inputMenu.RelativePosition = () => RelativeContentPosition;
			inputMenu.RelativeSize = () => RelativeContentSize;
			inputMenu.RelativePadding = () => RelativePadding();
			inputMenu.ItemForegroundColor = (s) => Color.White;
			inputMenu.ItemBackgroundColor = (s) => (s == ItemState.Hovered) ? Color.White * 0.3f : Color.White * 0.1f;
			inputMenu.ItemAlignX = HorizontalAlignment.Left;
			inputMenu.ItemAlignY = VerticalAlignment.Center;

			//die Texteingabe
			InputItem textInput = new InputItem(Screen, DisplayLayer.MenuItem, text, inputText);
			inputMenu.Add(textInput);
		}

		#endregion

		#region Methods

		/// <summary>
		///
		/// </summary>
		public override void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			base.OnKeyEvent (key, keyEvent, time);
		}

		#endregion

	}
}

