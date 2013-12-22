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

using Core;
using GameObjects;
using Screens;
using RenderEffects;
using Widgets;

namespace KnotData
{
    /// <summary>
    /// Eine Position im 3D-Raster. Die Werte für alle drei Koordinaten sind Integer, wobei 1 die Breite der Raster-Abschnitte angibt.
    /// Eine Skalierung auf Koordinaten im 3D-Raum und damit einhergehend eine Konvertierung in ein Vector3-Objekt des XNA-Frameworks kann mit der Methode ToVector() angefordert werden.
    /// </summary>
    public class Node
    {

        #region Properties

        /// <summary>
        /// X steht für eine x-Koordinate im dreidimensionalen Raster.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y steht für eine y-Koordinate im dreidimensionalen Raster.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Z steht für eine z-Koordinate im dreidimensionalen Raster.
        /// </summary>
        public int Z { get; set; }

        /// <summary>
        /// Ein Skalierungswert.
        /// </summary>
        private int scale { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt eine neue Instanz eines Node-Objekts und initialisiert diese mit Werten
        /// für die x-, y- und z-Koordinate.
        /// </summary>
        public Node (int x, int y, int z)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Liefert die x-, y- und z-Koordinaten im 3D-Raum als ein Vektor3 der Form (x, y, z).
        /// </summary>
        public virtual Vector3 ToVector ( )
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

