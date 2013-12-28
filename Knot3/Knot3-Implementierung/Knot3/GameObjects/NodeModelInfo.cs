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
    /// Enthält Informationen über ein 3D-Modell, das einen Kantenübergang darstellt.
    /// </summary>
    public sealed class NodeModelInfo : GameModelInfo, IJunction
    {
        #region Properties

        /// <summary>
        /// Die Kante vor dem Übergang.
        /// </summary>
        public Edge EdgeFrom { get; set; }

        /// <summary>
        /// Die Kante nach dem Übergang.
        /// </summary>
        public Edge EdgeTo { get; set; }

        /// <summary>
        /// Der Knoten, der die Kanten enthält.
        /// </summary>
        public Knot Knot { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues Informationsobjekt für ein 3D-Modell, das einen Kantenübergang darstellt.
        /// [base="node1", Angles3.Zero, new Vector3(1,1,1)]
        /// </summary>
        public NodeModelInfo (NodeMap nodeMap, Knot knot, Edge from, Edge to)
            : base("node1", Angles3.Zero, Vector3.One * 5f)
        {
			EdgeFrom = from;
			EdgeTo = to;
			IsVisible = EdgeFrom.Direction != EdgeTo.Direction;
			Position = nodeMap.To (EdgeFrom).ToVector ();
        }

        #endregion

		#region Methods

		public override bool Equals (GameObjectInfo other)
		{
			if (other == null) 
				return false;

			if (other is NodeModelInfo) {
				if (this.EdgeFrom == (other as NodeModelInfo).EdgeFrom
				    && this.EdgeTo == (other as NodeModelInfo).EdgeTo
				    && base.Equals (other))
					return true;
				else
					return false;
			} else {
				return base.Equals (other);
			}
		}

		#endregion
    }
}

