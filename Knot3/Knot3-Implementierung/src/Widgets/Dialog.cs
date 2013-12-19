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
    /// Ein Dialog ist ein im Vordergrund erscheinendes Fenster, das auf Nutzerinteraktionen wartet.
    /// </summary>
    public class Dialog : Widget
    {

        #region Properties

        /// <summary>
        /// Der Fenstertitel.
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// Der angezeigte Text.
        /// </summary>
        public String Text { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues Dialog-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
        /// Zudem sind Angaben zur Zeichenreihenfolge, einer Zeichenkette für den Titel und für den eingeblendeten Text Pflicht.
        /// </summary>
        public void Dialog (GameScreen screen, DisplayLayer drawOrder, String title, String text)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Durch Drücken der Entertaste wird die ausgewählte Aktion ausgeführt. Durch Drücken der Escape-Taste wird der Dialog abgebrochen.
        /// Mit Hilfe der Pfeiltasten kann zwischen den Aktionen gewechselt werden.
        /// </summary>
        public virtual void OnKeyEvent ()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt die Ausmaße des Dialogs zurück.
        /// </summary>
        public virtual Rectangle Bounds ()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Bei einem Linksklick geschieht nichts.
        /// </summary>
        public virtual void OnLeftClick (Vector2 position, ClickState state, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Bei einem Rechtsklick geschieht nichts.
        /// </summary>
        public virtual void OnRightClick (Vector2 position, ClickState state, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

