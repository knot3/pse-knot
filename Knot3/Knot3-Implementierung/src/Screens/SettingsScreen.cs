using System;
using System.Collections.Generic;
using System.Linq;

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
using RenderEffects;
using KnotData;
using Widgets;

namespace Screens
{
    /// <summary>
    /// Ein Spielzustand, der das Haupt-Einstellungsmenü zeichnet.
    /// </summary>
    public class SettingsScreen : MenuScreen
    {

        #region Properties

        /// <summary>
        /// Das Haupt-Einstellungsmenü.
        /// </summary>
        protected void navigation { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Fügt das Haupt-Einstellungsmenü in die Spielkomponentenliste ein.
        /// </summary>
        public void Entered (GameScreen previousScreen, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

