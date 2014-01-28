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
	public static class InputHelper
	{
		/// <summary>
		/// Wurde die aktuelle Taste gedrückt und war sie im letzten Frame nicht gedrückt?
		/// </summary>
		public static bool IsDown (this Keys key)
		{
			// Is the key down?
			if (InputManager.CurrentKeyboardState.IsKeyDown (key)) {
				// If not down last update, key has just been pressed.
				if (!InputManager.PreviousKeyboardState.IsKeyDown (key)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Wird die aktuelle Taste gedrückt gehalten?
		/// </summary>
		public static bool IsHeldDown (this Keys key)
		{
			return InputManager.CurrentKeyboardState.IsKeyDown (key);
		}
	}
}

