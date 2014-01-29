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

namespace Knot3.RenderEffects
{
	/// <summary>
	/// Ein Postprocessing-Effekt, der eine Überblendung zwischen zwei Spielzuständen darstellt.
	/// </summary>
	public class FadeEffect : RenderEffect
	{
		#region Properties

		private float alpha;

		/// <summary>
		/// Gibt an, ob die Überblendung abgeschlossen ist und das RenderTarget nur noch den neuen Spielzustand darstellt.
		/// </summary>
		public Boolean IsFinished { get { return alpha <= 0; } }

		/// <summary>
		/// Der zuletzt gerenderte Frame im bisherigen Spielzustand.
		/// </summary>
		private RenderTarget2D PreviousRenderTarget { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt einen Überblende-Effekt zwischen den angegebenen Spielzuständen.
		/// </summary>
		public FadeEffect (IGameScreen newScreen, IGameScreen oldScreen)
		: base(newScreen)
		{
			if (oldScreen != null) {
				PreviousRenderTarget = oldScreen.PostProcessingEffect.RenderTarget;
				alpha = 1.0f;
			}
			else {
				alpha = 0.0f;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Zeichnet das Rendertarget.
		/// </summary>
		protected override void DrawRenderTarget (GameTime GameTime)
		{
			if (PreviousRenderTarget != null) {
				alpha -= 0.05f;
				
				spriteBatch.Draw (
					PreviousRenderTarget,
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
			if (alpha <= 0) {
				PreviousRenderTarget = null;
				alpha = 0.0f;
			}
			
			spriteBatch.Draw (
				RenderTarget,
				new Vector2 (screen.Viewport.X, screen.Viewport.Y),
				null,
				Color.White * (1 - alpha),
				0f,
				Vector2.Zero,
				Vector2.One / Supersampling,
				SpriteEffects.None,
				1f
			);
		}

		#endregion
	}
}

