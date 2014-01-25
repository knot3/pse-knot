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
	class Pascal : RenderEffect
	{
		public Pascal(IGameScreen screen)
		: base(screen)
		{
			pascalEffect = screen.LoadEffect("Pascal");
		}


		protected override void DrawRenderTarget(GameTime GameTime)
		{
			spriteBatch.Draw(RenderTarget, Vector2.Zero, Color.White);
		}


		public override void RemapModel(Model model)
		{
			foreach (ModelMesh mesh in model.Meshes) {
				foreach (ModelMeshPart part in mesh.MeshParts) {
					part.Effect = pascalEffect;
				}
			}
		}

		public Color Color
		{
			get {
				return Color.Red;
			}
			set {

			}
		}



		public override void DrawModel(GameModel model, GameTime time)
		{
			Camera camera = model.World.Camera;


			//lightDirection = new Vector4(-Vector3.Cross(Vector3.Normalize(camera.TargetDirection), camera.UpVector), 1);
			pascalEffect.Parameters["World"].SetValue(model.WorldMatrix * camera.WorldMatrix);
			pascalEffect.Parameters["View"].SetValue(camera.ViewMatrix);
			pascalEffect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);

			pascalEffect.Parameters["color1"].SetValue(Color.Yellow.ToVector4());
			pascalEffect.Parameters["color2"].SetValue(Color.Red.ToVector4());

			pascalEffect.CurrentTechnique = pascalEffect.Techniques["Technique1"];

			foreach (ModelMesh mesh in model.Model.Meshes) {
				mesh.Draw();
			}
		}


		Effect pascalEffect;
		//Vector4 lightDirection; // Light source for toon shader
	}
}
