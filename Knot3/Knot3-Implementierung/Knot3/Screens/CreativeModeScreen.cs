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
using Knot3.Utilities;
using Knot3.Debug;
using Knot3.Audio;

namespace Knot3.Screens
{
	/// <summary>
	/// Der Spielzustand, der während dem Erstellen und Bearbeiten eines Knotens aktiv ist und für den Knoten eine 3D-Welt zeichnet.
	/// </summary>
	public class CreativeModeScreen : GameScreen
	{
		#region Properties

		/// <summary>
		/// Die Spielwelt in der die 3D-Objekte des dargestellten Knotens enthalten sind.
		/// </summary>
		private World world;
		/// <summary>
		/// Der Controller, der aus dem Knoten die 3D-Modelle erstellt.
		/// </summary>
		private KnotRenderer knotRenderer;

		/// <summary>
		/// Der Undo-Stack.
		/// </summary>
		public Stack<Knot> Undo { get; set; }

		/// <summary>
		/// Der Redo-Stack.
		/// </summary>
		public Stack<Knot> Redo { get; set; }

		/// <summary>
		/// Der Knoten, der vom Spieler bearbeitet wird.
		/// </summary>
		public Knot Knot
		{
			get {
				return knot;
			}
			set {
				knot = value;
				// Undo- und Redo-Stacks neu erstellen
				Redo = new Stack<Knot> ();
				Undo = new Stack<Knot> ();
				Undo.Push (knot.Clone () as Knot);
				// den Knoten den Inputhandlern und Renderern zuweisen
				registerCurrentKnot ();
				// die Events registrieren
				knot.EdgesChanged += OnEdgesChanged;
				knot.StartEdgeChanged += knotInput.OnStartEdgeChanged;
			}
		}

		private Knot knot;
		private KnotInputHandler knotInput;
		private ModelMouseHandler modelMouseHandler;
		private EdgeMovement edgeMovement;
		private EdgeColoring edgeColoring;
		private EdgeRectangles edgeRectangles;
		private MousePointer pointer;
		private Overlay overlay;
		private Button invisible;
		private DebugBoundings debugBoundings;
		// Undo-Button
		private Button undoButton;
		private Border undoButtonBorder;
		// Redo-Button
		private Button redoButton;
		private Border redoButtonBorder;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines CreativeModeScreen-Objekts und initialisiert diese mit einem Knot3Game-Objekt game, sowie einem Knoten knot.
		/// </summary>
		public CreativeModeScreen (Knot3Game game, Knot knot)
		: base (game)
		{
			// die Spielwelt
			world = new World (screen: this, bounds: Bounds);
			// der Input-Handler
			knotInput = new KnotInputHandler (screen: this, world: world);
			// das Overlay zum Debuggen
			overlay = new Overlay (screen: this, world: world);
			// der Mauszeiger
			pointer = new MousePointer (screen: this);
			// der Maus-Handler für die 3D-Modelle
			modelMouseHandler = new ModelMouseHandler (screen: this, world: world);

			// der Knoten-Renderer
			knotRenderer = new KnotRenderer (screen: this, position: Vector3.Zero);
			world.Add (knotRenderer);

			// visualisiert die BoundingSpheres
			debugBoundings = new DebugBoundings (screen: this, position: Vector3.Zero);
			world.Add (debugBoundings);

			// der Input-Handler zur Kanten-Verschiebung
			edgeMovement = new EdgeMovement (screen: this, world: world, knotInput: knotInput, position: Vector3.Zero);
			world.Add (edgeMovement);

			// der Input-Handler zur Kanten-Einfärbung
			edgeColoring = new EdgeColoring (screen: this);

			// Flächen zwischen Kanten
			edgeRectangles = new EdgeRectangles (screen: this);

			// assign the specified knot
			Knot = knot;

			// Hintergrund
			SkyCube skyCube = new SkyCube (screen: this, position: Vector3.Zero);
			world.Add (skyCube);

			// Undo-Button
			undoButton = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Undo",
			    onClick: (time) => OnUndo ()
			);
			undoButton.SetCoordinates (left: 0.05f, top: 0.900f, right: 0.15f, bottom: 0.95f);
			undoButton.AlignX = HorizontalAlignment.Center;
			undoButton.IsVisible = false;

			undoButtonBorder = new Border (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			                               widget: undoButton, lineWidth: 2, padding: 0);

			// Redo-Button
			redoButton = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "Redo",
			    onClick: (time) => OnRedo ()
			);
			redoButton.SetCoordinates (left: 0.20f, top: 0.900f, right: 0.30f, bottom: 0.95f);
			redoButton.AlignX = HorizontalAlignment.Center;
			redoButton.IsVisible = false;

			redoButtonBorder = new Border (screen: this, drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			                               widget: redoButton, lineWidth: 2, padding: 0);

			invisible = new Button (
			    screen: this,
			    drawOrder: DisplayLayer.ScreenUI + DisplayLayer.MenuItem,
			    name: "menu",
			onClick: (time) => {
				// erstelle einen neuen Pausedialog
				knotInput.IsEnabled = false;
				Dialog pauseDialog = new CreativePauseDialog (screen: this, drawOrder: DisplayLayer.Dialog, knot: knot);
				// füge ihn in die Spielkomponentenliste hinzu
				pauseDialog.Close += (t) => knotInput.IsEnabled = true;
				AddGameComponents (time, pauseDialog);
			}
			);
			invisible.SetCoordinates (left: 1.00f, top: 1.00f, right: 1.10f, bottom: 1.10f);
			invisible.IsVisible = true;
			invisible.AddKey (Keys.Escape);
		}

		#endregion

		#region Methods

		private void OnEdgesChanged ()
		{
			Knot push = knot.Clone ()as Knot;
			Undo.Push (push);
			Redo.Clear ();
			redoButton.IsVisible = false;
			undoButton.IsVisible = true;
		}

		private void OnUndo ()
		{
			Console.WriteLine ("Undo: Undo.Count=" + Undo.Count);
			if (Undo.Count >= 2) {
				Knot current = Undo.Pop ();
				Knot prev = Undo.Peek ();
				Knot previous = prev.Clone () as Knot;
				Knot curr = current.Clone () as Knot;
				Redo.Push (curr);
				knot = previous;
				// den Knoten den Inputhandlern und Renderern zuweisen
				registerCurrentKnot ();
				knot.EdgesChanged += OnEdgesChanged;
				redoButton.IsVisible = true;
			}
			if (Undo.Count == 1) {
				undoButton.IsVisible = false;
			}
		}

		private void OnRedo ()
		{
			Console.WriteLine ("Redo: Redo.Count=" + Redo.Count);
			if (Redo.Count >= 1) {
				Knot next = Redo.Pop ();
				Knot push = next.Clone ()as Knot;
				Undo.Push (push);
				knot = next;
				knot.EdgesChanged += OnEdgesChanged;
				// den Knoten den Inputhandlern und Renderern zuweisen
				registerCurrentKnot ();
				undoButton.IsVisible = true;
			}
			if (Redo.Count == 0) {
				redoButton.IsVisible = false;
			}
		}

		private void registerCurrentKnot ()
		{
			// den Knoten dem KnotRenderer zuweisen
			knotRenderer.Knot = knot;
			// den Knoten dem Kantenverschieber zuweisen
			edgeMovement.Knot = knot;
			// den Knoten dem Kanteneinfärber zuweisen
			edgeColoring.Knot = knot;
			// den Knoten dem Flächendings zuweisen
			edgeRectangles.Knot = knot;
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
		}

		/// <summary>
		/// Fügt die 3D-Welt und den Inputhandler in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime time)
		{
			base.Entered (previousScreen, time);
			AddGameComponents (time, knotInput, overlay, pointer, world, modelMouseHandler,
			                   edgeColoring, edgeRectangles, undoButton, undoButtonBorder,
			                   redoButton, redoButtonBorder, invisible);
			Audio.BackgroundMusic = Sound.CreativeMusic;

			// Einstellungen anwenden
			debugBoundings.Info.IsVisible = Options.Default ["debug", "show-boundings", false];
		}

		#endregion
	}
}
