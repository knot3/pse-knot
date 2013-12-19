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
        public virtual Integer Value { get; set; }

        /// <summary>
        /// Der minimale Wert.
        /// </summary>
        public virtual Integer MinValue { get; set; }

        /// <summary>
        /// Der maximale Wert.
        /// </summary>
        public virtual Integer MaxValue { get; set; }

        /// <summary>
        /// Schrittweite zwischen zwei einstellbaren Werten.
        /// </summary>
        public virtual Integer Step { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public virtual void SliderItem (GameScreen screen, DisplayLayer drawOrder, Integer max, Integer min, Integer step, Integer value)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

