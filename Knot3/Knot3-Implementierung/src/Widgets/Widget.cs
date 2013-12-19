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

using Core;
using GameObjects;
using Screens;
using RenderEffects;
using KnotData;

namespace Widgets
{
    /// <summary>
    /// Eine abstrakte Klasse, von der alle Element der grafischen Benutzeroberfläche erben.
    /// </summary>
    public class Widget : DrawableGameScreenComponent
    {

        #region Properties

        /// <summary>
        /// Die von der Auflösung unabhängige Größe in Prozent.
        /// </summary>
        public Vector2 RelativeSize { get; set; }

        /// <summary>
        /// Die von der Auflösung unabhängige Position in Prozent.
        /// </summary>
        public Vector2 RelativePosition { get; set; }

        /// <summary>
        /// Gibt an, ob das grafische Element sichtbar ist.
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Die Hintergrundfarbe.
        /// </summary>
        public Func<Color> BackgroundColor { get; set; }

        /// <summary>
        /// Die Vordergrundfarbe.
        /// </summary>
        public Func<Color> ForegroundColor { get; set; }

        /// <summary>
        /// Die horizontale Ausrichtung.
        /// </summary>
        public HorizontalAlignment AlignX { get; set; }

        /// <summary>
        /// Die vertikale Ausrichtung.
        /// </summary>
        public VerticalAlignment AlignY { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues grafisches Benutzerschnittstellenelement in dem angegebenen Spielzustand
        /// mit der angegebenen Zeichenreihenfolge.
        /// </summary>
        public void Widget (GameScreen screen, DisplayLayer drawOrder)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Die Ausmaße des grafischen Elements
        /// </summary>
        public Rectangle BoundingBox ()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

