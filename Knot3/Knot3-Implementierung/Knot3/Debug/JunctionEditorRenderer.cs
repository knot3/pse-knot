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
using Knot3.GameObjects;

namespace Knot3.Debug
{
	public class JunctionEditorRenderer : IGameObject, IEnumerable<IGameObject>
	{
		#region Properties

		private IGameScreen screen;

		/// <summary>
		/// Enthält Informationen über die Position des Knotens.
		/// </summary>
		public GameObjectInfo Info { get; private set; }

		/// <summary>
		/// Die Spielwelt, in der die 3D-Modelle erstellt werden sollen.
		/// </summary>
		public World World { get; set; }

		/// <summary>
		/// Die Liste der 3D-Modelle der Kantenübergänge.
		/// </summary>
		private List<NodeModel> nodes;

		/// <summary>
		/// Die Liste der 3D-Modelle der Kanten.
		/// </summary>
		private List<PipeModel> pipes;

		/// <summary>
		/// Der Zwischenspeicher für die 3D-Modelle der Kanten. Hier wird das Fabrik-Entwurfsmuster verwendet.
		/// </summary>
		private ModelFactory pipeFactory;

		/// <summary>
		/// Der Zwischenspeicher für die 3D-Modelle der Kantenübergänge. Hier wird das Fabrik-Entwurfsmuster verwendet.
		/// </summary>
		private ModelFactory nodeFactory;

		/// <summary>
		/// Die Zuordnung zwischen Kanten und den dreidimensionalen Rasterpunkten, an denen sich die die Kantenübergänge befinden.
		/// </summary>
		private JunctionEditorNodeMap nodeMap;

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues KnotRenderer-Objekt für den angegebenen Spielzustand mit den angegebenen
		/// Spielobjekt-Informationen, die unter Anderem die Position des Knotenursprungs enthalten.
		/// </summary>
		public JunctionEditorRenderer (IGameScreen screen, Vector3 position)
		{
			this.screen = screen;
			Info = new GameObjectInfo (position: position);
			pipes = new List<PipeModel> ();
			nodes = new List<NodeModel> ();
			pipeFactory = new ModelFactory ((s, i) => new PipeModel (s, i as PipeModelInfo));
			nodeFactory = new ModelFactory ((s, i) => new NodeModel (s, i as NodeModelInfo));
			nodeMap = new JunctionEditorNodeMap ();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gibt den Ursprung des Knotens zurück.
		/// </summary>
		public Vector3 Center ()
		{
			return Info.Position;
		}

		/// <summary>
		/// Ruft die Intersects(Ray)-Methode der Kanten, Übergänge und Pfeile auf und liefert das beste Ergebnis zurück.
		/// </summary>
		public GameObjectDistance Intersects (Ray ray)
		{
			GameObjectDistance nearest = null;
			if (!screen.Input.GrabMouseMovement) {
				foreach (PipeModel pipe in pipes) {
					GameObjectDistance intersection = pipe.Intersects (ray);
					if (intersection != null) {
						if (intersection.Distance > 0 && (nearest == null || intersection.Distance < nearest.Distance)) {
							nearest = intersection;
						}
					}
				}
			}
			return nearest;
		}

		public void Render (Tuple<Direction, Direction, Direction> directions)
		{
			if (directions.Item1.Axis != directions.Item2.Axis && directions.Item1.Axis != directions.Item3.Axis &&
			        directions.Item2.Axis != directions.Item3.Axis) {
				nodeMap.Render (directions);
				nodeMap.Offset = Info.Position;

				CreatePipes ();
				CreateNodes ();

				World.Redraw = true;
			}
			else {
				pipes.Clear ();
				nodes.Clear ();

				World.Redraw = true;
			}
		}

		private void CreatePipes ()
		{
			pipes.Clear ();
			foreach (Edge edge in nodeMap.Edges) {
				PipeModelInfo info = new PipeModelInfo (nodeMap: nodeMap, knot: null, edge: edge);
				PipeModel pipe = pipeFactory [screen, info] as PipeModel;
				pipe.Info.IsVisible = true;
				pipe.World = World;
				pipes.Add (pipe);
			}
		}

		private void CreateNodes ()
		{
			nodes.Clear ();

			foreach (Node node in nodeMap.Nodes) {
				List<IJunction> junctions = nodeMap.JunctionsAtNode (node);
				// zeige zwischen zwei Kanten in der selben Richtung keinen Übergang an,
				// wenn sie alleine an dem Kantenpunkt sind
				if (junctions.Count == 1 && junctions [0].EdgeFrom.Direction == junctions [0].EdgeTo.Direction) {
					continue;
				}

				foreach (NodeModelInfo junction in junctions.OfType<NodeModelInfo>()) {
					NodeModel model = nodeFactory [screen, junction] as NodeModel;
					model.World = World;
					nodes.Add (model);
				}
			}
		}

		/// <summary>
		/// Ruft die Update()-Methoden der Kanten, Übergänge und Pfeile auf.
		/// </summary>
		public void Update (GameTime time)
		{
			foreach (PipeModel pipe in pipes) {
				pipe.Update (time);
			}
			foreach (NodeModel node in nodes) {
				node.Update (time);
			}
		}

		/// <summary>
		/// Ruft die Draw()-Methoden der Kanten, Übergänge und Pfeile auf.
		/// </summary>
		public void Draw (GameTime time)
		{
			if (Info.IsVisible) {
				Profiler.Values ["# InFrustum"] = 0;
				Profiler.Values ["RenderEffect"] = 0;
				Profiler.ProfileDelegate ["Pipes"] = () => {
					foreach (PipeModel pipe in pipes) {
						pipe.Draw (time);
					}
				};
				Profiler.ProfileDelegate ["Nodes"] = () => {
					foreach (NodeModel node in nodes) {
						node.Draw (time);
					}
				};
				Profiler.Values ["# Pipes"] = pipes.Count ();
				Profiler.Values ["# Nodes"] = nodes.Count ();
			}
		}

		/// <summary>
		/// Gibt einen Enumerator der aktuell vorhandenen 3D-Modelle zurück.
		/// [returntype=IEnumerator<IGameObject>]
		/// </summary>
		public IEnumerator<IGameObject> GetEnumerator ()
		{
			foreach (PipeModel pipe in pipes) {
				yield return pipe;
			}
			foreach (NodeModel node in nodes) {
				yield return node;
			}
		}

		// Explicit interface implementation for nongeneric interface
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator (); // Just return the generic version
		}

		#endregion
	}
}
