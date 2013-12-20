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

using GameObjects;
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace Core
{
    /// <summary>
    /// Jede Instanz der World-Klasse hält eine für diese Spielwelt verwendete Kamera als Attribut.
    /// Die Hauptfunktion der Kamera-Klasse ist das Berechnen der drei Matrizen, die für die Positionierung
    /// und Skalierung von 3D-Objekten in einer bestimmten Spielwelt benötigt werden, der View-, World- und Projection-Matrix.
    /// Um diese Matrizen zu berechnen, benötigt die Kamera unter Anderem Informationen über die aktuelle Kamera-Position,
    /// das aktuelle Kamera-Ziel und das Field of View.
    /// </summary>
    public class Camera : GameScreenComponent
    {

        #region Properties

        /// <summary>
        /// Die Position der Kamera.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Das Ziel der Kamera.
        /// </summary>
        public Vector3 Target { get; set; }

        /// <summary>
        /// Das Sichtfeld.
        /// </summary>
        public float FoV { get; set; }

        /// <summary>
        /// Die View-Matrix wird über die statische Methode CreateLookAt der Klasse Matrix des XNA-Frameworks
        /// mit Matrix.CreateLookAt (Position, Target, Vector3.Up) berechnet.
        /// </summary>
        public Matrix ViewMatrix { get; set; }

        /// <summary>
        /// Die World-Matrix wird mit Matrix.CreateFromYawPitchRoll und den drei Rotationswinkeln berechnet.
        /// </summary>
        public Matrix WorldMatrix { get; set; }

        /// <summary>
        /// Die Projektionsmatrix wird über die statische XNA-Methode Matrix.CreatePerspectiveFieldOfView berechnet.
        /// </summary>
        public Matrix ProjectionMatrix { get; set; }

        /// <summary>
        /// Eine Position, um die rotiert werden soll, wenn der User die rechte Maustaste gedrückt hält und die Maus bewegt.
        /// </summary>
        public Vector3 ArcballTarget { get; set; }

        /// <summary>
        /// Berechnet ein Bounding-Frustum, das benötigt wird, um festzustellen, ob ein 3D-Objekt sich im Blickfeld des Spielers befindet.
        /// </summary>
        public BoundingFrustum ViewFrustum { get; set; }

        /// <summary>
        /// Eine Referenz auf die Spielwelt, für welche die Kamera zuständig ist.
        /// </summary>
        private World World { get; set; }

        /// <summary>
        /// Die Rotationswinkel.
        /// </summary>
        public Angles3 Rotation { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt eine neue Kamera in einem bestimmten GameScreen für eine bestimmte Spielwelt.
        /// </summary>
        public Camera (GameScreen screen, World world)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Die Blickrichtung.
        /// </summary>
        public virtual Vector3 TargetDirection ( )
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Der Abstand zwischen der Kamera und dem Kamera-Ziel.
        /// </summary>
        public virtual float TargetDistance ( )
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
        /// Berechnet einen Strahl für die angegebenene 2D-Mausposition.
        /// </summary>
        public virtual Ray GetMouseRay (Vector2 mousePosition)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

