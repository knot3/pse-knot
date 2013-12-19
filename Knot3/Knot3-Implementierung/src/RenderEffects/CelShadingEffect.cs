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
    /// Ein Cel-Shading-Effekt.
    /// </summary>
    public class CelShadingEffect : RenderEffect
    {

        #region Constructors

        /// <summary>
        /// Erstellt einen neuen Cel-Shading-Effekt für den angegebenen GameScreen.
        /// </summary>
        public void CelShadingEffect (GameScreen screen)
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

        /// <summary>
        /// Zeichnet ein 3D-Modell auf das Rendertarget.
        /// </summary>
        public virtual void DrawModel (GameTime, GameModel GameModel)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Weist dem 3D-Modell den Cel-Shader zu.
        /// </summary>
        public virtual void RemapModel (GameModel GameModel)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

