using System;
using System.Collections.Generic;
using System.Linq;

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
    /// Stellt für jeden Frame die Maus- und Tastatureingaben bereit. Daraus werden die nicht von XNA bereitgestellten Mauseingaben berechnet.
    /// </summary>
    public class Input : GameScreenComponent
    {

        #region Properties

        /// <summary>
        /// Enthält den Klickzustand der rechten Maustaste.
        /// </summary>
        public ClickState RightMouseButton { get; set; }

        /// <summary>
        /// Enthält den Klickzustand der linken Maustaste.
        /// </summary>
        public ClickState LeftMouseButton { get; set; }

        /// <summary>
        /// Enthält den Mauszustand von XNA zum aktuellen Frames.
        /// </summary>
        public MouseState CurrentMouseState { get; set; }

        /// <summary>
        /// Enthält den Tastaturzustand von XNA zum aktuellen Frames.
        /// </summary>
        public KeyboardState CurrentKeyboardState { get; set; }

        /// <summary>
        /// Enthält den Mauszustand von XNA zum vorherigen Frames.
        /// </summary>
        public MouseState PreviousMouseState { get; set; }

        /// <summary>
        /// Enthält den Tastaturzustand von XNA zum vorherigen Frames.
        /// </summary>
        public KeyboardState PreviousKeyboardState { get; set; }

        /// <summary>
        /// Gibt an, ob die Mausbewegung für Kameradrehungen verwendet werden soll.
        /// </summary>
        public Boolean GrabMouseMovement { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues Input-Objekt, das an den übergebenen Spielzustand gebunden ist.
        /// </summary>
        public void Input (GameScreen screen)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

