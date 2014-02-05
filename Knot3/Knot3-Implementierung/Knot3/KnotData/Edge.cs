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

namespace Knot3.KnotData
{
	/// <summary>
	/// Eine Kante eines Knotens, die aus einer Richtung und einer Farbe, sowie optional einer Liste von Flächennummern besteht.
	/// </summary>
	public sealed class Edge : IEquatable<Edge>
	{
		#region Properties

		/// <summary>
		/// Die Farbe der Kante.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// Die Richtung der Kante.
		/// </summary>
		public Direction Direction { get; private set; }

		/// <summary>
		/// Die Liste der Flächennummern, die an die Kante angrenzen.
		/// </summary>
		public HashSet<int> Rectangles { get; private set; }

		private int id;
		private static int previousId = 0;

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt eine neue Kante mit der angegebenen Richtung.
		/// </summary>
		public Edge (Direction direction)
		{
			Direction = direction;
			Color = DefaultColor;
			id = ++previousId;
			Rectangles = new HashSet<int> ();
		}

		/// <summary>
		/// Erstellt eine neue Kante mit der angegebenen Richtung und Farbe.
		/// </summary>
		public Edge(Direction direction, Color color)
		{
			Direction = direction;
			Color = color;
			id = ++previousId;
			Rectangles = new HashSet<int>();
		}

		#endregion

		#region Methods

		public static bool operator == (Edge a, Edge b)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals (a, b)) {
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)a == null) || ((object)b == null)) {
				return false;
			}

			// Return true if the fields match:
			return a.id == b.id;
		}

		public static bool operator != (Edge a, Edge b)
		{
			return !(a == b);
		}

		public bool Equals (Edge other)
		{
			return other != null && this.id == other.id;
		}

		public override bool Equals (object other)
		{
			if (other == null) {
				return false;
			}
			else if (other is Edge) {
				return Equals (other as Edge);
			}
			else if (other is Direction) {
				return Direction.Equals (other as Direction);
			}
			else if (other is Vector3) {
				return Direction.Vector.Equals ((Vector3)other);
			}
			else if (other is Color) {
				return Color.Equals ((Color)other);
			}
			else {
				return false;
			}
		}

		public override int GetHashCode ()
		{
			return id;
		}

		public override string ToString ()
		{
			return Direction + "/" + id.ToString ();
		}

		public static implicit operator Direction (Edge edge)
		{
			return edge.Direction;
		}

		public static implicit operator Vector3 (Edge edge)
		{
			return edge.Direction;
		}

		public static implicit operator Color (Edge edge)
		{
			return edge.Color;
		}

		#endregion

		#region Helper Methods

		private static Random r = new Random ();

		public static Color RandomColor ()
		{
			return Colors [r.Next () % Colors.Count];
		}

		public static Color RandomColor (GameTime time)
		{
			return Colors [(int)time.TotalGameTime.TotalSeconds % Colors.Count];
		}

		public static Edge RandomEdge ()
		{
			int i = r.Next () % 6;
			return i == 0 ? Left : i == 1 ? Right : i == 2 ? Up : i == 3 ? Down : i == 4 ? Forward : Backward;
		}

		#endregion

		#region Static Properties

		public static List<Color> Colors = new List<Color> ()
		{
			Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Orange
		};
		public static Color DefaultColor = RandomColor ();

		public static Edge Zero { get { return new Edge (Direction.Zero); } }

		public static Edge UnitX { get { return new Edge (Direction.Right); } }

		public static Edge UnitY { get { return new Edge (Direction.Up); } }

		public static Edge UnitZ { get { return new Edge (Direction.Backward); } }

		public static Edge Up { get { return new Edge (Direction.Up); } }

		public static Edge Down { get { return new Edge (Direction.Down); } }

		public static Edge Right { get { return new Edge (Direction.Right); } }

		public static Edge Left { get { return new Edge (Direction.Left); } }

		public static Edge Forward { get { return new Edge (Direction.Forward); } }

		public static Edge Backward { get { return new Edge (Direction.Backward); } }

		#endregion
	}
}
