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
using System.ComponentModel;
using Knot3.Utilities;

namespace Knot3.GameObjects
{
	/// <summary>
	/// Verarbeitet die Maus- und Tastatureingaben des Spielers und modifiziert die Kamera-Position
	/// und das Kamera-Ziel.
	/// </summary>
	public class KnotInputHandler : GameScreenComponent, IKeyEventListener
	{
        #region Properties

		/// <summary>
		/// Die Spielwelt.
		/// </summary>
		private World world;

		/// <summary>
		/// Die Tasten, auf die die Klasse reagiert. Wird aus der aktuellen Tastenbelegung berechnet.
		/// </summary>
		public List<Keys> ValidKeys { get; set; }

		/// <summary>
		/// Zeigt an, ob die Klasse zur Zeit auf Tastatureingaben reagiert.
		/// </summary>
		public bool IsKeyEventEnabled { get { return true; } }

		/// <summary>
		/// Die aktuelle Tastenbelegung
		/// </summary>
		private static Dictionary<Keys, PlayerActions> CurrentKeyAssignment = new Dictionary<Keys, PlayerActions> ();

		/// <summary>
		/// Die Standard-Tastenbelegung.
		/// </summary>
		private static readonly Dictionary<Keys, PlayerActions> DefaultKeyAssignment
				= new Dictionary<Keys, PlayerActions>
		{
			{ Keys.W, 		PlayerActions.MoveUp },
			{ Keys.S, 		PlayerActions.MoveDown },
			{ Keys.A, 		PlayerActions.MoveLeft },
			{ Keys.D, 		PlayerActions.MoveRight},
			{ Keys.R, 		PlayerActions.MoveForward },
			{ Keys.F, 		PlayerActions.MoveBackward },
			{ Keys.Up, 		PlayerActions.RotateUp },
			{ Keys.Down, 	PlayerActions.RotateDown },
			{ Keys.Left, 	PlayerActions.RotateLeft },
			{ Keys.Right, 	PlayerActions.RotateRight },
			{ Keys.Q, 		PlayerActions.ZoomIn },
			{ Keys.E, 		PlayerActions.ZoomOut },
		};

		/// <summary>
		/// Was bei den jeweiligen Aktionen ausgeführt wird.
		/// </summary>
		private static Dictionary<PlayerActions, Action<GameTime>> ActionBindings;

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
			OnControlSettingsChanged ();

			// Lege die Bedeutungen der PlayerActions fest
			ActionBindings = new Dictionary<PlayerActions, Action<GameTime>>
			{
				{ PlayerActions.MoveUp, 		(time) => move (Vector3.Up, time) },
				{ PlayerActions.MoveDown, 		(time) => move (Vector3.Down, time) },
				{ PlayerActions.MoveLeft, 		(time) => move (Vector3.Left, time) },
				{ PlayerActions.MoveRight, 		(time) => move (Vector3.Right, time) },
				{ PlayerActions.MoveForward, 	(time) => move (Vector3.Forward, time) },
				{ PlayerActions.MoveBackward, 	(time) => move (Vector3.Backward, time) },
				{ PlayerActions.RotateUp, 		(time) => rotate (-Vector2.UnitY, time) },
				{ PlayerActions.RotateDown, 	(time) => rotate ( Vector2.UnitY, time) },
				{ PlayerActions.RotateLeft, 	(time) => rotate (-Vector2.UnitX, time) },
				{ PlayerActions.RotateRight, 	(time) => rotate ( Vector2.UnitX, time) },
				{ PlayerActions.ZoomIn, 		(time) => zoom (-1, time) },
				{ PlayerActions.ZoomOut, 		(time) => zoom (+1, time) },
			};
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

		private void ResetMousePosition ()
		{
			if (InputManager.CurrentMouseState != InputManager.PreviousMouseState) {
				if (Screen.Input.GrabMouseMovement || (Screen.Input.CurrentInputAction == InputAction.ArcballMove)) {
					Mouse.SetPosition (Screen.Viewport.Width / 2, Screen.Viewport.Height / 2);
					InputManager.CurrentMouseState = Mouse.GetState ();
				}
			}
		}

		/// <summary>
		/// Verschiebt die Kamera und das Target linear in die angegebene Richtung.
		/// </summary>
		private void move (Vector3 move, GameTime time)
		{
			if (move.Length () > 0) {
				move *= 10;
				Vector3 targetDirection = world.Camera.TargetDirection;
				Vector3 up = world.Camera.UpVector;
				world.Camera.Target = world.Camera.Target.MoveLinear (move, up, targetDirection);
				world.Camera.Position = world.Camera.Position.MoveLinear (move, up, targetDirection);
				Screen.Input.CurrentInputAction = InputAction.FirstPersonCameraMove;
			}
		}

		/// <summary>
		/// Rotiert die Kamera auf einem Arcball um das Target.
		/// </summary>
		private void rotate (Vector2 move, GameTime time)
		{
			if (move.Length () > 0) {
				move *= 3;
				Vector3 targetDirection = world.Camera.TargetDirection;
				Vector3 up = world.Camera.UpVector;
				float oldDistance = world.Camera.TargetDistance = world.Camera.TargetDistance.Clamp (500, 10000);
				world.Camera.Target = new Vector3 (world.Camera.ArcballTarget.X, world.Camera.Target.Y, world.Camera.ArcballTarget.Z);
				world.Camera.Position = world.Camera.ArcballTarget
					+ (world.Camera.Position - world.Camera.ArcballTarget).ArcBallMove (
						move, up, targetDirection
				);
				world.Camera.TargetDistance = oldDistance;
				Screen.Input.CurrentInputAction = InputAction.ArcballMove;
			}
		}

		/// <summary>
		/// Führt einen Zoom durch, indem die Distanz von der Kamera zum Target erhöht oder verringert wird.
		/// </summary>
		private void zoom (int value, GameTime time)
		{
			world.Camera.TargetDistance += value * 10;
		}

		public void OnKeyEvent (List<Keys> keys, KeyEvent keyEvent, GameTime time)
		{
			// Bei einem Tastendruck wird die Spielwelt auf jeden Fall neu gezeichnet.
			world.Redraw = true;

			// Iteriere über alle gedrückten Tasten
			foreach (Keys key in keys) {
				// Ist der Taste eine Aktion zugeordnet?
				if (CurrentKeyAssignment.ContainsKey (key)) {
					// Während die Tate gedrückt gehalten ist...
					if (key.IsHeldDown ()) {
						// führe die entsprechende Aktion aus!
						PlayerActions action = CurrentKeyAssignment [key];
						Action<GameTime> binding = ActionBindings [action];
						binding (time);
					}
				}
			}
		}

		/// <summary>
		/// Wird ausgeführt, sobald ein KnotInputHandler erstellt wird und danach,
		/// wenn sich die Tastenbelegung geändert hat.
		/// </summary>
		public void OnControlSettingsChanged ()
		{
			// Drehe die Zuordnung um; von (Taste -> Aktion) zu (Aktion -> Taste)
			Dictionary<PlayerActions, Keys> defaultReversed = DefaultKeyAssignment.ToDictionary (x => x.Value, x => x.Key);

			// Leere die aktuelle Zuordnung
			CurrentKeyAssignment.Clear ();

			// Fülle die aktuelle Zuordnung mit aus der Einstellungsdatei gelesenen werten.
			// Iteriere dazu über alle gültigen PlayerActions...
			foreach (PlayerActions action in typeof(PlayerActions).ToEnumValues<PlayerActions>()) {
				string controlName = action.ToEnumDescription ();

				// Erstelle eine Option...
				KeyOptionInfo option = new KeyOptionInfo (
					section: "controls",
					name: controlName,
					defaultValue: defaultReversed [action],
					configFile: Options.Default
				);
				// und lese den Wert aus und speichere ihn in der Zuordnung.
				CurrentKeyAssignment [option.Value] = action;
			}

			// Aktualisiere die Liste von Tasten, zu denen wir als IKeyEventListener benachrichtigt werden
			ValidKeys.Clear ();
			ValidKeys.AddRange (CurrentKeyAssignment.Keys.AsEnumerable ());
		}

        #endregion
	}
}

