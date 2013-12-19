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
    /// Ein Zwischenspeicher für 3D-Modelle.
    /// </summary>
    public class ModelFactory : 
    {

        #region Properties

        /// <summary>
        /// Die Zuordnung zwischen den Modellinformationen zu den 3D-Modellen.
        /// </summary>
        private Dictionary<GameModelInfo, GameModel> cache { get; set; }

        /// <summary>
        /// Ein Delegate, das beim Erstellen eines Zwischenspeichers zugewiesen wird und aus den
        /// angegebenen Modellinformationen und dem angegebenen Spielzustand ein 3D-Modell erstellt.
        /// </summary>
        private Func<GameScreen, GameModelInfo, GameModel> createModel { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt einen neuen Zwischenspeicher.
        /// </summary>
        public void ModelFactory (GameModelInfo, GameModel>, Func<GameScreen createModel)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Falls das 3D-Modell zwischengespeichert ist, wird es zurückgegeben, sonst mit createModel() erstellt.
        /// </summary>
        public GameModel this (GameScreen state, GameModelInfo info)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

