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
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.GameObjects
{
	/// <summary>
	/// Verarbeitet die Maus- und Tastatureingaben des Spielers und modifiziert die Kamera-Position
	/// und das Kamera-Ziel.
	/// </summary>
	public class KnotInputHandler : GameScreenComponent
	{
        #region Properties

		/// <summary>
		/// Die Spielwelt.
		/// </summary>
		private World world;

		public List<Keys> ValidKeys { get; set; }

		public bool IsKeyEventEnabled { get { return true; } }

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt einen neuen KnotInputHandler für den angegebenen Spielzustand und die angegebene Spielwelt.
		/// [base=screen]
		/// </summary>
		public KnotInputHandler (GameScreen screen, World world)
            : base(screen, DisplayLayer.None)
		{
			// Spielwelt
			this.world = world;

			// Setze die Standardwerte für die Mausposition
			screen.Input.GrabMouseMovement = false;
			ResetMousePosition ();
			
			// Tasten
			ValidKeys = new List<Keys> ();
			ValidKeys.AddRange (
				new []{
					Keys.A, Keys.D, Keys.W, Keys.S, Keys.R, Keys.F, Keys.Q, Keys.E, Keys.A, Keys.D, Keys.W,
					Keys.S, Keys.R, Keys.F, Keys.Q, Keys.E,
					Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.OemPlus, Keys.OemMinus, Keys.Enter,
					Keys.LeftAlt, Keys.Tab
				}
			);
		}

        #endregion

        #region Methods

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			UpdateMouse (time);
			ResetMousePosition ();
		}

		protected void UpdateMouse (GameTime time)
		{
			// wurde im letzten Frame in den oder aus dem Vollbildmodus gewechselt?
			// dann überpringe einen frame
			if (InputManager.FullscreenToggled) {
				InputManager.FullscreenToggled = false;
				return;
			}

			// ist der MouseState gleich geblieben?
			if (InputManager.CurrentMouseState == InputManager.PreviousMouseState) {
				return;
			}

			// die aktuelle Mausbewegung
			Vector2 mouseMove = new Vector2 (
					InputManager.CurrentMouseState.X - InputManager.PreviousMouseState.X,
					InputManager.CurrentMouseState.Y - InputManager.PreviousMouseState.Y
			);

			// TODO
			mouseMove.ToString ();
			// TODO
		}

		protected void UpdateKeys (GameTime time)
		{
			world.Redraw = true;

			// TODO
		}

		private void ResetMousePosition ()
		{
			if (InputManager.CurrentMouseState != InputManager.PreviousMouseState) {
				if (Screen.Input.GrabMouseMovement || (Screen.Input.CurrentInputAction == InputAction.ArcballMove)) {
					Mouse.SetPosition (Screen.Viewport.Width / 2, Screen.Viewport.Height / 2);
					InputManager.CurrentMouseState = Mouse.GetState ();
				}
			}
		}

		public void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			UpdateKeys (time);
		}

        #endregion

	}
}

