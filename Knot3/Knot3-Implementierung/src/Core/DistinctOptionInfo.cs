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
    /// Diese Klasse repräsentiert eine Option, die eine distinkte Werteliste annehmen kann.
    /// </summary>
    public class DistinctOptionInfo : OptionInfo
    {

        #region Properties

        /// <summary>
        /// Eine Menge von Texten, welche die für die Option gültigen Werte beschreiben.
        /// </summary>
        public HashSet<string> ValidValues { get; set; }

        /// <summary>
        /// Eine Eigenschaft, die den aktuell abgespeicherten Wert zurück gibt.
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Erstellt eine neue Option, die einen der angegebenen Werte aus validValues annehmen kann, mit dem angegebenen Namen in dem
        /// angegebenen Abschnitt der angegebenen Einstellungsdatei.
        /// [base=section, name, defaultValue, configFile]
        /// </summary>
        public DistinctOptionInfo (string section, string name, string defaultValue, IEnumerable<string> validValues, ConfigFile configFile)
            : base(section, name, defaultValue, configFile)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}

