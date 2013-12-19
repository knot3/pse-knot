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


namespace Core
{

	public enum DisplayLayer : int
	{
		None = 0,
		Background = 1,
		GameWorld = 2,
		Dialog = 3,
		Menu = 4,
		MenuItem = 5,
		SubMenu = 6,
		SubMenuItem = 7,
		Overlay = 8,
		Cursor = 9,
	}
}
