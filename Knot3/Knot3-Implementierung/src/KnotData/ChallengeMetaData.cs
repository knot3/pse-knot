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
    /// Enthält Metadaten zu einer Challenge.
    /// </summary>
    public class ChallengeMetaData : 
    {

        #region Properties

        /// <summary>
        /// Der Name der Challenge.
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Der Ausgangsknoten, den der Spieler in den Referenzknoten transformiert.
        /// </summary>
        public virtual KnotMetaData Start { get; set; }

        /// <summary>
        /// Der Referenzknoten, in den der Spieler den Ausgangsknoten transformiert.
        /// </summary>
        public virtual KnotMetaData Target { get; set; }

        /// <summary>
        /// Das Format, aus dem die Metadaten der Challenge gelesen wurden oder null.
        /// </summary>
        public virtual IChallengeIO Format { get; set; }

        /// <summary>
        /// Der Dateiname, aus dem die Metadaten der Challenge gelesen wurden oder in den sie abgespeichert werden.
        /// </summary>
        public virtual String Filename { get; set; }

        /// <summary>
        /// Ein öffentlicher Enumerator, der die Bestenliste unabhängig von der darunterliegenden Datenstruktur zugänglich macht.
        /// </summary>
        public virtual IEnumerator<KeyValuePair<String, Integer>> Highscore { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein Challenge-Metadaten-Objekt mit einem gegebenen Namen und den Metadaten des Ausgangs- und Referenzknotens.
        /// </summary>
        public virtual void ChallengeMetaData (String name, KnotMetaData start, KnotMetaData target, String filename, IChallengeIO format)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

