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
    /// Diese Schnittstelle enthält Methoden, die von Speicherformaten für Challenges implementiert werden müssen.
    /// </summary>
    public interface IChallengeIO
    {

        #region Methods

        /// <summary>
        /// Speichert eine Challenge.
        /// </summary>
        void Save (Challenge challenge)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Lädt eine Challenge.
        /// </summary>
        Challenge Load (string filename)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Lädt die Metadaten einer Challenge.
        /// </summary>
        ChallengeMetaData LoadMetaData (string filename)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

