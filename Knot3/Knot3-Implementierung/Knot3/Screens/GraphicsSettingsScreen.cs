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
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Screens
{
    /// <summary>
    /// Der Spielzustand, der die Grafik-Einstellungen darstellt.
    /// </summary>
    public class GraphicsSettingsScreen : SettingsScreen
    {

        #region Properties

        /// <summary>
        /// Das Menü, das die Einstellungen enthält.
        /// </summary>
        private VerticalMenu settingsMenu { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues GraphicsSettingsScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
        /// </summary>
        public GraphicsSettingsScreen (Knot3Game game)
			: base(game)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public override void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Fügt das Menü mit den Einstellungen in die Spielkomponentenliste ein.
        /// </summary>
        public override void Entered (GameScreen previousScreen, GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

