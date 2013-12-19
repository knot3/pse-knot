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
    /// Ein Menüeintrag, der einen Schieberegler bereitstellt, mit dem man einen Wert zwischen einem minimalen
    /// und einem maximalen Wert über Verschiebung einstellen kann.
    /// </summary>
    public class SliderItem : MenuItem
    {

        #region Properties

        /// <summary>
        /// Der aktuelle Wert.
        /// </summary>
        public Integer Value { get; set; }

        /// <summary>
        /// Der minimale Wert.
        /// </summary>
        public Integer MinValue { get; set; }

        /// <summary>
        /// Der maximale Wert.
        /// </summary>
        public Integer MaxValue { get; set; }

        /// <summary>
        /// Schrittweite zwischen zwei einstellbaren Werten.
        /// </summary>
        public Integer Step { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt eine neue Instanz eines SliderItem-Objekts und initialisiert diese
        /// mit dem zugehörigen GameScreen-Objekt. Zudem ist die Angabe der Zeichenreihenfolge,
        /// einem minimalen einstellbaren Wert, einem maximalen einstellbaren Wert und einem Standardwert Pflicht.
        /// </summary>
        public  SliderItem (GameScreen screen, DisplayLayer drawOrder, Integer max, Integer min, Integer step, Integer value)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

