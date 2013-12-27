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
using Knot3.Utilities;

namespace Knot3.Widgets
{
	public sealed class Lines : DrawableGameScreenComponent
	{
		private Texture2D texture;
		private SpriteBatch spriteBatch;

		// die Punkte, zwischen denen die Linien gezeichnet werden sollen
		private List<Vector2> points;

		// die Dicke der Linien
		private int lineWidth;
		public static Color LineColor = new Color (0xb4, 0xff, 0x00);
		public static Color OutlineColor = new Color (0x3b, 0x54, 0x00);

		public Lines (GameScreen screen, DisplayLayer drawOrder, int lineWidth)
			: base(screen, drawOrder)
		{
			this.lineWidth = lineWidth;
			points = new List<Vector2> ();
			spriteBatch = new SpriteBatch (screen.Device);
			texture = TextureHelper.Create (Screen.Device, Color.White);
		}

		public override void Draw (GameTime time)
		{
			int scaledLineWidth = (int)new Vector2 (lineWidth, lineWidth).Scale (Screen.Viewport).X;

			if (points.Count >= 2) {
				Rectangle[] rects = new Rectangle[points.Count - 1];
				for (int i = 1; i < points.Count; ++i) {
					Vector2 nodeA = points [i - 1];
					Vector2 nodeB = points [i];
					if (nodeA.X == nodeB.X || nodeA.Y == nodeB.Y) {
						Vector2 direction = (nodeB - nodeA).PrimaryDirection ();
						Vector2 position = nodeA.Scale (Screen.Viewport);
						int length = (int)(nodeB - nodeA).Scale (Screen.Viewport).Length ();
						if (direction.X == 0 && direction.Y > 0) {
							rects [i - 1] = VectorHelper.CreateRectangle (scaledLineWidth, position.X, position.Y, 0, length);
						} else if (direction.X == 0 && direction.Y < 0) {
							rects [i - 1] = VectorHelper.CreateRectangle (scaledLineWidth, position.X, position.Y - length, 0, length);
						} else if (direction.Y == 0 && direction.X > 0) {
							rects [i - 1] = VectorHelper.CreateRectangle (scaledLineWidth, position.X, position.Y, length, 0);
						} else if (direction.Y == 0 && direction.X < 0) {
							rects [i - 1] = VectorHelper.CreateRectangle (scaledLineWidth, position.X - length, position.Y, length, 0);
						}
					}
				}

				spriteBatch.Begin ();
				foreach (Rectangle inner in rects) {
					Rectangle outer = new Rectangle (inner.X - 1, inner.Y - 1, inner.Width + 2, inner.Height + 2);
					spriteBatch.Draw (texture, outer, OutlineColor);
				}
				foreach (Rectangle rect in rects) {
					spriteBatch.Draw (texture, rect, LineColor);
				}
				spriteBatch.End ();
			}
		}

		public void AddPoints (float startX, float startY, params float[] xyxy)
		{
			Vector2 start = new Vector2 (startX, startY);
			if (start.X > 1 || start.Y > 1)
				start /= 1000f;
			points.Add (start);

			Vector2 current = start;
			for (int i = 0; i < xyxy.Count(); ++i) {
				// this is a new X value
				if (i % 2 == 0) 
					current.X = xyxy [i] > 1 ? xyxy [i] / 1000f : xyxy [i];
				// this is a new Y value
				else
					current.Y = xyxy [i] > 1 ? xyxy [i] / 1000f : xyxy [i];

				points.Add (current);
			}
		}
	}
}

