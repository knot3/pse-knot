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
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;

namespace Knot3.GameObjects
{
	public abstract class ModelColoring
	{
		public ModelColoring ()
		{
			HighlightColor = Color.Transparent;
			HighlightIntensity = 0f;
			Alpha = 1f;
		}

		/// <summary>
		/// Die Auswahlfarbe des Modells.
		/// </summary>
		public Color HighlightColor { get; private set; }

		/// <summary>
		/// Die Intensität der Auswahlfarbe.
		/// </summary>
		public float HighlightIntensity { get; private set; }

		public void Highlight (float intensity, Color color)
		{
			HighlightColor = color;
			HighlightIntensity = intensity;
		}

		public void Unhighlight ()
		{
			HighlightColor = Color.Transparent;
			HighlightIntensity = 0f;
		}

		/// <summary>
		/// Die Transparenz des Modells.
		/// </summary>
		public float Alpha { get; set; }

		public abstract Color MixedColor { get; }

		public abstract bool IsTransparent { get; }
	}

	public sealed class SingleColor : ModelColoring
	{
		public SingleColor (Color color)
		: base()
		{
			BaseColor = color;
		}

		public SingleColor (Color color, float alpha)
		: this(color)
		{
			Alpha = alpha;
		}

		/// <summary>
		/// Die Farbe des Modells.
		/// </summary>
		public Color BaseColor { get; set; }

		public override Color MixedColor { get { return BaseColor; } }

		public override bool IsTransparent { get { return BaseColor == Color.Transparent; } }
	}

	public sealed class GradientColor : ModelColoring
	{
		public GradientColor (Color color1, Color color2)
		: base()
		{
			Color1 = color1;
			Color2 = color2;
		}
		public GradientColor (Color color1, Color color2, float alpha)
		: this(color1, color2)
		{
			Alpha = alpha;
		}

		/// <summary>
		/// Die erste Farbe des Modells.
		/// </summary>
		public Color Color1 { get; set; }

		/// <summary>
		/// Die zweite Farbe des Modells.
		/// </summary>
		public Color Color2 { get; set; }

		public override Color MixedColor
		{
			get {
				return Color1.Mix (Color2, 0.5f);
			}
		}

		public override bool IsTransparent
		{
			get {
				return Color1 == Color.Transparent && Color2 == Color.Transparent;
			}
		}
	}
}

