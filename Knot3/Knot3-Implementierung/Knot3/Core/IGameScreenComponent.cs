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

using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Core
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
        IEnumerable<IGameScreenComponent> SubComponents (GameTime time);

        #endregion

    }
}

