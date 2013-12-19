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

	public class NodeModel : GameModel
	{
		public virtual NodeModelInfo Info
		{
			get;
			set;
		}

		public virtual NodeModelInfo NodeModelInfo
		{
			get;
			set;
		}

		public NodeModel(GameScreen screen, NodeModelInfo info)
		{
		}

		public override void Draw(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

		public override void Update(GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

	}
}

