using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	public static class VectorHelper
	{
		private static readonly float MinAngleY = 0.1f;
		private static readonly float MaxAngleY = MathHelper.Pi - 0.1f;

		public static Vector3 ArcBallMove (this Vector3 position, Vector2 mouse, Vector3 up, Vector3 forward)
		{
			Vector3 side = Vector3.Normalize (Vector3.Cross (up, forward));
			//Vector3 relUp = Vector3.Normalize (Vector3.Cross (side, forward));

			// horizontal rotation
			float diffAngleX = MathHelper.Pi / 300f * mouse.X;
			Vector3 rotated = position.RotateAroundVector (up, diffAngleX);

			// vertical rotation
			float currentAngleY = position.AngleBetween (up);
			float diffAngleY = MathHelper.Pi / 200f * mouse.Y;
			if (currentAngleY + diffAngleY > MinAngleY && currentAngleY + diffAngleY < MaxAngleY) {
				rotated = rotated.RotateAroundVector (-side, diffAngleY);
			}
			Console.WriteLine ("currentAngleY = " + MathHelper.ToDegrees (currentAngleY) + ", "
			                   + "diffAngleY = " + MathHelper.ToDegrees (diffAngleY) + "°" + ", "
			                   + "position = " + position + ", " + "length=" + position.Length ()
			                  );

			return rotated;
		}

		public static Vector3 MoveLinear (this Vector3 vectorToMove, Vector3 mouse, Vector3 up, Vector3 forward)
		{
			Vector3 side = Vector3.Cross (up, forward);
			side.Normalize ();
			Vector3 relUp = Vector3.Cross (side, forward);
			relUp.Normalize ();
			Vector3 movedVector = vectorToMove - side * mouse.X - relUp * mouse.Y - forward * mouse.Z;
			return movedVector;
		}

		public static Vector3 MoveLinear (this Vector3 vectorToMove, Vector2 mouse, Vector3 up, Vector3 forward)
		{
			return vectorToMove.MoveLinear (new Vector3 (mouse.X, mouse.Y, 0), up, forward);
		}

		public static float AngleBetween (this Vector2 a, Vector2 b)
		{
			return ((b.X - a.X) > 0 ? 1 : -1)
			       * (float)Math.Acos ((double)Vector2.Dot (Vector2.Normalize (a), Vector2.Normalize (b)));
		}

		public static float AngleBetween (this Vector3 a, Vector3 b)
		{
			return //((b.X - a.X) > 0 ? 1 : -1) *
			    (float)Math.Acos ((double)Vector3.Dot (Vector3.Normalize (a), Vector3.Normalize (b)));
		}

		public static Vector3 RotateX (this Vector3 vectorToRotate, float angleRadians)
		{
			return Vector3.Transform (vectorToRotate, Matrix.CreateRotationX (angleRadians));
		}

		public static Vector3 RotateY (this Vector3 vectorToRotate, float angleRadians)
		{
			return Vector3.Transform (vectorToRotate, Matrix.CreateRotationY (angleRadians));
		}

		public static Vector3 RotateZ (this Vector3 vectorToRotate, float angleRadians)
		{
			return Vector3.Transform (vectorToRotate, Matrix.CreateRotationZ (angleRadians));
		}

		public static Vector3 RotateAroundVector (this Vector3 vectorToRotate, Vector3 axis, float angleRadians)
		{
			return Vector3.Transform (vectorToRotate, Quaternion.CreateFromAxisAngle (Vector3.Normalize (axis), angleRadians));
		}

		public static Vector3 Clamp (this Vector3 v, Vector3 lower, Vector3 higher)
		{
			return new Vector3 (
			           MathHelper.Clamp (v.X, lower.X, higher.X),
			           MathHelper.Clamp (v.Y, lower.Y, higher.Y),
			           MathHelper.Clamp (v.Z, lower.Z, higher.Z)
			       );
		}

		public static Vector3 Clamp (this Vector3 v, int minLength, int maxLength)
		{
			if (v.Length () < minLength) {
				return v * minLength / v.Length ();
			}
			else if (v.Length () > maxLength) {
				return v * maxLength / v.Length ();
			}
			else {
				return v;
			}
		}

		public static Vector2 PrimaryVector (this Vector2 v)
		{
			if (v.X.Abs () > v.Y.Abs ()) {
				return new Vector2 (v.X, 0);
			}
			else if (v.Y.Abs () > v.X.Abs ()) {
				return new Vector2 (0, v.Y);
			}
			else {
				return new Vector2 (v.X, 0);
			}
		}

		public static Vector3 PrimaryVector (this Vector3 v)
		{
			if (v.X.Abs () > v.Y.Abs () && v.X.Abs () > v.Z.Abs ()) {
				return new Vector3 (v.X, 0, 0);
			}
			else if (v.Y.Abs () > v.X.Abs () && v.Y.Abs () > v.Z.Abs ()) {
				return new Vector3 (0, v.Y, 0);
			}
			else if (v.Z.Abs () > v.Y.Abs () && v.Z.Abs () > v.X.Abs ()) {
				return new Vector3 (0, 0, v.Z);
			}
			else {
				return new Vector3 (v.X, 0, 0);
			}
		}

		public static Vector2 PrimaryDirection (this Vector2 v)
		{
			Vector2 vector = v.PrimaryVector ();
			return new Vector2 (Math.Sign (vector.X), Math.Sign (vector.Y));
		}

		public static Vector3 PrimaryDirection (this Vector3 v)
		{
			Vector3 vector = v.PrimaryVector ();
			return new Vector3 (Math.Sign (vector.X), Math.Sign (vector.Y), Math.Sign (vector.Z));
		}

		public static Vector3 PrimaryDirectionExcept (this Vector3 v, Vector3 wrongDirection)
		{
			Vector3 copy = v;
			if (wrongDirection.X != 0) {
				copy.X = 0;
			}
			else if (wrongDirection.Y != 0) {
				copy.Y = 0;
			}
			else if (wrongDirection.Z != 0) {
				copy.Z = 0;
			}
			return copy.PrimaryDirection ();
		}

		public static float Abs (this float v)
		{
			return Math.Abs (v);
		}

		public static float Clamp (this float v, int min, int max)
		{
			return MathHelper.Clamp (v, min, max);
		}

		public static BoundingSphere[] Bounds (this Model model)
		{
			BoundingSphere[] bounds = new BoundingSphere[model.Meshes.Count];
			int i = 0;
			foreach (ModelMesh mesh in model.Meshes) {
				bounds [i++] = mesh.BoundingSphere;
			}
			return bounds;
		}

		public static BoundingBox Bounds (this Vector3 a, Vector3 diff)
		{
			return new BoundingBox (a, a + diff);
		}

		public static BoundingSphere Scale (this BoundingSphere sphere, float scale)
		{
			return new BoundingSphere (sphere.Center, sphere.Radius * scale);
		}

		public static BoundingSphere Scale (this BoundingSphere sphere, Vector3 scale)
		{
			return new BoundingSphere (sphere.Center, sphere.Radius * scale.PrimaryVector ().Length ());
		}

		public static BoundingSphere Translate (this BoundingSphere sphere, Vector3 position)
		{
			return new BoundingSphere (Vector3.Transform (sphere.Center, Matrix.CreateTranslation (position)), sphere.Radius);
		}

		public static BoundingSphere Rotate (this BoundingSphere sphere, Angles3 rotation)
		{
			return new BoundingSphere (Vector3.Transform (sphere.Center, Matrix.CreateFromYawPitchRoll (rotation.Y, rotation.X, rotation.Z)), sphere.Radius);
		}

		public static BoundingBox Scale (this BoundingBox box, float scale)
		{
			return new BoundingBox (box.Min * scale, box.Max * scale);
		}

		public static BoundingBox Translate (this BoundingBox box, Vector3 position)
		{
			Matrix translation = Matrix.CreateTranslation (position);
			return new BoundingBox (Vector3.Transform (box.Min, translation), Vector3.Transform (box.Max, translation));
		}

		public static Vector2 ToVector2 (this MouseState screen)
		{
			return new Vector2 (screen.X, screen.Y);
		}

		public static Point ToPoint (this MouseState screen)
		{
			return new Point (screen.X, screen.Y);
		}

		public static Vector2 ToVector2 (this Viewport viewport)
		{
			return new Vector2 (viewport.Width, viewport.Height);
		}

		public static Vector2 Center (this Viewport viewport)
		{
			return new Vector2 (viewport.Width, viewport.Height) / 2;
		}

		public static Vector2 ToVector2 (this Point v)
		{
			return new Vector2 (v.X, v.Y);
		}

		public static Point ToPoint (this Vector2 v)
		{
			return new Point ((int)v.X, (int)v.Y);
		}

		public static Point Plus (this Point a, Point b)
		{
			return new Point (a.X + b.X, a.Y + b.Y);
		}

		public static string Join (this string delimiter, List<int> list)
		{
			StringBuilder builder = new StringBuilder ();
			foreach (int elem in list) {
				// Append each int to the StringBuilder overload.
				builder.Append (elem).Append (delimiter);
			}
			return builder.ToString ();
		}

		public static Vector2 ScaleFactor (this Viewport viewport)
		{
			Vector2 max = viewport.ToVector2 ();
			return max / 1000f;
		}

		public static Vector2 RelativeTo (this Vector2 v, Viewport viewport)
		{
			Vector2 max = viewport.ToVector2 ();
			return v / max;
		}

		public static Vector2 Scale (this Vector2 v, Viewport viewport)
		{
			Vector2 max = viewport.ToVector2 ();
			if (v.X > 1 || v.Y > 1) {
				return v / 1000f * max;
			}
			else {
				return v * max;
			}
		}

		public static Rectangle Scale (this Rectangle rect, Viewport viewport)
		{
			Point max = viewport.ToVector2 ().ToPoint ();
			return new Rectangle (
			           rect.X * max.X / 1000, rect.Y * max.Y / 1000,
			           rect.Width * max.X / 1000, rect.Height * max.Y / 1000
			       );
		}

		public static Rectangle Grow (this Rectangle rect, int x, int y)
		{
			return new Rectangle (rect.X - x, rect.Y - y, rect.Width + x * 2, rect.Height + y * 2);
		}

		public static Rectangle Shrink (this Rectangle rect, int x, int y)
		{
			return Grow (rect, -x, -y);
		}

		public static Rectangle Grow (this Rectangle rect, int xy)
		{
			return Grow (rect, xy, xy);
		}

		public static Rectangle Shrink (this Rectangle rect, int xy)
		{
			return Grow (rect, -xy, -xy);
		}

		public static Rectangle Translate (this Rectangle rect, int x, int y)
		{
			return new Rectangle (rect.X + x, rect.Y + y, rect.Width, rect.Height);
		}

		public static Rectangle Resize (this Rectangle rect, int w, int h)
		{
			return new Rectangle (rect.X, rect.Y, rect.Width + w, rect.Height + h);
		}

		public static void Swap<T> (ref T lhs, ref T rhs)
		{
			T temp;
			temp = lhs;
			lhs = rhs;
			rhs = temp;
		}

		public static string Print (this Vector3 v)
		{
			return "(" + v.X + "," + v.Y + "," + v.Z + ")";
		}

		public static BoundingSphere[] CylinderBounds (float length, float radius, Vector3 direction, Vector3 position)
		{
			float distance = radius / 4;
			BoundingSphere[] bounds = new BoundingSphere[(int)(length / distance)];
			for (int offset = 0; offset < (int)(length / distance); ++offset) {
				bounds [offset] = new BoundingSphere (position + direction * offset * distance, radius);
				//Console.WriteLine ("sphere[" + offset + "]=" + Bounds [offset]);
			}
			return bounds;
		}

		public static Rectangle CreateRectangle (this Vector2 topLeft, Vector2 size)
		{
			return CreateRectangle (0, topLeft.X, topLeft.Y, size.X, size.Y);
		}

		public static Rectangle CreateRectangle (this Vector2 topLeft, Vector2 size, int lineWidth)
		{
			return CreateRectangle (lineWidth, topLeft.X, topLeft.Y, size.X, size.Y);
		}

		public static Rectangle CreateRectangle (int lineWidth, float x, float y, float w, float h)
		{
			if (w == 0) {
				return new Rectangle ((int)x - lineWidth / 2, (int)y - lineWidth / 2, lineWidth, (int)h + lineWidth);
			}
			else if (h == 0) {
				return new Rectangle ((int)x - lineWidth / 2, (int)y - lineWidth / 2, (int)w + lineWidth, lineWidth);
			}
			else {
				return new Rectangle ((int)x, (int)y, (int)w, (int)h);
			}
		}

		public static T At<T> (this List<T> list, int index)
		{
			while (index < 0) {
				index += list.Count;
			}
			if (index >= list.Count) {
				index = index % list.Count;
			}
			return list [index];
		}

		public static T At<T> (this IEnumerable<T> list, int index)
		{
			int count = list.Count ();
			while (index < 0) {
				index += count;
			}
			if (index >= count) {
				index = index % count;
			}
			return list.ElementAt (index);
		}

		private static Random random = new Random (Guid.NewGuid ().GetHashCode ());

		public static int RandomIndex<T> (this IEnumerable<T> list)
		{
			int index = random.Next (list.Count ());
			return index;
		}

		public static T RandomElement<T> (this IEnumerable<T> list)
		{
			return list.At (list.RandomIndex ());
		}

		public static void SetCoordinates (this Widget widget, float left, float top, float right, float bottom)
		{
			widget.RelativePosition = () => new Vector2 (left, top);
			widget.RelativeSize = () => new Vector2 (right - left, bottom - top);
		}

		public static Dictionary<A, B> ReverseDictionary<A,B> (this Dictionary<B,A> dict)
		{
			return dict.ToDictionary (x => x.Value, x => x.Key);
		}

		public static float DistanceTo (this Vector3 origin, Vector3 target)
		{
			Vector3 toPosition = origin - target;
			return toPosition.Length ();
		}

		public static Vector3 SetDistanceTo (this Vector3 origin, Vector3 target, float distance)
		{
			Vector3 to = origin - target;
			float oldDistance = to.Length ();
			double scale = (double)distance / (double)to.Length ();
			if (Math.Abs (oldDistance) > 1 && Math.Abs (oldDistance - distance) > 1 && Math.Abs (distance) > 1) {
				return target + to * (float)scale;
			}
			else {
				return origin;
			}
		}
	}
}
