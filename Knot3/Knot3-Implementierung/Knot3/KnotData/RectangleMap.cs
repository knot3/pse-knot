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
using Knot3.RenderEffects;
using Knot3.Widgets;
using Knot3.Utilities;

namespace Knot3.KnotData
{
	public sealed class RectangleMap
	{
		#region Properties

		private NodeMap NodeMap;
		private Dictionary<Vector3, List<PossibleRectanglePosition>> positions
		    = new Dictionary<Vector3, List<PossibleRectanglePosition>> ();

		#endregion

		#region Constructors

		public RectangleMap (NodeMap nodeMap)
		{
			NodeMap = nodeMap;
		}

		#endregion

		#region Methods

		public void AddEdge (Edge edge, bool isVirtual)
		{
			Node a = NodeMap.NodeBeforeEdge (edge);
			Node b = NodeMap.NodeAfterEdge (edge);
			AddEdge (edge, a, b, isVirtual);
		}

		public void AddEdge (Edge edge, Node nodeA, Node nodeB, bool isVirtual)
		{
			Vector3 edgeCenter = nodeA.CenterBetween (nodeB);
			foreach (Direction direction in Direction.Values) {
				if (direction.Axis != edge.Direction.Axis) {
					Vector3 rectangleCenter = edgeCenter + direction * Node.Scale / 2;
					PossibleRectanglePosition rectanglePosition = new PossibleRectanglePosition {
						Edge = edge,
						NodeA = nodeA,
						NodeB = nodeB,
						Position = rectangleCenter,
						IsVirtual = isVirtual
					};
					positions.Add (rectangleCenter, rectanglePosition);
				}
			}
		}

		public bool ContainsEdge (Node a, Node b)
		{
			foreach (List<PossibleRectanglePosition> many in positions.Values) {
				foreach (PossibleRectanglePosition position in many) {
					if ((position.NodeA == a && position.NodeB == b) || (position.NodeA == b && position.NodeB == a)) {
						return true;
					}
				}
			}
			return false;
		}

		public IEnumerable<ValidRectanglePosition> ValidPositions ()
		{
			foreach (List<PossibleRectanglePosition> many in positions.Values) {
				foreach (var pair in many.SelectMany ((value, index) => many.Skip (index + 1),
				(first, second) => new { first, second })) {
					List<PossibleRectanglePosition> pos
					    = new PossibleRectanglePosition[] { pair.first, pair.second } .ToList ();
					if (pos.Count == 2) {
						for (int i = 0; i <= 1; ++i) {
							int first = i % 2;
							int second = (i + 1) % 2;
							Edge edgeAB = pos [first].Edge;
							Edge edgeCD = pos [second].Edge;
							Node nodeA = pos [first].NodeA;
							Node nodeB = pos [first].NodeB;
							Node nodeC = pos [second].NodeA;
							Node nodeD = pos [second].NodeB;
							if (nodeB == nodeC || (nodeA-nodeB) == (nodeC-nodeD)) {
								var valid = new ValidRectanglePosition {
									EdgeAB = edgeAB,
									EdgeCD = edgeCD,
									NodeA = nodeA,
									NodeB = nodeB,
									NodeC = nodeC,
									NodeD = nodeD,
									Position = pos[first].Position,
									IsVirtual = pos[first].IsVirtual || pos[second].IsVirtual
								};
								yield return valid;
							}
						}
					}
				}
			}
		}

		#endregion
	}

	public struct PossibleRectanglePosition {
		public Edge Edge;
		public Node NodeA;
		public Node NodeB;
		public Vector3 Position;
		public bool IsVirtual;
	}

	public struct ValidRectanglePosition {
		public Edge EdgeAB;
		public Edge EdgeCD;
		public Node NodeA;
		public Node NodeB;
		public Node NodeC;
		public Node NodeD;
		public Vector3 Position;
		public bool IsVirtual;
	}
}

