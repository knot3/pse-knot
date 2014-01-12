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
using Knot3.Screens;
using Knot3.Utilities;

namespace Knot3.Debug
{
	public class DebugBoundings : IGameObject
	{
        #region Properties

		private GameScreen screen;

		public GameObjectInfo Info { get; private set; }

		public World World { get; set; }

		private VertexBuffer vertBuffer;
		private BasicEffect effect;
		private int sphereResolution;

        #endregion

        #region Constructors

		public DebugBoundings (GameScreen screen, GameObjectInfo info)
		{
			this.screen = screen;
			Info = info;

			sphereResolution = 40;
			effect = new BasicEffect (screen.Device);
			effect.LightingEnabled = false;
			effect.VertexColorEnabled = false;
 
			VertexPositionColor[] verts = new VertexPositionColor[(sphereResolution + 1) * 3];
			int index = 0;
			float step = MathHelper.TwoPi / (float)sphereResolution;
			for (float a = 0f; a <= MathHelper.TwoPi; a += step) {
				verts [index++] = new VertexPositionColor (
					position: new Vector3 ((float)Math.Cos (a), (float)Math.Sin (a), 0f),
					color: Color.White
				);
			}
			for (float a = 0f; a <= MathHelper.TwoPi; a += step) {
				verts [index++] = new VertexPositionColor (
					position: new Vector3 ((float)Math.Cos (a), 0f, (float)Math.Sin (a)),
					color: Color.White
				);
			}
			for (float a = 0f; a <= MathHelper.TwoPi; a += step) {
				verts [index++] = new VertexPositionColor (
					position: new Vector3 (0f, (float)Math.Cos (a), (float)Math.Sin (a)),
					color: Color.White
				);
			}
			vertBuffer = new VertexBuffer (screen.Device, typeof(VertexPositionColor), verts.Length, BufferUsage.None);
			vertBuffer.SetData (verts);
		}

        #endregion

        #region Methods

		/// <summary>
		/// Gibt den Ursprung des Knotens zurück.
		/// </summary>
		public Vector3 Center ()
		{
			return Info.Position;
		}

		public void Update (GameTime time)
		{
		}

		public void Draw (GameTime time)
		{
			if (!Info.IsVisible) {
				return;
			}

			foreach (GameModel model in World.OfType<GameModel>()) {
				if (model.Info.IsVisible) {
					screen.Device.SetVertexBuffer (vertBuffer);

					foreach (BoundingSphere sphere in model.Bounds) {
						effect.World = Matrix.CreateScale (sphere.Radius) * Matrix.CreateTranslation (sphere.Center);
						effect.View = World.Camera.ViewMatrix;
						effect.Projection = World.Camera.ProjectionMatrix;
						effect.DiffuseColor = Color.White.ToVector3 ();

						foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
							pass.Apply ();

							screen.Device.DrawPrimitives (PrimitiveType.LineStrip, 0, sphereResolution);
							screen.Device.DrawPrimitives (PrimitiveType.LineStrip, sphereResolution + 1, sphereResolution);
							screen.Device.DrawPrimitives (PrimitiveType.LineStrip, (sphereResolution + 1) * 2, sphereResolution);
						}
					}
				}
			}
		}

		public GameObjectDistance Intersects (Ray ray)
		{
			return null;
		}

		#endregion
	}
}

