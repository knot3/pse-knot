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

	public interface IMouseEventListener 
	{
		DisplayLayer Index { get;set; }

		bool IsMouseEventEnabled { get;set; }

		DisplayLayer DisplayLayer { get;set; }

		Rectangle Bounds();

		void OnLeftClick(Vector2 position, ClickState state, GameTime time);

		void OnRightClick(Vector2 position, ClickState state, GameTime time);

	}
}

