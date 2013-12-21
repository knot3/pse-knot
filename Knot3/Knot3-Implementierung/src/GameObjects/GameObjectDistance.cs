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
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace GameObjects
{
    /// <summary>
    /// Die Klasse GameObjectDistance beschreibt den Abstand zu einem Spielobjekt.
    /// Dies ist z.B. hilfreich, um Schnittstellen, Verdeckungen, ... zu berechnen.
    /// </summary>
    public class GameObjectDistance
    {

        #region Properties

        /// <summary>
        /// Ein Spielobjekt.
        /// </summary>
        public IGameObject Object { get; set; }

        /// <summary>
        /// Distance hält den Abstand als Gleitkommawert.
        /// </summary>
        public float Distance { get; set; }

        #endregion

    }
}

