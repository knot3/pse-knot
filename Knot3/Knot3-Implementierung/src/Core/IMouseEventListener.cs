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
    /// Eine Schnittstelle, die von Klassen implementiert wird, die auf Maus-Klicks reagieren.
    /// </summary>
    public interface IMouseEventListener : 
    {

        #region Properties

        /// <summary>
        /// Die Eingabepriorität.
        /// </summary>
        public DisplayLayer Index { get; set; }

        /// <summary>
        /// Ob die Klasse zur Zeit auf Mausklicks reagiert.
        /// </summary>
        public Boolean IsMouseEventEnabled { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Die Ausmaße des von der Klasse repräsentierten Objektes.
        /// </summary>
        public virtual Rectangle Bounds ()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Die Reaktion auf einen Linksklick.
        /// </summary>
        public virtual void OnLeftClick (Vector2 position, ClickState state, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Die Reaktion auf einen Rechtsklick.
        /// </summary>
        public virtual void OnRightClick (Vector2 position, ClickState state, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

