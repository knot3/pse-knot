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

namespace Knot3.Core
{
	/// <summary>
	/// Diese Klasse repräsentiert eine Option, welche die Werte \glqq Wahr\grqq~oder \glqq Falsch\grqq~annehmen kann.
	/// </summary>
	public sealed class BooleanOptionInfo : DistinctOptionInfo
	{
		#region Properties

		/// <summary>
		/// Eine Eigenschaft, die den aktuell abgespeicherten Wert zurückgibt.
		/// </summary>
		public new bool Value
		{
			get {
				return base.Value == ConfigFile.True ? true : false;
			}
			set {
				base.Value = value ? ConfigFile.True : ConfigFile.False;
			}
		}

		public new static string[] ValidValues = new string[] {
			ConfigFile.True,
			ConfigFile.False
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt eine neue Option, welche die Werte \glqq Wahr\grqq~oder \glqq Falsch\grqq~annehmen kann. Mit dem angegebenen Namen, in dem
		/// angegebenen Abschnitt der angegebenen Einstellungsdatei.
		/// [base=section, name, defaultValue?ConfigFile.True:ConfigFile.False, ValidValues, configFile]
		/// </summary>
		public BooleanOptionInfo (string section, string name, bool defaultValue, ConfigFile configFile)
		: base(section, name, defaultValue?ConfigFile.True:ConfigFile.False, ValidValues, configFile)
		{
		}

		#endregion
	}
}
