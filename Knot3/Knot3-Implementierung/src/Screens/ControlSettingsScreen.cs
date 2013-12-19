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
    /// Der Spielzustand, der die Steuerungs-Einstellungen darstellt.
    /// </summary>
    public class ControlSettingsScreen : SettingsScreen
    {

        #region Properties

        /// <summary>
        /// Das Menü, das die Einstellungen enthält.
        /// </summary>
        protected void settingsMenu { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues ControlSettingsScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
        /// </summary>
        public  ControlSettingsScreen (Knot3Game game)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public virtual void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Fügt das Menü mit den Einstellungen in die Spielkomponentenliste ein.
        /// </summary>
        public virtual void Entered (GameScreen previousScreen, GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

