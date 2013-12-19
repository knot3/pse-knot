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


namespace RenderEffects
{
	using Core;
	using GameObjects;

	public abstract class RenderEffect : IRenderEffect
	{
		public virtual RenderTarget2D RenderTarget
		{
			get;
			set;
		}

		protected virtual GameScreen screen
		{
			get;
			set;
		}

		protected virtual SpriteBatch spriteBatch
		{
			get;
			set;
		}

		public virtual void Begin(object GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual void End(object GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual void DrawModel(GameModel GameModel, object GameTime)
		{
			throw new System.NotImplementedException();
		}

		public virtual void RemapModel(GameModel GameModel)
		{
			throw new System.NotImplementedException();
		}

		protected abstract void DrawRenderTarget(GameTime time);

	}
}

