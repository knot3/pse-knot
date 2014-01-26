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
		public WidgetMouseHandler (IGameScreen screen)
		: base(screen, DisplayLayer.None)
		{
		}

		private class ClickEventComponent
		{
			public IMouseClickEventListener receiver;
			public DisplayLayer layer = DisplayLayer.None;
			public Vector2 relativePosition;
		}

		private class MoveEventComponent
		{
			public IMouseMoveEventListener receiver;
			public DisplayLayer layer = DisplayLayer.None;
			public Vector2 relativePositionPrevious;
			public Vector2 relativePositionCurrent;
			public Vector2 relativePositionMove;
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			UpdateMouseClick (time);
			UpdateMouseMove (time);
		}

		private void UpdateMouseClick (GameTime time)
		{
			// Mausklicks
			ClickEventComponent best = null;
			foreach (IMouseClickEventListener receiver in Screen.Game.Components.OfType<IMouseClickEventListener>()) {
				Rectangle bounds = receiver.Bounds ();
				bool hovered = bounds.Contains (InputManager.CurrentMouseState.ToPoint ());
				receiver.SetHovered (hovered);
				if (hovered && receiver.IsMouseClickEventEnabled && (best == null || receiver.Index > best.layer)) {
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

		private void UpdateMouseMove (GameTime time)
		{
			// Mausbewegungen
			MoveEventComponent best = null;
			foreach (IMouseMoveEventListener receiver in Screen.Game.Components.OfType<IMouseMoveEventListener>()) {
				Rectangle bounds = receiver.Bounds ();
				bool hovered = bounds.Contains (InputManager.PreviousMouseState.ToPoint ());

				if (hovered && receiver.IsMouseMoveEventEnabled && (best == null || receiver.Index > best.layer)) {
					Vector2 current = InputManager.CurrentMouseState.ToVector2 ();
					Vector2 previous = InputManager.PreviousMouseState.ToVector2 ();

					best = new MoveEventComponent {
						receiver = receiver,
						layer = receiver.Index,
						relativePositionPrevious = previous-bounds.Location.ToVector2(),
						relativePositionCurrent = current-bounds.Location.ToVector2(),
						relativePositionMove = current - previous
					};
				}
			}
			if (best != null) {
				if (best.relativePositionMove.Length () > 0) {
					if (InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed) {
						best.receiver.OnLeftMove (
							previousPosition: best.relativePositionPrevious,
							currentPosition: best.relativePositionCurrent,
							move: best.relativePositionMove,
							time: time
						);
					}
					else if (InputManager.CurrentMouseState.RightButton == ButtonState.Pressed) {
						best.receiver.OnRightMove (
							previousPosition: best.relativePositionPrevious,
							currentPosition: best.relativePositionCurrent,
							move: best.relativePositionMove,
							time: time
						);
					}
					else {
						best.receiver.OnMove (
							previousPosition: best.relativePositionPrevious,
							currentPosition: best.relativePositionCurrent,
							move: best.relativePositionMove,
							time: time
						);
					}
				}
			}
		}
	}
}

