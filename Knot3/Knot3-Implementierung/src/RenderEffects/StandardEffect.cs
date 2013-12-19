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

	public class StandardEffect : RenderEffect
	{
		protected virtual void DrawRenderTarget(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public StandardEffect(GameScreen screen)
		{
		}

	}
}

