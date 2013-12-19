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

	public class ShadowGameObject : IGameObject
	{
		public virtual GameObjectInfo Info
		{
			get;
			set;
		}

		public virtual World World
		{
			get;
			set;
		}

		public virtual Vector3 ShadowPosition
		{
			get;
			set;
		}

		public virtual Vector3 OriginalPosition
		{
			get;
			set;
		}

		public virtual Vector3 Center()
		{
			throw new System.NotImplementedException();
		}

		public virtual void Update(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual void Draw(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual GameObjectDistance Intersects(Ray Ray)
		{
			throw new System.NotImplementedException();
		}

		public ShadowGameObject(GameScreen screen, IGameObject decoratedObj)
		{
		}

	}
}

