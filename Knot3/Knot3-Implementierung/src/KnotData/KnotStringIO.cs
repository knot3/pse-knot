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
using Widgets;

namespace KnotData
{
    /// <summary>
    /// Diese Klasse repräsentiert einen Parser für das Knoten-Austauschformat und enthält die
    /// eingelesenen Informationen wie den Namen des Knotens und die Kantenliste als Eigenschaften.
    /// </summary>
    public class KnotStringIO
    {

        #region Properties

        /// <summary>
        /// Der Name der eingelesenen Knotendatei oder des zugewiesenen Knotenobjektes.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Die Kanten der eingelesenen Knotendatei oder des zugewiesenen Knotenobjektes.
        /// </summary>
        public IEnumerable<Edge> Edges { get; set; }

        /// <summary>
        /// Die Anzahl der Kanten der eingelesenen Knotendatei oder des zugewiesenen Knotenobjektes.
        /// </summary>
        public int CountEdges { get; set; }

        /// <summary>
        /// Erstellt aus den \glqq Name\grqq~- und \glqq Edges\grqq~-Eigenschaften einen neue Zeichenkette,
        /// die als Dateiinhalt in einer Datei eines Spielstandes einen gültigen Knoten repräsentiert.
        /// </summary>
        public string Content { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Liest das in der angegebenen Zeichenkette enthaltene Dateiformat ein. Enthält es einen gültigen Knoten,
        /// so werden die \glqq Name\grqq~- und \glqq Edges\grqq~-Eigenschaften auf die eingelesenen Werte gesetzt.
        /// Enthält es einen ungültigen Knoten, so wird eine IOException geworfen und das Objekt wird nicht erstellt.
        /// </summary>
        public  KnotStringIO (string content)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Erstellt ein neues Objekt und setzt die \glqq Name\grqq~- und \glqq Edge\grqq~-Eigenschaften auf die
        /// im angegebenen Knoten enthaltenen Werte.
        /// </summary>
        public  KnotStringIO (Knot knot)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

