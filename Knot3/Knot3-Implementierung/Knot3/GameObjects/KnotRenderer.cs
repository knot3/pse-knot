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
using Knot3.Debug;

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
		/// Die Liste der Flächen zwischen den Kanten.
		/// </summary>
		private List<TexturedRectangle> rectangles;

		/// <summary>
		/// Der Knoten, für den 3D-Modelle erstellt werden sollen.
		/// </summary>
		public Knot Knot
		{
			get {
				return knot;
			}
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

		/// <summary>
		/// Die Zuordnung zwischen Kanten und den dreidimensionalen Rasterpunkten, an denen sich die die Kantenübergänge befinden.
		/// </summary>
		private NodeMap nodeMap;

		/// <summary>
		/// Gibt an, ob Pfeile anzuzeigen sind. Wird aus der Einstellungsdatei gelesen.
		/// </summary>
		private bool showArrows { get { return Options.Default ["video", "arrows", false]; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues KnotRenderer-Objekt für den angegebenen Spielzustand mit den angegebenen
		/// Spielobjekt-Informationen, die unter Anderem die Position des Knotenursprungs enthalten.
		/// </summary>
		public KnotRenderer (IGameScreen screen, Vector3 position)
		{
			this.screen = screen;
			Info = new GameObjectInfo (position: position);
			pipes = new List<PipeModel> ();
			nodes = new List<NodeModel> ();
			arrows = new List<ArrowModel> ();
			rectangles = new List<TexturedRectangle> ();
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
			if (showArrows) {
				CreateArrows ();
			}
			CreateRectangles ();

			World.Redraw = true;
		}

		private void OnSelectionChanged ()
		{
			if (showArrows) {
				CreateArrows ();
			}
			World.Redraw = true;
		}

		private void CreatePipes ()
		{
			pipes.Clear ();
			foreach (Edge edge in knot) {
				PipeModelInfo info = new PipeModelInfo (nodeMap, knot, edge);
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
				Node node1 = nodeMap.NodeBeforeEdge (edge);
				Node node2 = nodeMap.NodeAfterEdge (edge);
				foreach (Direction direction in Direction.Values) {
					if (knot.IsValidMove (direction)) {
						Vector3 towardsCamera = World.Camera.PositionToTargetDirection;
						ArrowModelInfo info = new ArrowModelInfo (
						    position: node1.CenterBetween (node2) - 25 * towardsCamera - 25 * towardsCamera.PrimaryDirection (),
						    direction: direction
						);
						ArrowModel arrow = arrowFactory [screen, info] as ArrowModel;
						arrow.Info.IsVisible = true;
						arrow.World = World;
						arrows.Add (arrow);
					}
				}
			}
			catch (NullReferenceException ex) {
				Console.WriteLine (ex.ToString ());
			}
		}

		private void CreateRectangles ()
		{
			foreach (TexturedRectangle rectangle in rectangles) {
				rectangle.Dispose ();
			}
			rectangles.Clear ();

			foreach (Node node in nodeMap.Nodes) {
				List<IJunction> junctions = nodeMap.JunctionsAtNode (node);

				if (junctions.Count == 1) {
					CreateRectangle (junctions [0]);
				}
			}
		}

		private void CreateRectangle (IJunction junction)
		{
			Edge from = junction.EdgeFrom;
			Edge to = junction.EdgeTo;
			if (from.Rectangles.Intersect (to.Rectangles).Count () > 0) {
				Node node = nodeMap.NodeAfterEdge (from);
				Vector3 origin = node.Vector + (to.Direction - from.Direction) / 2 * Node.Scale;
				Texture2D texture = CreateRectangleTexture (from.Color, to.Color);
				TexturedRectangleInfo info = new TexturedRectangleInfo (
					texture: texture,
					origin: origin,
					left: from.Direction,
					width: Node.Scale,
					up: to.Direction.Reverse,
					height: Node.Scale
				);
				TexturedRectangle rectangle = new TexturedRectangle (screen: screen, info: info);
				rectangle.World = World;
				Console.WriteLine ("rectangle=" + rectangle);
				rectangles.Add (rectangle);
			}
		}

		private Texture2D CreateRectangleTexture (Color fromColor, Color toColor)
		{
			int width = 100;
			int height = 100;
			Texture2D texture = new Texture2D (screen.Device, width, height);
			Color[] colors = new Color[width * height];
			for (int w = 0; w < width; ++w) {
				for (int h = 0; h < height; ++h) {
					//Console.WriteLine((w - h));
					colors [h * width + w] = toColor.Mix (fromColor, 0.5f + (float)(w - h) / (float)Math.Max(width, height)) * 0.9f;
				}
			}
			texture.SetData (colors);
			return texture;
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
			foreach (TexturedRectangle rectangle in rectangles) {
				rectangle.Update (time);
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
				Profiler.ProfileDelegate ["Arrows"] = () => {
					foreach (ArrowModel arrow in arrows) {
						arrow.Draw (time);
					}
				};
				Profiler.ProfileDelegate ["Rectangles"] = () => {
					foreach (TexturedRectangle rectangle in rectangles) {
						rectangle.Draw (time);
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
			foreach (ArrowModel arrow in arrows) {
				yield return arrow;
			}
			foreach (TexturedRectangle rectangle in rectangles) {
				yield return rectangle;
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

