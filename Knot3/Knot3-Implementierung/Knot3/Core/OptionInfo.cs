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

using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Development;

namespace Knot3.Core
{
	/// <summary>
	/// Enthält Informationen über einen Eintrag in einer Einstellungsdatei.
	/// </summary>
	public class OptionInfo
	{
		#region Properties

		/// <summary>
		/// Die Einstellungsdatei.
		/// </summary>
		private ConfigFile configFile;

		/// <summary>
		/// Der Abschnitt der Einstellungsdatei.
		/// </summary>
		public string Section { get; private set; }

		/// <summary>
		/// Der Name der Option.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Der Standardwert der Option.
		/// </summary>
		public string DefaultValue { get; private set; }

		/// <summary>
		/// Der Wert der Option.
		/// </summary>
		public virtual string Value
		{
			get {
				Log.Debug ("OptionInfo: " + Section + "." + Name + " => " + configFile [Section, Name, DefaultValue]);
				return configFile [Section, Name, DefaultValue];
			}
			set {
				Log.Debug ("OptionInfo: " + Section + "." + Name + " <= " + value);
				configFile [Section, Name, DefaultValue] = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues OptionsInfo-Objekt aus den übergegebenen Werten.
		/// </summary>
		public OptionInfo (string section, string name, string defaultValue, ConfigFile configFile)
		{
			Section = section;
			Name = name;
			DefaultValue = defaultValue;
			this.configFile = configFile != null ? configFile : Options.Default;
		}

		#endregion
	}
}
