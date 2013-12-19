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
    /// Eine statische Klasse, die eine Referenz auf die zentrale Einstellungsdatei des Spiels enthält.
    /// </summary>
    public class Options
    {

        #region Properties

        /// <summary>
        /// Die zentrale Einstellungsdatei des Spiels.
        /// </summary>
        public ConfigFile Default { get; set; }

        #endregion

    }
}

