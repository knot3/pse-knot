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

	public class SettingsScreen : MenuScreen
	{
		protected virtual object navigation
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

