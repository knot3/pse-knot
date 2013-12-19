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
    /// Der Zustand eines Menüeintrags.
    /// </summary>
    public enum ItemState
    {
        /// <summary>
        /// Ausgewählt.
        /// </summary>
        Selected=1,
        /// <summary>
        /// Die Maus wurde direkt über den Menüeintrag navigiert und verweilt dort.
        /// </summary>
        Hovered=2,
        /// <summary>
        /// Steht für einen ungültigen oder einen undefinierten Zustand. Keine Reihenfolge.
        /// Kein gültiger oder ein undefinierter Zustand.
        /// Keine Reihenfolge.
        /// 
        /// </summary>
        None=0,
    }
}

