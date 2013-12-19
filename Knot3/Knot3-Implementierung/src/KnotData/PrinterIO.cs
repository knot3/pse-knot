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
    /// Ein Exportformat für 3D-Drucker.
    /// </summary>
    public class PrinterIO : 
    {

        #region Properties

        /// <summary>
        /// Die gültigen Dateiendungen für das 3D-Drucker-Format.
        /// </summary>
        public IEnumerable<string> FileExtensions { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues PrinterIO-Objekt.
        /// </summary>
        public void PrinterIO ()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Exportiert den Knoten in einem gültigen 3D-Drucker-Format.
        /// </summary>
        public void Save (Knot knot)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt eine IOException zurück.
        /// </summary>
        public Knot Load (String filename)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gibt eine IOException zurück.
        /// </summary>
        public KnotMetaData LoadMetaData (String filename)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

