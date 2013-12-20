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
    /// Eine Schnittstelle für eine Spielkomponente, die in einem angegebenen Spielzustand verwendet wird und eine bestimmte Priorität hat.
    /// </summary>
    public interface IGameScreenComponent : IGameComponent
    {

        #region Properties

        /// <summary>
        /// Die Zeichen- und Eingabepriorität.
        /// </summary>
        DisplayLayer Index { get; set; }

        /// <summary>
        /// Der zugewiesene Spielzustand.
        /// </summary>
        GameScreen Screen { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gibt Spielkomponenten zurück, die in dieser Spielkomponente enthalten sind.
        /// [returntype=IEnumerable<IGameScreenComponent>]
        /// </summary>
        IEnumerable<IGameScreenComponent> SubComponents (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

