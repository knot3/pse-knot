using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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

namespace Knot3.Debug
{
	public static class Profiler
	{
		public static TimeSpan Time (Action action)
		{
			Stopwatch stopwatch = Stopwatch.StartNew ();
			action ();
			stopwatch.Stop ();
			return stopwatch.Elapsed;
		}

		public static Hashtable ProfilerMap = new Hashtable ();

		public static HashtableActionWrapper ProfileDelegate = new HashtableActionWrapper ();
		public static HashtableWrapper Values = new HashtableWrapper ();


		public class HashtableWrapper
		{
			public double this [string str]
			{
				get {
					return (double)ProfilerMap [str];
				}
				set {
					ProfilerMap [str] = value;
				}
			}

			public bool ContainsKey (string str)
			{
				return ProfilerMap.ContainsKey (str);
			}
		}

		public class HashtableActionWrapper
		{
			public Action this [string str]
			{
				set {
					ProfilerMap [str] = Time (value).TotalMilliseconds;
				}
			}
		}
	}
}

