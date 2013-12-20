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
    /// Ein Inputhandler, der Mauseingaben auf 3D-Modellen verarbeitet.
    /// </summary>
    public class ModelMouseHandler : GameScreenComponent
    {

        #region Constructors

        /// <summary>
        /// Erzeugt eine neue Instanz eines ModelMouseHandler-Objekts und ordnet dieser ein GameScreen-Objekt screen zu,
        /// sowie eine Spielwelt world.
        /// </summary>
        public  ModelMouseHandler (GameScreen screen, World world)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public virtual void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

