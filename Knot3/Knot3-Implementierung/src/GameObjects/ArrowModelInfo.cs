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
    /// Ein Objekt der Klasse ArrowModelInfo hält alle Informationen, die zur Erstellung eines Pfeil-3D-Modelles (s. ArrowModel) notwendig sind.
    /// </summary>
    public class ArrowModelInfo : GameModelInfo
    {

        #region Properties

        /// <summary>
        /// Gibt die Richtung, in die der Pfeil zeigen soll an.
        /// </summary>
        public virtual Vector3 Direction { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues ArrowModelInfo-Objekt an einer bestimmten Position position im 3D-Raum. Dieses zeigt in eine durch direction bestimmte Richtung.
        /// </summary>
        public virtual void ArrowModelInfo (Vector3 position, Vector3 direction)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

