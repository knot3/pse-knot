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
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;

namespace Knot3.RenderEffects
{
	class Z_Nebula : RenderEffect
	{
		public Z_Nebula(IGameScreen screen)
		: base(screen)
		{
			zNebulaEffect = screen.LoadEffect("Z_Nebula");
		}

		protected override void DrawRenderTarget(GameTime GameTime)
		{
			spriteBatch.Draw(RenderTarget, Vector2.Zero, Color.White);
		}

		public override void RemapModel(Model model)
		{
			foreach (ModelMesh mesh in model.Meshes) {
				foreach (ModelMeshPart part in mesh.MeshParts) {
					part.Effect = zNebulaEffect;
				}
			}
		}

		public Color Color
		{
			get {
                return Color.Red; new Color();
			}
			set {

			}
		}

		public override void DrawModel(GameModel model, GameTime time)
		{
			// Setze den Viewport auf den der aktuellen Spielwelt
			Viewport original = screen.Viewport;
			screen.Viewport = model.World.Viewport;

			Camera camera = model.World.Camera;

            zNebulaEffect.Parameters["World"].SetValue(model.WorldMatrix * camera.WorldMatrix);
            zNebulaEffect.Parameters["View"].SetValue(camera.ViewMatrix);
            zNebulaEffect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);


            zNebulaEffect.CurrentTechnique = zNebulaEffect.Techniques["Simplest"];

			foreach (ModelMesh mesh in model.Model.Meshes) {
				mesh.Draw();
			}

			// Setze den Viewport wieder auf den ganzen Screen
			screen.Viewport = original;
		}

		Effect zNebulaEffect;
		//Vector4 lightDirection; // Light source for toon shader
	}
}
