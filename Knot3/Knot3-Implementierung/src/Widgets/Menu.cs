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

using Core;
using GameObjects;
using Screens;
using RenderEffects;
using KnotData;

namespace Widgets
{
    /// <summary>
    /// Ein Menü enthält Bedienelemente zur Benutzerinteraktion. Diese Klasse bietet Standardwerte für
    /// Positionen, Größen, Farben und Ausrichtungen der Menüeinträge. Sie werden gesetzt, wenn die Werte
    /// der Menüeinträge \glqq null\grqq~sind.
    /// </summary>
    public class Menu : Widget, IEnumerable<MenuItem>
    {

        #region Properties

        /// <summary>
        /// Die von der Auflösung unabhängige Größe in Prozent.
        /// </summary>
        public Func<int, Vector2> RelativeItemSize { get; set; }

        /// <summary>
        /// Die von der Auflösung unabhängige Position in Prozent.
        /// </summary>
        public Func<int, Vector2> RelativeItemPosition { get; set; }

        /// <summary>
        /// Die vom Zustand des Menüeintrags abhängige Vordergrundfarbe des Menüeintrags.
        /// </summary>
        public Func<ItemState, Color> ItemForegroundColor { get; set; }

        /// <summary>
        /// Die vom Zustand des Menüeintrags abhängige Hintergrundfarbe des Menüeintrags.
        /// </summary>
        public Func<ItemState, Color> ItemBackgroundColor { get; set; }

        /// <summary>
        /// Die horizontale Ausrichtung der Menüeinträge.
        /// </summary>
        public HorizontalAlignment ItemAlignX { get; set; }

        /// <summary>
        /// Die vertikale Ausrichtung der Menüeinträge.
        /// </summary>
        public VerticalAlignment ItemAlignY { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues Menu-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
        /// Zudem ist die Angabe der Zeichenreihenfolge Pflicht.
        /// </summary>
        public Menu (GameScreen screen, DisplayLayer drawOrder)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fügt einen Eintrag in das Menü ein. Falls der Menüeintrag \glqq null\grqq~oder leere Werte für
        /// Position, Größe, Farbe oder Ausrichtung hat, werden die Werte mit denen des Menüs überschrieben.
        /// </summary>
        public virtual void Add (MenuItem item)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Entfernt einen Eintrag aus dem Menü.
        /// </summary>
        public virtual void Delete (MenuItem item)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt einen Eintrag des Menüs zurück.
        /// </summary>
        public virtual MenuItem GetItem (int i)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt die Anzahl der Einträge des Menüs zurück.
        /// </summary>
        public virtual int Size ( )
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt einen Enumerator über die Einträge des Menüs zurück.
        /// [returntype=IEnumerator<MenuItem>]
        /// </summary>
        public virtual IEnumerator<MenuItem> GetEnumerator ( )
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

