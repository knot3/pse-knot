using System;
using System.Collections.Generic;
using System.Linq;

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
    /// Stellt eine Schnittstelle für Klassen bereit, die Rendereffekte ermöglichen.
    /// </summary>
    public interface IRenderEffect : 
    {

        #region Properties

        /// <summary>
        /// Das Rendertarget, in das zwischen dem Aufruf der Begin()- und der End()-Methode gezeichnet wird,
        /// weil es in Begin() als primäres Rendertarget des XNA-Frameworks gesetzt wird.
        /// </summary>
        public RenderTarget2D RenderTarget { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// In der Methode Begin() wird das aktuell von XNA genutzte Rendertarget auf einem Stapel gesichert
        /// und das Rendertarget des Effekts wird als aktuelles Rendertarget gesetzt.
        /// </summary>
        public void Begin (GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Das auf dem Stapel gesicherte, vorher genutzte Rendertarget wird wiederhergestellt und
        /// das Rendertarget dieses Rendereffekts wird, unter Umständen in Unterklassen verändert,
        /// auf dieses ubergeordnete Rendertarget gezeichnet.
        /// </summary>
        public void End (GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Zeichnet das übergebene 3D-Modell auf das Rendertarget.
        /// </summary>
        public void DrawModel (GameTime, GameModel model)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Beim Laden des Modells wird von der XNA-Content-Pipeline jedem ModelMeshPart ein Shader der Klasse
        /// BasicEffect zugewiesen. Für die Nutzung des Modells in diesem Rendereffekt kann jedem ModelMeshPart
        /// ein anderer Shader zugewiesen werden.
        /// </summary>
        public void RemapModel (GameModel model)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

