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
    /// Eine abstrakte Klasse, die eine Implementierung von IRenderEffect darstellt.
    /// </summary>
    public class RenderEffect : IRenderEffect
    {

        #region Properties

        /// <summary>
        /// Das Rendertarget, in das zwischen dem Aufruf der Begin()- und der End()-Methode gezeichnet wird,
        /// weil es in Begin() als primäres Rendertarget des XNA-Frameworks gesetzt wird.
        /// </summary>
        public RenderTarget2D RenderTarget { get; set; }

        /// <summary>
        /// Der Spielzustand, in dem der Effekt verwendet wird.
        /// </summary>
        protected GameScreen screen { get; set; }

        /// <summary>
        /// Ein Spritestapel (s. Glossar oder http://msdn.microsoft.com/en-us/library/bb203919.aspx), der verwendet wird, um das Rendertarget dieses Rendereffekts auf das übergeordnete Rendertarget zu zeichnen.
        /// </summary>
        protected SpriteBatch spriteBatch { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// In der Methode Begin() wird das aktuell von XNA genutzte Rendertarget auf einem Stack gesichert
        /// und das Rendertarget des Effekts wird als aktuelles Rendertarget gesetzt.
        /// </summary>
        public virtual void Begin (GameTime gametime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Das auf dem Stack gesicherte, vorher genutzte Rendertarget wird wiederhergestellt und
        /// das Rendertarget dieses Rendereffekts wird, unter Umständen in Unterklassen verändert,
        /// auf dieses ubergeordnete Rendertarget gezeichnet.
        /// </summary>
        public virtual void End (GameTime gametime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Zeichnet das Spielmodell model mit diesem Rendereffekt.
        /// </summary>
        public virtual void DrawModel (GameModel GameModel, GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Beim Laden des Modells wird von der XNA-Content-Pipeline jedem ModelMeshPart ein Shader der Klasse
        /// BasicEffect zugewiesen. Für die Nutzung des Modells in diesem Rendereffekt kann jedem ModelMeshPart
        /// ein anderer Shader zugewiesen werden.
        /// </summary>
        public virtual void RemapModel (GameModel GameModel)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Zeichnet das Rendertarget.
        /// </summary>
        protected virtual void DrawRenderTarget (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

