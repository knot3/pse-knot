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

namespace Knot3.GameObjects
{
	/// <summary>
	/// Die 3D-Modelle, die während einer Verschiebung von Kanten die Vorschaumodelle repräsentieren.
	/// </summary>
	public sealed class ShadowGameModel : ShadowGameObject
	{
		#region Properties

		private GameModel decoratedModel
		{
			get {
				return decoratedObject as GameModel;
			}
		}

		/// <summary>
		/// Die Farbe der Vorschaumodelle.
		/// </summary>
		public Color ShadowColor { get; set; }

		/// <summary>
		/// Die Transparenz der Vorschaumodelle.
		/// </summary>
		public float ShadowAlpha { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues Vorschaumodell in dem angegebenen Spielzustand für das angegebene zu dekorierende Modell.
		/// </summary>
		public ShadowGameModel (GameScreen screen, GameModel decoratedModel)
		: base(screen, decoratedModel)
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Zeichnet das Vorschaumodell.
		/// </summary>
		public override void Draw (GameTime time)
		{
			// swap position, colors, alpha
			Vector3 originalPositon = decoratedModel.Info.Position;
			float originalHighlightIntensity = decoratedModel.HighlightIntensity;
			float originalAlpha = decoratedModel.Alpha;
			decoratedModel.Info.Position = ShadowPosition;
			decoratedModel.HighlightIntensity = 0f;
			decoratedModel.Alpha = ShadowAlpha;

			// draw
			screen.CurrentRenderEffects.CurrentEffect.DrawModel (decoratedModel, time);

			// swap everything back
			decoratedModel.Info.Position = originalPositon;
			decoratedModel.HighlightIntensity = originalHighlightIntensity;
			decoratedModel.Alpha = originalAlpha;
		}

		#endregion

	}
}

