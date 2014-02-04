using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;

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
using Knot3.Audio;
using Knot3.Screens;

namespace Knot3.Development
{
	public class JunctionEditorScreen : GameScreen
	{
		#region Properties

		/// <summary>
		/// Die Spielwelt in der die 3D-Objekte des dargestellten Knotens enthalten sind.
		/// </summary>
		private World world;

		/// <summary>
		/// Der Controller, der aus dem Knoten die 3D-Modelle erstellt.
		/// </summary>
		private JunctionEditorRenderer knotRenderer;
		private KnotInputHandler knotInput;
		private ModelMouseHandler modelMouseHandler;
		private MousePointer pointer;
		private Overlay overlay;
		private DebugBoundings debugBoundings;
		private MenuEntry backButton;
		private Menu settingsMenu;
		private DropDownMenuItem[] itemBumpRotation;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines CreativeModeScreen-Objekts und initialisiert diese mit einem Knot3Game-Objekt game, sowie einem Knoten knot.
		/// </summary>
		public JunctionEditorScreen (Knot3Game game)
		: base(game)
		{
			// die Spielwelt
			world = new World (screen: this, bounds: Bounds.FromLeft (0.60f));
			// der Input-Handler
			knotInput = new KnotInputHandler (screen: this, world: world);
			// das Overlay zum Debuggen
			overlay = new Overlay (screen: this, world: world);
			// der Mauszeiger
			pointer = new MousePointer (screen: this);
			// der Maus-Handler für die 3D-Modelle
			modelMouseHandler = new ModelMouseHandler (screen: this, world: world);

			// der Knoten-Renderer
			knotRenderer = new JunctionEditorRenderer (screen: this, position: Vector3.Zero);
			world.Add (knotRenderer);

			// visualisiert die BoundingSpheres
			debugBoundings = new DebugBoundings (screen: this, position: Vector3.Zero);
			world.Add(debugBoundings);

			// Hintergrund
			SkyCube skyCube = new SkyCube (screen: this, position: Vector3.Zero, distance: world.Camera.MaxPositionDistance + 500);
			world.Add (skyCube);

			// Backbutton
			backButton = new MenuEntry (
			    screen: this,
			    drawOrder: DisplayLayer.Overlay + DisplayLayer.MenuItem,
			    name: "Back",
			    onClick: (time) => NextScreen = new StartScreen (Game)
			);
			backButton.AddKey (Keys.Escape);
			backButton.IsVisible = true;

			// Menü
			settingsMenu = new Menu (this, DisplayLayer.Overlay + DisplayLayer.Menu);
			settingsMenu.Bounds = Bounds.FromRight (0.40f).FromBottom (0.9f).FromLeft (0.8f);
			settingsMenu.Bounds.Padding = new ScreenPoint (this, 0.010f, 0.010f);
			settingsMenu.ItemForegroundColor = (state) => Color.White;
			settingsMenu.ItemBackgroundColor = (state) => Color.Black;
			settingsMenu.ItemAlignX = HorizontalAlignment.Left;
			settingsMenu.ItemAlignY = VerticalAlignment.Center;

			Direction[] validDirections = Direction.Values;
			for (int i = 0; i < 3; ++i) {
				DistinctOptionInfo option = new DistinctOptionInfo (
				    section: "debug",
				    name: "debug_junction_direction" + i,
				    defaultValue: validDirections [i * 2],
				    validValues: validDirections.Select (d => d.Description),
				    configFile: Options.Default
				);
				DropDownMenuItem item = new DropDownMenuItem (
				    screen: this,
				    drawOrder: DisplayLayer.Overlay + DisplayLayer.MenuItem,
				    text: "Direction " + i
				);
				item.AddEntries (option);
				item.ValueChanged += OnDirectionsChanged;
				settingsMenu.Add (item);
			}

			itemBumpRotation = new DropDownMenuItem[3];
			for (int i = 0; i < 3; ++i) {
				DropDownMenuItem item = new DropDownMenuItem (
				    screen: this,
				    drawOrder: DisplayLayer.Overlay + DisplayLayer.MenuItem,
				    text: "Bump Angle " + i
				);
				item.ValueChanged += OnAnglesChanged;
				settingsMenu.Add (item);
				itemBumpRotation [i] = item;
			}

			OnDirectionsChanged (null);

			settingsMenu.Add (backButton);

			world.Camera.PositionToTargetDistance = 180;
		}

		#endregion

		#region Methods

		private void OnDirectionsChanged (GameTime time)
		{
			var directions = Directions;
			float[] validAngles = new float[] {
				0, 45, 90, 135, 180, 225, 270, 315
			};
			for (int i = 0; i < 3; ++i) {
				FloatOptionInfo option = new FloatOptionInfo (
				    section: NodeConfigKey (directions.ToEnumerable ()),
				    name: "bump" + i,
				    defaultValue: 0,
				    validValues: validAngles,
				    configFile: Options.Models
				);
				itemBumpRotation [i].AddEntries (option);
				RemoveGameComponents (time, itemBumpRotation [i]);
				AddGameComponents (time, itemBumpRotation [i]);
			}

			/*
			for (int i = 0; i < 3; ++i) {
				Options.Default ["debug", "debug_junction_angle_bump" + i, 0f] = Options.Models [, "i, 0f];
			}
			*/
			knotRenderer.Render (directions: directions);
		}

		private void OnAnglesChanged (GameTime time)
		{
			var directions = Directions;
			/*
			for (int i = 0; i < 3; ++i) {
				Options.Models [NodeConfigKey (directions.ToEnumerable ()), "bump" + i, 0f] = Options.Default ["debug", "debug_junction_angle_bump" + i, 0f];
			}
			*/

			knotRenderer.Render (directions: Directions);
		}

		private Tuple<Direction, Direction, Direction> Directions
		{
			get {
				Direction[] validDirections = Direction.Values;
				Direction d1 = Direction.FromString (Options.Default ["debug", "debug_junction_direction" + 0, validDirections [0]]);
				Direction d2 = Direction.FromString (Options.Default ["debug", "debug_junction_direction" + 1, validDirections [2]]);
				Direction d3 = Direction.FromString (Options.Default ["debug", "debug_junction_direction" + 2, validDirections [4]]);
				return Tuple.Create (d1, d2, d3);
			}
		}

		public static string NodeConfigKey (IEnumerable<Direction> directions)
		{
			IEnumerable<string> _directions = directions.Select(direction => direction + String.Empty + direction);
			return "Node" + directions.Count () + ":" + string.Join (",", _directions);
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			Profiler.ProfilerMap.Clear ();
		}

		/// <summary>
		/// Fügt die 3D-Welt und den Inputhandler in die Spielkomponentenliste ein.
		/// </summary>
		public override void Entered (IGameScreen previousScreen, GameTime time)
		{
			base.Entered (previousScreen, time);
			AddGameComponents (time, knotInput, overlay, pointer, world, modelMouseHandler, settingsMenu);
			Audio.BackgroundMusic = Sound.CreativeMusic;

			// Einstellungen anwenden
			debugBoundings.Info.IsVisible = Options.Default ["debug", "show-boundings", false];
		}

		#endregion
	}
}
