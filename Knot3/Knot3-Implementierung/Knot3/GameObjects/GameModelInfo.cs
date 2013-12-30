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
    /// Enthält Informationen über ein 3D-Modell wie den Dateinamen, die Rotation und die Skalierung.
    /// </summary>
    public abstract class GameModelInfo : GameObjectInfo
    {
        #region Properties

        /// <summary>
        /// Der Dateiname des Modells.
        /// </summary>
        public string Modelname { get; set; }

        /// <summary>
        /// Die Rotation des Modells.
        /// </summary>
        public Angles3 Rotation { get; set; }

        /// <summary>
        /// Die Skalierung des Modells.
        /// </summary>
        public Vector3 Scale { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues Informations-Objekt eines 3D-Modells mit den angegebenen Informationen zu
        /// Dateiname, Rotation und Skalierung.
        /// </summary>
        public GameModelInfo (string modelname, Angles3 rotation, Vector3 scale)
        {
        }

        /// <summary>
        /// Erzeugt eine neue Instanz eines GameModelInfo-Objekts.
        /// In modelname wird der Name der Datei angegeben, welche das Model repräsentiert.
        /// </summary>
        public GameModelInfo (string modelname)
        {
        }

        #endregion
    }
}

