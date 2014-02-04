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

		private BoundingSphere[] _bounds;

		public override BoundingSphere[] Bounds
		{
			get { return _bounds; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues Pfeilmodell in dem angegebenen IGameScreen mit einem bestimmten Info-Objekt, das Position und Richtung des Pfeils festlegt.
		/// </summary>
		public ArrowModel (IGameScreen screen, ArrowModelInfo info)
		: base(screen, info)
		{
			_bounds = VectorHelper.CylinderBounds (
			              length: Info.Length,
			              radius: Info.Diameter / 2,
			              direction: Info.Direction.Vector,
			              position: info.Position - info.Direction.Vector * Info.Length / 2
			          );
		}

		#endregion

		#region Methods

		/// <summary>
		/// Zeichnet den Pfeil.
		/// </summary>
		public override void Draw (GameTime time)
		{
			Coloring = new SingleColor (Color.Red);
			if (World.SelectedObject == this) {
				Coloring.Highlight (intensity: 1f, color: Color.Orange);
			}
			else {
				Coloring.Unhighlight ();
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
