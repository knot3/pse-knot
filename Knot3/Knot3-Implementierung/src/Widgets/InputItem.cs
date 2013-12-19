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
    /// Ein Menüeintrag, der Texteingaben vom Spieler annimmt.
    /// </summary>
    public class InputItem : MenuItem
    {

        #region Properties

        /// <summary>
        /// Beinhaltet den vom Spieler eingegebenen Text.
        /// </summary>
        public String InputText { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues InputItem-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
        /// Zudem sind Angaben zur Zeichenreihenfolge und für evtl. bereits vor-eingetragenen Text Pflicht.
        /// </summary>
        public  InputItem (GameScreen screen, DisplayLayer drawOrder, String text)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

