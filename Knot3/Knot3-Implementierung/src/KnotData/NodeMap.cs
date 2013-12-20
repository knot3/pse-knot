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
using RenderEffects;
using Widgets;

namespace KnotData
{
    /// <summary>
    /// Eine Zuordnung zwischen Kanten und Kantenübergänge.
    /// </summary>
    public class NodeMap
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public int Scale { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gibt den Übergang am Anfang der Kante zurück.
        /// </summary>
        public virtual Node From (Edge edge)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt den Übergang am Ende der Kante zurück.
        /// </summary>
        public virtual Node To (Edge edge)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Aktualisiert die Zuordnung, wenn sich die Kanten geändert haben.
        /// </summary>
        public virtual void OnEdgesChanged ( )
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

