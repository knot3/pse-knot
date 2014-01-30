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
	/// Diese Klasse repräsentiert eine Option, die einen Wert aus einer distinkten Werteliste annehmen kann.
	/// </summary>
	public class DistinctOptionInfo : OptionInfo
	{
		#region Properties

		/// <summary>
		/// Eine Menge von Texten, welche die für die Option gültigen Werte beschreiben.
		/// </summary>
		public HashSet<string> ValidValues { get; private set; }

		public virtual Dictionary<string,string> DisplayValidValues { get; private set; }
		/// <summary>
		/// Eine Eigenschaft, die den aktuell abgespeicherten Wert zurück gibt.
		/// </summary>
		public override string Value
		{
			get {
				return base.Value;
			}
			set {
				if (ValidValues.Contains (value)) {
					base.Value = value;
				}
				else {
					base.Value = DefaultValue;
				}
			}
		}
		public virtual string DisplayValue
		{

			get {
				return Value;
			}

		}

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
			ValidValues = new HashSet<string> (validValues);
			ValidValues.Add (defaultValue);
			DisplayValidValues = new Dictionary<string,string> (ValidValues.ToDictionary(x=>x,x=>x));
		}

		#endregion
	}
}

