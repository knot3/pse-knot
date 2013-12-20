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
    /// 
    /// </summary>
    public enum InputAction
    {
        /// <summary>
        /// 
        /// </summary>
        None=0,
        /// <summary>
        /// 
        /// </summary>
        CameraTargetMove,
        /// <summary>
        /// 
        /// </summary>
        ArcballMove,
        /// <summary>
        /// 
        /// </summary>
        FreeMouse,
        /// <summary>
        /// 
        /// </summary>
        FirstPersonCameraMove,
        /// <summary>
        /// 
        /// </summary>
        SelectedObjectMove,
        /// <summary>
        /// 
        /// </summary>
        SelectedObjectShadowMove,
    }
}

