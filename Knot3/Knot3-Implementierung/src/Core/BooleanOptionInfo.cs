using System;
using System.Collections.Generic;
using System.Linq;

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
    /// Diese Klasse repräsentiert eine Option, welche die Werte \glqq Wahr\grqq~oder \glqq Falsch\grqq~annehmen kann.
    /// </summary>
    public class BooleanOptionInfo : DistinctOptionInfo
    {

        #region Properties

        /// <summary>
        /// Ein Property, das den aktuell abgespeicherten Wert zurückgibt.
        /// </summary>
        public bool Value { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt eine neue Option, welche die Werte \glqq Wahr\grqq~oder \glqq Falsch\grqq~annehmen kann. Mit dem angegebenen Namen, in dem
        /// angegebenen Abschnitt der angegebenen Einstellungsdatei.
        /// </summary>
        public void BooleanOptionInfo (String section, String name, String defaultValue, ConfigFile configFile)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

