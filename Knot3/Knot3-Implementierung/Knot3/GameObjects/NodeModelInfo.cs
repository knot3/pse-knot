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
	/// Enthält Informationen über ein 3D-Modell, das einen Kantenübergang darstellt.
	/// </summary>
	public sealed class NodeModelInfo : GameModelInfo, IJunction
	{
		#region Properties

		/// <summary>
		/// Die Kante vor dem Übergang.
		/// </summary>
		public Edge EdgeFrom { get; set; }

		/// <summary>
		/// Die Kante nach dem Übergang.
		/// </summary>
		public Edge EdgeTo { get; set; }

		public Node Node { get; set; }

		private INodeMap NodeMap;

		public int Index { get; private set; }

		public List<IJunction> JunctionsAtNode
		{
			get {
				return (from j in NodeMap.JunctionsAtNode (NodeMap.NodeAfterEdge (EdgeFrom)) orderby j.Index ascending select j).ToList ();
			}
		}

		public int JunctionsAtNodeIndex
		{
			get {
				int i = 0;
				foreach (IJunction junction in JunctionsAtNode) {
					if (junction == this as IJunction) {
						break;
					}
					++i;
				}
				return i;
			}
		}

		public List<IJunction> OtherJunctionsAtNode
		{
			get {
				return JunctionsAtNode.Where (x => x != this as IJunction).ToList ();
			}
		}

		public JunctionType Type
		{
			get {
				return EdgeFrom.Direction == EdgeTo.Direction ? JunctionType.Straight : JunctionType.Angled;
			}
		}

		public string NodeConfigKey
		{
			get {
				IEnumerable<string> _directions = JunctionsAtNode.Select (junction => junction.EdgeFrom.Direction + "" + junction.EdgeTo.Direction);
				return "Node" + JunctionsAtNode.Count () + ":" + string.Join (",", _directions);
			}
		}

		private static Dictionary<Tuple<Direction, Direction>, JunctionDirection> angledJunctionDirectionMap
		    = new Dictionary<Tuple<Direction, Direction>, JunctionDirection> ()
		{
			{ Tuple.Create(Direction.Up, Direction.Up), 			JunctionDirection.UpUp },
			{ Tuple.Create(Direction.Up, Direction.Left), 			JunctionDirection.UpLeft },
			{ Tuple.Create(Direction.Up, Direction.Right), 			JunctionDirection.UpRight },
			{ Tuple.Create(Direction.Up, Direction.Forward), 		JunctionDirection.UpForward },
			{ Tuple.Create(Direction.Up, Direction.Backward), 		JunctionDirection.UpBackward },

			{ Tuple.Create(Direction.Down, Direction.Down), 		JunctionDirection.UpUp },
			{ Tuple.Create(Direction.Down, Direction.Left), 		JunctionDirection.DownLeft },
			{ Tuple.Create(Direction.Down, Direction.Right), 		JunctionDirection.DownRight },
			{ Tuple.Create(Direction.Down, Direction.Forward), 		JunctionDirection.DownForward },
			{ Tuple.Create(Direction.Down, Direction.Backward), 	JunctionDirection.DownBackward },

			{ Tuple.Create(Direction.Left, Direction.Left), 		JunctionDirection.RightRight },
			{ Tuple.Create(Direction.Left, Direction.Up), 			JunctionDirection.DownRight },
			{ Tuple.Create(Direction.Left, Direction.Down), 		JunctionDirection.UpRight },
			{ Tuple.Create(Direction.Left, Direction.Forward), 		JunctionDirection.LeftForward },
			{ Tuple.Create(Direction.Left, Direction.Backward), 	JunctionDirection.LeftBackward },

			{ Tuple.Create(Direction.Right, Direction.Right), 		JunctionDirection.RightRight },
			{ Tuple.Create(Direction.Right, Direction.Up), 			JunctionDirection.DownLeft },
			{ Tuple.Create(Direction.Right, Direction.Down), 		JunctionDirection.UpLeft },
			{ Tuple.Create(Direction.Right, Direction.Forward), 	JunctionDirection.RightForward },
			{ Tuple.Create(Direction.Right, Direction.Backward), 	JunctionDirection.RightBackward },

			{ Tuple.Create(Direction.Forward, Direction.Forward), 	JunctionDirection.BackwardBackward },
			{ Tuple.Create(Direction.Forward, Direction.Left), 		JunctionDirection.RightBackward },
			{ Tuple.Create(Direction.Forward, Direction.Right), 	JunctionDirection.LeftBackward },
			{ Tuple.Create(Direction.Forward, Direction.Up), 		JunctionDirection.DownBackward },
			{ Tuple.Create(Direction.Forward, Direction.Down), 		JunctionDirection.UpBackward },

			{ Tuple.Create(Direction.Backward, Direction.Backward), JunctionDirection.BackwardBackward },
			{ Tuple.Create(Direction.Backward, Direction.Left), 	JunctionDirection.RightForward },
			{ Tuple.Create(Direction.Backward, Direction.Right), 	JunctionDirection.LeftForward },
			{ Tuple.Create(Direction.Backward, Direction.Up), 		JunctionDirection.DownForward },
			{ Tuple.Create(Direction.Backward, Direction.Down), 	JunctionDirection.UpForward },
		};
		private static Dictionary<JunctionDirection, Angles3> angledJunctionRotationMap
		    = new Dictionary<JunctionDirection, Angles3> ()
		{
			{ JunctionDirection.UpForward, 			Angles3.FromDegrees (0, 0, 0) },
			{ JunctionDirection.UpBackward, 		Angles3.FromDegrees (0, 180, 0) },
			{ JunctionDirection.UpLeft, 			Angles3.FromDegrees (0, 90, 0) },
			{ JunctionDirection.UpRight, 			Angles3.FromDegrees (0, 270, 0) },
			{ JunctionDirection.DownForward, 		Angles3.FromDegrees (180, 180, 0) },
			{ JunctionDirection.DownBackward, 		Angles3.FromDegrees (180, 0, 0) },
			{ JunctionDirection.DownLeft, 			Angles3.FromDegrees (180, 270, 0) },
			{ JunctionDirection.DownRight, 			Angles3.FromDegrees (180, 90, 0) },
			{ JunctionDirection.RightForward, 		Angles3.FromDegrees (0, 0, 270) },
			{ JunctionDirection.RightBackward, 		Angles3.FromDegrees (0, 90, 270) },
			{ JunctionDirection.LeftForward, 		Angles3.FromDegrees (0, 270, 270) },
			{ JunctionDirection.LeftBackward, 		Angles3.FromDegrees (0, 180, 270) },
			{ JunctionDirection.UpUp, 				Angles3.FromDegrees (90, 0, 0) },
			{ JunctionDirection.RightRight, 		Angles3.FromDegrees (0, 90, 0) },
			{ JunctionDirection.BackwardBackward, 	Angles3.FromDegrees (0, 0, 0) },
		};
		private static Dictionary<Direction, Angles3> straightJunctionRotationMap
		    = new Dictionary<Direction, Angles3> ()
		{
			{ Direction.Up,			Angles3.FromDegrees (90, 0, 0) },
			{ Direction.Down,		Angles3.FromDegrees (270, 0, 0) },
			{ Direction.Left,		Angles3.FromDegrees (0, 90, 0) },
			{ Direction.Right,		Angles3.FromDegrees (0, 270, 0) },
			{ Direction.Forward,	Angles3.FromDegrees (0, 0, 0) },
			{ Direction.Backward,	Angles3.FromDegrees (0, 0, 180) },
		};
		private static Dictionary<Tuple<Direction, Direction>, Tuple<float, float>> curvedJunctionBumpRotationMap
		    = new Dictionary<Tuple<Direction, Direction>, Tuple<float, float>> ()
		{
			{ Tuple.Create(Direction.Up, Direction.Left), 			Tuple.Create(90f, 0f) }, // works
			{ Tuple.Create(Direction.Up, Direction.Right), 			Tuple.Create(-90f, 0f) }, // works
			{ Tuple.Create(Direction.Up, Direction.Forward), 		Tuple.Create(0f, 180f) }, // works
			{ Tuple.Create(Direction.Up, Direction.Backward), 		Tuple.Create(0f, 0f) }, // works

			{ Tuple.Create(Direction.Down, Direction.Left), 		Tuple.Create(-90f, 0f) }, // works
			{ Tuple.Create(Direction.Down, Direction.Right), 		Tuple.Create(90f, 0f) }, // works
			{ Tuple.Create(Direction.Down, Direction.Forward), 		Tuple.Create(0f, 180f) }, // works
			{ Tuple.Create(Direction.Down, Direction.Backward), 	Tuple.Create(0f, 0f) }, // works

			{ Tuple.Create(Direction.Left, Direction.Up), 			Tuple.Create(0f, 90f) }, // works
			{ Tuple.Create(Direction.Left, Direction.Down), 		Tuple.Create(0f, -90f) },
			{ Tuple.Create(Direction.Left, Direction.Forward), 		Tuple.Create(-90f, 90f) }, // works
			{ Tuple.Create(Direction.Left, Direction.Backward), 	Tuple.Create(-90f, -90f) }, // works

			{ Tuple.Create(Direction.Right, Direction.Up), 			Tuple.Create(0f, -90f) }, // works
			{ Tuple.Create(Direction.Right, Direction.Down), 		Tuple.Create(0f, 90f) }, // works
			{ Tuple.Create(Direction.Right, Direction.Forward), 	Tuple.Create(90f, -90f) }, // works
			{ Tuple.Create(Direction.Right, Direction.Backward), 	Tuple.Create(-90f, -90f) }, // works

			{ Tuple.Create(Direction.Forward, Direction.Left), 		Tuple.Create(90f, -90f) }, // works
			{ Tuple.Create(Direction.Forward, Direction.Right), 	Tuple.Create(90f, -90f) }, // works
			{ Tuple.Create(Direction.Forward, Direction.Up), 		Tuple.Create(180f, 0f) }, // works
			{ Tuple.Create(Direction.Forward, Direction.Down), 		Tuple.Create(180f, 0f) }, // works

			{ Tuple.Create(Direction.Backward, Direction.Left), 	Tuple.Create(90f, 90f) }, // works
			{ Tuple.Create(Direction.Backward, Direction.Right), 	Tuple.Create(-90f, -90f) }, // works
			{ Tuple.Create(Direction.Backward, Direction.Up), 		Tuple.Create(0f, 0f) }, // works
			{ Tuple.Create(Direction.Backward, Direction.Down), 	Tuple.Create(0f, 0f) }, // works
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues Informationsobjekt für ein 3D-Modell, das einen Kantenübergang darstellt.
		/// [base="node1", Angles3.Zero, new Vector3(1,1,1)]
		/// </summary>
		public NodeModelInfo (INodeMap nodeMap, Edge from, Edge to, Node node, int index)
		: base("pipe-straight", Angles3.Zero, Vector3.One * 25f)
		{
			EdgeFrom = from;
			EdgeTo = to;
			Node = node;
			NodeMap = nodeMap;
			Index = index;
			Position = nodeMap.NodeAfterEdge (EdgeFrom);

			// Kanten sind sichtbar, nicht auswählbar und nicht verschiebbar
			IsVisible = true;
			IsSelectable = false;
			IsMovable = false;

			// Wähle das Modell aus
			nodeMap.IndexRebuilt += () => {
				try {
					chooseModel ();
				}
				catch (Exception ex) {
					Console.WriteLine (ex);
				}
			};
		}

		#endregion

		#region Methods

		private void chooseModel ()
		{
			if (JunctionsAtNode.Count == 1) {
				chooseModelOneJunction ();
			}
			else if (JunctionsAtNode.Count == 2) {
				chooseModelTwoJunctions ();
			}
			else if (JunctionsAtNode.Count == 3) {
				chooseModelThreeJunctions ();
			}
		}

		private void chooseModelOneJunction ()
		{
			if (Type == JunctionType.Angled) {
				Modelname = Options.Models [NodeConfigKey, "modelname" + JunctionsAtNodeIndex, "pipe-angled"];
				Rotation = angledJunctionRotationMap [angledJunctionDirectionMap [Tuple.Create (EdgeFrom.Direction, EdgeTo.Direction)]];
			}
		}

		private void chooseModelTwoJunctions ()
		{
			if (Type == JunctionType.Angled) {
				Modelname = Options.Models [NodeConfigKey, "modelname" + JunctionsAtNodeIndex, "pipe-angled"];
				Rotation = angledJunctionRotationMap [angledJunctionDirectionMap [Tuple.Create (EdgeFrom.Direction, EdgeTo.Direction)]];
			}
			else if (Type == JunctionType.Straight) {
				if (OtherJunctionsAtNode [0].Type == JunctionType.Straight) {
					// Drehung des Übergangs
					Modelname = Options.Models [NodeConfigKey, "modelname" + JunctionsAtNodeIndex, "pipe-curved1"];
					Rotation = straightJunctionRotationMap [EdgeFrom.Direction];

					// Drehung der Delle
					var directionTuple = Tuple.Create (JunctionsAtNode [0].EdgeFrom.Direction, JunctionsAtNode [1].EdgeFrom.Direction);
					float defaultRotation = curvedJunctionBumpRotationMap [directionTuple].At (JunctionsAtNodeIndex);
					float bumpRotationZ = Options.Models [NodeConfigKey, "bump" + JunctionsAtNodeIndex, defaultRotation];
					Rotation += Angles3.FromDegrees (0, 0, bumpRotationZ);

					// debug
					Console.WriteLine ("Index="
						+ Index + ", Directions=" + directionTuple + ", Rotation=" + Rotation + ", bumpRotationZ=" + bumpRotationZ + ", ...="
						+ Angles3.FromDegrees (0, 0, bumpRotationZ)
					);
				}
				else {
					Modelname = Options.Models [NodeConfigKey, "modelname" + JunctionsAtNodeIndex, "pipe-straight"];
					Rotation = straightJunctionRotationMap [EdgeFrom.Direction];
				}
			}
		}

		private void chooseModelThreeJunctions ()
		{
			if (Type == JunctionType.Angled) {
				Modelname = Options.Models [NodeConfigKey, "modelname" + JunctionsAtNodeIndex, "pipe-angled"];
				Rotation = angledJunctionRotationMap [angledJunctionDirectionMap [Tuple.Create (EdgeFrom.Direction, EdgeTo.Direction)]];
			}
			else if (Type == JunctionType.Straight) {

				// Drehung des Übergangs
				Modelname = Options.Models [NodeConfigKey, "modelname" + JunctionsAtNodeIndex, "pipe-curved2"];
				Rotation = Angles3.FromDegrees (0, 0, 0) + straightJunctionRotationMap [EdgeFrom.Direction];

				// Drehung der Delle
				float bumpRotationZ = Options.Models [NodeConfigKey, "bump" + JunctionsAtNodeIndex, 0];
				Rotation += Angles3.FromDegrees (0, 0, bumpRotationZ);
			}
		}

		public override bool Equals (GameObjectInfo other)
		{
			if (other == null) {
				return false;
			}

			if (other is NodeModelInfo) {
				if (this.EdgeFrom == (other as NodeModelInfo).EdgeFrom
					&& this.EdgeTo == (other as NodeModelInfo).EdgeTo
					&& base.Equals (other)) {
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

	enum JunctionDirection
	{
		UpForward,
		UpBackward,
		UpLeft,
		UpRight,
		DownForward,
		DownBackward,
		DownLeft,
		DownRight,
		RightForward,
		RightBackward,
		LeftForward,
		LeftBackward,
		UpUp,
		RightRight,
		BackwardBackward,
	}
}

