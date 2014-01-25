using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Utilities
{
	public static class ShaderHelper
	{
		public static Effect LoadEffect (this IGameScreen screen, string name)
		{
			if (MonoHelper.IsRunningOnMono ()) {
				return LoadEffectMono (screen, name);
			}
			else {
				return LoadEffectDotnet (screen, name);
			}
		}

		private static Effect LoadEffectMono (IGameScreen screen, string name)
		{
			return new Effect (screen.Device, System.IO.File.ReadAllBytes ("Content/" + name + ".mgfx"));
		}

		private static Effect LoadEffectDotnet (IGameScreen screen, string name)
		{
			return screen.Content.Load<Effect> (name);
		}
	}
}

