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
    /// Ein Menüeintrag, der einen Tastendruck entgegennimmt und in der enthaltenen Option als Zeichenkette speichert.
    /// </summary>
    public class KeyInputItem : InputItem
    {

        #region Properties

        /// <summary>
        /// Die Option in einer Einstellungsdatei.
        /// </summary>
        private OptionInfo option { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues CheckBoxItem-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
        /// Zudem sind Angaben zur Zeichenreihenfolge und der Eingabeoption Pflicht.
        /// </summary>
        public void KeyInputItem (GameScreen screen, DisplayLayer drawOrder, OptionInfo option)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Speichert die aktuell gedrückte Taste in der Option.
        /// </summary>
        public void OnKeyEvent ()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

