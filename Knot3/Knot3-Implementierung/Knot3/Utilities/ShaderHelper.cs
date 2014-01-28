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

			if (MonoHelper.IsRunningOnMono () || MonoHelper.IsRunningOnMonogame ()) {
				return LoadEffectMono (screen, name);
			}
			else {
				return LoadEffectDotnet (screen, name);
			}
		}

		private static Effect LoadEffectMono (IGameScreen screen, string name)
		{
			string[] filenames = {
				"Content/Shader/" + name + ".mgfx",
				"Content/Shader/" + name + "_3.0.mgfx",
				"Content/Shader/" + name + "_3.1.mgfx"
			};
			Exception lastException = new Exception ("Could not find shader: " + name);
			foreach (string filename in filenames) {
				try {
					Effect effect = new Effect (screen.Device, System.IO.File.ReadAllBytes (filename));
					return effect;
				}
				catch (Exception ex) {
					lastException = ex;
				}
			}
			throw lastException;
		}

		private static Effect LoadEffectDotnet (IGameScreen screen, string name)
		{
			return screen.Content.Load<Effect> ("Shader/" + name);
		}
	}
}

