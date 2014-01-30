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

		private class KeyEventComponent
		{
			public IKeyEventListener receiver;
			public DisplayLayer layer = DisplayLayer.None;
			public KeyEvent keyEvent;
			public List<Keys> keys;
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			KeyEventComponent best = null;
			foreach (IKeyEventListener component in Screen.Game.Components.OfType<IKeyEventListener>()) {
				// keyboard input
				IKeyEventListener receiver = component as IKeyEventListener;
				KeyEvent keyEvent = KeyEvent.None;
				List<Keys> keysInvolved = new List<Keys> ();

				foreach (Keys key in receiver.ValidKeys) {
					//Console.WriteLine("receiver="+receiver+", validkeys="+key+", receiver.IsKeyEventEnabled="+((dynamic)receiver).IsVisible);

				if (key.IsDown ()) {
						keysInvolved.Add (key);
						keyEvent = KeyEvent.KeyDown;
					}
					else if (key.IsHeldDown ()) {
						keysInvolved.Add (key);
						keyEvent = KeyEvent.KeyHeldDown;
					}
				}

				if (keysInvolved.Count > 0 && receiver.IsKeyEventEnabled && (best == null || (int)component.Index >= (int)best.layer)) {
					best = new KeyEventComponent {
						receiver = receiver,
						layer = receiver.Index,
						keyEvent = keyEvent,
						keys = keysInvolved
					};
				}
			}
			if (best != null) {
				best.receiver.OnKeyEvent (best.keys, best.keyEvent, time);
			}
		}
	}
}

