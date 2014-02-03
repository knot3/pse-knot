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
			foreach (IMouseClickEventListener component in Screen.Game.Components.OfType<IMouseClickEventListener>()
			         .Where(c => c.IsMouseClickEventEnabled).OrderByDescending(c => c.Index.Index)) {
				Rectangle bounds = component.MouseClickBounds;
				bool hovered = bounds.Contains (InputManager.CurrentMouseState.ToPoint ());
				component.SetHovered (hovered, time);
				if (hovered) {
					Vector2 relativePosition = InputManager.CurrentMouseState.ToVector2 () - bounds.Location.ToVector2 ();
					if (InputManager.LeftMouseButton != ClickState.None) {
						component.OnLeftClick (relativePosition, InputManager.LeftMouseButton, time);
					}
					else if (InputManager.RightMouseButton != ClickState.None) {
						component.OnRightClick (relativePosition, InputManager.RightMouseButton, time);
					}
				}
			}
		}

		private void UpdateMouseScroll (GameTime time)
		{
			foreach (IMouseScrollEventListener component in Screen.Game.Components.OfType<IMouseScrollEventListener>()
			         .Where(c => c.IsMouseScrollEventEnabled).OrderByDescending(c => c.Index.Index)) {
				Rectangle bounds = component.MouseScrollBounds;
				bool hovered = bounds.Contains (InputManager.CurrentMouseState.ToPoint ());

				if (hovered) {
					if (InputManager.CurrentMouseState.ScrollWheelValue > InputManager.PreviousMouseState.ScrollWheelValue) {
						component.OnScroll (-1);
					}
					else if (InputManager.CurrentMouseState.ScrollWheelValue < InputManager.PreviousMouseState.ScrollWheelValue) {
						component.OnScroll (+1);
					}
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

			// aktuelle Position und die des letzten Frames
			Vector2 current = InputManager.CurrentMouseState.ToVector2 ();
			Vector2 previous = InputManager.PreviousMouseState.ToVector2 ();

			foreach (IMouseMoveEventListener component in Screen.Game.Components.OfType<IMouseMoveEventListener>()
			         .Where(c => c.IsMouseMoveEventEnabled).OrderByDescending(c => c.Index.Index)) {
				Rectangle bounds = component.MouseMoveBounds;

				Vector2 relativePositionPrevious = previous - bounds.Location.ToVector2 ();
				Vector2 relativePositionCurrent = current - bounds.Location.ToVector2 ();
				Vector2 relativePositionMove = current - previous;

				bool notify = false;
				// wenn die Komponente die Position beim Drücken der linken Maustaste enthält
				if (lastLeftClickPosition.HasValue && bounds.Contains (lastLeftClickPosition.Value)) {
					notify = true;
				}

				// wenn die Komponente die Position beim Drücken der rechten Maustaste enthält
				else if (lastRightClickPosition.HasValue && bounds.Contains (lastRightClickPosition.Value)) {
					notify = true;
				}

				// wenn die Komponente die aktuelle Position enthält
				else if (!lastLeftClickPosition.HasValue && !lastRightClickPosition.HasValue
					&& bounds.Contains (previous.ToPoint ())) {
					notify = true;
				}

				// Console.WriteLine("notify="+notify+", component="+component+", cntains="+bounds.Contains (lastLeftClickPosition ?? Point.Zero));

				if (notify && relativePositionMove.Length () > 0
					|| InputManager.PreviousMouseState.LeftButton != InputManager.CurrentMouseState.LeftButton
					|| InputManager.PreviousMouseState.RightButton != InputManager.CurrentMouseState.RightButton) {

					if (InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed) {
						component.OnLeftMove (
						    previousPosition: relativePositionPrevious,
						    currentPosition: relativePositionCurrent,
						    move: relativePositionMove,
						    time: time
						);
					}
					else if (InputManager.CurrentMouseState.RightButton == ButtonState.Pressed) {
						component.OnRightMove (
						    previousPosition: relativePositionPrevious,
						    currentPosition: relativePositionCurrent,
						    move: relativePositionMove,
						    time: time
						);
					}
					else {
						component.OnMove (
						    previousPosition: relativePositionPrevious,
						    currentPosition: relativePositionCurrent,
						    move: relativePositionMove,
						    time: time
						);
					}
					break;
				}
			}
		}
	}
}
