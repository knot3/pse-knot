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

using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;

namespace Knot3.Core
{
	/// <summary>
	/// Stellt für jeden Frame die Maus- und Tastatureingaben bereit. Daraus werden die nicht von XNA bereitgestellten Mauseingaben berechnet. Zusätzlich wird die aktuelle Eingabeaktion berechnet.
	/// </summary>
	public sealed class InputManager : GameScreenComponent
	{
        #region Properties

		/// <summary>
		/// Enthält den Klickzustand der rechten Maustaste.
		/// </summary>
		public static ClickState RightMouseButton { get; private set; }

		/// <summary>
		/// Enthält den Klickzustand der linken Maustaste.
		/// </summary>
		public static ClickState LeftMouseButton { get; private set; }

		/// <summary>
		/// Enthält den Mauszustand von XNA zum aktuellen Frames.
		/// </summary>
		public static MouseState CurrentMouseState { get; set; }

		/// <summary>
		/// Enthält den Tastaturzustand von XNA zum aktuellen Frames.
		/// </summary>
		public static KeyboardState CurrentKeyboardState { get; private set; }

		/// <summary>
		/// Enthält den Mauszustand von XNA zum vorherigen Frames.
		/// </summary>
		public static MouseState PreviousMouseState { get; private set; }

		/// <summary>
		/// Enthält den Tastaturzustand von XNA zum vorherigen Frames.
		/// </summary>
		public static KeyboardState PreviousKeyboardState { get; private set; }

		/// <summary>
		/// Gibt an, ob die Mausbewegung für Kameradrehungen verwendet werden soll.
		/// </summary>
		public Boolean GrabMouseMovement { get; set; }

		/// <summary>
		/// Gibt die aktuelle Eingabeaktion an, die von den verschiedenen Inputhandlern genutzt werden können.
		/// </summary>
		public InputAction CurrentInputAction { get; set; }

		private static double LeftButtonClickTimer;
		private static double RightButtonClickTimer;
		private static MouseState PreviousClickMouseState;

		public static bool FullscreenToggled { get; set; }

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt ein neues InputManager-Objekt, das an den übergebenen Spielzustand gebunden ist.
		/// </summary>
		public InputManager (GameScreen screen)
			: base(screen, DisplayLayer.None)
		{
			CurrentInputAction = InputAction.FreeMouse;

			PreviousKeyboardState = CurrentKeyboardState = Keyboard.GetState ();
			PreviousMouseState = CurrentMouseState = Mouse.GetState ();
		}

        #endregion

        #region Methods

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			PreviousKeyboardState = CurrentKeyboardState;
			PreviousMouseState = CurrentMouseState;
			CurrentKeyboardState = Keyboard.GetState ();
			CurrentMouseState = Mouse.GetState ();

			if (time != null) {
				bool mouseMoved;
				if (CurrentMouseState != PreviousMouseState) {
					// mouse movements
					Vector2 mouseMove = CurrentMouseState.ToVector2 () - PreviousClickMouseState.ToVector2 ();
					mouseMoved = mouseMove.Length () > 3;
				} else {
					mouseMoved = false;
				}

				LeftButtonClickTimer += time.ElapsedGameTime.TotalMilliseconds;
				if (CurrentMouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton != ButtonState.Pressed) {
					LeftMouseButton = LeftButtonClickTimer < 500 && !mouseMoved
						? ClickState.DoubleClick : ClickState.SingleClick;
					LeftButtonClickTimer = 0;
					PreviousClickMouseState = PreviousMouseState;
					Console.WriteLine ("LeftButton=" + LeftMouseButton.ToString ());
				} else {
					LeftMouseButton = ClickState.None;
				}
				RightButtonClickTimer += time.ElapsedGameTime.TotalMilliseconds;
				if (CurrentMouseState.RightButton == ButtonState.Pressed && PreviousMouseState.RightButton != ButtonState.Pressed) {
					RightMouseButton = RightButtonClickTimer < 500 && !mouseMoved
						? ClickState.DoubleClick : ClickState.SingleClick;
					RightButtonClickTimer = 0;
					PreviousClickMouseState = PreviousMouseState;
					Console.WriteLine ("RightButton=" + RightMouseButton.ToString ());
				} else {
					RightMouseButton = ClickState.None;
				}
			}

			// fullscreen
			if (Keys.G.IsDown () || Keys.F11.IsDown ()) {
				Screen.Game.IsFullScreen = !Screen.Game.IsFullScreen;
				FullscreenToggled = true;
			}
		}

        #endregion
	}
}

