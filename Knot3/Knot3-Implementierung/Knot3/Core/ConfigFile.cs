using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
using Knot3.Utilities;

namespace Knot3.Core
{
	/// <summary>
	/// Repräsentiert eine Einstellungsdatei.
	/// </summary>
	public sealed class ConfigFile
	{
        #region Properties

		/// <summary>
		/// Die Repräsentation des Wahrheitswerts "wahr" als String in einer Einstellungsdatei.
		/// </summary>
		public static string True { get { return "true"; } }

		/// <summary>
		/// Die Repräsentation des Wahrheitswerts "falsch" als String in einer Einstellungsdatei.
		/// </summary>
		public static string False { get { return "false"; } }
		
		private string Filename;
		private IniFile ini;

        #endregion

		#region Constructors
		
		public ConfigFile (string filename)
		{
			// load ini file
			Filename = filename;
			
			// create a new ini parser
			using (StreamWriter w = File.AppendText(Filename)) {
			}
			ini = new IniFile (Filename);
		}

		#endregion

        #region Methods

		/// <summary>
		/// Setzt den Wert der Option mit dem angegebenen Namen in den angegebenen Abschnitt auf den angegebenen Wert.
		/// </summary>
		public void SetOption (string section, string option, string _value)
		{
			ini [section, option] = _value;
		}

		/// <summary>
		/// Gibt den aktuell in der Datei vorhandenen Wert für die angegebene Option in dem angegebenen Abschnitt zurück.
		/// </summary>
		public string GetOption (string section, string option, string defaultValue)
		{
			return ini [section, option, defaultValue];
		}

		/// <summary>
		/// Setzt den Wert der Option mit dem angegebenen Namen in den angegebenen Abschnitt auf den angegebenen Wert.
		/// </summary>
		public void SetOption (string section, string option, bool _value)
		{
			SetOption (section, option, _value ? True : False);
		}

		/// <summary>
		/// Gibt den aktuell in der Datei vorhandenen Wert für die angegebene Option in dem angegebenen Abschnitt zurück.
		/// </summary>
		public bool GetOption (string section, string option, bool defaultValue)
		{
			return GetOption (section, option, defaultValue ? True : False) == True ? true : false;
		}

		public bool this [string section, string option, bool defaultValue = false] {
			get {
				return GetOption (section, option, defaultValue);
			}
			set {
				SetOption (section, option, value);
			}
		}

		public string this [string section, string option, string defaultValue = null] {
			get {
				return GetOption (section, option, defaultValue);
			}
			set {
				SetOption (section, option, value);
			}
		}

        #endregion
	}
}

