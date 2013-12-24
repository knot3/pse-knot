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
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Utilities
{
	public static class FrustumHelper
	{
		public static Vector3 GetNegativeVertex (this BoundingBox aabb, ref Vector3 normal)
		{
			Vector3 p = aabb.Max;
			if (normal.X >= 0)
				p.X = aabb.Min.X;
			if (normal.Y >= 0)
				p.Y = aabb.Min.Y;
			if (normal.Z >= 0)
				p.Z = aabb.Min.Z;

			return p;
		}

		public static Vector3 GetPositiveVertex (this BoundingBox aabb, ref Vector3 normal)
		{
			Vector3 p = aabb.Min;
			if (normal.X >= 0)
				p.X = aabb.Max.X;
			if (normal.Y >= 0)
				p.Y = aabb.Max.Y;
			if (normal.Z >= 0)
				p.Z = aabb.Max.Z;

			return p;
		}

		public static bool FastIntersects (this BoundingFrustum boundingfrustum, ref BoundingSphere aabb)
		{
			if (boundingfrustum == null)
				return false;
			var box = BoundingBox.CreateFromSphere (aabb);
			return boundingfrustum.FastIntersects (ref box);
		}

		public static bool FastIntersects (this BoundingFrustum boundingfrustum, ref BoundingBox aabb)
		{
			if (boundingfrustum == null)
				return false; 

			Plane plane;
			Vector3 normal, p;

			plane = boundingfrustum.Bottom;
			normal = plane.Normal;
			normal.X = -normal.X;
			normal.Y = -normal.Y;
			normal.Z = -normal.Z;
			p = aabb.GetPositiveVertex (ref normal);
			if (-plane.D + normal.X * p.X + normal.Y * p.Y + normal.Z * p.Z < 0)
				return false;

			plane = boundingfrustum.Far;
			normal = plane.Normal;
			normal.X = -normal.X;
			normal.Y = -normal.Y;
			normal.Z = -normal.Z;
			p = aabb.GetPositiveVertex (ref normal);
			if (-plane.D + normal.X * p.X + normal.Y * p.Y + normal.Z * p.Z < 0)
				return false;

			plane = boundingfrustum.Left;
			normal = plane.Normal;
			normal.X = -normal.X;
			normal.Y = -normal.Y;
			normal.Z = -normal.Z;
			p = aabb.GetPositiveVertex (ref normal);
			if (-plane.D + normal.X * p.X + normal.Y * p.Y + normal.Z * p.Z < 0)
				return false;

			plane = boundingfrustum.Near;
			normal = plane.Normal;
			normal.X = -normal.X;
			normal.Y = -normal.Y;
			normal.Z = -normal.Z;
			p = aabb.GetPositiveVertex (ref normal);
			if (-plane.D + normal.X * p.X + normal.Y * p.Y + normal.Z * p.Z < 0)
				return false;

			plane = boundingfrustum.Right;
			normal = plane.Normal;
			normal.X = -normal.X;
			normal.Y = -normal.Y;
			normal.Z = -normal.Z;
			p = aabb.GetPositiveVertex (ref normal);
			if (-plane.D + normal.X * p.X + normal.Y * p.Y + normal.Z * p.Z < 0)
				return false;

			plane = boundingfrustum.Top;
			normal = plane.Normal;
			normal.X = -normal.X;
			normal.Y = -normal.Y;
			normal.Z = -normal.Z;
			p = aabb.GetPositiveVertex (ref normal);
			if (-plane.D + normal.X * p.X + normal.Y * p.Y + normal.Z * p.Z < 0)
				return false;

			return true;
		}

	}
}

