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
    /// Repräsentiert einen Mauszeiger.
    /// </summary>
    public class MousePointer : DrawableGameScreenComponent
    {

        #region Constructors

        /// <summary>
        /// Erstellt einen neuen Mauszeiger für den angegebenen Spielzustand.
        /// </summary>
        public void MousePointer (GameScreen screen)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Zeichnet den Mauszeiger.
        /// </summary>
        public void Draw (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

