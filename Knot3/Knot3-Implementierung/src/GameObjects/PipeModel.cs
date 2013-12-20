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
    /// Ein 3D-Modell, das eine Kante darstellt.
    /// </summary>
    public class PipeModel : GameModel
    {

        #region Properties

        /// <summary>
        /// Enthält Informationen über die darzustellende Kante.
        /// </summary>
        public PipeModelInfo Info { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues 3D-Modell mit dem angegebenen Spielzustand und den angegebenen Spielinformationen.
        /// [base=screen, info]
        /// </summary>
        public PipeModel (GameScreen screen, PipeModelInfo info)
            : base(screen, info)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prüft, ob der angegebene Mausstrahl das 3D-Modell schneidet.
        /// </summary>
        public virtual GameObjectDistance Intersects (Ray ray)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

