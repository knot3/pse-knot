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
    /// Eine statische Klasse, die Bezeichner in lokalisierten Text umsetzen kann.
    /// </summary>
    public class Localizer : 
    {

        #region Properties

        /// <summary>
        /// Die Datei, welche Informationen für die Lokalisierung enthält.
        /// </summary>
        private ConfigFile localization { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Liefert zu dem übergebenen Bezeichner den zugehörigen Text aus der Lokalisierungsdatei der
        /// aktuellen Sprache zurück, die dabei aus der Einstellungsdatei des Spiels gelesen wird.
        /// </summary>
        public virtual String Localize (String text)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

