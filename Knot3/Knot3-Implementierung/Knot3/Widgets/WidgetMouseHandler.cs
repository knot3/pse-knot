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

		private ScreenPoint lastLeftClickPosition;
		private ScreenPoint lastRightClickPosition;

		private void UpdateMouseMove (GameTime time)
		{
			// aktuelle Position und die des letzten Frames
			ScreenPoint current = InputManager.CurrentMouseState.ToScreenPoint (Screen);
			ScreenPoint previous = InputManager.PreviousMouseState.ToScreenPoint (Screen);

			// zuweisen der Positionen beim Drücken der Maus
			if (InputManager.PreviousMouseState.LeftButton == ButtonState.Released && InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed) {
				lastLeftClickPosition = current;
			}
			else if (InputManager.CurrentMouseState.LeftButton == ButtonState.Released) {
				lastLeftClickPosition = null;
			}
			if (InputManager.PreviousMouseState.RightButton == ButtonState.Released && InputManager.CurrentMouseState.RightButton == ButtonState.Pressed) {
				lastRightClickPosition = current;
			}
			else if (InputManager.CurrentMouseState.RightButton == ButtonState.Released) {
				lastRightClickPosition = null;
			}
			//Log.WriteLine("left="+(lastLeftClickPosition ?? ScreenPoint.Zero(Screen))+"right="+(lastRightClickPosition ?? ScreenPoint.Zero(Screen)));

			foreach (IMouseMoveEventListener component in Screen.Game.Components.OfType<IMouseMoveEventListener>()
			         .Where(c => c.IsMouseMoveEventEnabled).OrderByDescending(c => c.Index.Index)) {
				Bounds bounds = component.MouseMoveBounds;

				ScreenPoint relativePositionPrevious = previous - bounds.Position;
				ScreenPoint relativePositionCurrent = current - bounds.Position;
				ScreenPoint relativePositionMove = current - previous;

				bool notify = false;
				// wenn die Komponente die Position beim Drücken der linken Maustaste enthält
				if (lastLeftClickPosition != null && bounds.Contains (lastLeftClickPosition)) {
					notify = true;
					if (bounds.Contains(current) && InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed) {
						lastLeftClickPosition = current;
					}
				}
				// wenn die Komponente die Position beim Drücken der rechten Maustaste enthält
				else if (lastRightClickPosition != null && bounds.Contains (lastRightClickPosition)) {
					notify = true;
					if (bounds.Contains(current) && InputManager.CurrentMouseState.RightButton == ButtonState.Pressed) {
						lastRightClickPosition = current;
					}
				}
				// wenn die Komponente die aktuelle Position enthält
				else if (lastLeftClickPosition == null && lastRightClickPosition == null && bounds.Contains (previous)) {
					notify = true;
				}

				//Log.WriteLine("notify="+notify+", component="+component+", cntains="+(lastLeftClickPosition != null ? bounds.Contains (lastLeftClickPosition)+"" : "") +",bounds="+bounds+",precious="+previous);

				if (notify && (relativePositionMove
				               || InputManager.PreviousMouseState.LeftButton != InputManager.CurrentMouseState.LeftButton
				               || InputManager.PreviousMouseState.RightButton != InputManager.CurrentMouseState.RightButton)) {
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
