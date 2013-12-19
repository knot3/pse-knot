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
    /// !!!
    /// </summary>
    public class GameObjectDistance : 
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public IGameObject Object { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Distance { get; set; }

        #endregion

    }
}

