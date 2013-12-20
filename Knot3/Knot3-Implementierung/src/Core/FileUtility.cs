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
    /// Eine Hilfsklasse für Dateioperationen.
    /// </summary>
    public class FileUtility
    {

        #region Properties

        /// <summary>
        /// Das Einstellungsverzeichnis.
        /// </summary>
        public string SettingsDirectory { get; set; }

        /// <summary>
        /// Das Spielstandverzeichnis.
        /// </summary>
        public string SavegameDirectory { get; set; }

        /// <summary>
        /// Das Bildschirmfotoverzeichnis.
        /// </summary>
        public string ScreenshotDirectory { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Konvertiert einen Namen eines Knotens oder einer Challenge in einen gültigen Dateinamen durch Weglassen ungültiger Zeichen.
        /// </summary>
        public virtual string ConvertToFileName (string name)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Liefert einen Hash-Wert zu der durch filename spezifizierten Datei.
        /// </summary>
        public virtual string GetHash (string filename)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

