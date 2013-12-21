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
    /// Ein Inputhandler, der für das Verschieben der Kanten zuständig ist.
    /// </summary>
    public class EdgeMovement : IGameObject, IEnumerable<IGameObject>
    {

        #region Properties

        /// <summary>
        /// Enthält Informationen über die Position des Knotens.
        /// </summary>
        public GameObjectInfo Info { get; set; }

        /// <summary>
        /// Der Knoten, dessen Kanten verschoben werden können.
        /// </summary>
        public Knot Knot { get; set; }

        /// <summary>
        /// Die Spielwelt, in der sich die 3D-Modelle der Kanten befinden.
        /// </summary>
        public World World { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt eine neue Instanz eines EdgeMovement-Objekts und initialisiert diese
        /// mit ihrem zugehörigen GameScreen-Objekt screen, der Spielwelt world und
        /// Objektinformationen info.
        /// </summary>
        public EdgeMovement (GameScreen screen, World world, GameObjectInfo info)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gibt den Ursprung des Knotens zurück.
        /// </summary>
        public virtual Vector3 Center ( )
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt immer \glqq null\grqq~zurück.
        /// </summary>
        public virtual GameObjectDistance Intersects (Ray Ray)
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
        /// Gibt einen Enumerator über die während einer Verschiebeaktion dynamisch erstellten 3D-Modelle zurück.
        /// [returntype=IEnumerator<IGameObject>]
        /// </summary>
        public virtual IEnumerator<IGameObject> GetEnumerator ( )
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Zeichnet die während einer Verschiebeaktion dynamisch erstellten 3D-Modelle.
        /// </summary>
        public virtual void Draw (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

