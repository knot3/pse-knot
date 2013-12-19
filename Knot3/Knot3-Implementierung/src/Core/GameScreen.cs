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
    /// Ein Spielzustand, der zu einem angegebenen Spiel gehört und einen Inputhandler und Rendereffekte enthält.
    /// </summary>
    public class GameScreen
    {

        #region Properties

        /// <summary>
        /// Das Spiel, zu dem der Spielzustand gehört.
        /// </summary>
        public Knot3Game Game { get; set; }

        /// <summary>
        /// Der Inputhandler des Spielzustands.
        /// </summary>
        public Input Input { get; set; }

        /// <summary>
        /// Der aktuelle Postprocessing-Effekt des Spielzustands
        /// </summary>
        public RenderEffect PostProcessingEffect { get; set; }

        /// <summary>
        /// Ein Stack, der während dem Aufruf der Draw-Methoden der Spielkomponenten die jeweils aktuellen Rendereffekte enthält.
        /// </summary>
        public RenderEffectStack CurrentRenderEffects { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues GameScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
        /// </summary>
        public  GameScreen (Knot3Game game)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Beginnt mit dem Füllen der Spielkomponentenliste des XNA-Frameworks und fügt sowohl für Tastatur- als auch für
        /// Mauseingaben einen Inputhandler für Widgets hinzu. Wird in Unterklassen von GameScreen reimplementiert und fügt zusätzlich weitere
        /// Spielkomponenten hinzu.
        /// </summary>
        public virtual void Entered (GameScreen previousScreen, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Leert die Spielkomponentenliste des XNA-Frameworks.
        /// </summary>
        public virtual void BeforeExit (GameScreen nextScreen, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public virtual void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void AddGameComponents (IGameScreenComponent[] components)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void RemoveGameComponents (IGameScreenComponent[] components)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

