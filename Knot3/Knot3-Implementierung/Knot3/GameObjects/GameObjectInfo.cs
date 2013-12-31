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

namespace Knot3.GameObjects
{
	/// <summary>
	/// Enthält Informationen über ein 3D-Objekt wie die Position, Sichtbarkeit, Verschiebbarkeit und Auswählbarkeit.
	/// </summary>
	public class GameObjectInfo : IEquatable<GameObjectInfo>
	{
        #region Properties

		/// <summary>
		/// Die Verschiebbarkeit des Spielobjektes.
		/// </summary>
		public Boolean IsMovable { get; set; }

		/// <summary>
		/// Die Auswählbarkeit des Spielobjektes.
		/// </summary>
		public Boolean IsSelectable { get; set; }

		/// <summary>
		/// Die Sichtbarkeit des Spielobjektes.
		/// </summary>
		public Boolean IsVisible { get; set; }

		/// <summary>
		/// Die Position des Spielobjektes.
		/// </summary>
		public Vector3 Position { get; set; }

        #endregion

		#region Constructors

		public GameObjectInfo ()
		{
			Position = Vector3.Zero;
			IsVisible = true;
			IsSelectable = true;
			IsMovable = false;
		}

		#endregion

        #region Methods

		/// <summary>
		/// Vergleicht zwei Informationsobjekte für Spielobjekte.
		/// [parameters=GameObjectInfo other]
		/// </summary>
		public virtual bool Equals (GameObjectInfo other)
		{
			if (other == null) 
				return false;

			if (this.Position == other.Position)
				return true;
			else
				return false;
		}

		public override bool Equals (Object obj)
		{
			GameObjectInfo infoObj = obj as GameObjectInfo;
			return Equals (infoObj);
		}

		public override int GetHashCode ()
		{
			return Position.GetHashCode ();
		}

		public static bool operator == (GameObjectInfo o1, GameObjectInfo o2)
		{
			if ((object)o1 == null || ((object)o2) == null)
				return Object.Equals (o1, o2);

			return o2.Equals (o2);
		}

		public static bool operator != (GameObjectInfo o1, GameObjectInfo o2)
		{
			return !(o1 == o2);
		}

        #endregion
	}
}

