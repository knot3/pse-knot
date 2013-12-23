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

namespace Knot3.Widgets
{
	public static class TextureHelper
	{
		#region Real Textures
		
		public static Texture2D LoadTexture (ContentManager content, string name)
		{
			try {
				return content.Load<Texture2D> (name);
			} catch (ContentLoadException ex) {
				Console.WriteLine (ex.ToString ());
				return null;
			}
		}

		#endregion

		#region Dummy Textures

		public static Texture2D CreateColorTexture (GraphicsDevice graphicsDevice)
		{
			return Create (graphicsDevice, 1, 1, new Color ());
		}
 
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
			} else {
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

		#endregion
	}
}

