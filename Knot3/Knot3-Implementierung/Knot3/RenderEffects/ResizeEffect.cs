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
	/// Ein Rendereffekt, der 3D-Modelle mit dem von der XNA-Content-Pipeline standardmäßig zugewiesenen
	/// BasicEffect-Shader zeichnet und die gesamte 3D-Szene skaliert und an einer bestimmten Position darstellt.
	/// </summary>
	public sealed class ResizeEffect : RenderEffect
	{
		#region Properties

		private Rectangle rectangle
		{
			get {
				PresentationParameters pp = screen.Device.PresentationParameters;
				Point resolution = new Point (pp.BackBufferWidth, pp.BackBufferHeight);
				if (!rectangles.ContainsKey (resolution)) {
					rectangles [resolution] = relativePosition.Scale (screen.Viewport).CreateRectangle (relativeSize.Scale (screen.Viewport));
				}
				return rectangles [resolution];
			}
		}

		private Dictionary<Point, Rectangle> rectangles = new Dictionary<Point, Rectangle> ();
		private Vector2 relativePosition = Vector2.Zero;
		private Vector2 relativeSize = Vector2.One;

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt einen neuen Standardeffekt mit der angegebenen relativen Position und Größe in Prozent,
		/// relativ zur aktuellen Auflösung des Viewports.
		/// </summary>
		public ResizeEffect (IGameScreen screen, Vector2 relativePosition, Vector2 relativeSize)
		: base(screen)
		{
			this.relativePosition = relativePosition;
			this.relativeSize = relativeSize;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Zeichnet das Rendertarget.
		/// </summary>
		protected override void DrawRenderTarget (GameTime GameTime)
		{
			spriteBatch.Draw (RenderTarget, rectangle, Color.White);
		}

		#endregion
	}
}
