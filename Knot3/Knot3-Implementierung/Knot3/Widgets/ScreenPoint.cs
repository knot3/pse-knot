using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Knot3.Core;
using Knot3.Utilities;

namespace Knot3.Widgets
{
	public class ScreenPoint : IEquatable<ScreenPoint>
	{
		#region Properties

		public IGameScreen Screen { get; private set; }

		public Vector2 Relative
		{
			get {
				return RelativeFunc ();
			}
			set {
				RelativeFunc = () => value;
			}
		}

		public Func<Vector2> RelativeFunc
		{
			set;
			private get;
		}

		public Point Absolute
		{
			get {
				return Relative.Scale (Screen.Viewport).ToPoint ();
			}
		}

		public ScreenPoint OnlyX
		{
			get {
				return new ScreenPoint (Screen, () => new Vector2 (RelativeFunc ().X, 0));
			}
		}

		public ScreenPoint OnlyY
		{
			get {
				return new ScreenPoint (Screen, () => new Vector2 (0, RelativeFunc ().Y));
			}
		}
		
		#endregion

		#region Constructors

		public ScreenPoint (IGameScreen screen, Func<Vector2> func)
		{
			Screen = screen;
			RelativeFunc = func;
		}

		public ScreenPoint (IGameScreen screen, Vector2 vector)
		{
			Screen = screen;
			Relative = vector;
		}

		public ScreenPoint (IGameScreen screen, float x, float y)
		{
			Screen = screen;
			Relative = new Vector2 (x, y);
		}

		public ScreenPoint (IGameScreen screen, Func<float> x, Func<float> y)
		{
			Screen = screen;
			RelativeFunc = () => new Vector2 (x (), y ());
		}

		#endregion

		#region Methods and Operators

		public void Assign (ScreenPoint other)
		{
			Screen = other.Screen;
			RelativeFunc = other.RelativeFunc;
		}

		public static ScreenPoint TopLeft (IGameScreen screen)
		{
			return new ScreenPoint (screen, Vector2.Zero);
		}

		public static ScreenPoint BottomRight (IGameScreen screen)
		{
			return new ScreenPoint (screen, Vector2.One);
		}

		public static ScreenPoint Centered (IGameScreen screen, Bounds sizeOf)
		{
			return new ScreenPoint (screen, () => (ScreenPoint.BottomRight (screen) - sizeOf.Size) / 2);
		}

		public static implicit operator Vector2 (ScreenPoint point)
		{
			return point.Relative;
		}

		public static implicit operator Func<Vector2> (ScreenPoint point)
		{
			return point.RelativeFunc;
		}

		public static implicit operator Point (ScreenPoint point)
		{
			return point.Absolute;
		}
		
		public override string ToString ()
		{
			return "(" + Relative.X + "x" + Relative.Y + ")";
		}

		public static ScreenPoint operator * (ScreenPoint a, float b)
		{
			return new ScreenPoint (a.Screen, () => a.Relative * b);
		}

		public static ScreenPoint operator * (ScreenPoint a, ScreenPoint b)
		{
			return new ScreenPoint (a.Screen, () => new Vector2 (a.Relative.X * b.Relative.X, a.Relative.X * b.Relative.Y));
		}

		public static ScreenPoint operator / (ScreenPoint a, float b)
		{
			return new ScreenPoint (a.Screen, () => a.Relative / b);
		}

		public static ScreenPoint operator + (ScreenPoint a, ScreenPoint b)
		{
			return new ScreenPoint (a.Screen, () => a.Relative + b.Relative);
		}

		public static ScreenPoint operator - (ScreenPoint a, ScreenPoint b)
		{
			return new ScreenPoint (a.Screen, () => a.Relative - b.Relative);
		}

		public static bool operator == (ScreenPoint a, ScreenPoint b)
		{
			if (System.Object.ReferenceEquals (a, b)) {
				return true;
			}
			if (((object)a == null) || ((object)b == null)) {
				return false;
			}
			return a.Equals (b);
		}

		public static bool operator != (ScreenPoint d1, ScreenPoint d2)
		{
			return !(d1 == d2);
		}

		public bool Equals (ScreenPoint other)
		{
			return other != null && Relative == other.Relative;
		}

		public override bool Equals (object other)
		{
			if (other == null) {
				return false;
			}
			else if (other is Vector2) {
				return Relative.Equals ((Vector2)other);
			}
			else if (other is Point) {
				return Absolute.Equals ((Point)other);
			}
			else if (other is string) {
				return ToString ().Equals ((string)other);
			}
			else {
				return false;
			}
		}

		public override int GetHashCode ()
		{
			return Relative.GetHashCode ();
		}

		#endregion
	}
}

