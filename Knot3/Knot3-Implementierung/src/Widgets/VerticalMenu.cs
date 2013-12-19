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
    /// Ein Menü, das alle Einträge vertikal anordnet.
    /// </summary>
    public class VerticalMenu : Menu
    {

        #region Constructors

        /// <summary>
        /// Erzeugt eine neue Instanz eines VerticalMenu-Objekts und initialisiert diese mit dem zugehörigen GameScreen-Objekt.
        /// Zudem ist die Angaben der Zeichenreihenfolge Pflicht.
        /// </summary>
        public void VerticalMenu (GameScreen screen, DisplayLayer drawOrder)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Ordnet die Einträge vertikal an.
        /// </summary>
        public void AlignItems ()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

