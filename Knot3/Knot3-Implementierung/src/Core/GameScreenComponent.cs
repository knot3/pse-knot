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
    /// Eine Spielkomponente, die in einem GameScreen verwendet wird und eine bestimmte Priorität hat.
    /// </summary>
    public class GameScreenComponent : XNA.GameComponent
    {

        #region Properties

        /// <summary>
        /// Die Zeichen- und Eingabepriorität.
        /// </summary>
        public DisplayLayer Index { get; set; }

        /// <summary>
        /// Der zugewiesene Spielzustand.
        /// </summary>
        public GameScreen Screen { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gibt Spielkomponenten zurück, die in dieser Spielkomponente enthalten sind.
        /// </summary>
        public IEnumerable SubComponents (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Erstellt eine neue Spielkomponente in dem angegebenen Spielzustand mit der angegebenen Priorität.
        /// </summary>
        public void GameStateComponent (GameScreen screen, DisplayLayer index)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

