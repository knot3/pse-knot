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
	public static class DirectionHelper
	{
		public static Direction ToDirection (this Vector3 v)
		{
			if (v == Vector3.Up)
				return Direction.Up;
			else if (v == Vector3.Down)
				return Direction.Down;
			else if (v == Vector3.Left)
				return Direction.Left;
			else if (v == Vector3.Right)
				return Direction.Right;
			else if (v == Vector3.Forward)
				return Direction.Forward;
			else if (v == Vector3.Backward)
				return Direction.Backward;
			else
				return Direction.Zero;
		}

		public static Vector3 ToVector3 (this Direction d)
		{
			if (d == Direction.Up)
				return Vector3.Up;
			else if (d == Direction.Down)
				return Vector3.Down;
			else if (d == Direction.Left)
				return Vector3.Left;
			else if (d == Direction.Right)
				return Vector3.Right;
			else if (d == Direction.Forward)
				return Vector3.Forward;
			else if (d == Direction.Backward)
				return Vector3.Backward;
			else 
				return Vector3.Zero;
		}

		public static Direction ReverseDirection (this Direction dir)
		{
			return (-dir.ToVector3 ()).ToDirection ();
		}

		private static Direction[] allDirections = new Direction[]{
			Direction.Up, Direction.Down, Direction.Left, Direction.Right, Direction.Forward, Direction.Backward
		};

		public static Direction[] AllDirections ()
		{
			return allDirections;
		}
	}
}
