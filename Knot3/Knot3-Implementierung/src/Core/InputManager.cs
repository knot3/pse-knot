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

using GameObjects;
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace Core
{
    /// <summary>
    /// Stellt für jeden Frame die Maus- und Tastatureingaben bereit. Daraus werden die nicht von XNA bereitgestellten Mauseingaben berechnet. Zusätzlich wird die aktuelle Eingabeaktion berechnet.
    /// </summary>
    public class InputManager : GameScreenComponent
    {

        #region Properties

        /// <summary>
        /// Enthält den Klickzustand der rechten Maustaste.
        /// </summary>
        public static ClickState RightMouseButton { get; set; }

        /// <summary>
        /// Enthält den Klickzustand der linken Maustaste.
        /// </summary>
        public static ClickState LeftMouseButton { get; set; }

        /// <summary>
        /// Enthält den Mauszustand von XNA zum aktuellen Frames.
        /// </summary>
        public static MouseState CurrentMouseState { get; set; }

        /// <summary>
        /// Enthält den Tastaturzustand von XNA zum aktuellen Frames.
        /// </summary>
        public static KeyboardState CurrentKeyboardState { get; set; }

        /// <summary>
        /// Enthält den Mauszustand von XNA zum vorherigen Frames.
        /// </summary>
        public static MouseState PreviousMouseState { get; set; }

        /// <summary>
        /// Enthält den Tastaturzustand von XNA zum vorherigen Frames.
        /// </summary>
        public static KeyboardState PreviousKeyboardState { get; set; }

        /// <summary>
        /// Gibt an, ob die Mausbewegung für Kameradrehungen verwendet werden soll.
        /// </summary>
        public Boolean GrabMouseMovement { get; set; }

        /// <summary>
        /// Gibt die aktuelle Eingabeaktion an, die von den verschiedenen Inputhandlern genutzt werden können.
        /// </summary>
        public InputAction CurrentInputAction { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues InputManager-Objekt, das an den übergebenen Spielzustand gebunden ist.
        /// </summary>
        public InputManager (GameScreen screen)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public virtual void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

