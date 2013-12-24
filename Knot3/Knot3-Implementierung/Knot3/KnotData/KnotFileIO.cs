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
    /// Implementiert das Speicherformat für Knoten.
    /// </summary>
    public class KnotFileIO : IKnotIO
    {

        #region Properties

        /// <summary>
        /// Die für eine Knoten-Datei gültigen Dateiendungen.
        /// </summary>
        public IEnumerable<string> FileExtensions { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein KnotFileIO-Objekt.
        /// </summary>
        public KnotFileIO ( )
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Speichert einen Knoten in dem Dateinamen, der in dem Knot-Objekt enthalten ist.
        /// </summary>
        public virtual void Save (Knot knot)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Lädt eines Knotens aus einer angegebenen Datei.
        /// </summary>
        public virtual Knot Load (string filename)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Lädt die Metadaten eines Knotens aus einer angegebenen Datei.
        /// </summary>
        public virtual KnotMetaData LoadMetaData (string filename)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

