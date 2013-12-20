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
    /// Diese Schnittstelle repräsentiert ein Spielobjekt und enthält eine Referenz auf die Spielwelt, in der sich dieses
    /// Game befindet, sowie Informationen zu dem Game.
    /// </summary>
    public interface IGameObject
    {

        #region Properties

        /// <summary>
        /// Informationen über das Spielobjekt, wie z.B. die Position.
        /// </summary>
        public GameObjectInfo Info { get; set; }

        /// <summary>
        /// Eine Referenz auf die Spielwelt, in der sich das Spielobjekt befindet.
        /// </summary>
        public World World { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Die Mitte des Spielobjektes im 3D-Raum.
        /// </summary>
        public virtual Vector3 Center ( )
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public virtual void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Zeichnet das Spielobjekt.
        /// </summary>
        public virtual void Draw (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Überprüft, ob der Mausstrahl das Spielobjekt schneidet.
        /// </summary>
        public virtual GameObjectDistance Intersects (Ray ray)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

