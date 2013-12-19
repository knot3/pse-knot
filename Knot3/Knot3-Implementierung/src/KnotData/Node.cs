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
    public class Node : 
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual Integer X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Integer Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Integer Z { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private virtual Integer scale { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public virtual void Node (Integer x, Integer y, Integer z)
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

