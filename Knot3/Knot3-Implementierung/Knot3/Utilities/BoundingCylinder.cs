using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

using Microsoft.Xna.Framework;

namespace Knot3.Knot3.Utilities
{
	public struct BoundingCylinder : IEquatable<BoundingCylinder> {
		public Vector3 SideA;
		public Vector3 SideB;
		public float Radius;

		public BoundingCylinder (Vector3 sideA, Vector3 sideB, float radius)
		{
			this.SideA = sideA;
			this.SideB = sideB;
			this.Radius = radius;
		}

		public static bool operator == (BoundingCylinder a, BoundingCylinder b)
		{
			if (System.Object.ReferenceEquals (a, b)) {
				return true;
			}
			if (((object)a == null) || ((object)b == null)) {
				return false;
			}
			return a.Equals (b);
		}

		public static bool operator != (BoundingCylinder a, BoundingCylinder b)
		{
			return !(a == b);
		}

		public bool Equals (BoundingCylinder other)
		{
			return SideA == other.SideA && SideB == other.SideB && Radius == other.Radius;
		}

		public override bool Equals (object other)
		{
			return other != null && Equals ((BoundingCylinder)other);
		}

		public override int GetHashCode ()
		{
			// irgendwas möglichst eindeutiges
			return (Radius * (SideA + SideB)).GetHashCode ();
		}
	}
}
