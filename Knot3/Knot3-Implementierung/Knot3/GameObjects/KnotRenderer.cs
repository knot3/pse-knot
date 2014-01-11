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
	/// Erstellt aus einem Knoten-Objekt die zu dem Knoten gehörenden 3D-Modelle sowie die 3D-Modelle der Pfeile,
	/// die nach einer Auswahl von Kanten durch den Spieler angezeigt werden. Ist außerdem ein IGameObject und ein
	/// Container für die erstellten Spielobjekte.
	/// </summary>
	public sealed class KnotRenderer : IGameObject, IEnumerable<IGameObject>
	{
        #region Properties

		private GameScreen screen;

		/// <summary>
		/// Enthält Informationen über die Position des Knotens.
		/// </summary>
		public GameObjectInfo Info { get; private set; }

		/// <summary>
		/// Die Spielwelt, in der die 3D-Modelle erstellt werden sollen.
		/// </summary>
		public World World { get; set; }

		/// <summary>
		/// Die Liste der 3D-Modelle der Pfeile,
		/// die nach einer Auswahl von Kanten durch den Spieler angezeigt werden.
		/// </summary>
		private List<ArrowModel> arrows;

		/// <summary>
		/// Die Liste der 3D-Modelle der Kantenübergänge.
		/// </summary>
		private List<NodeModel> nodes;

		/// <summary>
		/// Die Liste der 3D-Modelle der Kanten.
		/// </summary>
		private List<PipeModel> pipes;

		/// <summary>
		/// Der Knoten, für den 3D-Modelle erstellt werden sollen.
		/// </summary>
		public Knot Knot {
			set {
				knot = value;
				nodeMap.Edges = knot;
				knot.EdgesChanged += OnEdgesChanged;
				knot.SelectionChanged += OnSelectionChanged;
				OnEdgesChanged ();
			}
		}
		
		private Knot knot;

		/// <summary>
		/// Der Zwischenspeicher für die 3D-Modelle der Kanten. Hier wird das Fabrik-Entwurfsmuster verwendet.
		/// </summary>
		private ModelFactory pipeFactory;

		/// <summary>
		/// Der Zwischenspeicher für die 3D-Modelle der Kantenübergänge. Hier wird das Fabrik-Entwurfsmuster verwendet.
		/// </summary>
		private ModelFactory nodeFactory;

		/// <summary>
		/// Der Zwischenspeicher für die 3D-Modelle der Pfeile. Hier wird das Fabrik-Entwurfsmuster verwendet.
		/// </summary>
		private ModelFactory arrowFactory;
		private NodeMap nodeMap;

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt ein neues KnotRenderer-Objekt für den angegebenen Spielzustand mit den angegebenen
		/// Spielobjekt-Informationen, die unter Anderem die Position des Knotenursprungs enthalten.
		/// </summary>
		public KnotRenderer (GameScreen screen, GameObjectInfo info)
		{
			this.screen = screen;
			Info = info;
			pipes = new List<PipeModel> ();
			nodes = new List<NodeModel> ();
			arrows = new List<ArrowModel> ();
			pipeFactory = new ModelFactory ((s, i) => new PipeModel (s, i as PipeModelInfo));
			nodeFactory = new ModelFactory ((s, i) => new NodeModel (s, i as NodeModelInfo));
			arrowFactory = new ModelFactory ((s, i) => new ArrowModel (s, i as ArrowModelInfo));
			nodeMap = new NodeMap ();
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
				foreach (ArrowModel arrow in arrows) {
					GameObjectDistance intersection = arrow.Intersects (ray);
					if (intersection != null) {
						if (intersection.Distance > 0 && (nearest == null || intersection.Distance < nearest.Distance)) {
							nearest = intersection;
						}
					}
				}
			}
			return nearest;
		}

		/// <summary>
		/// Wird mit dem EdgesChanged-Event des Knotens verknüft.
		/// </summary>
		private void OnEdgesChanged ()
		{
			nodeMap.Offset = Info.Position;
			nodeMap.OnEdgesChanged ();

			CreatePipes ();
			CreateNodes ();
			CreateArrows ();
		}

		private void OnSelectionChanged ()
		{
			CreateArrows ();
		}

		private void CreatePipes ()
		{
			pipes.Clear ();
			foreach (Edge edge in knot) {
				PipeModelInfo info = new PipeModelInfo (nodeMap, knot, edge);
				PipeModel pipe = pipeFactory [screen, info] as PipeModel;
				// pipe.OnDataChange = () => UpdatePipes (edges);
				pipe.World = World;
				pipes.Add (pipe);
			}
		}

		private void CreateNodes ()
		{
			nodes.Clear ();

			var nodeJunctionMap = new Dictionary<Node, List<IJunction>> ();
			var edgeList = new List<Edge> (knot);
			for (int n = 0; n < edgeList.Count; n++) {
				Edge edgeA = edgeList.At (n);
				Edge edgeB = edgeList.At (n + 1);
				Node node = nodeMap.To (edgeA);
				IJunction junction = new NodeModelInfo (nodeMap, knot, edgeA, edgeB);
				nodeJunctionMap.Add (node, junction);
			}

			foreach (Node node in nodeJunctionMap.Keys) {
				List<IJunction> junctions = nodeJunctionMap [node];
				foreach (NodeModelInfo junction in junctions.OfType<NodeModelInfo>()) {
					junction.JunctionCountAtNode = junctions.Count;
					NodeModel model = nodeFactory [screen, junction] as NodeModel;
					model.World = World;
					nodes.Add (model);
				}
			}
		}

		private void CreateArrows ()
		{
			arrows.Clear ();
			int selectedEdgesCount = knot.SelectedEdges.Count ();
			if (selectedEdgesCount > 0) {
				CreateArrow (knot.SelectedEdges.ElementAt ((int)selectedEdgesCount / 2));
			}
		}

		private void CreateArrow (Edge edge)
		{
			try {
				Node node1 = nodeMap.From (edge);
				Node node2 = nodeMap.To (edge);
				foreach (Direction direction in DirectionHelper.AllDirections()) {
					if (knot.IsValidMove (direction, -1)) {
						ArrowModelInfo info = new ArrowModelInfo (
							position: node1.CenterBetween (node2) - 50 * World.Camera.TargetDirection.PrimaryDirection (),
							direction: direction
						);
						ArrowModel arrow = arrowFactory [screen, info] as ArrowModel;
						arrow.World = World;
						arrows.Add (arrow);
					}
				}
			} catch (NullReferenceException ex) {
				Console.WriteLine (ex.ToString ());
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
			foreach (ArrowModel arrow in arrows) {
				arrow.Update (time);
			}
		}

		/// <summary>
		/// Ruft die Draw()-Methoden der Kanten, Übergänge und Pfeile auf.
		/// </summary>
		public void Draw (GameTime time)
		{
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
			Profiler.ProfileDelegate ["Arrows"] = () => {
				foreach (ArrowModel arrow in arrows) {
					arrow.Draw (time);
				}
			};
			Profiler.Values ["# Pipes"] = pipes.Count ();
			Profiler.Values ["# Nodes"] = nodes.Count ();
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
			foreach (ArrowModel arrow in arrows) {
				yield return arrow;
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

