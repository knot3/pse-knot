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

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;

namespace Knot3.Widgets
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
        /// Ein undefinierter Zustand.
        /// </summary>
        None=0,
    }
}

