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
	public class Overlay : DrawableGameScreenComponent
	{
		// graphics-related classes
		private SpriteBatch spriteBatch;
		private BasicEffect effect;

		private World World { get; set; }

		// fonts
		private SpriteFont font;

		public Overlay (GameScreen screen, World world)
			: base(screen, DisplayLayer.Overlay)
		{
			// game world
			World = world;

			// create a new SpriteBatch, which can be used to draw textures
			effect = new BasicEffect (screen.Device);
			spriteBatch = new SpriteBatch (screen.Device);
			effect.VertexColorEnabled = true;
			effect.World = Matrix.CreateFromYawPitchRoll (0, 0, 0);
		}

		protected override void LoadContent ()
		{
			// load fonts
			try {
				font = Screen.Content.Load<SpriteFont> ("Font");
			} catch (ContentLoadException ex) {
				font = null;
				Console.WriteLine (ex.Message);
			}
		}

		public override void Draw (GameTime time)
		{
			if (Options.Default ["video", "debug-coordinates", false])
				DrawCoordinates (time);
			if (Options.Default ["video", "camera-overlay", true])
				DrawOverlay (time);
			if (Options.Default ["video", "fps-overlay", true])
				DrawFPS (time);
			DrawProfiler (time);
		}
		
		public override void Update (GameTime time)
		{
			UpdateFPS (time);
		}

		private void DrawCoordinates (GameTime time)
		{
			int length = 2000;
			var vertices = new VertexPositionColor[6];
			vertices [0].Position = new Vector3 (-length, 0, 0);
			vertices [0].Color = Color.Green;
			vertices [1].Position = new Vector3 (+length, 0, 0);
			vertices [1].Color = Color.Green;
			vertices [2].Position = new Vector3 (0, -length, 0);
			vertices [2].Color = Color.Red;
			vertices [3].Position = new Vector3 (0, +length, 0);
			vertices [3].Color = Color.Red;
			vertices [4].Position = new Vector3 (0, 0, -length);
			vertices [4].Color = Color.Yellow;
			vertices [5].Position = new Vector3 (0, 0, +length);
			vertices [5].Color = Color.Yellow;
            
			effect.View = World.Camera.ViewMatrix;
			effect.Projection = World.Camera.ProjectionMatrix;
          
			effect.CurrentTechnique.Passes [0].Apply ();
            
			Screen.Device.DrawUserPrimitives (PrimitiveType.LineList, vertices, 0, 3, VertexPositionColor.VertexDeclaration);
		}

		private void DrawOverlay (GameTime time)
		{
			spriteBatch.Begin ();

			int height = 20;
			int width1 = 20, width2 = 150, width3 = 210, width4 = 270;
			DrawString ("Rotation: ", width1, height, Color.White);
			float x, y, z;
			World.Camera.Rotation.ToDegrees (out x, out y, out z);
			DrawString (x, width2, height, Color.Green);
			DrawString (y, width3, height, Color.Red);
			DrawString (z, width4, height, Color.Yellow);
			height += 20;
			DrawString ("Camera Position: ", width1, height, Color.White);
			DrawVectorCoordinates (World.Camera.Position, width2, width3, width4, height);
			height += 20;
			DrawString ("Camera Target: ", width1, height, Color.White);
			DrawVectorCoordinates (World.Camera.Target, width2, width3, width4, height);
			height += 20;
			DrawString ("Distance: ", width1, height, Color.White);
			DrawString (World.Camera.TargetDistance, width2, height, Color.White);
			height += 20;
			DrawString ("Selected Object: ", width1, height, Color.White);
			if (World.SelectedObject != null) {
				Vector3 selectedObjectCenter = World.SelectedObject.Center ();
				DrawVectorCoordinates (selectedObjectCenter, width2, width3, width4, height);
			}
			height += 20;
			DrawString ("Distance: ", width1, height, Color.White);
			DrawString (World.SelectedObjectDistance, width2, height, Color.White);
			height += 20;
			DrawString ("FoV: ", width1, height, Color.White);
			DrawString (World.Camera.FoV, width2, height, Color.White);
			height += 20;

			spriteBatch.End ();
		}

		private void DrawVectorCoordinates (Vector3 vector, int width2, int width3, int width4, int height)
		{
			DrawString ((int)vector.X, width2, height, Color.Green);
			DrawString ((int)vector.Y, width3, height, Color.Red);
			DrawString ((int)vector.Z, width4, height, Color.Yellow);
		}

		private void DrawString (string str, int width, int height, Color color)
		{
			if (font != null) {
				try {
					spriteBatch.DrawString (font, str, new Vector2 (width, height), color);

				} catch (ArgumentException exp) {
					Console.WriteLine (exp.ToString ());
				} catch (InvalidOperationException exp) {
					Console.WriteLine (exp.ToString ());
				}
			}
		}

		private void DrawString (float n, int width, int height, Color color)
		{
			DrawString ("" + n, width, height, color);
		}
		
		int _total_frames = 0;
		float _elapsed_time = 0.0f;
		int _fps = 0;

		private void UpdateFPS (GameTime time)
		{
			_elapsed_time += (float)time.ElapsedGameTime.TotalMilliseconds;

			if (_elapsed_time >= 1000.0f) {
				_fps = _total_frames;
				_total_frames = 0;
				_elapsed_time = 0;
			}
		}

		private void DrawFPS (GameTime time)
		{
			_total_frames++;
			spriteBatch.Begin ();
			DrawString ("FPS: " + _fps, Screen.Viewport.Width - 200, 20, Color.White);
			spriteBatch.End ();
		}

		private void DrawProfiler (GameTime time)
		{
			spriteBatch.Begin ();
			int height = 40;
			foreach (string name in Profiler.ProfilerMap.Keys) {
				DrawString (name + ": " + Profiler.ProfilerMap [name], Screen.Viewport.Width - 200, height, Color.White);
				height += 20;
			}
			spriteBatch.End ();
		}
	}
}

