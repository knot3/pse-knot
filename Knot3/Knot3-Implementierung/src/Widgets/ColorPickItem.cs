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
    /// Ein Menüeintrag, der eine aktuelle Farbe anzeigt und zum Ändern der Farbe per Mausklick einen ColorPicker öffnet.
    /// </summary>
    public class ColorPickItem : MenuItem
    {

        #region Properties

        /// <summary>
        /// Die aktuelle Farbe.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Der ColorPicker, der bei einem Mausklick auf den Menüeintrag geöffnet wird.
        /// </summary>
        private ColorPicker picker { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues ColorPickItem-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
        /// Zudem sind Angaben zur Zeichenreihenfolge und der Auswahloption Pflicht.
        /// </summary>
        public void ColorPickItem (GameScreen screen, DisplayLayer drawOrder, Color color)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

