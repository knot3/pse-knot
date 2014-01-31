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
	public sealed class FloatOptionInfo : DistinctOptionInfo
	{
		#region Properties

		/// <summary>
		/// Eine Eigenschaft, die den aktuell abgespeicherten Wert zurückgibt.
		/// </summary>
		public new float Value
		{
			get {
				return stringToFloat (base.Value);
			}
			set {
				base.Value = convertToString (value);
			}
		}

		public override string DisplayValue
		{
			get {
				return ""+stringToFloat(base.Value);
			}
		}

		public override Dictionary<string,string> DisplayValidValues
		{
			get {
				return new Dictionary<string,string> (base.ValidValues.ToDictionary(s =>""+stringToFloat(s),s=>s));
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt eine neue Option, welche die Werte \glqq Wahr\grqq~oder \glqq Falsch\grqq~annehmen kann. Mit dem angegebenen Namen, in dem
		/// angegebenen Abschnitt der angegebenen Einstellungsdatei.
		/// [base=section, name, defaultValue?ConfigFile.True:ConfigFile.False, ValidValues, configFile]
		/// </summary>
		public FloatOptionInfo (string section, string name, float defaultValue, IEnumerable<float> validValues, ConfigFile configFile)
		: base(section, name, convertToString( defaultValue),validValues.Select(convertToString), configFile)
		{
		}

		private static String convertToString(float f)
		{
			return ("" + (int)(f*1000f));
		}
		private static float stringToFloat (string s)
		{
			int i;
			bool result = Int32.TryParse (s, out i);
			if (true == result) {
				return ((float)i) / 1000f;
			}
			else {
				return 0;
			}
		}
		#endregion
	}
}
