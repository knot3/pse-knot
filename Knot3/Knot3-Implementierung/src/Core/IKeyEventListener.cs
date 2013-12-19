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

using GameObjects;
using Screens;
using RenderEffects;
using KnotData;
using Widgets;

namespace Core
{
    /// <summary>
    /// Eine Schnittstelle, die von Klassen implementiert wird, welche auf Tastatureingaben reagieren.
    /// </summary>
    public interface IKeyEventListener : 
    {

        #region Properties

        /// <summary>
        /// Die Eingabepriorität.
        /// </summary>
        public DisplayLayer Index { get; set; }

        /// <summary>
        /// Zeigt an, ob die Klasse zur Zeit auf Tastatureingaben reagiert.
        /// </summary>
        public Boolean IsKeyEventEnabled { get; set; }

        /// <summary>
        /// Die Tasten, auf die die Klasse reagiert.
        /// </summary>
        public List<Keys> ValidKeys { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Die Reaktion auf eine Tasteneingabe.
        /// </summary>
        public virtual void OnKeyEvent ()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

