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
using Knot3.Utilities;

namespace Knot3.GameObjects
{
	/// <summary>
	/// Ein Objekt der Klasse ArrowModelInfo hält alle Informationen, die zur Erstellung eines Pfeil-3D-Modelles (s. ArrowModel) notwendig sind.
	/// </summary>
	public sealed class ArrowModelInfo : GameModelInfo
	{
        #region Properties

		/// <summary>
		/// Gibt die Richtung, in die der Pfeil zeigen soll an.
		/// </summary>
		public Direction Direction { get; private set; }

		public float Length { get { return 40f; } }

		public float Diameter { get { return 8f; } }

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt ein neues ArrowModelInfo-Objekt an einer bestimmten Position position im 3D-Raum. Dieses zeigt in eine durch direction bestimmte Richtung.
		/// </summary>
		public ArrowModelInfo (Vector3 position, Direction direction)
			: base("arrow")
		{
			Direction = direction;
			Position = position + Direction.ToVector3 () * Node.Scale / 3;
			//Scale = new Vector3 (Diameter, Diameter, Length / 10f);
			Scale = Vector3.One * 15;
			IsMovable = true;
		}

        #endregion
	}
}

