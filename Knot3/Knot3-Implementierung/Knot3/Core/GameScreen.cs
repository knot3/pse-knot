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
using Knot3.Audio;

namespace Knot3.Core
{
	/// <summary>
	/// Ein Spielzustand, der zu einem angegebenen Spiel gehört und einen Inputhandler und Rendereffekte enthält.
	/// </summary>
	public class GameScreen : IGameScreen, IDisposable
	{
		#region Properties

		/// <summary>
		/// Das Spiel, zu dem der Spielzustand gehört.
		/// </summary>
		public Knot3Game Game { get; set; }

		/// <summary>
		/// Der Inputhandler des Spielzustands.
		/// </summary>
		public InputManager Input { get; set; }

		public AudioManager Audio { get; private set; }

		/// <summary>
		/// Der aktuelle Postprocessing-Effekt des Spielzustands
		/// </summary>
		public IRenderEffect PostProcessingEffect { get; set; }

		/// <summary>
		/// Ein Stack, der während dem Aufruf der Draw-Methoden der Spielkomponenten die jeweils aktuellen Rendereffekte enthält.
		/// </summary>
		public IRenderEffectStack CurrentRenderEffects { get; set; }

		/// <summary>
		/// Der nächste Spielstand, der von Knot3Game gesetzt werden soll.
		/// </summary>
		public IGameScreen NextScreen { get; set; }

		public GraphicsDeviceManager Graphics { get { return Game.Graphics; } }

		public GraphicsDevice Device { get { return Game.GraphicsDevice; } }

		public Viewport Viewport
		{
			get { return Device.Viewport; }
			set { Device.Viewport = value; }
		}

		public ContentManager Content { get { return Game.Content; } }

		public Color BackgroundColor { get; protected set; }

		public Bounds Bounds
		{
			get { return new Bounds (screen: this, relX: 0f, relY: 0f, relWidth: 1f, relHeight: 1f); }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues IGameScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
		/// </summary>
		public GameScreen (Knot3Game game)
		{
			Game = game;
			NextScreen = this;
			CurrentRenderEffects = new RenderEffectStack (
			    screen: this,
			    defaultEffect: new StandardEffect (this)
			);
			PostProcessingEffect = new StandardEffect (this);
			Input = new InputManager (this);
			Audio = new AudioManager (this);
			BackgroundColor = Color.Black;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Beginnt mit dem Füllen der Spielkomponentenliste des XNA-Frameworks und fügt sowohl für Tastatur- als auch für
		/// Mauseingaben einen Inputhandler für Widgets hinzu. Wird in Unterklassen von IGameScreen reimplementiert und fügt zusätzlich weitere
		/// Spielkomponenten hinzu.
		/// </summary>
		public virtual void Entered (IGameScreen previousScreen, GameTime time)
		{
			Console.WriteLine ("Entered: " + this);
			AddGameComponents (time, Input, Audio, new WidgetKeyHandler (this), new WidgetMouseHandler (this));
		}

		/// <summary>
		/// Leert die Spielkomponentenliste des XNA-Frameworks.
		/// </summary>
		public virtual void BeforeExit (IGameScreen nextScreen, GameTime time)
		{
			Console.WriteLine ("BeforeExit: " + this);
			Game.Components.Clear ();
		}

		/// <summary>
		/// Zeichnet die Teile des IGameScreens, die keine Spielkomponenten sind.
		/// </summary>
		public virtual void Draw (GameTime time)
		{
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public virtual void Update (GameTime time)
		{
		}

		protected virtual Color MenuItemBackgroundColor (State itemState)
		{
			return Color.Transparent;
		}

		protected virtual Color MenuItemForegroundColor (State itemState)
		{
			if (itemState == State.Hovered) {
				return Color.White;
			}
			else {
				return Color.White * 0.7f;
			}
		}
		/// <summary>
		/// Fügt die angegebenen GameComponents in die Components-Liste des Games ein.
		/// </summary>
		public virtual void AddGameComponents (GameTime time, params IGameScreenComponent[] components)
		{
			foreach (IGameScreenComponent component in components) {
				Console.WriteLine ("AddGameComponents: " + component);
				Game.Components.Add (component);
				AddGameComponents (time, component.SubComponents (time).ToArray ());
			}
		}

		/// <summary>
		/// Entfernt die angegebenen GameComponents aus der Components-Liste des Games.
		/// </summary>
		public virtual void RemoveGameComponents (GameTime time, params IGameScreenComponent[] components)
		{
			foreach (IGameScreenComponent component in components) {
				Console.WriteLine ("RemoveGameComponents: " + component);
				RemoveGameComponents (time, component.SubComponents (time).ToArray ());
				Game.Components.Remove (component);
			}
		}

		public void Dispose()
		{
			//
		}

		#endregion
	}
}
