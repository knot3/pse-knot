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
    /// Ein Objekt dieser Klasse repräsentiert eine Challenge.
    /// </summary>
    public class Challenge : 
    {

        #region Properties

        /// <summary>
        /// Der Ausgangsknoten, den der Spieler in den Referenzknoten transformiert.
        /// </summary>
        public Knot Start { get; set; }

        /// <summary>
        /// Der Referenzknoten, in den der Spieler den Ausgangsknoten transformiert.
        /// </summary>
        public Knot Target { get; set; }

        /// <summary>
        /// Eine sortierte Bestenliste.
        /// </summary>
        private SortedList<Integer, String> highscore { get; set; }

        /// <summary>
        /// Das Speicherformat der Challenge.
        /// </summary>
        private IChallengeIO format { get; set; }

        /// <summary>
        /// Ein öffentlicher Enumerator, der die Bestenliste unabhängig von der darunterliegenden Datenstruktur zugänglich macht.
        /// </summary>
        public IEnumerator<KeyValuePair<String, Integer>> Highscore { get; set; }

        /// <summary>
        /// Die Metadaten der Challenge.
        /// </summary>
        public ChallengeMetaData MetaData { get; set; }

        /// <summary>
        /// Der Name der Challenge.
        /// </summary>
        public String Name { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein Challenge-Objekt aus einem gegebenen Challenge-Metadaten-Objekt.
        /// Erstellt ein Challenge-Objekt aus einer gegebenen Challenge-Datei.
        /// </summary>
        public void Challenge (ChallengeMetaData meta, Knot start, Knot target)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fügt eine neue Bestzeit eines bestimmten Spielers in die Bestenliste ein.
        /// </summary>
        public void AddToHighscore (String name, Integer time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

