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
	/// <summary>
	/// Eine statische Klasse, die eine Referenz auf die zentrale Einstellungsdatei des Spiels enthält.
	/// </summary>
	public static class Options
	{
		#region Properties

		/// <summary>
		/// Die zentrale Einstellungsdatei des Spiels.
		/// </summary>
		public static ConfigFile Default
		{
			get {
				if (_default == null) {
					_default = new ConfigFile (FileUtility.SettingsDirectory + FileUtility.Separator.ToString () + "knot3.ini");
				}
				return _default;
			}
		}

		private static ConfigFile _default;

		public static ConfigFile Models
		{
			get {
				if (_models == null) {
					String seperatorString = FileUtility.Separator.ToString();
					_models = new ConfigFile (FileUtility.BaseDirectory + seperatorString
					                          + "Content" + seperatorString + "models.ini");
				}
				return _models;
			}
		}

		private static ConfigFile _models;

		#endregion
	}
}
