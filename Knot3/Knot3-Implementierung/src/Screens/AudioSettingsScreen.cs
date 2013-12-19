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

	public class AudioSettingsScreen : SettingsScreen
	{
		protected virtual object settingsMenu
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

		public AudioSettingsScreen(Knot3Game game)
		{
		}

	}
}

