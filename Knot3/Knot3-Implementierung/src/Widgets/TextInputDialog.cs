using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Widgets
{
    using Core;

	public class TextInputDialog : ConfirmDialog
	{
		public virtual string InputText
		{
			get;
			set;
		}

		public TextInputDialog(GameScreen screen, DisplayLayer drawOrder, string title, string text, string inputText)
             : base(screen, drawOrder, title, text)
		{
		}

	}
}

