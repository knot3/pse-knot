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
	/// <summary>
	/// Ein Cel-Shading-Effekt.
	/// </summary>
	public class CelShadingEffect : RenderEffect
	{
		Effect celShader;       // Toon shader effect
		Texture2D celMap;       // Texture map for cell shading
		Vector4 lightDirection; // Light source for toon shader

		Effect outlineShader;   // Outline shader effect
		float outlineThickness = 1.0f;  // current outline thickness
		float outlineThreshold = 0.2f;  // current edge detection threshold

		#region Constructors

		/// <summary>
		/// Erstellt einen neuen Cel-Shading-Effekt für den angegebenen IGameScreen.
		/// </summary>
		public CelShadingEffect (IGameScreen screen)
		: base(screen)
		{
			/* Set our light direction for the cel-shader
			 */
			lightDirection = new Vector4 (0.0f, 0.0f, 1.0f, 1.0f);

			/* Load and initialize the cel-shader effect
			 */
			celShader = screen.LoadEffect ("CelShader");
			celShader.Parameters ["LightDirection"].SetValue (lightDirection);
			celMap = screen.LoadTexture("CelMap");
			celShader.Parameters ["Color"].SetValue (Color.Green.ToVector4 ());
			celShader.Parameters ["CelMap"].SetValue (celMap);

			/* Load and initialize the outline shader effect
			 */
			outlineShader = screen.LoadEffect ("OutlineShader");
			outlineShader.Parameters ["Thickness"].SetValue (outlineThickness);
			outlineShader.Parameters ["Threshold"].SetValue (outlineThreshold);
			outlineShader.Parameters ["ScreenSize"].SetValue (
			    new Vector2 (screen.Viewport.Bounds.Width, screen.Viewport.Bounds.Height));
		}

		#endregion

		#region Methods

		/// <summary>
		/// Zeichnet das Rendertarget.
		/// </summary>
		protected override void DrawRenderTarget (GameTime GameTime)
		{
			spriteBatch.End ();
			spriteBatch.Begin (SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, outlineShader);

			spriteBatch.Draw (
				RenderTarget,
				new Vector2 (screen.Viewport.X, screen.Viewport.Y),
				null,
				Color.White,
				0f,
				Vector2.Zero,
				Vector2.One / Supersampling,
				SpriteEffects.None,
				1f
			);
		}

		/// <summary>
		/// Zeichnet das Spielmodell model mit dem Cel-Shading-Effekt.
		/// Eine Anwendung des NVIDIA-Toon-Shaders.
		/// </summary>
		public override void DrawModel (GameModel model, GameTime time)
		{
			// Setze den Viewport auf den der aktuellen Spielwelt
			Viewport original = screen.Viewport;
			screen.Viewport = model.World.Viewport;

			Camera camera = model.World.Camera;
			lightDirection = new Vector4 (-Vector3.Cross (Vector3.Normalize (camera.PositionToTargetDirection), camera.UpVector), 1);
			celShader.Parameters ["LightDirection"].SetValue (lightDirection);
			celShader.Parameters ["World"].SetValue (model.WorldMatrix * camera.WorldMatrix);
			celShader.Parameters ["InverseWorld"].SetValue (Matrix.Invert (model.WorldMatrix * camera.WorldMatrix));
			celShader.Parameters ["View"].SetValue (camera.ViewMatrix);
			celShader.Parameters ["Projection"].SetValue (camera.ProjectionMatrix);
			celShader.CurrentTechnique = celShader.Techniques ["ToonShader"];

			if (!model.Coloring.IsTransparent) {
				Color = model.Coloring.MixedColor;
			}

			foreach (ModelMesh mesh in model.Model.Meshes) {
				mesh.Draw ();
			}

			// Setze den Viewport wieder auf den ganzen Screen
			screen.Viewport = original;
		}

		/// <summary>
		/// Weist dem 3D-Modell den Cel-Shader zu.
		/// </summary>
		public override void RemapModel (Model model)
		{
			foreach (ModelMesh mesh in model.Meshes) {
				foreach (ModelMeshPart part in mesh.MeshParts) {
					part.Effect = celShader;
				}
			}
		}

		public Color Color
		{
			get {
				return new Color (celShader.Parameters ["Color"].GetValueVector4 ());
			}
			set {
				celShader.Parameters ["Color"].SetValue (value.ToVector4 ());
			}
		}

		#endregion
	}
}

