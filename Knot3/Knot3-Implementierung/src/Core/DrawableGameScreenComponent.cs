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

	public abstract class DrawableGameScreenComponent : XNA.DrawableGameComponent, IGameScreenComponent
	{
		public virtual GameScreen Screen
		{
			get;
			set;
		}

		public virtual DisplayLayer Index
		{
			get;
			set;
		}

		public virtual IEnumerable<T> SubComponents(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual void DrawableGameStateComponent(GameScreen screen, DisplayLayer index)
		{
			throw new System.NotImplementedException();
		}

	}
}

