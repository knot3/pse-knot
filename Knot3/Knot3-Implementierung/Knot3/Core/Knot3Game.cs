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
using Knot3.Development;

namespace Knot3.Core
{
	/// <summary>
	/// Die zentrale Spielklasse, die von der \glqq Game\grqq~-Klasse des XNA-Frameworks erbt.
	/// </summary>
	public class Knot3Game : Game
	{
		#region Properties

		private string lastResolution;
		private bool isFullscreen;
		public Action FullScreenChanged = () => {};
		/// <summary>
		/// Wird dieses Attribut ausgelesen, dann gibt es einen Wahrheitswert zurück, der angibt,
		/// ob sich das Spiel im Vollbildmodus befindet. Wird dieses Attribut auf einen Wert gesetzt,
		/// dann wird der Modus entweder gewechselt oder beibehalten, falls es auf denselben Wert gesetzt wird.
		/// </summary>
		public bool IsFullScreen
		{
			get {
				return isFullscreen;
			}
			set {
				if (value != isFullscreen) {
					Log.Debug ("Fullscreen Toggle");
					if (value) {
						Graphics.PreferredBackBufferWidth = Graphics.GraphicsDevice.DisplayMode.Width;
						Graphics.PreferredBackBufferHeight = Graphics.GraphicsDevice.DisplayMode.Height;
					}
					else {
						string currentResolution = Graphics.GraphicsDevice.DisplayMode.Width.ToString ()
							+ "x"
							+ Graphics.GraphicsDevice.DisplayMode.Height.ToString ();

						Options.Default ["video", "resolution", currentResolution] = "1280x720";
					}
					Graphics.ToggleFullScreen ();
					Graphics.ApplyChanges ();
					isFullscreen = value;
					Graphics.ApplyChanges ();
					FullScreenChanged ();
					toDefaultSize (isFullscreen);
				}
			}
		}

		/// <summary>
		/// Enthält als oberste Element den aktuellen Spielzustand und darunter die zuvor aktiven Spielzustände.
		/// </summary>
		public Stack<IGameScreen> Screens { get; set; }

		/// <summary>
		/// Dieses Attribut dient sowohl zum Setzen des Aktivierungszustandes der vertikalen Synchronisation,
		/// als auch zum Auslesen dieses Zustandes.
		/// </summary>
		public Boolean VSync
		{
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

		private static readonly Vector2 defaultSize = MonoHelper.IsRunningOnMono ()
		        ? new Vector2 (1024, 600) : new Vector2 (1280, 720);

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

			if (MonoHelper.IsRunningOnLinux ()) {
				IsMouseVisible = true;
			}
			else if (MonoHelper.IsRunningOnWindows ()) {
				IsMouseVisible = false;
				System.Windows.Forms.Cursor.Hide ();
			}
			else {
				throw new Exception ("Unsupported Plattform Exception");
			}

			Content.RootDirectory = "Content";
			Window.Title = "Knot3 " + Program.Version;
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
			Graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 8;
			Graphics.PreferMultiSampling = true;

			// screens
			Screens = new Stack<IGameScreen> ();
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
			IGameScreen current = Screens.Peek ();

			// Starte den Post-Processing-Effekt des Screens
			current.PostProcessingEffect.Begin (time);
			Graphics.GraphicsDevice.Clear (current.BackgroundColor);

			try {
				// Rufe Draw() auf dem aktuellen Screen auf
				current.Draw (time);

				// Rufe Draw() auf den Spielkomponenten auf
				base.Draw (time);
			}
			catch (Exception ex) {
				// Error Screen
				ShowError (ex);
			}

			// Beende den Post-Processing-Effekt des Screens
			current.PostProcessingEffect.End (time);
		}

		/// <summary>
		/// Macht nichts. Das Freigeben aller Objekte wird von der automatischen Speicherbereinigung übernommen.
		/// </summary>
		protected override void UnloadContent ()
		{
		}

		public void ShowError (Exception ex)
		{
			Screens = new Stack<IGameScreen> ();
			Screens.Push (new ErrorScreen (this, ex));
			Screens.Peek ().Entered (null, null);
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		protected override void Update (GameTime time)
		{
			try {
				updateResolution ();
				// falls der Screen gewechselt werden soll...
				IGameScreen current = Screens.Peek ();
				IGameScreen next = current.NextScreen;
				if (current != next) {
					next.PostProcessingEffect = new FadeEffect (next, current);
					current.BeforeExit (next, time);
					current.NextScreen = current;
					next.NextScreen = next;
					Screens.Push (next);
					next.Entered (current, time);
				}

				if (current.PostProcessingEffect is FadeEffect && (current.PostProcessingEffect as FadeEffect).IsFinished) {
					current.PostProcessingEffect = new StandardEffect (current);
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
			catch (Exception ex) {
				// Error Screen
				ShowError (ex);
			}
		}

		private void toDefaultSize (bool fullscreen)
		{
			if (!fullscreen) {
				Graphics.PreferredBackBufferWidth = (int)Knot3Game.defaultSize.X;
				Graphics.PreferredBackBufferHeight = (int)Knot3Game.defaultSize.Y;
				Graphics.ApplyChanges ();
			}
		}

		private void updateResolution ()
		{
			int width;
			int height;
			string currentResolution = Graphics.GraphicsDevice.DisplayMode.Width.ToString ()
				+ "x"
				+ Graphics.GraphicsDevice.DisplayMode.Height.ToString ();
			if (lastResolution != Options.Default ["video", "resolution", currentResolution] && !isFullscreen) {
				String strReso = Options.Default ["video", "resolution", currentResolution];
				string[] reso = strReso.Split ('x');
				width = int.Parse (reso [0]);
				height = int.Parse (reso [1]);
				Graphics.PreferredBackBufferWidth = width;
				Graphics.PreferredBackBufferHeight = height;
				Graphics.ApplyChanges ();
			}
			lastResolution = Options.Default ["video", "resolution", currentResolution];
		}

		#endregion
	}
}
