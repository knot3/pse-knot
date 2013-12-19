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
    /// Ein Dialog, der Schaltflächen zum Bestätigen einer Aktion anzeigt.
    /// </summary>
    public class ConfirmDialog : Dialog
    {

        #region Properties

        /// <summary>
        /// Das Menü, das Schaltflächen enthält.
        /// </summary>
        private Menu buttons { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues ConfirmDialog-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
        /// Zudem sind Angaben zur Zeichenreihenfolge, einer Zeichenkette für den Titel und für den eingeblendeten Text Pflicht.
        /// </summary>
        public void ConfirmDialog (GameScreen screen, DisplayLayer drawOrder, String title, String text)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

