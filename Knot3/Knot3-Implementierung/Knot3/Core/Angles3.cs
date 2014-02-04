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

using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Core
{
	/// <summary>
	/// Diese Klasse repräsentiert die Rollwinkel der drei Achsen X, Y und Z.
	/// Sie bietet Möglichkeit vordefinierte Winkelwerte zu verwenden, z.B. stellt Zero den Nullvektor dar.
	/// Die Umwandlung zwischen verschiedenen Winkelmaßen wie Grad- und Bogenmaß unterstützt sie durch entsprechende Methoden.
	/// </summary>
	public sealed class Angles3 : IEquatable<Angles3>
	{
		#region Properties

		/// <summary>
		/// Der Winkel im Bogenmaß für das Rollen um die X-Achse. Siehe statische Methode Matrix.CreateRotationX(float) des XNA-Frameworks.
		/// </summary>
		public float X { get; set; }

		/// <summary>
		/// Der Winkel im Bogenmaß für das Rollen um die Y-Achse. Siehe statische Methode Matrix.CreateRotationY(float) des XNA-Frameworks.
		/// </summary>
		public float Y { get; set; }

		/// <summary>
		/// Der Winkel im Bogenmaß für das Rollen um die Z-Achse. Siehe statische Methode Matrix.CreateRotationZ(float) des XNA-Frameworks.
		/// </summary>
		public float Z { get; set; }

		/// <summary>
		/// Eine statische Eigenschaft mit dem Wert X = 0, Y = 0, Z = 0.
		/// </summary>
		public static Angles3 Zero
		{
			get { return new Angles3 (0f, 0f, 0f); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Konstruiert ein neues Angles3-Objekt mit drei gegebenen Winkeln im Bogenmaß.
		/// </summary>
		public Angles3 (float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Angles3 (Vector3 v)
		{
			X = v.X;
			Y = v.Y;
			Z = v.Z;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Eine statische Methode, die Grad in Bogenmaß konvertiert.
		/// </summary>
		public static Angles3 FromDegrees (float x, float y, float z)
		{
			return new Angles3 (
			           MathHelper.ToRadians (x),
			           MathHelper.ToRadians (y),
			           MathHelper.ToRadians (z)
			       );
		}

		/// <summary>
		/// Konvertiert Bogenmaß in Grad.
		/// </summary>
		public void ToDegrees (out float x, out float y, out float z)
		{
			x = (int)MathHelper.ToDegrees (X) % 360;
			y = (int)MathHelper.ToDegrees (Y) % 360;
			z = (int)MathHelper.ToDegrees (Z) % 360;
		}

		public override bool Equals (object obj)
		{
			return (obj is Angles3) ? this == (Angles3)obj : false;
		}

		public bool Equals (Angles3 other)
		{
			return this == other;
		}

		public override int GetHashCode ()
		{
			return (int)(this.X + this.Y + this.Z);
		}

		#endregion

		#region Operators

		public static bool operator == (Angles3 value1, Angles3 value2)
		{
			return value1.X == value2.X
			       && value1.Y == value2.Y
			       && value1.Z == value2.Z;
		}

		public static bool operator != (Angles3 value1, Angles3 value2)
		{
			return !(value1 == value2);
		}

		public static Angles3 operator + (Angles3 value1, Angles3 value2)
		{
			return new Angles3 (value1.X + value2.X, value1.Y + value2.Y, value1.Z + value2.Z);
		}

		public static Angles3 operator - (Angles3 value)
		{
			value = new Angles3 (-value.X, -value.Y, -value.Z);
			return value;
		}

		public static Angles3 operator - (Angles3 value1, Angles3 value2)
		{
			return new Angles3 (value1.X - value2.X, value1.Y - value2.Y, value1.Z - value2.Z);
		}

		public static Angles3 operator * (Angles3 value1, Angles3 value2)
		{
			return new Angles3 (value1.X * value2.X, value1.Y * value2.Y, value1.Z * value2.Z);
		}

		public static Angles3 operator * (Angles3 value, float scaleFactor)
		{
			return new Angles3 (value.X * scaleFactor, value.Y * scaleFactor, value.Z * scaleFactor);
		}

		public static Angles3 operator * (float scaleFactor, Angles3 value)
		{
			return new Angles3 (value.X * scaleFactor, value.Y * scaleFactor, value.Z * scaleFactor);
		}

		public static Angles3 operator / (Angles3 value1, Angles3 value2)
		{
			return new Angles3 (value1.X / value2.X, value1.Y / value2.Y, value1.Z / value2.Z);
		}

		public static Angles3 operator / (Angles3 value, float divider)
		{
			float scaleFactor = 1 / divider;
			return new Angles3 (value.X * scaleFactor, value.Y * scaleFactor, value.Z * scaleFactor);
		}

		public override string ToString()
		{
			float x, y, z;
			ToDegrees (out x, out y, out z);

			return   "Angles3("
			         + x.ToString()
			         + ","
			         + y.ToString()
			         + ","
			         + z.ToString()
			         + ")";
		}

		#endregion
	}
}
