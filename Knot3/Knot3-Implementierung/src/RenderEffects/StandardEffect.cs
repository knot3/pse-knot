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
using GameObjects;
using Screens;
using KnotData;
using Widgets;

namespace RenderEffects
{
    /// <summary>
    /// Ein Rendereffekt, der 3D-Modelle mit dem von der XNA-Content-Pipeline standardmäßig zugewiesenen
    /// BasicEffect-Shader zeichnet und keinen Post-Processing-Effekt anwendet.
    /// </summary>
    public class StandardEffect : RenderEffect
    {

        #region Constructors

        /// <summary>
        /// Erstellt einen neuen Standardeffekt.
        /// </summary>
        public StandardEffect (GameScreen screen)
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

