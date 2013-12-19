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
    /// Ein Stapel, der während der Draw-Aufrufe die Hierarchie der aktuell verwendeten Rendereffekte verwaltet
    /// und automatisch das aktuell von XNA verwendete Rendertarget auf das Rendertarget des obersten Rendereffekts
    /// setzt.
    /// </summary>
    public class RenderEffectStack : 
    {

        #region Properties

        /// <summary>
        /// Der oberste Rendereffekt.
        /// </summary>
        public IRenderEffect CurrentEffect { get; set; }

        /// <summary>
        /// Der Standard-Rendereffekt, der verwendet wird, wenn der Stapel leer ist.
        /// </summary>
        private IRenderEffect DefaultEffect { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt einen neuen Rendereffekt-Stapel.
        /// </summary>
        public void RenderEffectStack (IRenderEffect defaultEffect)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Entfernt den obersten Rendereffekt vom Stapel.
        /// </summary>
        public virtual IRenderEffect Pop ()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Legt einen Rendereffekt auf den Stapel.
        /// </summary>
        public virtual void Push (IRenderEffect effect)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

