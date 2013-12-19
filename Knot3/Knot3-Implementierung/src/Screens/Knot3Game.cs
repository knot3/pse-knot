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

	public class Knot3Game : XNA.Game
	{
		public virtual bool IsFullScreen
		{
			get;
			set;
		}

		public virtual Stack<GameScreen> Screens
		{
			get;
			set;
		}

		public virtual bool VSync
		{
			get;
			set;
		}

		public virtual GraphicsDeviceManager Graphics
		{
			get;
			set;
		}

		public virtual IEnumerable<GameScreen> GameScreen
		{
			get;
			set;
		}

		public Knot3Game()
		{
		}

		public override void Draw(GameTime time)
		{
			throw new System.NotImplementedException();
		}

		public override void Initialize()
		{
			throw new System.NotImplementedException();
		}

		public override void UnloadContent()
		{
			throw new System.NotImplementedException();
		}

		public override void Update(GameTime time)
		{
			throw new System.NotImplementedException();
		}

	}
}

