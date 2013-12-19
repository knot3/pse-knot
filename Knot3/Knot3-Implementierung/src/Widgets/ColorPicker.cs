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
    /// Ein Steuerelement der grafischen Benutzeroberfläche, das eine Auswahl von Farben ermöglicht.
    /// </summary>
    public class ColorPicker : Widget
    {

        #region Properties

        /// <summary>
        /// Die ausgewählte Farbe.
        /// </summary>
        public virtual Color Color { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reagiert auf Tastatureingaben.
        /// </summary>
        public virtual void OnKeyEvent ()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt die Ausmaße des ColorPickers zurück.
        /// </summary>
        public virtual Rectangle Bounds ()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Bei einem Linksklick wird eine Farbe ausgewählt und im Attribut Color abgespeichert.
        /// </summary>
        public virtual void OnLeftClick (Vector2 position, ClickState state, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Bei einem Rechtsklick geschieht nichts.
        /// </summary>
        public virtual void OnRightClick (Vector2 position, ClickState state, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

