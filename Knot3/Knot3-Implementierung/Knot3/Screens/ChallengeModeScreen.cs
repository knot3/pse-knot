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
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Debug;
using Knot3.Utilities;
using Knot3.Audio;

namespace Knot3.Screens
{
	/// <summary>
	/// Der Spielzustand, der während dem Spielen einer Challenge aktiv ist und für den Ausgangs- und Referenzknoten je eine 3D-Welt zeichnet.
	/// </summary>
	public class ChallengeModeScreen : GameScreen
	{
		#region Properties

		/// <summary>
		/// Die Spielwelt in der die 3D-Modelle des dargestellten Referenzknotens enthalten sind.
		/// </summary>
		private World ChallengeWorld;
		/// <summary>
		/// Die Spielwelt in der die 3D-Modelle des dargestellten Spielerknotens enthalten sind.
		/// </summary>
		private World PlayerWorld;
		/// <summary>
		/// Der Controller, der aus dem Referenzknoten die 3D-Modelle erstellt.
		/// </summary>
		private KnotRenderer ChallengeKnotRenderer;
		/// <summary>
		/// Der Controller, der aus dem Spielerknoten die 3D-Modelle erstellt.
		/// </summary>
		private KnotRenderer PlayerKnotRenderer;
		/// <summary>
		/// Der Inputhandler, der die Kantenverschiebungen des Spielerknotens durchführt.
		/// </summary>
		private EdgeMovement PlayerEdgeMovement;

		/// <summary>
		/// Der Undo-Stack.
		/// </summary>
		public Stack<Knot> Undo { get; set; }

		/// <summary>
		/// Der Redo-Stack.
		/// </summary>
		public Stack<Knot> Redo { get; set; }

		private bool returnFromPause;

		/// <summary>
		/// Die Challenge.
		/// </summary>
		public Challenge Challenge
		{
			get {
				return _challenge;
			}
			set {
				_challenge = value;
			}
		}

		private Challenge _challenge;

		/// <summary>
		/// Der Spielerknoten, der durch die Transformation des Spielers aus dem Ausgangsknoten entsteht.
		/// </summary>
		public Knot PlayerKnot
		{
			get {
				return _playerKnot;
			}
			set {
				_playerKnot = value;
				// Undo- und Redo-Stacks neu erstellen
				Redo = new Stack<Knot> ();
				Undo = new Stack<Knot> ();
				Undo.Push (_playerKnot.Clone () as Knot);
				// den Knoten dem KnotRenderer zuweisen
				registerCurrentKnot ();
				// Event registrieren
				_playerKnot.EdgesChanged += OnEdgesChanged;
				// coloring.Knot = knot;
			}
		}
		// der Knoten
		private Knot _playerKnot;
		// Spielkomponenten
		private KnotInputHandler knotInput;
		private ModelMouseHandler modelMouseHandler;
		private MousePointer pointer;
		private Overlay overlay;
		private Lines lines;
		private DebugBoundings debugBoundings;
		// Zeitmessung und Zeitanzeige
		private TimeSpan playTime;
		private TextItem playTimeDisplay;
		private Border playTimeBorder;
		// Undo-Button
		private MenuEntry undoButton;
		private Border undoButtonBorder;
		// Undo-Button
		private MenuEntry redoButton;
		private Border redoButtonBorder;
		// Der Status, z.b. ist die Challenge beendet?
		private ChallengeModeState state;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines ChallengeModeScreen-Objekts und initialisiert diese mit einem Knot3Game-Objekt, einem Spielerknoten playerKnot und dem Knoten challengeKnot, den der Spieler nachbauen soll.
		/// </summary>
		public ChallengeModeScreen (Knot3Game game, Challenge challenge)
		: base (game)
		{
			// world
			PlayerWorld = new World (screen: this, bounds: Bounds.FromRight (percent: 0.5f));
			ChallengeWorld = new World (screen: this, bounds: Bounds.FromLeft (percent: 0.5f));
			ChallengeWorld.Camera = PlayerWorld.Camera;
			PlayerWorld.OnRedraw += () => ChallengeWorld.Redraw = true;
			// input
			knotInput = new KnotInputHandler (screen: this, world: PlayerWorld);
			// overlay
			overlay = new Overlay (screen: this, world: PlayerWorld);
			// pointer
			pointer = new MousePointer (screen: this);
			// model mouse handler
			modelMouseHandler = new ModelMouseHandler (screen: this, world: PlayerWorld);

			// knot renderer
			PlayerKnotRenderer = new KnotRenderer (screen: this, position: Vector3.Zero);
			PlayerWorld.Add (PlayerKnotRenderer);
			ChallengeKnotRenderer = new KnotRenderer (screen: this, position: Vector3.Zero);
			ChallengeWorld.Add (ChallengeKnotRenderer);

			// debug displays
			debugBoundings = new DebugBoundings (screen: this, position: Vector3.Zero);

			// edge movements
			PlayerEdgeMovement = new EdgeMovement (screen: this, world: PlayerWorld, position: Vector3.Zero);
			PlayerWorld.Add (PlayerEdgeMovement);

			// assign the specified challenge
			Challenge = challenge;
			// assign the specified player knot
			PlayerKnot = challenge.Start.Clone () as Knot;
			// assign the specified target knot
			ChallengeKnotRenderer.Knot = challenge.Target;

			SkyCube playerSkyCube = new SkyCube (screen: this, position: Vector3.Zero, distance: PlayerWorld.Camera.MaxPositionDistance + 500);
			PlayerWorld.Add (playerSkyCube);
			SkyCube challengeSkyCube = new SkyCube (screen: this, position: Vector3.Zero, distance: ChallengeWorld.Camera.MaxPositionDistance + 500);
			ChallengeWorld.Add (challengeSkyCube);

			// Die Spielzeit-Anzeige
			playTimeDisplay = new TextItem (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem, name: "");
			playTimeDisplay.Bounds.Position = new ScreenPoint (this, 0.800f, 0.01f);
			playTimeDisplay.Bounds.Size = new ScreenPoint (this, 0.15f, 0.04f);
			playTimeDisplay.BackgroundColor = () => Color.Black;
			playTimeDisplay.ForegroundColor = () => Color.White;
			playTimeBorder = new Border (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			                             widget: playTimeDisplay, lineWidth: 2, padding: 0);
			//Undo-Button
			undoButton = new Button (screen: this,
			                            drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			                            name: "Undo",
			                            onClick: (time) => OnUndo ());
			undoButton.SetCoordinates (left: 0.55f, top: 0.900f, right: 0.65f, bottom: 0.95f);

			undoButtonBorder = new Border (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			                               widget: undoButton, lineWidth: 2, padding: 0);
			undoButton.AlignX = HorizontalAlignment.Center;
			undoButton.IsVisible = false;


			// die Linien
			lines = new Lines (screen: this, drawOrder: DisplayLayer.Dialog, lineWidth: 2);
			lines.AddPoints (500, 0, 500, 1000);

			// Redo-Button
			redoButton = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Redo",
			    onClick: (time) => OnRedo ()
			);
			redoButton.SetCoordinates (left: 0.70f, top: 0.900f, right: 0.80f, bottom: 0.95f);
			redoButton.BackgroundColor = () => base.MenuItemBackgroundColor (redoButton.State);
			redoButton.ForegroundColor = () => base.MenuItemForegroundColor (redoButton.State);
			redoButtonBorder = new Border (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			                               widget: redoButton, lineWidth: 2, padding: 0);
			redoButton.AlignX = HorizontalAlignment.Center;

			// die Linien
			lines = new Lines (screen: this, drawOrder: DisplayLayer.Dialog, lineWidth: 2);
			lines.AddPoints (500, 0, 500, 1000);

			// Status
			state = ChallengeModeState.Start;
		}

		#endregion

		#region Methods

		private void OnEdgesChanged ()
		{
			Knot push = _playerKnot.Clone ()as Knot;
			Undo.Push (push);
			Redo.Clear ();
			redoButton.IsVisible = false;
			undoButton.IsVisible = true;

			// Status
			if (state == ChallengeModeState.Start) {
				state = ChallengeModeState.Running;
			}
		}

		private void OnUndo ()
		{
			if (Undo.Count >= 2) {
				Knot current = Undo.Pop ();
				Knot prev = Undo.Peek ();
				Knot previous = prev.Clone () as Knot;
				Knot curr = current.Clone () as Knot;
				Redo.Push (curr);
				_playerKnot = previous;
				registerCurrentKnot ();
				_playerKnot.EdgesChanged += OnEdgesChanged;
				redoButton.IsVisible = true;
			}
			if (Undo.Count == 1) {
				undoButton.IsVisible = false;
			}
		}

		private void OnRedo ()
		{
			if (Redo.Count >= 1) {
				Knot next = Redo.Pop ();
				Knot push = next.Clone ()as Knot;
				//Undo.Push (push);
				Undo.Push (push);
				_playerKnot = next;
				_playerKnot.EdgesChanged += OnEdgesChanged;
				// den Knoten den Inputhandlern und Renderern zuweisen
				registerCurrentKnot ();

				redoButton.IsVisible = true;
			}
			if (Redo.Count == 0) {

				redoButton.IsVisible = false;
			}
		}

		private void registerCurrentKnot ()
		{
			// den Knoten dem KnotRenderer zuweisen
			PlayerKnotRenderer.Knot = _playerKnot;
			// den Knoten dem Kantenverschieber zuweisen
			PlayerEdgeMovement.Knot = _playerKnot;
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			// während die Challenge läuft...
			if (state == ChallengeModeState.Running || state == ChallengeModeState.Start) {
				ChallengeModeState oldState = state;
				// wenn zur Zeit kein Dialog vorhanden ist, und Escape gedrückt wurde...
				if (Keys.Escape.IsDown () && !returnFromPause) {
					// erstelle einen neuen Pausedialog
					knotInput.IsEnabled = false;
					Dialog pauseDialog = new ChallengePauseDialog (screen: this, drawOrder: DisplayLayer.Dialog);
					// pausiere die Zeitmessung
					state = ChallengeModeState.Paused;
					// wenn der Dialog geschlossen wird, starte die Zeitmessung wieder
					pauseDialog.Close += (t) => {
						state = oldState;
						knotInput.IsEnabled = true;
						returnFromPause = true;
					};
					// füge ihn zur Spielkomponentenliste hinzu
					AddGameComponents (time, pauseDialog);
				}
				returnFromPause = false;
			}

			// während die Challenge läuft...
			if (state == ChallengeModeState.Running) {
				// vergleiche den Spielerknoten mit dem Zielknoten
				if (PlayerKnot.Equals (Challenge.Target)) {
					Console.WriteLine ("Playerknot equals Target!");
					state = ChallengeModeState.Finished;
					OnChallengeFinished (time);
				}

				// die Zeit, die der Spieler zum Spielen der Challenge braucht
				playTime += time.ElapsedGameTime;
				// zeige die Zeit an
				playTimeDisplay.Text = (playTime.Hours * 60 + playTime.Minutes).ToString ("D2") + ":" + playTime.Seconds.ToString ("D2");
			}
		}

		public void OnChallengeFinished (GameTime time)
		{
			knotInput.IsEnabled = false;
			// erstelle einen Dialog zum Eingeben des Spielernamens
			TextInputDialog nameDialog = new TextInputDialog (screen: this, drawOrder: DisplayLayer.Dialog,
			        title: "Challenge", text: "Your name:",
			        inputText: Options.Default ["profile", "name", ""]);
			// füge ihn zur Spielkomponentenliste hinzu
			AddGameComponents (time, nameDialog);

			// wenn der Dialog geschlossen wird...
			nameDialog.Close += (t) => {
				Challenge.AddToHighscore (name: nameDialog.InputText, time: (int)playTime.TotalSeconds);
				// erstelle einen Highscoredialog
				Dialog highscoreDialog = new HighscoreDialog (screen: this, drawOrder: DisplayLayer.Dialog,
				        challenge: Challenge);
				// füge ihn zur Spielkomponentenliste hinzu
				AddGameComponents (time, highscoreDialog);
			};
		}

		/// <summary>
		/// Fügt die 3D-Welten und den Inputhandler in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime time)
		{
			base.Entered (previousScreen, time);
			AddGameComponents (time, knotInput, overlay, pointer, ChallengeWorld, PlayerWorld,

			                   modelMouseHandler, lines, playTimeDisplay, playTimeBorder, undoButton, undoButtonBorder, redoButton, redoButtonBorder);

			Audio.BackgroundMusic = Sound.ChallengeMusic;

			// Einstellungen anwenden
			debugBoundings.Info.IsVisible = Options.Default ["debug", "show-boundings", false];
		}

		#endregion

		enum ChallengeModeState {
			Start,
			Running,
			Finished,
			Paused
		}
	}
}
