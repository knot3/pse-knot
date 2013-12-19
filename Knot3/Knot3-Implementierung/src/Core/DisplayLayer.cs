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

using GameObjects;
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace Core
{
    /// <summary>
    /// Die Zeichenreihenfolge der Elemente der grafischen Benutzeroberfläche.
    /// </summary>
    public enum DisplayLayer
    {
        /// <summary>
        /// Kein gültiger oder ein undefinierter Zustand.
        /// Keine Reihenfolge.
        /// 
        /// </summary>
        None=0,
        /// <summary>
        /// !!!
        /// </summary>
        Background=1,
        /// <summary>
        /// 
        /// </summary>
        GameWorld=2,
        /// <summary>
        /// 
        /// </summary>
        Dialog=3,
        /// <summary>
        /// 
        /// </summary>
        Menu=4,
        /// <summary>
        /// 
        /// </summary>
        MenuItem=5,
        /// <summary>
        /// 
        /// </summary>
        SubMenu=6,
        /// <summary>
        /// 
        /// </summary>
        SubMenuItem=7,
        /// <summary>
        /// 
        /// </summary>
        Overlay=8,
        /// <summary>
        /// 
        /// </summary>
        Cursor=9,
    }
}

