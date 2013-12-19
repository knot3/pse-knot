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
using KnotData;

namespace Widgets
{
    /// <summary>
    /// Repräsentiert einen Eintrag in einem Dropdown-Menü.
    /// </summary>
    public class DropDownEntry : 
    {

        #region Properties

        /// <summary>
        /// Der Text des Eintrags.
        /// </summary>
        public String Text { get; set; }

        #endregion

    }
}

