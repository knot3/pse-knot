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
    /// Enthält Informationen über ein 3D-Modell, das einen Kantenübergang darstellt.
    /// </summary>
    public class NodeModelInfo : GameModelInfo
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

        /// <summary>
        /// Die Position des Übergangs.
        /// </summary>
        public Vector3 Position { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues Informationsobjekt für ein 3D-Modell, das einen Kantenübergang darstellt.
        /// </summary>
        public  NodeModelInfo (Knot knot, Edge from, Edge to)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

