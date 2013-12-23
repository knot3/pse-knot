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
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.GameObjects
{
    /// <summary>
    /// Die Klasse GameObjectDistance beschreibt den Abstand zu einem Spielobjekt.
    /// Dies ist z.B. hilfreich, um Schnittstellen, Verdeckungen, ... zu berechnen.
    /// </summary>
    public sealed class GameObjectDistance
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

