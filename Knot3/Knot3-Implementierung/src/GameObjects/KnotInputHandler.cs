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
    /// Verarbeitet die Maus- und Tastatureingaben des Spielers und modifiziert die Kamera-Position
    /// und das Kamera-Ziel.
    /// </summary>
    public class KnotInputHandler : GameScreenComponent
    {

        #region Properties

        /// <summary>
        /// Die Spielwelt.
        /// </summary>
        private World world { get; set; }

        /// <summary>
        /// Der Spielzustand.
        /// </summary>
        private GameScreen screen { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt einen neuen KnotInputHandler für den angegebenen Spielzustand und die angegebene Spielwelt.
        /// </summary>
        public KnotInputHandler (GameScreen screen, World world)
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

