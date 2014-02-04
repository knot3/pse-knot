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
	public class MonoHelper
	{
		public static bool IsRunningOnMono ()
		{
			return Type.GetType ("Mono.Runtime") != null;
		}

		public static bool IsRunningOnMonogame ()
		{
			return true;
		}

		public static bool IsRunningOnLinux ()
		{
			return Environment.OSVersion.Platform == PlatformID.Unix;
		}

		public static bool IsRunningOnWindows ()
		{
			return !IsRunningOnLinux ();
		}
	}
}
