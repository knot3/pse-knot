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
    using KnotData;
	using GameObjects;

	public class CreativeModeScreen : GameScreen
	{
		public virtual Knot Knot
		{
			get;
			set;
		}

		private World World
		{
			get;
			set;
		}

		private KnotRenderer KnotRenderer
		{
			get;
			set;
		}

		public virtual Stack<Knot> Undo
		{
			get;
			set;
		}

		public virtual Stack<Knot> Redo
		{
			get;
			set;
		}

		public override void Update(GameTime time)
		{
			throw new System.NotImplementedException();
		}

		public override void Entered(GameScreen previousScreen, GameTime time)
		{
			throw new System.NotImplementedException();
		}

	}
}

