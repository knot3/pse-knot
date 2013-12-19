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
    /// Enthält Informationen über ein 3D-Modell wie den Dateinamen, die Rotation und die Skalierung.
    /// </summary>
    public class GameModelInfo : GameObjectInfo
    {

        #region Properties

        /// <summary>
        /// Der Dateiname des Modells.
        /// </summary>
        public virtual String Modelname { get; set; }

        /// <summary>
        /// Die Rotation des Modells.
        /// </summary>
        public virtual Angles3 Rotation { get; set; }

        /// <summary>
        /// Die Skalierung des Modells.
        /// </summary>
        public virtual Vector3 Scale { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues Informations-Objekt eines 3D-Modells mit den angegebenen Informationen zu
        /// Dateiname, Rotation und Skalierung.
        /// </summary>
        public virtual void GameModelInfo (String modelname, Angles3 rotation, Vector3 scale)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

