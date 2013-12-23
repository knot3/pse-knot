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
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;

namespace Knot3.Widgets
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
        public ColorPickItem (GameScreen screen, DisplayLayer drawOrder, string text, Color color)
			: base(screen, drawOrder, text)
        {
        }

        #endregion

    }
}

