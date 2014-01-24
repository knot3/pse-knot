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
	public static class TextureHelper
	{
		#region Real Textures

		public static Texture2D LoadTexture (ContentManager content, string name)
		{
			try {
				return content.Load<Texture2D> (name);
			}
			catch (ContentLoadException ex) {
				Console.WriteLine (ex.ToString ());
				return null;
			}
		}

		#endregion

		#region Dummy Textures

		public static Texture2D Create (GraphicsDevice graphicsDevice, Color color)
		{
			return Create (graphicsDevice, 1, 1, color);
		}

		private static Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D> ();

		public static Texture2D Create (GraphicsDevice graphicsDevice, int width, int height, Color color)
		{
			string key = color.ToString () + width + "x" + height;
			if (textureCache.ContainsKey (key)) {
				return textureCache [key];
			}
			else {
				// create a texture with the specified size
				Texture2D texture = new Texture2D (graphicsDevice, width, height);

				// fill it with the specified colors
				Color[] colors = new Color[width * height];
				for (int i = 0; i < colors.Length; i++) {
					colors [i] = new Color (color.ToVector3 ());
				}
				texture.SetData (colors);
				textureCache [key] = texture;
				return texture;
			}
		}

		public static Texture2D CreateGradient (GraphicsDevice graphicsDevice, Color color1, Color color2)
		{
			string key = color1.ToString () + color2.ToString () + "gradient";
			if (textureCache.ContainsKey (key)) {
				return textureCache [key];
			}
			else {
				// create a texture with the specified size
				Texture2D texture = new Texture2D (graphicsDevice, 2, 2);

				// fill it with the specified colors
				Color[] colors = new Color[texture.Width*texture.Height];
				colors[0] = color1;
				colors[1] = color2;
				colors[2] = color2;
				colors[3] = color1;
				texture.SetData (colors);
				textureCache [key] = texture;
				return texture;
			}
		}

		public static void DrawColoredRectangle (this SpriteBatch spriteBatch, Color color, Rectangle bounds)
		{
			Texture2D texture = TextureHelper.Create (spriteBatch.GraphicsDevice, Color.White);
			spriteBatch.Draw (
			    texture, bounds, null, color, 0f, Vector2.Zero, SpriteEffects.None, 0.1f
			);
		}

		public static void DrawStringInRectangle (this SpriteBatch spriteBatch, SpriteFont font,
		        string text, Color color, Rectangle bounds,
		        HorizontalAlignment alignX, VerticalAlignment alignY)
		{
			Vector2 scaledPosition = new Vector2 (bounds.X, bounds.Y);
			Vector2 scaledSize = new Vector2 (bounds.Width, bounds.Height);
			try {
				// finde die richtige Skalierung
				Vector2 scale = scaledSize / font.MeasureString (text) * 0.9f;
				scale.Y = scale.X = MathHelper.Min (scale.X, scale.Y);

				// finde die richtige Position
				Vector2 textPosition = TextPosition (
				                           font: font, text: text, scale: scale,
				                           position: scaledPosition, size: scaledSize,
				                           alignX: alignX, alignY: alignY
				);

				// zeichne die Schrift
				spriteBatch.DrawString (font, text, textPosition, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0.6f);

			}
			catch (ArgumentException exp) {
				Console.WriteLine (exp.ToString ());
			}
			catch (InvalidOperationException exp) {
				Console.WriteLine (exp.ToString ());
			}
		}

		public static Vector2 TextPosition (SpriteFont font, string text, Vector2 scale, Vector2 position, Vector2 size,
		                                    HorizontalAlignment alignX, VerticalAlignment alignY)
		{
			Vector2 textPosition = position;
			Vector2 minimumSize = font.MeasureString (text);
			switch (alignX) {
			case HorizontalAlignment.Left:
				textPosition.Y += (size.Y - minimumSize.Y * scale.Y) / 2;
				break;
			case HorizontalAlignment.Center:
				textPosition += (size - minimumSize * scale) / 2;
				break;
			case HorizontalAlignment.Right:
				textPosition.Y += (size.Y - minimumSize.Y * scale.Y) / 2;
				textPosition.X += size.X - minimumSize.X * scale.X;
				break;
			}
			return textPosition;
		}

		#endregion
	}
}

