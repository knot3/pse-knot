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

		private class ScrollEventComponent
		{
			public IMouseScrollEventListener receiver;
			public DisplayLayer layer = DisplayLayer.None;
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
			UpdateMouseScroll (time);
			UpdateMouseMove (time);
		}

		private void UpdateMouseClick (GameTime time)
		{
			// Mausklicks
			ClickEventComponent best = null;
			foreach (IMouseClickEventListener receiver in Screen.Game.Components.OfType<IMouseClickEventListener>()) {
				Rectangle bounds = receiver.MouseClickBounds;
				bool hovered = bounds.Contains (InputManager.CurrentMouseState.ToPoint ());
				receiver.SetHovered (hovered, time);
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
			}
		}

		private void UpdateMouseScroll (GameTime time)
		{
			ScrollEventComponent best = null;
			foreach (IMouseScrollEventListener receiver in Screen.Game.Components.OfType<IMouseScrollEventListener>()) {
				Rectangle bounds = receiver.MouseScrollBounds;
				bool hovered = bounds.Contains (InputManager.CurrentMouseState.ToPoint ());

				if (hovered && receiver.IsMouseScrollEventEnabled && (best == null || receiver.Index > best.layer)) {
					best = new ScrollEventComponent {
						receiver = receiver,
						layer = receiver.Index,
					};
				}
			}
			if (best != null) {
				if (InputManager.CurrentMouseState.ScrollWheelValue > InputManager.PreviousMouseState.ScrollWheelValue) {
					best.receiver.OnScroll (-1);
				}
				else if (InputManager.CurrentMouseState.ScrollWheelValue < InputManager.PreviousMouseState.ScrollWheelValue) {
					best.receiver.OnScroll (+1);
				}
			}
		}

		private Point? lastLeftClickPosition;
		private Point? lastRightClickPosition;

		private void UpdateMouseMove (GameTime time)
		{
			// zuweisen der Positionen beim Drücken der Maus
			if (InputManager.PreviousMouseState.LeftButton == ButtonState.Released && InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed) {
				lastLeftClickPosition = InputManager.CurrentMouseState.ToPoint ();
			}
			else if (InputManager.CurrentMouseState.LeftButton == ButtonState.Released) {
				lastLeftClickPosition = null;
			}
			if (InputManager.PreviousMouseState.RightButton == ButtonState.Released && InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed) {
				lastRightClickPosition = InputManager.CurrentMouseState.ToPoint ();
			}
			else if (InputManager.CurrentMouseState.RightButton == ButtonState.Released) {
				lastRightClickPosition = null;
			}

			// die obersten Komponenten
			MoveEventComponent best = null;

			// aktuelle Position und die des letzten Frames
			Vector2 current = InputManager.CurrentMouseState.ToVector2 ();
			Vector2 previous = InputManager.PreviousMouseState.ToVector2 ();

			foreach (IMouseMoveEventListener receiver in Screen.Game.Components.OfType<IMouseMoveEventListener>()) {
				Rectangle bounds = receiver.MouseMoveBounds;

				// wenn die Komponente die Position beim Drücken der linken Maustaste enthält
				if (lastLeftClickPosition.HasValue && bounds.Contains (lastLeftClickPosition.Value)) {
					if (receiver.IsMouseMoveEventEnabled && (best == null || receiver.Index > best.layer)) {
						best = new MoveEventComponent {
							receiver = receiver,
							layer = receiver.Index,
							relativePositionPrevious = previous-bounds.Location.ToVector2(),
							relativePositionCurrent = current-bounds.Location.ToVector2(),
							relativePositionMove = current - previous
						};
					}
				}

				// wenn die Komponente die Position beim Drücken der rechten Maustaste enthält
				else if (lastRightClickPosition.HasValue && bounds.Contains (lastRightClickPosition.Value)) {
					if (receiver.IsMouseMoveEventEnabled && (best == null || receiver.Index > best.layer)) {
						best = new MoveEventComponent {
							receiver = receiver,
							layer = receiver.Index,
							relativePositionPrevious = previous-bounds.Location.ToVector2(),
							relativePositionCurrent = current-bounds.Location.ToVector2(),
							relativePositionMove = current - previous
						};
					}
				}

				// wenn die Komponente die aktuelle Position enthält
				else if (!lastLeftClickPosition.HasValue && !lastRightClickPosition.HasValue
				         && bounds.Contains (previous.ToPoint())) {
					if (receiver.IsMouseMoveEventEnabled && (best == null || receiver.Index > best.layer)) {
						best = new MoveEventComponent {
							receiver = receiver,
							layer = receiver.Index,
							relativePositionPrevious = previous-bounds.Location.ToVector2(),
							relativePositionCurrent = current-bounds.Location.ToVector2(),
							relativePositionMove = current - previous
						};
					}
				}
			}
			if (best != null) {
				if (best.relativePositionMove.Length () > 0
					|| InputManager.PreviousMouseState.LeftButton != InputManager.CurrentMouseState.LeftButton
					|| InputManager.PreviousMouseState.RightButton != InputManager.CurrentMouseState.RightButton) {
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

