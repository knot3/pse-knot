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
    /// Eine Wertesammlung der möglichen Klickzustände einer Maustaste.
    /// </summary>
    public enum ClickState
    {
        /// <summary>
        /// Steht für einen ungültigen oder einen undefinierten Zustand. Keine Reihenfolge.
        /// Kein gültiger oder ein undefinierter Zustand.
        /// Keine Reihenfolge.
        /// 
        /// </summary>
        None=0,
        /// <summary>
        /// Ein Einzelklick.
        /// </summary>
        SingleClick=1,
        /// <summary>
        /// Ein Doppelklick.
        /// </summary>
        DoubleClick=2,
    }
}

