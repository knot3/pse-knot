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
    /// Repräsentiert einen Übergang zwischen zwei Kanten.
    /// </summary>
    public interface IJunction : 
    {

        #region Properties

        /// <summary>
        /// Die Kante vor dem Übergang.
        /// </summary>
        public Edge EdgeFrom { get; set; }

        /// <summary>
        /// Die Kante nach dem Übergang.
        /// </summary>
        public Edge EdgeTo { get; set; }

        #endregion

    }
}

