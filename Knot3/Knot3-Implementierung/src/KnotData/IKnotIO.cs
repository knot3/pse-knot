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
    public interface IKnotIO
    {

        #region Properties

        /// <summary>
        /// Aufzählung der Dateierweiterungen.
        /// </summary>
        IEnumerable<string> FileExtensions { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Speichert einen Knoten.
        /// </summary>
        void Save (Knot knot)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Lädt einen Knoten.
        /// </summary>
        Knot Load (string filename)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Lädt die Metadaten eines Knotens.
        /// </summary>
        KnotMetaData LoadMetaData (string filename)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

