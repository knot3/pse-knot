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
using RenderEffects;
using KnotData;

namespace Widgets
{
    /// <summary>
    /// Die vertikale Ausrichtung.
    /// </summary>
    public enum VerticalAlignment
    {
        /// <summary>
        /// Oben.
        /// </summary>
        Top=1,
        /// <summary>
        /// Mittig.
        /// </summary>
        Center=0,
        /// <summary>
        /// Unten.
        /// </summary>
        Bottom=2,
    }
}

