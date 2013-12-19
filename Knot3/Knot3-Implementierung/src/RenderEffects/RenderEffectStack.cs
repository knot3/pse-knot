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

	public class RenderEffectStack
	{
		public virtual IRenderEffect CurrentEffect
		{
			get;
			set;
		}

		private IRenderEffect DefaultEffect
		{
			get;
			set;
		}

		public virtual IEnumerable<IRenderEffect> IRenderEffect
		{
			get;
			set;
		}

		public virtual IRenderEffect Pop()
		{
			throw new System.NotImplementedException();
		}

		public virtual void Push(IRenderEffect effect)
		{
			throw new System.NotImplementedException();
		}

		public RenderEffectStack(IRenderEffect defaultEffect)
		{
		}

	}
}

