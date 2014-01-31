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
	public static class ColorHelper
	{
		public static Color Mix (this Color a, Color b, float percent = 0.5f)
		{
			percent = MathHelper.Clamp (percent, 0f, 1f);
			return new Color (a.ToVector3 () * (1f - percent) + b.ToVector3 () * percent);
		}

		public static int Luminance (this Color color)
		{
			return (color.R * 3 + color.B + color.G * 4) >> 3;
		}

		public static int SortColorsByLuminance (Color left, Color right)
		{
			return left.Luminance ().CompareTo (right.Luminance ());
		}
	}
}
