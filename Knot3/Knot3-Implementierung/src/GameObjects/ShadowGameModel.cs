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

	public class ShadowGameModel : ShadowGameObject
	{
		public virtual Color ShadowColor
		{
			get;
			set;
		}

		public virtual float ShadowAlpha
		{
			get;
			set;
		}

		public ShadowGameModel(GameScreen sreen, GameModel decoratedModel)
		{
		}

		public override void Draw(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

	}
}

