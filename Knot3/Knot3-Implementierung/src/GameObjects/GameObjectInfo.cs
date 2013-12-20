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
    /// Enthält Informationen über ein 3D-Objekt wie die Position, Sichtbarkeit, Verschiebbarkeit und Auswählbarkeit.
    /// </summary>
    public class GameObjectInfo
    {

        #region Properties

        /// <summary>
        /// Die Verschiebbarkeit des Spielobjektes.
        /// </summary>
        public Boolean IsMovable { get; set; }

        /// <summary>
        /// Die Auswählbarkeit des Spielobjektes.
        /// </summary>
        public Boolean IsSelectable { get; set; }

        /// <summary>
        /// Die Sichtbarkeit des Spielobjektes.
        /// </summary>
        public Boolean IsVisible { get; set; }

        /// <summary>
        /// Die Position des Spielobjektes.
        /// </summary>
        public Vector3 Position { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Vergleicht zwei Informationsobjekte für Spielobjekte.
        /// [parameters=GameObjectInfo other]
        /// </summary>
        public virtual Boolean Equals (GameObjectInfo other)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

