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
using Widgets;

namespace KnotData
{
    /// <summary>
    /// Diese Schnittstelle enthält Methoden, die von Speicherformaten für Knoten implementiert werden müssen.
    /// </summary>
    public interface IKnotIO : 
    {

        #region Properties

        /// <summary>
        /// Aufzählung der Dateierweiterungen.
        /// </summary>
        public IEnumerable<string> FileExtensions { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Speichert einen Knoten.
        /// </summary>
        public void Save (Knot knot)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Lädt einen Knoten.
        /// </summary>
        public Knot Load (String filename)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Lädt die Metadaten eines Knotens.
        /// </summary>
        public KnotMetaData LoadMetaData (String filename)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

