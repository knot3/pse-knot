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
	/// Ein Rendereffekt, der 3D-Modelle mit dem von der XNA-Content-Pipeline standardmäßig zugewiesenen
	/// BasicEffect-Shader zeichnet und keinen Post-Processing-Effekt anwendet.
	/// </summary>
	public sealed class StandardEffect : RenderEffect
	{

		#region Constructors

		/// <summary>
		/// Erstellt einen neuen Standardeffekt.
		/// </summary>
		public StandardEffect (GameScreen screen)
		: base(screen)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Zeichnet das Rendertarget.
		/// </summary>
		protected override void DrawRenderTarget (GameTime GameTime)
		{
			spriteBatch.Draw (RenderTarget, Vector2.Zero, Color.White);
		}

		#endregion

	}
}

