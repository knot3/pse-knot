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
using RenderEffects;
using KnotData;
using Widgets;

namespace Screens
{
    /// <summary>
    /// Die zentrale Spielklasse, die von der \glqq Game \grqq -Klasse des XNA-Frameworks erbt.
    /// </summary>
    public class Knot3Game : XNA.Game
    {

        #region Properties

        /// <summary>
        /// Wird dieses Attribut ausgelesen, dann gibt es einen Wahrheitswert zurück, der angibt,
        /// ob sich das Spiel im Vollbildmodus befindet. Wird dieses Attribut auf einen Wert gesetzt,
        /// dann wird der Modus entweder gewechselt oder beibehalten, falls es auf denselben Wert gesetzt wird.
        /// </summary>
        public Boolean IsFullScreen { get; set; }

        /// <summary>
        /// Enthält als oberste Element den aktuellen Spielzustand und darunter die zuvor aktiven Spielzustände.
        /// </summary>
        public Stack<GameScreen> Screens { get; set; }

        /// <summary>
        /// Dieses Attribut dient sowohl zum Setzen des Aktivierungszustandes der vertikalen Synchronisation,
        /// als auch zum Auslesen dieses Zustandes.
        /// </summary>
        public Boolean VSync { get; set; }

        /// <summary>
        /// Der aktuelle Grafikgeräteverwalter des XNA-Frameworks.
        /// </summary>
        public GraphicsDeviceManager Graphics { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt ein neues zentrales Spielobjekt und setzt die Auflösung des BackBuffers auf
        /// die in der Einstellungsdatei gespeicherte Auflösung oder falls nicht vorhanden auf die aktuelle
        /// Bildschirmauflösung und wechselt in den Vollbildmodus.
        /// </summary>
        public  Knot3Game ()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Ruft die Draw()-Methode des aktuellen Spielzustands auf.
        /// </summary>
        public virtual void Draw (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Initialisiert die Attribute dieser Klasse.
        /// </summary>
        public virtual void Initialize ()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Macht nichts. Das Freigeben aller Objekte wird von der automatischen Speicherbereinigung übernommen.
        /// </summary>
        public virtual void UnloadContent ()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public virtual void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

