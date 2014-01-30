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

		private Dictionary<Direction, Angles3> RotationMap = new Dictionary<Direction, Angles3> ()
		{
			{ Direction.Up, 		Angles3.FromDegrees (90, 0, 0) },
			{ Direction.Down, 		Angles3.FromDegrees (270, 0, 0) },
			{ Direction.Right, 		Angles3.FromDegrees (0, 90, 0) },
			{ Direction.Left, 		Angles3.FromDegrees (0, 270, 0) },
			{ Direction.Forward, 	Angles3.FromDegrees (0, 0, 0) },
			{ Direction.Backward, 	Angles3.FromDegrees (0, 0, 0) },
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues Informationsobjekt für ein 3D-Modell, das eine Kante darstellt.
		/// [base="pipe1", Angles3.Zero, new Vector3(10,10,10)]
		/// </summary>
		public PipeModelInfo (NodeMap nodeMap, Knot knot, Edge edge)
		: base("pipe-straight", Angles3.Zero, new Vector3(25f, 25f, 25f))
		{
			// Weise Knoten und Kante zu
			Knot = knot;
			Edge = edge;

			// Berechne die beiden Positionen, zwischen denen die Kante gezeichnet wird
			Node node1 = nodeMap.NodeBeforeEdge (edge);
			Node node2 = nodeMap.NodeAfterEdge (edge);
			PositionFrom = node1;
			PositionTo = node2;
			Position = node1.CenterBetween (node2);

			// Kanten sind verschiebbar und auswählbar
			IsMovable = true;
			IsSelectable = true;

			// Berechne die Drehung
			Rotation += RotationMap [Edge.Direction];

			// Berechne die Skalierung bei abgeschnittenen Übergängen
			List<IJunction> junctions1 = nodeMap.JunctionsBeforeEdge (edge);
			List<IJunction> junctions2 = nodeMap.JunctionsAfterEdge (edge);
			IJunction myJunction1 = junctions1.Where (j => j.EdgeTo == edge).ElementAtOrDefault (0) ?? junctions1 [0];
			IJunction myJunction2 = junctions2.Where (j => j.EdgeFrom == edge).ElementAtOrDefault (0) ?? junctions2 [0];
			/*
			if (myJunction1.EdgeFrom.Direction != myJunction1.EdgeTo.Direction) {
				Scale += new Vector3 (0, 0, 8f);
				Position -= edge.Direction * 8f;
			}
			if (myJunction2.EdgeFrom.Direction != myJunction2.EdgeTo.Direction) {
				Scale += new Vector3 (0, 0, 8f);
				Position += edge.Direction * 8f;
			}
			*/

			// Berechne die Skalierung bei überlangen Kanten
			if (junctions1.Count == 1 && junctions1 [0].EdgeFrom.Direction == junctions1 [0].EdgeTo.Direction) {
				Scale += new Vector3 (0, 0, 12.5f);
				Position -= edge.Direction * 12.5f;
			}
			if (junctions2.Count == 1 && junctions2 [0].EdgeFrom.Direction == junctions2 [0].EdgeTo.Direction) {
				Scale += new Vector3 (0, 0, 12.5f);
				Position += edge.Direction * 12.5f;
			}
		}

		#endregion

		#region Methods

		public override bool Equals (GameObjectInfo other)
		{
			if (other == null) {
				return false;
			}

			if (other is PipeModelInfo) {
				if (this.Edge == (other as PipeModelInfo).Edge && base.Equals (other)) {
					return true;
				}
				else {
					return false;
				}
			}
			else {
				return base.Equals (other);
			}
		}

		#endregion
	}
}

