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
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Screens;
using Knot3.Utilities;

namespace Knot3.Core
{
	/// <summary>
	/// Die zentrale Spielklasse, die von der \glqq Game\grqq~-Klasse des XNA-Frameworks erbt.
	/// </summary>
	public class Knot3Game : Game
	{

        #region Properties

		private bool isFullscreen;

		/// <summary>
		/// Wird dieses Attribut ausgelesen, dann gibt es einen Wahrheitswert zurück, der angibt,
		/// ob sich das Spiel im Vollbildmodus befindet. Wird dieses Attribut auf einen Wert gesetzt,
		/// dann wird der Modus entweder gewechselt oder beibehalten, falls es auf denselben Wert gesetzt wird.
		/// </summary>
		public bool IsFullScreen {
			get {
				return isFullscreen;
			}
			set {
				if (value != isFullscreen) {
					Console.WriteLine ("Fullscreen Toggle");
					if (value) {
						Graphics.PreferredBackBufferWidth = Graphics.GraphicsDevice.DisplayMode.Width;
						Graphics.PreferredBackBufferHeight = Graphics.GraphicsDevice.DisplayMode.Height;
					} else {
						Graphics.PreferredBackBufferWidth = (int)Knot3Game.defaultSize.X;
						Graphics.PreferredBackBufferHeight = (int)Knot3Game.defaultSize.Y;
					}
					Graphics.ApplyChanges ();
					Graphics.ToggleFullScreen ();
					Graphics.ApplyChanges ();
					isFullscreen = value;
				}
			}
		}

		/// <summary>
		/// Enthält als oberste Element den aktuellen Spielzustand und darunter die zuvor aktiven Spielzustände.
		/// </summary>
		public Stack<GameScreen> Screens { get; set; }

		/// <summary>
		/// Dieses Attribut dient sowohl zum Setzen des Aktivierungszustandes der vertikalen Synchronisation,
		/// als auch zum Auslesen dieses Zustandes.
		/// </summary>
		public Boolean VSync {
			get {
				return Graphics.SynchronizeWithVerticalRetrace;
			}
			set {
				Graphics.SynchronizeWithVerticalRetrace = value;
				this.IsFixedTimeStep = value;
				Graphics.ApplyChanges ();
			}
		}

		/// <summary>
		/// Der aktuelle Grafikgeräteverwalter des XNA-Frameworks.
		/// </summary>
		public GraphicsDeviceManager Graphics { get; private set; }

		private static Vector2 defaultSize = new Vector2 (1280, 720);

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt ein neues zentrales Spielobjekt und setzt die Auflösung des BackBuffers auf
		/// die in der Einstellungsdatei gespeicherte Auflösung oder falls nicht vorhanden auf die aktuelle
		/// Bildschirmauflösung und wechselt in den Vollbildmodus.
		/// </summary>
		public Knot3Game ()
		{
			Graphics = new GraphicsDeviceManager (this);

			Graphics.PreferredBackBufferWidth = (int)defaultSize.X;
			Graphics.PreferredBackBufferHeight = (int)defaultSize.Y;

			Graphics.IsFullScreen = false;
			isFullscreen = false;
			Graphics.ApplyChanges ();

			Content.RootDirectory = "Content";
			Window.Title = "Test Game 1";
		}

        #endregion

        #region Methods

		/// <summary>
		/// Initialisiert die Attribute dieser Klasse.
		/// </summary>
		protected override void Initialize ()
		{
			// vsync
			VSync = true;

			// anti aliasing
			Graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 4;
			Graphics.PreferMultiSampling = true;

			// screens
			Screens = new Stack<GameScreen> ();
			Screens.Push (new StartScreen (this));
			Screens.Peek ().Entered (null, null);

			// base method
			base.Initialize ();
		}

		/// <summary>
		/// Ruft die Draw()-Methode des aktuellen Spielzustands auf.
		/// </summary>
		protected override void Draw (GameTime time)
		{
			// Lade den aktuellen Screen
			GameScreen current = Screens.Peek ();

			// Starte den Post-Processing-Effekt des Screens
			current.PostProcessingEffect.Begin (current.BackgroundColor, time);

			// Rufe Draw() auf dem aktuellen Screen auf
			current.Draw (time);

			// Rufe Draw() auf den Spielkomponenten auf
			base.Draw (time);

			// Beende den Post-Processing-Effekt des Screens
			current.PostProcessingEffect.End (time);
		}

		/// <summary>
		/// Macht nichts. Das Freigeben aller Objekte wird von der automatischen Speicherbereinigung übernommen.
		/// </summary>
		protected override void UnloadContent ()
		{
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		protected override void Update (GameTime time)
		{
			// falls der Screen gewechselt werden soll...
			GameScreen current = Screens.Peek ();
			GameScreen next = current.NextScreen;
			if (current != next) {
				next.PostProcessingEffect = new FadeEffect (next, current);
				current.BeforeExit (next, time);
				current.NextScreen = current;
				next.NextScreen = next;
				Screens.Push (next);
				next.Entered (current, time);
			}

			if (Keys.F8.IsDown ()) {
				this.Exit ();
				return;
			}

			// Rufe Update() auf dem aktuellen Screen auf
			Screens.Peek ().Update (time);

			// base method
			base.Update (time);
		}

        #endregion

	}
}

