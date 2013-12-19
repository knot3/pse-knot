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
	using GameObjects;
    using KnotData;

	public class ChallengeModeScreen : GameScreen
	{
		public virtual object PlayerKnot
		{
			get;
			set;
		}

		public virtual object ChallengeKnot
		{
			get;
			set;
		}

		private World ChallengeWorld
		{
			get;
			set;
		}

		private World PlayerWorld
		{
			get;
			set;
		}

		private KnotRenderer ChallengeKnotRenderer
		{
			get;
			set;
		}

		private KnotRenderer PlayerKnotRenderer
		{
			get;
			set;
		}

		private PipeMovement PlayerKnotMovement
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

		public virtual void Entered(GameScreen previousScreen, GameTime GameTime)
		{
			throw new System.NotImplementedException();
		}

	}
}

