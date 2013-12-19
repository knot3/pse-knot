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
    /// Pausiert ein Spieler im Creative- oder Challenge-Modus das Spiel,
    /// wird dieser Dialog über anderen Spielkomponenten angezeigt.
    /// </summary>
    public class PauseDialog : Dialog
    {

        #region Properties

        /// <summary>
        /// Das Menü, das verschiedene Schaltflächen enthält.
        /// </summary>
        private VerticalMenu pauseMenu { get; set; }

        #endregion

    }
}

