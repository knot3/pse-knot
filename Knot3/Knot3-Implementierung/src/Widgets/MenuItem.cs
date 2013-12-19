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
    /// Ein abstrakte Klasse für Menüeinträge, die
    /// </summary>
    public class MenuItem : Widget
    {

        #region Properties

        /// <summary>
        /// Gibt an, ob die Maus sich über dem Eintrag befindet, ohne ihn anzuklicken, ob er ausgewählt ist
        /// oder nichts von beidem.
        /// </summary>
        public ItemState ItemState { get; set; }

        /// <summary>
        /// Die Zeichenreihenfolge.
        /// </summary>
        public Integer ItemOrder { get; set; }

        /// <summary>
        /// Der Anzeigetext der Schaltfläche.
        /// </summary>
        public String Text { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reaktionen auf einen Linksklick.
        /// </summary>
        public void OnLeftClick (Vector2 position, ClickState state, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Reaktionen auf einen Rechtsklick.
        /// </summary>
        public void OnRightClick (Vector2 position, ClickState state, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Reaktionen auf Tasteneingaben.
        /// </summary>
        public void OnKeyEvent ()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt die Ausmaße des Eintrags zurück.
        /// </summary>
        public Rectangle Bounds ()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

