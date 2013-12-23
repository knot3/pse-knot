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

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.Widgets;

namespace Knot3.KnotData
{
    /// <summary>
    /// Enthält Metadaten zu einer Challenge.
    /// </summary>
    public class ChallengeMetaData
    {

        #region Properties

        /// <summary>
        /// Der Name der Challenge.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Der Ausgangsknoten, den der Spieler in den Referenzknoten transformiert.
        /// </summary>
        public KnotMetaData Start { get; set; }

        /// <summary>
        /// Der Referenzknoten, in den der Spieler den Ausgangsknoten transformiert.
        /// </summary>
        public KnotMetaData Target { get; set; }

        /// <summary>
        /// Das Format, aus dem die Metadaten der Challenge gelesen wurden oder null.
        /// </summary>
        public IChallengeIO Format { get; set; }

        /// <summary>
        /// Der Dateiname, aus dem die Metadaten der Challenge gelesen wurden oder in den sie abgespeichert werden.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Ein öffentlicher Enumerator, der die Bestenliste unabhängig von der darunterliegenden Datenstruktur zugänglich macht.
        /// </summary>
        public IEnumerator<KeyValuePair<string, int>> Highscore { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein Challenge-Metadaten-Objekt mit einem gegebenen Namen und den Metadaten des Ausgangs- und Referenzknotens.
        /// </summary>
        public ChallengeMetaData (string name, KnotMetaData start, KnotMetaData target, string filename, IChallengeIO format)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

