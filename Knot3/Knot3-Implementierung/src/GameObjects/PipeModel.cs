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


namespace GameObjects
{
	using Core;

	public class PipeModel : GameModel
	{
		public virtual PipeModelInfo Info
		{
			get;
			set;
		}

		public virtual PipeModelInfo PipeModelInfo
		{
			get;
			set;
		}

		public virtual GameObjectDistance Intersects(Ray ray)
		{
			throw new System.NotImplementedException();
		}

		public PipeModel(GameScreen screen, PipeModelInfo info)
		{
		}

	}
}

