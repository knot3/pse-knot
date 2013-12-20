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
using Widgets;

namespace KnotData
{
    /// <summary>
    /// Eine Wertesammlung der möglichen Richtungen in einem dreidimensionalen Raum.
    /// Wird benutzt, damit keine ungültigen Kantenrichtungen angegeben werden können.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Links.
        /// </summary>
        Left=1,
        /// <summary>
        /// Rechts.
        /// </summary>
        Right=2,
        /// <summary>
        /// Hoch.
        /// </summary>
        Up=3,
        /// <summary>
        /// Runter.
        /// </summary>
        Down=4,
        /// <summary>
        /// Vorwärts.
        /// </summary>
        Forward=5,
        /// <summary>
        /// Rückwärts.
        /// </summary>
        Backward=6,
        /// <summary>
        /// Keine Richtung.
        /// </summary>
        Zero=0,
    }
}

