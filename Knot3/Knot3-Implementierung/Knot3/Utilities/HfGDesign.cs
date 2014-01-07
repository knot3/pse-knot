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
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Utilities
{
	public static class HfGDesign
	{
		private static SpriteFont menuFont;

		public static SpriteFont MenuFont (GameScreen screen)
		{
			if (menuFont != null) {
				return menuFont;
			} else {
				// lade die Schriftart der Menüs in das private Attribut
				try {
					menuFont = screen.Content.Load<SpriteFont> ("font-menu");
				} catch (ContentLoadException ex) {
					menuFont = null;
					Console.WriteLine (ex.Message);
				}
				return menuFont;
			}
		}

	}
}

