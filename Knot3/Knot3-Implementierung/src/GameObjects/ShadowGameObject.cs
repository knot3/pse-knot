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
    /// Eine abstrakte Klasse, die ein Vorschau-Spielobjekt darstellt.
    /// </summary>
    public class ShadowGameObject : IGameObject
    {

        #region Properties

        /// <summary>
        /// Enthält Informationen über das Vorschau-Spielobjekt.
        /// </summary>
        public GameObjectInfo Info { get; set; }

        /// <summary>
        /// Eine Referenz auf die Spielwelt, in der sich das Spielobjekt befindet.
        /// </summary>
        public World World { get; set; }

        /// <summary>
        /// Die Position, an der das Vorschau-Spielobjekt gezeichnet werden soll.
        /// </summary>
        public Vector3 ShadowPosition { get; set; }

        /// <summary>
        /// Die Position, an der sich das zu dekorierende Objekt befindet.
        /// </summary>
        public Vector3 OriginalPosition { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues Vorschauobjekt in dem angegebenen Spielzustand für das angegebene zu dekorierende Objekt.
        /// </summary>
        public ShadowGameObject (GameScreen screen, IGameObject decoratedObj)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Die Position, an der das Vorschau-Spielobjekt gezeichnet werden soll.
        /// </summary>
        public virtual Vector3 Center ( )
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public virtual void Update (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Zeichnet das Vorschau-Spielobjekt.
        /// </summary>
        public virtual void Draw (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Prüft, ob der angegebene Mausstrahl das Vorschau-Spielobjekt schneidet.
        /// </summary>
        public virtual GameObjectDistance Intersects (Ray Ray)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

