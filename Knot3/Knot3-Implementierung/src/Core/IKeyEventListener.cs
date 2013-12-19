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

	public interface IKeyEventListener 
	{
		DisplayLayer Index { get;set; }

		bool IsKeyEventEnabled { get;set; }

		List<Keys> ValidKeys { get;set; }

		DisplayLayer DisplayLayer { get;set; }

		void OnKeyEvent();

	}
}

