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

	public class ModelFactory
	{
		private Dictionary<GameModelInfo, GameModel> cache
		{
			get;
			set;
		}

		private Func<GameScreen, GameModelInfo, GameModel> createModel
		{
			get;
			set;
		}

		public virtual GameModel this[GameScreen state, GameModelInfo info]
        {
            get
            {
                throw new System.NotImplementedException();
            }
		}

		public ModelFactory(Func<GameScreen, GameModelInfo, GameModel> createModel)
		{
		}

	}
}

