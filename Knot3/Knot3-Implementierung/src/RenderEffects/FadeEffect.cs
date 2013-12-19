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
using GameObjects;
using Screens;
using KnotData;
using Widgets;

namespace RenderEffects
{
    /// <summary>
    /// Ein Postprocessing-Effekt, der eine Überblendung zwischen zwei Spielzuständen darstellt.
    /// </summary>
    public class FadeEffect : RenderEffect
    {

        #region Properties

        /// <summary>
        /// Gibt an, ob die Überblendung abgeschlossen ist und das RenderTarget nur noch den neuen Spielzustand darstellt.
        /// </summary>
        private Boolean IsFinished { get; set; }

        /// <summary>
        /// Der zuletzt gerenderte Frame im bisherigen Spielzustand.
        /// </summary>
        private RenderTarget2D PreviousRenderTarget { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt einen Überblende-Effekt zwischen den angegebenen Spielzuständen.
        /// </summary>
        public void FadeEffect (GameScreen newScreen, GameScreen oldScreen)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// !!!
        /// </summary>
        protected virtual void DrawRenderTarget (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

