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
	public class TexturedRectangleInfo : GameObjectInfo
	{
		public string Texturename;
		public Texture2D Texture;
		public Vector3 Up;
		public Vector3 Left;
		public float Width;
		public float Height;

		public TexturedRectangleInfo (string texturename, Vector3 origin, Vector3 left, float width, Vector3 up, float height)
		: base(position: origin, isVisible: true, isSelectable: false, isMovable: false)
		{
			Texturename = texturename;
			Left = left;
			Width = width;
			Up = up;
			Height = height;
			Position = origin;
		}

		public TexturedRectangleInfo (Texture2D texture, Vector3 origin, Vector3 left, float width, Vector3 up, float height)
		: base(position: origin, isVisible: true, isSelectable: false, isMovable: false)
		{
			Texture = texture;
			Left = left;
			Width = width;
			Up = up;
			Height = height;
			Position = origin;
		}

		public override bool Equals (GameObjectInfo other)
		{
			if (other == null) {
				return false;
			}

			if (other is GameModelInfo) {
				if (this.Texturename == (other as GameModelInfo).Modelname && base.Equals (other)) {
					return true;
				}
				else {
					return false;
				}
			}
			else {
				return base.Equals (other);
			}
		}
	}
}
