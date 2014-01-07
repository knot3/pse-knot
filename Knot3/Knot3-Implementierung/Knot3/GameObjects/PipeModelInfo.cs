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
	/// Enthält Informationen über ein 3D-Modell, das eine Kante darstellt.
	/// </summary>
	public sealed class PipeModelInfo : GameModelInfo
	{
        #region Properties

		/// <summary>
		/// Die Kante, die durch das 3D-Modell dargestellt wird.
		/// </summary>
		public Edge Edge { get; set; }

		/// <summary>
		/// Der Knoten, der die Kante enthält.
		/// </summary>
		public Knot Knot { get; set; }

		/// <summary>
		/// Die Position, an der die Kante beginnt.
		/// </summary>
		public Vector3 PositionFrom { get; set; }

		/// <summary>
		/// Die Position, an der die Kante endet.
		/// </summary>
		public Vector3 PositionTo { get; set; }

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt ein neues Informationsobjekt für ein 3D-Modell, das eine Kante darstellt.
		/// [base="pipe1", Angles3.Zero, new Vector3(10,10,10)]
		/// </summary>
		public PipeModelInfo (NodeMap nodeMap, Knot knot, Edge edge)
            : base("pipe-straight", Angles3.Zero, Vector3.One * 10f)
		{
			// Weise Knoten und Kante zu
			Knot = knot;
			Edge = edge;

			// Berechne die beiden Positionen, zwischen denen die Kante gezeichnet wird
			Node node1 = nodeMap.From (edge);
			Node node2 = nodeMap.To (edge);
			PositionFrom = node1.ToVector ();
			PositionTo = node2.ToVector ();
			Position = node1.CenterBetween (node2);

			// Kanten sind verschiebbar
			IsMovable = true;

			// Berechne die Drehung
			switch (Edge.Direction) {
			case Direction.Up:
				Rotation += Angles3.FromDegrees (90, 0, 0);
				break;
			case Direction.Down:
				Rotation += Angles3.FromDegrees (270, 0, 0);
				break;
			case Direction.Right:
				Rotation += Angles3.FromDegrees (0, 90, 0);
				break;
			case Direction.Left:
				Rotation += Angles3.FromDegrees (0, 270, 0);
				break;
			}
		}

		public override bool Equals (GameObjectInfo other)
		{
			if (other == null) 
				return false;

			if (other is PipeModelInfo) {
				if (this.Edge == (other as PipeModelInfo).Edge && base.Equals (other))
					return true;
				else
					return false;
			} else {
				return base.Equals (other);
			}
		}

        #endregion
	}
}

