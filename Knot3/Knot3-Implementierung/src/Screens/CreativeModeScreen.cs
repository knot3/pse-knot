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
using RenderEffects;
using KnotData;
using Widgets;

namespace Screens
{
    /// <summary>
    /// Der Spielzustand, der während dem Erstellen und Bearbeiten eines Knotens aktiv ist und für den Knoten eine 3D-Welt zeichnet.
    /// </summary>
    public class CreativeModeScreen : GameScreen
    {

        #region Properties

        /// <summary>
        /// Der Knoten, der vom Spieler bearbeitet wird.
        /// </summary>
        public void Knot { get; set; }

        /// <summary>
        /// Die Spielwelt in der die 3D-Objekte des dargestellten Knotens enthalten sind.
        /// </summary>
        private World World { get; set; }

        /// <summary>
        /// Der Controller, der aus dem Knoten die 3D-Modelle erstellt.
        /// </summary>
        private KnotRenderer KnotRenderer { get; set; }

        /// <summary>
        /// Der Undo-Stack.
        /// </summary>
        public Stack<Knot> Undo { get; set; }

        /// <summary>
        /// Der Redo-Stack.
        /// </summary>
        public Stack<Knot> Redo { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Fügt die 3D-Welt und den Inputhandler in die Spielkomponentenliste ein.
        /// </summary>
        public void Entered (GameScreen previousScreen, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

