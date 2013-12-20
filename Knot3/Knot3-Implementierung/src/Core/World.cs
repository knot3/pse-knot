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
    /// Repräsentiert eine Spielwelt, in der sich 3D-Modelle befinden und gezeichnet werden können.
    /// </summary>
    public class World : DrawableGameScreenComponent
    {

        #region Properties

        /// <summary>
        /// Die Kamera dieser Spielwelt.
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// Die Liste von Spielobjekten.
        /// </summary>
        public List<IGameObject> Objects { get; set; }

        /// <summary>
        /// Das aktuell ausgewählte Spielobjekt.
        /// </summary>
        public IGameObject SelectedObject { get; set; }

        /// <summary>
        /// Der aktuell angewendete Rendereffekt.
        /// </summary>
        public IRenderEffect CurrentEffect { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt eine neue Spielwelt im angegebenen Spielzustand.
        /// </summary>
        public  World (GameScreen screen)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Ruft auf allen Spielobjekten die Update()-Methode auf.
        /// </summary>
        public virtual void Update (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Ruft auf allen Spielobjekten die Draw()-Methode auf.
        /// </summary>
        public virtual void Draw (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Liefert einen Enumerator über die Spielobjekte dieser Spielwelt.
        /// [returntype=IEnumerator<IGameObject>]
        /// </summary>
        public virtual IEnumerator<IGameObject> GetEnumerator ( )
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

