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
using Knot3.Utilities;

namespace Knot3.Widgets
{
	/// <summary>
	/// Ein Inputhandler, der Tastatureingaben auf Widgets verarbeitet.
	/// </summary>
	public sealed class WidgetKeyHandler : GameScreenComponent
	{
		public WidgetKeyHandler (IGameScreen screen)
		: base(screen, DisplayLayer.None)
		{
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			foreach (IKeyEventListener component in Screen.Game.Components.OfType<IKeyEventListener>()
			         .Where(c => c.IsKeyEventEnabled).OrderByDescending(c => c.Index.Index)) {
				// keyboard input
				KeyEvent keyEvent = KeyEvent.None;
				List<Keys> keysInvolved = new List<Keys> ();

				foreach (Keys key in component.ValidKeys) {
					// Console.WriteLine("receiver="+receiver+",validkeys="+key+", receiver.IsKeyEventEnabled="+((dynamic)receiver).IsVisible);

					if (key.IsDown ()) {
						keysInvolved.Add (key);
						keyEvent = KeyEvent.KeyDown;
					}
					else if (key.IsHeldDown ()) {
						keysInvolved.Add (key);
						keyEvent = KeyEvent.KeyHeldDown;
					}
				}

				if (keysInvolved.Count > 0) {
					component.OnKeyEvent (keysInvolved, keyEvent, time);
					break;
				}
				if (component.IsModal) {
					break;
				}
			}
		}
	}
}
