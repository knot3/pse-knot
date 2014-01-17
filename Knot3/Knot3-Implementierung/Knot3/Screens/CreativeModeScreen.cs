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
				Undo.Push (knot);
				// den Knoten dem KnotRenderer zuweisen
				knotRenderer.Knot = knot;
				// den Knoten dem Kantenverschieber zuweisen
				edgeMovement.Knot = knot;
				// Event registrieren
				knot.EdgesChanged += OnEdgesChanged;
				// coloring.Knot = knot;
			}
		}

		private Knot knot;
		private KnotInputHandler knotInput;
		private ModelMouseHandler modelMouseHandler;
		private EdgeMovement edgeMovement;
		private MousePointer pointer;
		private Overlay overlay;
		private Dialog currentDialog;
		private DebugBoundings debugBoundings;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines CreativeModeScreen-Objekts und initialisiert diese mit einem Knot3Game-Objekt game, sowie einem Knoten knot.
		/// </summary>
		public CreativeModeScreen (Knot3Game game, Knot knot)
		: base(game)
		{
			// world
			world = new World (screen: this);
			// input
			knotInput = new KnotInputHandler (screen: this, world: world);
			// overlay
			overlay = new Overlay (screen: this, world: world);
			// pointer
			pointer = new MousePointer (screen: this);
			// model mouse handler
			modelMouseHandler = new ModelMouseHandler (screen: this, world: world);

			// knot renderer
			knotRenderer = new KnotRenderer (screen: this, position: Vector3.Zero);
			world.Add (knotRenderer);

			// debug displays
			debugBoundings = new DebugBoundings (screen: this, position: Vector3.Zero);

			// edge movements
			edgeMovement = new EdgeMovement (screen: this, world: world, position: Vector3.Zero);
			world.Add (edgeMovement);

			// assign the specified knot
			Knot = knot;
		}

		#endregion

		#region Methods

		private void OnEdgesChanged ()
		{
			Undo.Push (knot);
			Redo.Clear ();
		}

		private void OnUndo ()
		{
			Knot current = Undo.Pop ();
			Knot previous = Undo.Peek ();
			Redo.Push (current);
			knot = previous;
		}

		private void OnRedo ()
		{
			Knot next = Redo.Pop ();
			Redo.Push (knot);
			Undo.Push (knot);
			knot = next;
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			// wenn zur Zeit kein Dialog vorhanden ist, und Escape gedrückt wurde...
			if (currentDialog == null && Keys.Escape.IsDown ()) {
				// erstelle einen neuen Pausedialog
				Dialog pauseDialog = new CreativePauseDialog (screen: this, drawOrder: DisplayLayer.Dialog, knot: knot);
				// füge ihn in die Spielkomponentenliste hinzu
				AddGameComponents (time, pauseDialog);
				// weise ihn als den aktuellen Dialog zu
				currentDialog = pauseDialog;
			}

			// wenn der aktuelle Dialog unsichtbar ist,
			// befinden wir uns im 1. Frame nach dem Schließen des Dialogs
			if (currentDialog != null && !currentDialog.IsVisible) {
				currentDialog = null;
			}
		}

		/// <summary>
		/// Fügt die 3D-Welt und den Inputhandler in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (GameScreen previousScreen, GameTime time)
		{
			base.Entered (previousScreen, time);
			AddGameComponents (time, knotInput, overlay, pointer, world, modelMouseHandler);

			// Einstellungen anwenden
			debugBoundings.Info.IsVisible = Options.Default ["debug", "show-boundings", false];
		}

		#endregion
	}
}

