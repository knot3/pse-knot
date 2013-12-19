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
    /// Ein 3D-Modell, das einen Kantenübergang darstellt.
    /// </summary>
    public class NodeModel : GameModel
    {

        #region Properties

        /// <summary>
        /// Enthält Informationen über den darzustellende 3D-Modell des Kantenübergangs.
        /// </summary>
        public NodeModelInfo Info { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues 3D-Modell mit dem angegebenen Spielzustand und dem angegebenen Informationsobjekt.
        /// </summary>
        public void NodeModel (GameScreen screen, NodeModelInfo info)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Zeichnet das 3D-Modell mit dem aktuellen Rendereffekt.
        /// </summary>
        public void Draw (GameTime GameTime)
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

