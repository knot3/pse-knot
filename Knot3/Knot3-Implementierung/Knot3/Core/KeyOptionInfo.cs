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
using Knot3.Utilities;

namespace Knot3.Core
{
	public class KeyOptionInfo : DistinctOptionInfo
	{
		#region Properties

		/// <summary>
		/// Eine Eigenschaft, die den aktuell abgespeicherten Wert zurückgibt.
		/// </summary>
		public new Keys Value
		{
			get {
				return base.Value.ToEnumValue<Keys> ();
			}
			set {
				base.Value = value.ToEnumDescription<Keys> ();
			}
		}

		public new static IEnumerable<string> ValidValues = typeof(Keys).ToEnumValues<Keys> ().ToEnumDescriptions<Keys> ();

		#endregion

		#region Constructors

		public KeyOptionInfo (string section, string name, Keys defaultValue, ConfigFile configFile)
		: base(section, name, defaultValue.ToEnumDescription<Keys> (), ValidValues, configFile)
		{
		}

		#endregion
	}
}
