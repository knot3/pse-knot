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

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Utilities
{
	public static class DictionaryHelper
	{
		public static void Add<KeyType, ListType, ValueType> (this Dictionary<KeyType, ListType> dict,
		        KeyType key, ValueType value)
		where ListType : IList<ValueType>, new()
		{
			if (!dict.ContainsKey (key)) {
				dict.Add (key, new ListType ());
			}
			dict [key].Add (value);
		}
	}
}

