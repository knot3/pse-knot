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
using Knot3.Utilities;

namespace Knot3.GameObjects
{
	/// <summary>
	/// Diese Klasse ArrowModel repräsentiert ein 3D-Modell für einen Pfeil, zum Einblenden an selektierten Kanten (s. Edge).
	/// </summary>
	public sealed class ArrowModel : GameModel
	{
        #region Properties

		/// <summary>
		/// Das Info-Objekt, das die Position und Richtung des ArrowModel\grq s enthält.
		/// </summary>
		public new ArrowModelInfo Info { get { return base.Info as ArrowModelInfo; } set { base.Info = value; } }
		
		private BoundingSphere[] Bounds;

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt ein neues Pfeilmodell in dem angegebenen GameScreen mit einem bestimmten Info-Objekt, das Position und Richtung des Pfeils festlegt.
		/// </summary>
		public ArrowModel (GameScreen screen, ArrowModelInfo info)
			: base(screen, info)
		{
			if (Info.Direction == Direction.Up) {
				Info.Rotation += Angles3.FromDegrees (90, 0, 0);
			} else if (Info.Direction == Direction.Down) {
				Info.Rotation += Angles3.FromDegrees (270, 0, 0);
			}
			if (Info.Direction == Direction.Right) {
				Info.Rotation += Angles3.FromDegrees (0, 90, 0);
			} else if (Info.Direction == Direction.Left) {
				Info.Rotation += Angles3.FromDegrees (0, 270, 0);
			}

			Bounds = VectorHelper.CylinderBounds (
				length: Info.Length,
				radius: Info.Diameter / 2,
				direction: Info.Direction.ToVector3 (),
				position: info.Position - info.Direction.ToVector3 () * Info.Length / 2
			);
		}

        #endregion

        #region Methods

		/// <summary>
		/// Zeichnet den Pfeil.
		/// </summary>
		public override void Draw (GameTime time)
		{
			BaseColor = Color.Red;
			if (World.SelectedObject == this) {
				HighlightIntensity = 1f;
				HighlightColor = Color.Orange;
			} else {
				HighlightIntensity = 0f;
			}

			base.Draw (time);
		}

		/// <summary>
		/// Überprüft, ob der Mausstrahl den Pfeil schneidet.
		/// </summary>
		public override GameObjectDistance Intersects (Ray ray)
		{
			foreach (BoundingSphere sphere in Bounds) {
				float? distance = ray.Intersects (sphere);
				if (distance != null) {
					GameObjectDistance intersection = new GameObjectDistance () {
						Object=this, Distance=distance.Value
					};
					return intersection;
				}
			}
			return null;
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			base.Update (time);
		}

        #endregion
	}
}

