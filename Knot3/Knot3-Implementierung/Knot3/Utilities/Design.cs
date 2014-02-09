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
	public static class Design
	{
		private static string menuFontName;

		public static string MenuFontName
		{
			get {
				return menuFontName;
			}
			set {
				menuFontName = value;
				menuFont = null;
			}
		}

		private static SpriteFont menuFont;

		public static SpriteFont MenuFont (IGameScreen screen)
		{
			if (menuFont != null) {
				return menuFont;
			}
			else {
				// lade die Schriftart der Menüs in das private Attribut
				menuFont = screen.LoadFont ("font-menu");
				return menuFont;
			}
		}

		// die Standardfarben der Linien
		public static Color DefaultLineColor;
		public static Color DefaultOutlineColor;
		public static Color InGameBackground;
		public static Color WidgetBackground;
		public static Color WidgetForeground;
		public static Color ScreenBackground;
		public static Func<State, Color> WidgetBackgroundColorFunc;
		public static Func<State, Color> WidgetForegroundColorFunc;
		public static Func<State, Color> MenuItemBackgroundColorFunc;
		public static Func<State, Color> MenuItemForegroundColorFunc;
	}
}
