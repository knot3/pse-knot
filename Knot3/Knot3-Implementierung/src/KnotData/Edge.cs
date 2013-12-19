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
using GameObjects;
using Screens;
using RenderEffects;
using Widgets;

namespace KnotData
{
    /// <summary>
    /// Eine Kante eines Knotens, die aus einer Richtung und einer Farbe, sowie optional einer Liste von Flächennummern besteht.
    /// </summary>
    public class Edge : 
    {

        #region Properties

        /// <summary>
        /// Die Farbe der Kante.
        /// </summary>
        public virtual Color Color { get; set; }

        /// <summary>
        /// Die Richtung der Kante.
        /// </summary>
        public virtual Direction Direction { get; set; }

        /// <summary>
        /// Die Liste der Flächennummern, die an die Kante angrenzen.
        /// </summary>
        public virtual List<int> Rectangles { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt eine neue Kante mit der angegebenen Richtung.
        /// </summary>
        public virtual void Edge (Direction direction)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gibt die Richtung als normalisierten Vektor3 zurück.
        /// </summary>
        public virtual Vector3 Get3DDirection ()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

