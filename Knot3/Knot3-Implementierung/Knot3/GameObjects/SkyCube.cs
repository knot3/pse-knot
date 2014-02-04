using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
	public class SkyCube : IGameObject, IEnumerable<IGameObject>
	{
		#region Properties

		private IGameScreen Screen;

		/// <summary>
		/// Enthält Informationen über die Position des Mitte der Welt.
		/// </summary>
		public GameObjectInfo Info { get; private set; }

		/// <summary>
		/// Die Spielwelt.
		/// </summary>
		public World World
		{
			get { return _world; }
			set {
				_world = value;
				assignWorld ();
			}
		}

		private World _world;

		public float Distance
		{
			get { return _distance; }
			set {
				_distance = value;
				ConstructRectangles ();
			}
		}

		private float _distance;

		/// <summary>
		/// Die einzelnen texturierten Rechtecke.
		/// </summary>
		private List<TexturedRectangle> rectangles = new List<TexturedRectangle> ();
		private Random random;

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues KnotRenderer-Objekt für den angegebenen Spielzustand mit den angegebenen
		/// Spielobjekt-Informationen, die unter Anderem die Position des Knotenursprungs enthalten.
		/// </summary>
		public SkyCube (IGameScreen screen, Vector3 position)
		{
			Screen = screen;
			Info = new GameObjectInfo (position: position);
			random = new Random ();
		}

		public SkyCube (IGameScreen screen, Vector3 position, float distance)
		: this(screen, position)
		{
			Distance = distance;
			ConstructRectangles ();
		}

		#endregion

		#region Methods

		private void ConstructRectangles ()
		{
			rectangles.Clear ();
			foreach (Vector3 direction in Direction.Values) {
				Vector3 position = direction * Distance;
				Vector3 up = direction == Direction.Up || direction == Direction.Down ? Vector3.Forward : Vector3.Up;
				Vector3 left = Vector3.Normalize (Vector3.Cross (position, up));
				TexturedRectangleInfo info = new TexturedRectangleInfo (
				    // texturename: "sky1",
				    texture: CreateSkyTexture (),
				    origin: position,
				    left: left,
				    width: 2 * Distance,
				    up: up,
				    height: 2 * Distance
				);

				rectangles.Add (new TexturedRectangle (Screen, info));
			}
			assignWorld ();
		}

		private Texture2D CreateSkyTexture ()
		{
			string effectName = Options.Default ["video", "knot-shader", "default"];
			if (effectName == "celshader") {
				return TextureHelper.Create (Screen.Device, Color.CornflowerBlue);
			}
			else {
				return CreateSpaceTexture ();
			}
		}

		private Texture2D CreateSpaceTexture ()
		{
			int width = 2000;
			int height = 2000;
			Texture2D texture = new Texture2D (Screen.Device, width, height);
			Color[] colors = new Color[width * height];
			for (int i = 0; i < colors.Length; i++) {
				colors [i] = Color.Black;
			}
			for (int h = 2; h+2 < height; ++h) {
				for (int w = 2; w+2 < width; ++w) {
					int i = h * width + w;
					if (random.Next () % (width * 3) == w) {
						float alpha = (1 + random.Next () % 3) / 3f;
						Color white = Color.White * alpha;
						Color gray = Color.Gray * alpha;
						colors [i] = white;
						colors [i - 1] = white;
						colors [i + 1] = white;
						colors [i - width] = white;
						colors [i - width - 1] = gray;
						colors [i - width + 1] = gray;
						colors [i + width] = white;
						colors [i + width - 1] = gray;
						colors [i + width + 1] = gray;
					}
				}
			}
			texture.SetData (colors);
			return texture;
		}

		private void assignWorld ()
		{
			foreach (TexturedRectangle rect in rectangles) {
				rect.World = World;
			}
		}

		/// <summary>
		/// Gibt den Ursprung des Knotens zurück.
		/// </summary>
		public Vector3 Center ()
		{
			return Info.Position;
		}

		public void Update (GameTime time)
		{
			if (World.Camera.MaxPositionDistance + 500 != Distance) {
				Distance = World.Camera.MaxPositionDistance + 500;
			}
			foreach (TexturedRectangle rect in rectangles) {
				rect.Update (time);
			}
		}

		public void Draw (GameTime time)
		{
			foreach (TexturedRectangle rect in rectangles) {
				rect.Draw (time);
			}
		}

		public GameObjectDistance Intersects (Ray ray)
		{
			return null;
		}

		/// <summary>
		/// Gibt einen Enumerator der aktuell vorhandenen 3D-Modelle zurück.
		/// [returntype=IEnumerator<IGameObject>]
		/// </summary>
		public IEnumerator<IGameObject> GetEnumerator ()
		{
			foreach (TexturedRectangle rect in rectangles) {
				yield return rect;
			}
		}

		// Explicit interface implementation for nongeneric interface
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator (); // Just return the generic version
		}

		#endregion
	}
}
