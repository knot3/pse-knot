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
	/// Eine Wertesammlung der möglichen Richtungen in einem dreidimensionalen Raum.
	/// Wird benutzt, damit keine ungültigen Kantenrichtungen angegeben werden können.
	/// Dies ist eine Klasse und kein Enum, kann aber
	/// uneingeschränkt wie eines benutzt werden (Typesafe Enum Pattern).
	/// </summary>
	public sealed class Direction : IEquatable<Direction>
	{
		#region Enumeration Values

		/// <summary>
		/// Links.
		/// </summary>
		public static readonly Direction Left = new Direction (Vector3.Left, "Left");
		/// <summary>
		/// Rechts.
		/// </summary>
		public static readonly Direction Right = new Direction (Vector3.Right, "Right");
		/// <summary>
		/// Hoch.
		/// </summary>
		public static readonly Direction Up = new Direction (Vector3.Up, "Up");
		/// <summary>
		/// Runter.
		/// </summary>
		public static readonly Direction Down = new Direction (Vector3.Down, "Down");
		/// <summary>
		/// Vorwärts.
		/// </summary>
		public static readonly Direction Forward = new Direction (Vector3.Forward, "Forward");
		/// <summary>
		/// Rückwärts.
		/// </summary>
		public static readonly Direction Backward = new Direction (Vector3.Backward, "Backward");
		/// <summary>
		/// Keine Richtung.
		/// </summary>
		public static readonly Direction Zero = new Direction (Vector3.Zero, "Zero");

		#endregion

		#region Static Attributes

		public static readonly Direction[] Values = {
			Left, Right, Up, Down, Forward,	Backward
		};
		private static readonly Dictionary<Direction, Direction> ReverseMap
		    = new Dictionary<Direction, Direction> ()
		{
			{ Left, Right }, { Right, Left },
			{ Up, Down }, { Down, Up },
			{ Forward, Backward }, { Backward, Forward },
			{ Zero, Zero }
		};

		private static readonly Dictionary<Direction, Axis> AxisMap
		    = new Dictionary<Direction, Axis> ()
		{
			{ Left, Axis.X }, { Right, Axis.X },
			{ Up, Axis.Y }, { Down, Axis.Y },
			{ Forward, Axis.Z }, { Backward, Axis.Z },
			{ Zero, Axis.Zero }
		};

		#endregion

		#region Properties

		public Vector3 Vector { get; private set; }

		public string Description { get; private set; }

		public Direction Reverse { get { return ReverseMap [this]; } }

		public Axis Axis { get { return AxisMap[this]; } }

		#endregion

		#region Constructors

		private Direction (Vector3 vector, string desciption)
		{
			Vector = vector;
			Description = desciption;
		}

		#endregion

		#region Methods and Operators

		public static Direction FromAxis(Axis axis)
		{
			return axis == Axis.X ? Right : axis == Axis.Y ? Up : axis == Axis.Z ? Backward : Zero;
		}

		public static Direction FromString (string str)
		{
			foreach (Direction direction in Values) {
				if (str.ToLower() == direction.Description.ToLower()) {
					return direction;
				}
			}
			return null;
		}

		public override string ToString ()
		{
			return Description;
		}

		public static Vector3 operator + (Vector3 v, Direction d)
		{
			return v + d.Vector;
		}

		public static Vector3 operator - (Vector3 v, Direction d)
		{
			return v - d.Vector;
		}

		public static Vector3 operator / (Direction d, float i)
		{
			return d.Vector / i;
		}

		public static Vector3 operator * (Direction d, float i)
		{
			return d.Vector * i;
		}

		public static bool operator == (Direction a, Direction b)
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
			return a.Vector == b.Vector;
		}

		public static bool operator != (Direction d1, Direction d2)
		{
			return !(d1 == d2);
		}

		public bool Equals (Direction other)
		{
			return other != null && Vector == other.Vector;
		}

		public override bool Equals (object other)
		{
			if (other == null) {
				return false;
			}
			else if (other is Direction) {
				return Equals (other as Direction);
			}
			else if (other is Vector3) {
				return Vector.Equals ((Vector3)other);
			}
			else if (other is string) {
				return Description.Equals ((string)other);
			}
			else {
				return false;
			}
		}

		public static implicit operator string (Direction direction)
		{
			return direction.Description;
		}

		public static implicit operator Vector3 (Direction direction)
		{
			return direction.Vector;
		}

		public override int GetHashCode ()
		{
			return Description.GetHashCode ();
		}

		#endregion
	}

	public enum Axis {
		X, Y, Z, Zero
	}
}
