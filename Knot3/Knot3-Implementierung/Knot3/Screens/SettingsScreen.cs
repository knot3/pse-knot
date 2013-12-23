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
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Screens
{
    /// <summary>
    /// Ein Spielzustand, der das Haupt-Einstellungsmenü zeichnet.
    /// </summary>
    public class SettingsScreen : MenuScreen
    {
		#region Constructors

		public SettingsScreen (Knot3Game game)
			: base(game)
		{
		}

		#endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private VerticalMenu navigationMenu { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Wird für jeden Frame aufgerufen.
        /// </summary>
        public override void Update (GameTime time)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Fügt das Haupt-Einstellungsmenü in die Spielkomponentenliste ein.
        /// </summary>
        public override void Entered (GameScreen previousScreen, GameTime time)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

