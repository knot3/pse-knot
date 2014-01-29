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
using Knot3.Debug;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;

namespace Knot3.GameObjects
{
	/// <summary>
	/// Verarbeitet die Maus- und Tastatureingaben des Spielers und modifiziert die Kamera-Position
	/// und das Kamera-Ziel.
	/// </summary>
	public class KnotInputHandler : GameScreenComponent, IKeyEventListener, IMouseMoveEventListener, IMouseScrollEventListener
	{
		#region Properties

		/// <summary>
		/// Die Spielwelt.
		/// </summary>
		private World world;

		/// <summary>
		/// Die Tasten, auf die die Klasse reagiert. Wird aus der aktuellen Tastenbelegung berechnet.
		/// </summary>
		public List<Keys> ValidKeys { get; private set; }

		/// <summary>
		/// Zeigt an, ob die Klasse zur Zeit auf Eingaben reagiert.
		/// </summary>
		public bool IsEnabled { get; set; }

		/// <summary>
		/// Zeigt an, ob die Klasse zur Zeit auf Tastatureingaben reagiert.
		/// </summary>
		public bool IsKeyEventEnabled { get { return IsEnabled; } }

		/// <summary>
		/// Zeigt an, ob die Klasse zur Zeit auf Mausbewegungen reagiert.
		/// </summary>
		public bool IsMouseMoveEventEnabled { get { return IsEnabled; } }

		/// <summary>
		/// Zeigt an, ob die Klasse zur Zeit auf Mausrad-Bewegungen reagiert.
		/// </summary>
		public bool IsMouseScrollEventEnabled { get { return IsEnabled; } }

		/// <summary>
		/// Die aktuelle Tastenbelegung
		/// </summary>
		public static Dictionary<Keys, PlayerActions> CurrentKeyAssignment = new Dictionary<Keys, PlayerActions> ();
		/// <summary>
		/// Die Standard-Tastenbelegung.
		/// </summary>
		public static readonly Dictionary<Keys, PlayerActions> DefaultKeyAssignment
		= new Dictionary<Keys, PlayerActions> {
			{ Keys.W, 		PlayerActions.MoveUp },
			{ Keys.S, 		PlayerActions.MoveDown },
			{ Keys.A, 		PlayerActions.MoveLeft },
			{ Keys.D, 		PlayerActions.MoveRight },
			{ Keys.R, 		PlayerActions.MoveForward },
			{ Keys.F, 		PlayerActions.MoveBackward },
			{ Keys.Up, 		PlayerActions.RotateUp },
			{ Keys.Down, 	PlayerActions.RotateDown },
			{ Keys.Left, 	PlayerActions.RotateLeft },
			{ Keys.Right, 	PlayerActions.RotateRight },
			{ Keys.Q, 		PlayerActions.ZoomIn },
			{ Keys.E, 		PlayerActions.ZoomOut },
			{ Keys.Enter, 	PlayerActions.ResetCamera },
			{ Keys.Space,	PlayerActions.MoveToCenter },
			{ Keys.LeftAlt,	PlayerActions.ToggleMouseLock },
		};
		/// <summary>
		/// Was bei den jeweiligen Aktionen ausgeführt wird.
		/// </summary>
		private static Dictionary<PlayerActions, Action<GameTime>> ActionBindings;

		private Camera camera { get { return world.Camera; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt einen neuen KnotInputHandler für den angegebenen Spielzustand und die angegebene Spielwelt.
		/// [base=screen]
		/// </summary>
		public KnotInputHandler (IGameScreen screen, World world)
		: base (screen, world.Index)
		{
			// Standardmäßig aktiviert
			IsEnabled = true;

			// Spielwelt
			this.world = world;

			// Setze die Standardwerte für die Mausposition
			screen.Input.GrabMouseMovement = false;
			ResetMousePosition ();

			// Tasten
			ValidKeys = new List<Keys> ();
			OnControlSettingsChanged ();
			ControlSettingsScreen.ControlSettingsChanged += OnControlSettingsChanged;

			// Lege die Bedeutungen der PlayerActions fest
			ActionBindings = new Dictionary<PlayerActions, Action<GameTime>> {
				{ PlayerActions.MoveUp, 			(time) => move (Vector3.Up, time) },
				{ PlayerActions.MoveDown, 			(time) => move (Vector3.Down, time) },
				{ PlayerActions.MoveLeft, 			(time) => move (Vector3.Left, time) },
				{ PlayerActions.MoveRight, 			(time) => move (Vector3.Right, time) },
				{ PlayerActions.MoveForward, 		(time) => move (Vector3.Forward, time) },
				{ PlayerActions.MoveBackward, 		(time) => move (Vector3.Backward, time) },
				{ PlayerActions.RotateUp, 			(time) => rotate (-Vector2.UnitY * 4, time) },
				{ PlayerActions.RotateDown, 		(time) => rotate (Vector2.UnitY * 4, time) },
				{ PlayerActions.RotateLeft, 		(time) => rotate (-Vector2.UnitX * 4, time) },
				{ PlayerActions.RotateRight, 		(time) => rotate (Vector2.UnitX * 4, time) },
				{ PlayerActions.ZoomIn, 			(time) => zoom (-1, time) },
				{ PlayerActions.ZoomOut, 			(time) => zoom (+1, time) },
				{ PlayerActions.ResetCamera, 		(time) => camera.ResetCamera () },
				{ PlayerActions.MoveToCenter,		(time) => camera.StartSmoothMove (target: camera.ArcballTarget, time: time) },
				{ PlayerActions.ToggleMouseLock,	(time) => toggleMouseLock (time) },
			};
		}

		#endregion

		#region Methods

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			if (!IsEnabled) {
				Screen.Input.CurrentInputAction = InputAction.FreeMouse;
			}
		}

		public void OnLeftMove (Vector2 previousPosition, Vector2 currentPosition, Vector2 move, GameTime time)
		{
			UpdateMouse (move, time);
			ResetMousePosition ();
		}

		public void OnRightMove (Vector2 previousPosition, Vector2 currentPosition, Vector2 move, GameTime time)
		{
			UpdateMouse (move, time);
			ResetMousePosition ();
		}

		public void OnMove (Vector2 previousPosition, Vector2 currentPosition, Vector2 move, GameTime time)
		{
			UpdateMouse (move, time);
			ResetMousePosition ();
		}

		protected void UpdateMouse (Vector2 mouseMove, GameTime time)
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
			// Vector2 mouseMove = InputManager.CurrentMouseState.ToVector2 () - InputManager.PreviousMouseState.ToVector2 ();

			InputAction action;
			// wenn die Maus in der Mitte des Bildschirms gelockt ist
			if (Screen.Input.GrabMouseMovement) {
				// und die linke Maustaste gedrückt gehalten wird
				if (InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed) {
					action = InputAction.ArcballMove;
				}
				// und die rechte Maustaste gedrückt gehalten wird
				else if (InputManager.CurrentMouseState.RightButton == ButtonState.Pressed) {
					action = InputAction.ArcballMove;
				}
				// und alle Maustasten losgelassen sind
				else {
					action = InputAction.CameraTargetMove;
				}
			}
			// wenn die Maus frei bewegbar ist
			else {
				// und die linke Maustaste gedrückt gehalten wird
				if (InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed) {
					if (world.SelectedObject != null && world.SelectedObject.Info.IsMovable) {
						action = InputAction.SelectedObjectShadowMove;
					}
					else {
						action = InputAction.FreeMouse;
					}
				}
				// und die linke Maustaste gerade losgelassen wurde
				else if (InputManager.CurrentMouseState.LeftButton == ButtonState.Released && InputManager.PreviousMouseState.LeftButton == ButtonState.Pressed) {
					if (world.SelectedObject != null && world.SelectedObject.Info.IsMovable) {
						action = InputAction.SelectedObjectMove;
					}
					else {
						action = InputAction.FreeMouse;
					}
				}
				// und die rechte Maustaste gedrückt gehalten wird
				else if (InputManager.CurrentMouseState.RightButton == ButtonState.Pressed) {
					action = InputAction.ArcballMove;
				}
				// und alle Maustasten losgelassen sind
				else {
					action = InputAction.FreeMouse;
				}
			}
			Screen.Input.CurrentInputAction = action;

			switch (action) {
			case InputAction.ArcballMove:
				// rotieren
				rotate (mouseMove * 1.5f, time);
				break;
			case InputAction.CameraTargetMove:
				// verschieben
				moveTarget (new Vector3 (mouseMove.X, -mouseMove.Y, 0) * 0.3f, time);
				break;
			}
		}

		public void OnScroll (int scrollWheelValue)
		{
			// scroll wheel zoom
			if (InputManager.CurrentMouseState.ScrollWheelValue < InputManager.PreviousMouseState.ScrollWheelValue) {
				camera.FoV += 1;
				world.Redraw = true;
			}
			else if (InputManager.CurrentMouseState.ScrollWheelValue > InputManager.PreviousMouseState.ScrollWheelValue) {
				camera.FoV -= 1;
				world.Redraw = true;
			}
		}

		private void ResetMousePosition ()
		{

			if (InputManager.CurrentMouseState != InputManager.PreviousMouseState) {
				if (Screen.Input.GrabMouseMovement || (Screen.Input.CurrentInputAction == InputAction.ArcballMove)) {
					Mouse.SetPosition (world.Viewport.X + world.Viewport.Width / 2,
					                   world.Viewport.Y + world.Viewport.Height / 2);
					InputManager.CurrentMouseState = Mouse.GetState ();
				}
			}

		}

		/// <summary>
		/// Verschiebt die Kamera und das Target linear in die angegebene Richtung.
		/// </summary>
		private void move (Vector3 move, GameTime time)
		{
			Profiler.ProfileDelegate ["Move"] = () => {
				if (move.Length () > 0) {
					move *= 10;
					Vector3 targetDirection = camera.PositionToTargetDirection;
					Vector3 up = camera.UpVector;
					// Führe die lineare Verschiebung durch
					camera.Target = camera.Target.MoveLinear (move, up, targetDirection);
					camera.Position = camera.Position.MoveLinear (move, up, targetDirection);
					Screen.Input.CurrentInputAction = InputAction.FirstPersonCameraMove;
					world.Redraw = true;
				}
			};
		}

		/// <summary>
		/// Verschiebt das Target linear in die angegebene Richtung.
		/// </summary>
		private void moveTarget (Vector3 move, GameTime time)
		{
			Profiler.ProfileDelegate ["Move"] = () => {
				if (move.Length () > 0) {
					move *= 10;
					Vector3 targetDirection = camera.PositionToTargetDirection;
					Vector3 up = camera.UpVector;
					// Führe die lineare Verschiebung durch
					camera.Target = camera.Target.MoveLinear (move, up, targetDirection);
					Screen.Input.CurrentInputAction = InputAction.FirstPersonCameraMove;
					world.Redraw = true;
				}
			};
		}

		/// <summary>
		/// Rotiert die Kamera auf einem Arcball um das Target.
		/// </summary>
		private void rotate (Vector2 move, GameTime time)
		{

			if (Options.Default ["video", "arcball-around-center", true]) {
				rotateCenter (move, time);
			}
			else {
				rotateEverywhere (move, time);
			}

		}

		private void rotateCenter (Vector2 move, GameTime time)
		{

			// Wenn kein 3D-Objekt selektiert ist...
			if (world.SelectedObject == null && world.Count () > 0) {
				// selektiere das Objekt, das der Mausposition am nächsten ist!
				world.SelectedObject = world.FindNearestObjects (
				                           nearTo: InputManager.CurrentMouseState.ToVector2 ()
				                       ).ElementAt (0);
			}

			// Überprüfe, wie weit das Kamera-Target von dem Objekt, um das rotiert werden soll,
			// entfernt ist
			float arcballTargetDistance = Math.Abs (camera.Target.DistanceTo (camera.ArcballTarget));

			// Ist es mehr als 5 Pixel entfernt?
			if (arcballTargetDistance > 5) {
				// Falls noch kein SmoothMove gestartet ist, starte einen, um das Arcball-Target
				// in den Fokus der Kamera zu rücken
				if (!camera.InSmoothMove) {
					camera.StartSmoothMove (target: camera.ArcballTarget, time: time);
				}
				Screen.Input.CurrentInputAction = InputAction.ArcballMove;
			}
			// Ist es weiter als 5 Pixel weg?
			else if (move.Length () > 0) {
				Screen.Input.CurrentInputAction = InputAction.ArcballMove;
				world.Redraw = true;

				// Berechne die Rotation
				camera.Target = camera.ArcballTarget;
				float oldDistance = camera.Position.DistanceTo (camera.Target);
				Vector3 targetDirection = camera.PositionToTargetDirection;
				Vector3 up = camera.UpVector;
				camera.Position = camera.Target
				                  + (camera.Position - camera.Target).ArcBallMove (move, up, targetDirection);
				camera.Position = camera.Position.SetDistanceTo (camera.Target, oldDistance);
			}
		}

		private void rotateEverywhere (Vector2 move, GameTime time)
		{
			// Wenn kein 3D-Objekt selektiert ist...
			if (world.SelectedObject == null && world.Count () > 0) {
				// selektiere das Objekt, das der Mausposition am nächsten ist!
				world.SelectedObject = world.FindNearestObjects (
				                           nearTo: InputManager.CurrentMouseState.ToVector2 ()
				                       ).ElementAt (0);
			}

			if (move.Length () > 0) {
				Screen.Input.CurrentInputAction = InputAction.ArcballMove;
				world.Redraw = true;

				// Berechne die Rotation
				float oldPositionDistance = camera.Position.DistanceTo (camera.ArcballTarget);
				float oldTargetDistance = camera.Target.DistanceTo (camera.Position);
				Vector3 targetDirection = Vector3.Normalize (camera.ArcballTarget - camera.Position);
				Vector3 up = camera.UpVector;
				camera.Position = camera.ArcballTarget
				                  + (camera.Position - camera.ArcballTarget).ArcBallMove (move, up, targetDirection);
				camera.Target = camera.ArcballTarget
				                + (camera.Target - camera.ArcballTarget).ArcBallMove (move, up, targetDirection);
				camera.Position = camera.Position.SetDistanceTo (camera.ArcballTarget, oldPositionDistance);
				camera.Target = camera.Target.SetDistanceTo (camera.Position, oldTargetDistance);
			}
		}

		/// <summary>
		/// Führt einen Zoom durch, indem die Distanz von der Kamera zum Target erhöht oder verringert wird.
		/// </summary>
		private void zoom (int value, GameTime time)
		{
			camera.PositionToTargetDistance += value * 10;
		}

		public void OnKeyEvent (List<Keys> keys, KeyEvent keyEvent, GameTime time)
		{
			Profiler.ProfileDelegate ["OnKey"] = () => {
				if (IsEnabled) {
					// Bei einem Tastendruck wird die Spielwelt auf jeden Fall neu gezeichnet.
					world.Redraw = true;

					// Iteriere über alle gedrückten Tasten
					foreach (Keys key in keys) {
						// Ist der Taste eine Aktion zugeordnet?
						if (CurrentKeyAssignment.ContainsKey (key)) {
							// Während die Taste gedrückt gehalten ist...
							if (key.IsHeldDown ()) {
								// führe die entsprechende Aktion aus!
								PlayerActions action = CurrentKeyAssignment [key];
								Action<GameTime> binding = ActionBindings [action];
								binding (time);
							}
						}
					}
				}
			};
		}

		/// <summary>
		/// Wird ausgeführt, sobald ein KnotInputHandler erstellt wird und danach,
		/// wenn sich die Tastenbelegung geändert hat.
		/// </summary>
		public void OnControlSettingsChanged ()
		{
			// Drehe die Zuordnung um; von (Taste -> Aktion) zu (Aktion -> Taste)
			Dictionary<PlayerActions, Keys> defaultReversed = DefaultKeyAssignment.ReverseDictionary ();

			// Leere die aktuelle Zuordnung
			CurrentKeyAssignment.Clear ();

			// Fülle die aktuelle Zuordnung mit aus der Einstellungsdatei gelesenen werten.
			// Iteriere dazu über alle gültigen PlayerActions...
			foreach (PlayerActions action in typeof(PlayerActions).ToEnumValues<PlayerActions>()) {
				string actionName = action.ToEnumDescription ();

				// Erstelle eine Option...
				KeyOptionInfo option = new KeyOptionInfo (
				    section: "controls",
				    name: actionName,
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

		public void OnStartEdgeChanged (Vector3 direction)
		{
			Console.WriteLine ("OnStartEdgeChanged: " + direction);
			camera.Position -= direction * Node.Scale;
			camera.Target -= direction * Node.Scale;
			Screen.Input.CurrentInputAction = InputAction.FreeMouse;
		}

		private void toggleMouseLock (GameTime time)
		{
			Screen.Input.GrabMouseMovement = !Screen.Input.GrabMouseMovement;
		}

		public Rectangle MouseMoveBounds { get { return world.Bounds; } }

		public Rectangle MouseScrollBounds { get { return world.Bounds; } }

		#endregion
	}
}

