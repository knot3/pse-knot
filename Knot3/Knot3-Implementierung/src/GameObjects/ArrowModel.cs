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
    /// Diese Klasse ArrowModel repräsentiert ein 3D-Modell für einen Pfeil, zum Einblenden an selektierten Kanten (s. Edge).
    /// </summary>
    public class ArrowModel : GameModel
    {

        #region Properties

        /// <summary>
        /// Das Info-Objekt, das die Position und Richtung des ArrowModel\grq s enthält.
        /// </summary>
        public ArrowModelInfo Info { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues Pfeilmodell in dem angegebenen GameScreen mit einem bestimmten Info-Objekt, das Position und Richtung des Pfeils festlegt.
        /// </summary>
        public void ArrowModel (GameScreen screen, ArrowModelInfo info)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Zeichnet den Pfeil.
        /// </summary>
        public void Draw (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Überprüft, ob der Mausstrahl den Pfeil schneidet.
        /// </summary>
        public GameObjectDistance Intersects (Ray ray)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public void Update (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

