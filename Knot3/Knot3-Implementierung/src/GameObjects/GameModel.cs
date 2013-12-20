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
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace GameObjects
{
    /// <summary>
    /// Repräsentiert ein 3D-Modell in einer Spielwelt.
    /// </summary>
    public class GameModel
    {

        #region Properties

        /// <summary>
        /// Die Transparenz des Modells.
        /// </summary>
        public float Alpha { get; set; }

        /// <summary>
        /// Die Farbe des Modells.
        /// </summary>
        public Color BaseColor { get; set; }

        /// <summary>
        /// Die Auswahlfarbe des Modells.
        /// </summary>
        public Color HightlightColor { get; set; }

        /// <summary>
        /// Die Intensität der Auswahlfarbe.
        /// </summary>
        public float HighlightIntensity { get; set; }

        /// <summary>
        /// Die Modellinformationen wie Position, Skalierung und der Dateiname des 3D-Modells.
        /// </summary>
        public GameModelInfo Info { get; set; }

        /// <summary>
        /// Die Klasse des XNA-Frameworks, die ein 3D-Modell repräsentiert.
        /// </summary>
        public XNA.Model Model { get; set; }

        /// <summary>
        /// Die Spielwelt, in der sich das 3D-Modell befindet.
        /// </summary>
        public World World { get; set; }

        /// <summary>
        /// Die Weltmatrix des 3D-Modells in der angegebenen Spielwelt.
        /// </summary>
        public Matrix WorldMatrix { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues 3D-Modell in dem angegebenen Spielzustand mit den angegebenen Modellinformationen.
        /// </summary>
        public  GameModel (GameScreen screen, GameModelInfo info)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gibt die Mitte des 3D-Modells zurück.
        /// </summary>
        public virtual Vector3 Center ( )
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public virtual void Update (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Zeichnet das 3D-Modell in der angegebenen Spielwelt mit dem aktuellen Rendereffekt der Spielwelt.
        /// </summary>
        public virtual void Draw (GameTime GameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Überprüft, ob der Mausstrahl das 3D-Modell schneidet.
        /// </summary>
        public virtual GameObjectDistance Intersects (Ray Ray)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

