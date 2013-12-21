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
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace GameObjects
{
    /// <summary>
    /// Diese Schnittstelle repräsentiert ein Spielobjekt und enthält eine Referenz auf die Spielwelt, in der sich dieses
    /// Game befindet, sowie Informationen zu dem Game.
    /// </summary>
    public interface IGameObject
    {

        #region Properties

        /// <summary>
        /// Informationen über das Spielobjekt, wie z.B. die Position.
        /// </summary>
        GameObjectInfo Info { get; set; }

        /// <summary>
        /// Eine Referenz auf die Spielwelt, in der sich das Spielobjekt befindet.
        /// </summary>
        World World { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Die Mitte des Spielobjektes im 3D-Raum.
        /// </summary>
        Vector3 Center ( );

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        void Update (GameTime time);

        /// <summary>
        /// Zeichnet das Spielobjekt.
        /// </summary>
        void Draw (GameTime time);

        /// <summary>
        /// Überprüft, ob der Mausstrahl das Spielobjekt schneidet.
        /// </summary>
        GameObjectDistance Intersects (Ray ray);

        #endregion

    }
}

