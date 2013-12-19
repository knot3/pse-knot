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

	public abstract class GameModel : IGameObject
	{
		public virtual float Alpha
		{
			get;
			set;
		}

		public virtual Color BaseColor
		{
			get;
			set;
		}

		public virtual Color HightlightColor
		{
			get;
			set;
		}

		public virtual float HighlightIntensity
		{
			get;
			set;
		}

		public virtual GameModelInfo Info
		{
			get;
			set;
		}

		public virtual Model Model
		{
			get;
			set;
		}

		public virtual World World
		{
			get;
			set;
		}

		public virtual Matrix WorldMatrix
		{
			get;
			set;
		}

		public virtual GameModelInfo GameModelInfo
		{
			get;
			set;
		}

		public virtual Vector3 Center()
		{
			throw new System.NotImplementedException();
		}

		public abstract void Update(GameTime GameTime);

		public virtual void Draw(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual GameObjectDistance Intersects(Ray Ray)
		{
			throw new System.NotImplementedException();
		}

		public GameModel(GameScreen screen, GameModelInfo info)
		{
		}

	}
}

