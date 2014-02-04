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
	/// <summary>
	/// Eine Zuordnung zwischen Kanten und den dreidimensionalen Rasterpunkten, an denen sich die die Kantenübergänge befinden.
	/// </summary>
	public sealed class NodeMap : INodeMap
	{
		#region Properties

		private Hashtable fromMap = new Hashtable ();
		private Hashtable toMap = new Hashtable ();
		private Dictionary<Node, List<IJunction>> junctionMap = new Dictionary<Node, List<IJunction>> ();

		/// <summary>
		/// Die Skalierung, die bei einer Konvertierung in einen Vector3 des XNA-Frameworks durch die ToVector()-Methode der Node-Objekte verwendet wird.
		/// </summary>
		public int Scale { get; set; }

		public IEnumerable<Edge> Edges { get; set; }

		public Vector3 Offset { get; set; }

		public Action IndexRebuilt { get; set; }

		#endregion

		#region Constructors

		public NodeMap ()
		{
			IndexRebuilt = () => {};
		}

		public NodeMap (IEnumerable<Edge> edges)
		: this()
		{
			Edges = edges;
			BuildIndex ();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gibt die Rasterposition des Übergangs am Anfang der Kante zurück.
		/// </summary>
		public Node NodeBeforeEdge (Edge edge)
		{
			return (Node)fromMap [edge];
		}

		/// <summary>
		/// Gibt die Rasterposition des Übergangs am Ende der Kante zurück.
		/// </summary>
		public Node NodeAfterEdge (Edge edge)
		{
			return (Node)toMap [edge];
		}

		public List<IJunction> JunctionsAtNode (Node node)
		{
			return junctionMap [node];
		}

		public List<IJunction> JunctionsBeforeEdge (Edge edge)
		{
			return junctionMap [NodeBeforeEdge (edge)];
		}

		public List<IJunction> JunctionsAfterEdge (Edge edge)
		{
			return junctionMap [NodeAfterEdge (edge)];
		}

		public IEnumerable<Node> Nodes
		{
			get {
				return junctionMap.Keys;
			}
		}

		/// <summary>
		/// Aktualisiert die Zuordnung, wenn sich die Kanten geändert haben.
		/// </summary>
		public void OnEdgesChanged ()
		{
			BuildIndex ();
		}

		private void BuildIndex ()
		{
			fromMap.Clear ();
			toMap.Clear ();
			float x = Offset.X, y = Offset.Y, z = Offset.Z;
			foreach (Edge edge in Edges) {
				fromMap [edge] = new Node ((int)x, (int)y, (int)z);
				Vector3 v = edge.Direction.Vector;
				x += v.X;
				y += v.Y;
				z += v.Z;
				toMap [edge] = new Node ((int)x, (int)y, (int)z);
			}

			IndexRebuilt = () => {};
			junctionMap.Clear ();
			List<Edge> EdgeList = Edges.ToList ();
			for (int n = 0; n < EdgeList.Count; n++) {
				Edge edgeA = Edges.At (n);
				Edge edgeB = Edges.At (n + 1);
				Node node = NodeAfterEdge (edgeA);
				IJunction junction = new NodeModelInfo (nodeMap: this, from: edgeA, to: edgeB, node: node, index: n);
				junctionMap.Add (node, junction);
			}

			IndexRebuilt ();
		}

		#endregion
	}
}
