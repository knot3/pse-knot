using System;
using System.Collections.Generic;
using System.Linq;

using System;
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

using Core;
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace GameObjects
{
    /// <summary>
    /// Enthält Informationen über ein 3D-Modell, das eine Kante darstellt.
    /// </summary>
    public class PipeModelInfo : GameModelInfo
    {

        #region Properties

        /// <summary>
        /// Die Kante, die durch das 3D-Modell dargestellt wird.
        /// </summary>
        public Edge Edge { get; set; }

        /// <summary>
        /// Der Knoten, der die Kante enthält.
        /// </summary>
        public Knot Knot { get; set; }

        /// <summary>
        /// Die Position, an der die Kante beginnt.
        /// </summary>
        public Vector3 PositionFrom { get; set; }

        /// <summary>
        /// Die Position, an der die Kante endet.
        /// </summary>
        public Vector3 PositionTo { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues Informationsobjekt für ein 3D-Modell, das eine Kante darstellt.
        /// </summary>
        public void PipeModelInfo (Knot knot, Edge edge)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

