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
    /// Eine zeichenbare Spielkomponente, die in einem angegebenen Spielzustand verwendet wird und eine bestimmte Priorität hat.
    /// </summary>
    public class DrawableGameScreenComponent : XNA.DrawableGameComponent
    {

        #region Properties

        /// <summary>
        /// Der zugewiesene Spielzustand.
        /// </summary>
        public GameScreen Screen { get; set; }

        /// <summary>
        /// Die Zeichen- und Eingabepriorität.
        /// </summary>
        public DisplayLayer Index { get; set; }

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
        /// Erstellt eine neue zeichenbare Spielkomponente in dem angegebenen Spielzustand mit der angegebenen Priorität.
        /// </summary>
        public void DrawableGameStateComponent (GameScreen screen, DisplayLayer index)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

