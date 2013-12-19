using System;
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

namespace Widgets
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class ColorPickItem : MenuItem
	{
		public virtual Color Color
		{
			get;
			set;
		}

		private ColorPicker picker
		{
			get;
			set;
		}

		public ColorPickItem(GameScreen screen, DisplayLayer drawOrder, Color color)
		{
		}

	}
}

