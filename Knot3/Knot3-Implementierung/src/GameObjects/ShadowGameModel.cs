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
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace GameObjects
{
    /// <summary>
    /// Die 3D-Modelle, die während einer Verschiebung von Kanten die Vorschaumodelle repräsentieren.
    /// </summary>
    public class ShadowGameModel : ShadowGameObject
    {

        #region Properties

        /// <summary>
        /// Die Farbe der Vorschaumodelle.
        /// </summary>
        public virtual Color ShadowColor { get; set; }

        /// <summary>
        /// Die Transparenz der Vorschaumodelle.
        /// </summary>
        public virtual float ShadowAlpha { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues Vorschaumodell in dem angegebenen Spielzustand für das angegebene zu dekorierende Modell.
        /// </summary>
        public virtual void ShadowGameModel (GameScreen sreen, GameModel decoratedModel)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Zeichnet das Vorschaumodell.
        /// </summary>
        public virtual void Draw (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

