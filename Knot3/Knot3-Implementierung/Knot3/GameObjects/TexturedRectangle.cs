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
	/// <summary>
	/// Eine frei in der Spielwelt liegende Textur, die auf ein Rechteck gezeichnet wird.
	/// </summary>
	public sealed class TexturedRectangle : IGameObject, IDisposable, IEquatable<TexturedRectangle>
	{
		#region Attributes and Properties

		private IGameScreen Screen;

		GameObjectInfo IGameObject.Info { get { return Info; } }

		public TexturedRectangleInfo Info { get; private set; }

		public World World { get; set; }

		private Vector3 UpperLeft;
		private Vector3 LowerLeft;
		private Vector3 UpperRight;
		private Vector3 LowerRight;
		private Vector3 Normal;
		private VertexPositionNormalTexture[] Vertices;
		private short[] Indexes;
		private BasicEffect basicEffect;
		private Texture2D texture;

		#endregion

		#region Constructors

		public TexturedRectangle (IGameScreen screen, TexturedRectangleInfo info)
		{
			Screen = screen;
			Info = info;
			SetPosition (Info.Position);

			basicEffect = new BasicEffect (screen.Device);
			if (info.Texture != null) {
				texture = info.Texture;
			}
			else {
				texture = screen.LoadTexture (info.Texturename);
			}
			if (texture != null) {
				FillVertices ();
			}
		}

		#endregion

		#region Update

		public void Update (GameTime time)
		{
		}

		#endregion

		#region Draw

		public void Draw (GameTime time)
		{
			if (Info.IsVisible) {
				// Setze den Viewport auf den der aktuellen Spielwelt
				Viewport original = Screen.Viewport;
				Screen.Viewport = World.Viewport;

				//Console.WriteLine ("basicEffect=" + World);
				basicEffect.World = World.Camera.WorldMatrix;
				basicEffect.View = World.Camera.ViewMatrix;
				basicEffect.Projection = World.Camera.ProjectionMatrix;

				basicEffect.AmbientLightColor = new Vector3 (0.8f, 0.8f, 0.8f);
				//effect.LightingEnabled = true;
				basicEffect.TextureEnabled = true;
				basicEffect.VertexColorEnabled = false;
				basicEffect.Texture = texture;

				basicEffect.LightingEnabled = false;
				string effectName = Options.Default ["video", "knot-shader", "default"];
				if (Keys.Tab.IsHeldDown () || effectName == "celshader") {
					basicEffect.EnableDefaultLighting ();  // Beleuchtung aktivieren
				}

				foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes) {
					pass.Apply ();

					Screen.Device.DrawUserIndexedPrimitives<VertexPositionNormalTexture> (
					    PrimitiveType.TriangleList, Vertices, 0, Vertices.Length, Indexes, 0, Indexes.Length / 3
					);
				}

				// Setze den Viewport wieder auf den ganzen Screen
				Screen.Viewport = original;
			}
		}

		#endregion

		private void FillVertices ()
		{
			// Fill in texture coordinates to display full texture
			// on quad
			Vector2 textureUpperLeft = new Vector2 (0.0f, 0.0f);
			Vector2 textureUpperRight = new Vector2 (1.0f, 0.0f);
			Vector2 textureLowerLeft = new Vector2 (0.0f, 1.0f);
			Vector2 textureLowerRight = new Vector2 (1.0f, 1.0f);

			Vertices = new VertexPositionNormalTexture[4];
			// Provide a normal for each vertex
			for (int i = 0; i < Vertices.Length; i++) {
				Vertices [i].Normal = Normal;
			}
			// Set the position and texture coordinate for each
			// vertex
			Vertices [0].Position = LowerLeft;
			Vertices [0].TextureCoordinate = textureLowerLeft;
			Vertices [1].Position = UpperLeft;
			Vertices [1].TextureCoordinate = textureUpperLeft;
			Vertices [2].Position = LowerRight;
			Vertices [2].TextureCoordinate = textureLowerRight;
			Vertices [3].Position = UpperRight;
			Vertices [3].TextureCoordinate = textureUpperRight;


			// Set the index buffer for each vertex, using
			// clockwise winding
			Indexes = new short[12];
			Indexes [0] = 0;
			Indexes [1] = 1;
			Indexes [2] = 2;
			Indexes [3] = 2;
			Indexes [4] = 1;
			Indexes [5] = 3;

			Indexes [6] = 2;
			Indexes [7] = 1;
			Indexes [8] = 0;
			Indexes [9] = 3;
			Indexes [10] = 1;
			Indexes [11] = 2;
		}

		private void SetPosition (Vector3 position)
		{
			Info.Position = position;
			// Calculate the quad corners
			Normal = Vector3.Cross (Info.Left, Info.Up);
			Vector3 uppercenter = (Info.Up * Info.Height / 2) + position;
			UpperLeft = uppercenter + (Info.Left * Info.Width / 2);
			UpperRight = uppercenter - (Info.Left * Info.Width / 2);
			LowerLeft = UpperLeft - (Info.Up * Info.Height);
			LowerRight = UpperRight - (Info.Up * Info.Height);
			FillVertices ();
		}

		private Vector3 Length ()
		{
			return Info.Left * Info.Width + Info.Up * Info.Height;
		}

		public BoundingBox[] Bounds ()
		{
			return new BoundingBox[] {
				LowerLeft.Bounds (UpperRight - LowerLeft), LowerRight.Bounds (UpperLeft - LowerRight),
				UpperRight.Bounds (LowerLeft - UpperRight), UpperLeft.Bounds (LowerRight - UpperLeft)
			};
		}

		public GameObjectDistance Intersects (Ray ray)
		{
			foreach (BoundingBox bounds in Bounds()) {
				Nullable<float> distance = ray.Intersects (bounds);
				if (distance != null) {
					GameObjectDistance intersection = new GameObjectDistance () {
						Object=this, Distance=distance.Value
					};
					return intersection;
				}
			}
			return null;
		}

		public Vector3 Center ()
		{
			return LowerLeft + (UpperRight - LowerLeft) / 2;
		}

		public override string ToString ()
		{
			return "TexturedRectangle(" + UpperLeft + "," + UpperRight + "," + LowerRight + "," + LowerLeft + ")";
		}

		public void Dispose ()
		{
			if (texture != null) {
				texture.Dispose ();
				texture = null;
			}
		}

		public static bool operator == (TexturedRectangle a, TexturedRectangle b)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals (a, b)) {
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)a == null) || ((object)b == null)) {
				return false;
			}

			// Return true if the fields match:
			return a.Info.Position == b.Info.Position;
		}

		public static bool operator != (TexturedRectangle a, TexturedRectangle b)
		{
			return !(a == b);
		}

		public bool Equals (TexturedRectangle other)
		{
			return this.Info.Position == other.Info.Position;
		}

		public override bool Equals (object obj)
		{
			if (obj is Node) {
				return Equals ((Node)obj);
			}
			else {
				return false;
			}
		}

		public override int GetHashCode ()
		{
			return Info.Position.GetHashCode ();
		}
	}
}

