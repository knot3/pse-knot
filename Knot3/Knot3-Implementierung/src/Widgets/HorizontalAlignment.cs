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
using KnotData;

namespace Widgets
{
    /// <summary>
    /// Eine horizontale Ausrichtung.
    /// </summary>
    public enum HorizontalAlignment
    {
        /// <summary>
        /// Links.
        /// </summary>
        Left=0,
        /// <summary>
        /// Mittig.
        /// </summary>
        Center=1,
        /// <summary>
        /// Rechts.
        /// </summary>
        Right=2,
    }
}

