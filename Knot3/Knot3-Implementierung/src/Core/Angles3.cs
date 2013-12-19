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

using GameObjects;
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace Core
{
    /// <summary>
    /// Diese Klasse repräsentiert die Rotationswinkel der drei Achsen X, Y und Z.
    /// Sie bietet Möglichkeit vordefinierte Winkelwerte zu verwenden, z.B. stellt Zero den Nullvektor dar.
    /// Die Umwandlung zwischen verschiedenen Winkelmaßen wie Grad- und Bogenmaß unterstützt sie durch entsprechende Methoden.
    /// </summary>
    public class Angles3 : 
    {

        #region Properties

        /// <summary>
        /// Der Rotationswinkel um die X-Achse.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Der Rotationswinkel um die Y-Achse.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Der Rotationswinkel um die Z-Achse.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Eine statische Eigenschaft mit dem Wert X = 0, Y = 0, Z = 0.
        /// </summary>
        public Angles3 Zero { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Konstruiert ein neues Angles3-Objekt mit drei gegebenen Winkeln.
        /// </summary>
        public void Angles3 (float X, float Y, float Z)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Konvertiert Grad in Bogenmaß.
        /// </summary>
        public Angles3 FromDegrees (float X, float Y, float Z)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Konvertiert Bogenmaß in Grad.
        /// </summary>
        public void ToDegrees (float X, float Y, float Z)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

