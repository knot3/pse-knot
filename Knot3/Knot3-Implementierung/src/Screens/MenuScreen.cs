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


namespace Screens
{
	using Core;

	public abstract class MenuScreen : GameScreen
	{
		public override void Update(GameTime time)
		{
			throw new System.NotImplementedException();
		}

		public abstract void Entered(GameScreen previousScreen, GameTime GameTime);

	}
}

