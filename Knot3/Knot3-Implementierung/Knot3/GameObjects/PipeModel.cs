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
	/// Ein 3D-Modell, das eine Kante darstellt.
	/// </summary>
	public sealed class PipeModel : GameModel
	{
		#region Properties

		/// <summary>
		/// Enthält Informationen über die darzustellende Kante.
		/// </summary>
		public new PipeModelInfo Info { get { return base.Info as PipeModelInfo; } set { base.Info = value; } }

		private BoundingSphere[] _bounds;

		public override BoundingSphere[] Bounds
		{
			get { return _bounds; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues 3D-Modell mit dem angegebenen Spielzustand und den angegebenen Spielinformationen.
		/// [base=screen, info]
		/// </summary>
		public PipeModel (GameScreen screen, PipeModelInfo info)
		: base(screen, info)
		{
			float length = (info.PositionTo - info.PositionFrom).Length ();
			float radius = 10;
			_bounds = VectorHelper.CylinderBounds (
			              length: length,
			              radius: radius,
			              direction: Info.Edge.Direction.ToVector3 (),
			              position: info.PositionFrom
			          );
		}

		#endregion

		#region Methods

		public override void Draw (GameTime time)
		{
			BaseColor = Info.Edge.Color;
			if (World.SelectedObject == this) {
				HighlightIntensity = 0.40f;
				HighlightColor = Color.White;
			}
			else if (Info.Knot.SelectedEdges.Contains (Info.Edge)) {
				HighlightIntensity = 0.80f;
				HighlightColor = Color.White;
			}
			else {
				HighlightIntensity = 0f;
			}

			base.Draw (time);
		}

		/// <summary>
		/// Prüft, ob der angegebene Mausstrahl das 3D-Modell schneidet.
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

		#endregion
	}
}

