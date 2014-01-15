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
	/// Ein Inputhandler, der Mauseingaben auf Widgets verarbeitet.
	/// </summary>
	public sealed class WidgetMouseHandler : GameScreenComponent
	{
		public WidgetMouseHandler (GameScreen screen)
		: base(screen, DisplayLayer.None)
		{
		}

		private class ClickEventComponent
		{
			public IMouseEventListener receiver;
			public DisplayLayer layer = 0;
			public Vector2 relativePosition;
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			ClickEventComponent best = null;
			foreach (IMouseEventListener receiver in Screen.Game.Components.OfType<IMouseEventListener>()) {
				// mouse input
				Rectangle bounds = receiver.Bounds ();
				bool hovered = bounds.Contains (InputManager.CurrentMouseState.ToPoint ());
				receiver.SetHovered (hovered);
				if (hovered && receiver.IsMouseEventEnabled && (best == null || receiver.Index > best.layer)) {
					best = new ClickEventComponent {
						receiver = receiver,
						layer = receiver.Index,
						relativePosition = InputManager.CurrentMouseState.ToVector2()-bounds.Location.ToVector2()
					};
				}
			}
			if (best != null) {
				if (InputManager.LeftMouseButton != ClickState.None) {
					best.receiver.OnLeftClick (best.relativePosition, InputManager.LeftMouseButton, time);
				}
				else if (InputManager.RightMouseButton != ClickState.None) {
					best.receiver.OnRightClick (best.relativePosition, InputManager.RightMouseButton, time);
				}
				else if (InputManager.CurrentMouseState.ScrollWheelValue > InputManager.PreviousMouseState.ScrollWheelValue) {
					best.receiver.OnScroll (-1);
				}
				else if (InputManager.CurrentMouseState.ScrollWheelValue < InputManager.PreviousMouseState.ScrollWheelValue) {
					best.receiver.OnScroll (+1);
				}
			}
		}
	}
}

