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
	/// Eine abstrakte Klasse, die eine Implementierung von IRenderEffect darstellt.
	/// </summary>
	public abstract class RenderEffect : IRenderEffect
	{
		#region Properties

		/// <summary>
		/// Das Rendertarget, in das zwischen dem Aufruf der Begin()- und der End()-Methode gezeichnet wird,
		/// weil es in Begin() als primäres Rendertarget des XNA-Frameworks gesetzt wird.
		/// </summary>
		public RenderTarget2D RenderTarget { get; private set; }

		/// <summary>
		/// Der Spielzustand, in dem der Effekt verwendet wird.
		/// </summary>
		protected IGameScreen screen { get; set; }

		/// <summary>
		/// Ein Spritestapel (s. Glossar oder http://msdn.microsoft.com/en-us/library/bb203919.aspx), der verwendet wird, um das Rendertarget dieses Rendereffekts auf das übergeordnete Rendertarget zu zeichnen.
		/// </summary>
		protected SpriteBatch spriteBatch { get; set; }

		protected float Supersampling { get { return RenderEffectLibrary.Supersampling; } }

		#endregion

		#region Constructors

		public RenderEffect (IGameScreen screen)
		{
			this.screen = screen;
			spriteBatch = new SpriteBatch (screen.Device);
			screen.Game.FullScreenChanged += () => renderTargets.Clear ();
		}

		#endregion

		#region Methods

		/// <summary>
		/// In der Methode Begin() wird das aktuell von XNA genutzte Rendertarget auf einem Stack gesichert
		/// und das Rendertarget des Effekts wird als aktuelles Rendertarget gesetzt.
		/// </summary>
		public void Begin (GameTime time)
		{
			if (screen.CurrentRenderEffects.CurrentEffect == this) {
				throw new InvalidOperationException ("Begin() can be called only once on " + this + "!");
			}

			RenderTarget = CurrentRenderTarget;
			screen.CurrentRenderEffects.Push (this);
			screen.Device.Clear (Color.Transparent);

			//spriteBatch.Begin (SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			//spriteBatch.Draw (TextureHelper.Create(screen.Device, screen.Viewport.Width, screen.Viewport.Height, background),
			//                  Vector2.Zero, Color.White);
			//spriteBatch.End ();

			// set the stencil screen
			screen.Device.DepthStencilState = DepthStencilState.Default;
			// Setting the other screens isn't really necessary but good form
			screen.Device.BlendState = BlendState.Opaque;
			screen.Device.RasterizerState = RasterizerState.CullCounterClockwise;
			screen.Device.SamplerStates [0] = SamplerState.LinearWrap;
		}

		/// <summary>
		/// Das auf dem Stack gesicherte, vorher genutzte Rendertarget wird wiederhergestellt und
		/// das Rendertarget dieses Rendereffekts wird, unter Umständen in Unterklassen verändert,
		/// auf dieses ubergeordnete Rendertarget gezeichnet.
		/// </summary>
		public virtual void End (GameTime time)
		{
			screen.CurrentRenderEffects.Pop ();

			spriteBatch.Begin (SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			DrawRenderTarget (time);
			spriteBatch.End ();
		}

		/// <summary>
		/// Zeichnet das Spielmodell model mit diesem Rendereffekt.
		/// </summary>
		public virtual void DrawModel (GameModel model, GameTime time)
		{
			// Setze den Viewport auf den der aktuellen Spielwelt
			Viewport original = screen.Viewport;
			screen.Viewport = model.World.Viewport;

			foreach (ModelMesh mesh in model.Model.Meshes) {
				foreach (ModelMeshPart part in mesh.MeshParts) {
					if (part.Effect is BasicEffect) {
						ModifyBasicEffect (part.Effect as BasicEffect, model);
					}
				}
			}

			foreach (ModelMesh mesh in model.Model.Meshes) {
				mesh.Draw ();
			}

			// Setze den Viewport wieder auf den ganzen Screen
			screen.Viewport = original;
		}

		protected void ModifyBasicEffect (BasicEffect effect, GameModel model)
		{
			// lighting
			if (Keys.L.IsHeldDown ()) {
				effect.LightingEnabled = false;
			}
			else {
				effect.EnableDefaultLighting ();  // Beleuchtung aktivieren
			}

			// matrices
			effect.World = model.WorldMatrix * model.World.Camera.WorldMatrix;
			effect.View = model.World.Camera.ViewMatrix;
			effect.Projection = model.World.Camera.ProjectionMatrix;

			// colors
			if (!model.Coloring.IsTransparent) {
				effect.DiffuseColor = model.Coloring.MixedColor.ToVector3 ();
			}

			//effect.TextureEnabled = true;
			//effect.Texture = TextureHelper.CreateGradient (screen.Device, model.BaseColor, Color.White.Mix (Color.Black, 0.2f));

			effect.Alpha = model.Coloring.Alpha;
			effect.FogEnabled = false;
		}

		/// <summary>
		/// Beim Laden des Modells wird von der XNA-Content-Pipeline jedem ModelMeshPart ein Shader der Klasse
		/// BasicEffect zugewiesen. Für die Nutzung des Modells in diesem Rendereffekt kann jedem ModelMeshPart
		/// ein anderer Shader zugewiesen werden.
		/// </summary>
		public virtual void RemapModel (Model model)
		{
		}

		/// <summary>
		/// Zeichnet das Rendertarget.
		/// </summary>
		protected virtual void DrawRenderTarget (GameTime GameTime)
		{
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

		public void DrawLastFrame (GameTime time)
		{
			spriteBatch.Begin (SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			DrawRenderTarget (time);
			spriteBatch.End ();
		}

		#endregion

		#region RenderTarget Cache

		private Dictionary<Point,Dictionary<Rectangle, RenderTarget2D>> renderTargets
		    = new Dictionary<Point,Dictionary<Rectangle, RenderTarget2D>> ();

		public RenderTarget2D CurrentRenderTarget
		{
			get {
				PresentationParameters pp = screen.Device.PresentationParameters;
				Point resolution = new Point (pp.BackBufferWidth, pp.BackBufferHeight);
				Rectangle viewport = new Rectangle (screen.Viewport.X, screen.Viewport.Y,
				                                    screen.Viewport.Width, screen.Viewport.Height);
				if (!renderTargets.ContainsKey (resolution)) {
					renderTargets [resolution] = new Dictionary<Rectangle, RenderTarget2D> ();
				}
				if (!renderTargets [resolution].ContainsKey (viewport)) {
					renderTargets [resolution] [viewport] = new RenderTarget2D (
					    screen.Device, (int)(viewport.Width * Supersampling), (int)(viewport.Height * Supersampling),
					    false, SurfaceFormat.Color, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents
					);
				}
				return renderTargets [resolution] [viewport];
			}
		}

		#endregion
	}
}

