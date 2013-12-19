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
    /// Repräsentiert eine Einstellungsdatei.
    /// </summary>
    public class ConfigFile : 
    {

        #region Methods

        /// <summary>
        /// Setzt den Wert der Option mit dem angegebenen Namen in den angegebenen Abschnitt auf den angegebenen Wert.
        /// </summary>
        public void SetOption (String section, String option, String value)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt den aktuell in der Datei vorhandenen Wert für die angegebene Option in dem angegebenen Abschnitt zurück.
        /// </summary>
        public Boolean GetOption (String section, String option, Boolean defaultValue)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt den aktuell in der Datei vorhandenen Wert für die angegebene Option in dem angegebenen Abschnitt zurück.
        /// </summary>
        public String GetOption (String section, String option, String defaultValue)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Setzt den Wert der Option mit dem angegebenen Namen in den angegebenen Abschnitt auf den angegebenen Wert.
        /// </summary>
        public void SetOption (String section, String option, Boolean _value)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

