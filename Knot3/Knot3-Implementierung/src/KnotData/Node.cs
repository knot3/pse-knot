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
    /// 
    /// </summary>
    public class Node
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Integer X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Integer Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Integer Z { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Integer scale { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt eine neue Instanz eines Node-Objekts und initialisiert diese mit Werten
        /// für die x-, y- und z-Koordinate.
        /// </summary>
        public void Node (Integer x, Integer y, Integer z)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public virtual Vector3 ToVector ()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

