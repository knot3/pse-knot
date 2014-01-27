using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Knot3.Utilities;
using Knot3.Core;

namespace Knot3.Widgets
{
	public class Bounds
	{
		#region Properties
		
		/// <summary>
		/// Die von der Auflösung unabhängige Position in Prozent.
		/// </summary>
		public ScreenPoint Position
		{
			get { return _position; }
			set { _position.Assign (value); }
		}

		private ScreenPoint _position;

		/// <summary>
		/// Die von der Auflösung unabhängige Größe in Prozent.
		/// </summary>
		public ScreenPoint Size
		{
			get { return _size; }
			set { _size.Assign (value); }
		}

		private ScreenPoint _size;

		/// <summary>
		/// Der von der Auflösung unabhängige Abstand in Prozent.
		/// </summary>
		public ScreenPoint Padding
		{
			get { return _padding; }
			set { _padding.Assign (value); }
		}

		private ScreenPoint _padding;

		/// <summary>
		/// Gibt ein auf die Auflösujng skaliertes Rechteck zurück, das in den XNA-Klassen verwendet werden kann.
		/// </summary>
		public Rectangle Rectangle
		{
			get {
				Point pos = Position.Absolute;
				Point size = Size.Absolute;
				return new Rectangle (pos.X, pos.Y, size.X, size.Y);
			}
		}
		
		#endregion

		#region Constructors

		public Bounds (ScreenPoint position, ScreenPoint size, ScreenPoint padding)
		{
			_position = position;
			_size = size;
			_padding = padding;
		}

		public Bounds (ScreenPoint position, ScreenPoint size)
		{
			_position = position;
			_size = size;
			_padding = new ScreenPoint (position.Screen, Vector2.Zero);
		}

		#endregion

		#region Methods and Operators

		public static Bounds Zero (IGameScreen screen)
		{
			return new Bounds (
				position: new ScreenPoint (screen, Vector2.Zero),
				size: new ScreenPoint (screen, Vector2.Zero),
				padding: new ScreenPoint (screen, Vector2.Zero)
			);
		}

		public Bounds FromLeft (Func<float> percent)
		{
			return new Bounds (
				position: Position,
				size: new ScreenPoint (Size.Screen, () => Size.Relative.X * percent (), () => Size.Relative.Y),
				padding: Padding
			);
		}

		public Bounds FromRight (Func<float> percent)
		{
			return new Bounds (
				position: Position + new ScreenPoint (Size.Screen, () => Size.Relative.X * (1f - percent ()), () => 0),
				size: new ScreenPoint (Size.Screen, () => Size.Relative.X * percent (), () => Size.Relative.Y),
				padding: Padding
			);
		}

		public Bounds FromLeft (float percent)
		{
			return FromLeft (() => percent);
		}

		public Bounds FromRight (float percent)
		{
			return FromRight (() => percent);
		}

		public static implicit operator Rectangle (Bounds bounds)
		{
			return bounds.Rectangle;
		}

		public override string ToString ()
		{
			return "(" + Position.Relative.X + "x" + Position.Relative.Y + Size.Relative.X + "x" + Size.Relative.Y + ")";
		}

		#endregion
	}
}

