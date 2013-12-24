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

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Screens
{
    /// <summary>
    /// Der Spielzustand, der den Ladebildschirm für Knoten darstellt.
    /// </summary>
    public class CreativeLoadScreen : MenuScreen
    {

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues CreativeLoadScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
        /// </summary>
        public CreativeLoadScreen (Knot3Game game)
			: base(game)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fügt das Menü mit den Spielständen in die Spielkomponentenliste ein.
        /// </summary>
        public override void Entered (GameScreen previousScreen, GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public override void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

