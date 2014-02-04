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
	public interface INodeMap
	{
		#region Properties

		/// <summary>
		/// Die Skalierung, die bei einer Konvertierung in einen Vector3 des XNA-Frameworks durch die ToVector()-Methode der Node-Objekte verwendet wird.
		/// </summary>
		int Scale { get; set; }

		IEnumerable<Edge> Edges { get; set; }

		Vector3 Offset { get; set; }

		Action IndexRebuilt { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Gibt die Rasterposition des Übergangs am Anfang der Kante zurück.
		/// </summary>
		Node NodeBeforeEdge (Edge edge);

		/// <summary>
		/// Gibt die Rasterposition des Übergangs am Ende der Kante zurück.
		/// </summary>
		Node NodeAfterEdge (Edge edge);

		List<IJunction> JunctionsAtNode (Node node);

		List<IJunction> JunctionsBeforeEdge (Edge edge);

		List<IJunction> JunctionsAfterEdge (Edge edge);

		IEnumerable<Node> Nodes { get; }

		/// <summary>
		/// Aktualisiert die Zuordnung, wenn sich die Kanten geändert haben.
		/// </summary>
		void OnEdgesChanged ();

		#endregion
	}
}
