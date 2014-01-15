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

namespace Knot3.Core
{
	/// <summary>
	/// Ein Spielzustand, der zu einem angegebenen Spiel gehört und einen Inputhandler und Rendereffekte enthält.
	/// </summary>
	public class GameScreen
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

		/// <summary>
		/// Der aktuelle Postprocessing-Effekt des Spielzustands
		/// </summary>
		public RenderEffect PostProcessingEffect { get; set; }

		/// <summary>
		/// Ein Stack, der während dem Aufruf der Draw-Methoden der Spielkomponenten die jeweils aktuellen Rendereffekte enthält.
		/// </summary>
		public RenderEffectStack CurrentRenderEffects { get; set; }

		/// <summary>
		/// Der nächste Spielstand, der von Knot3Game gesetzt werden soll.
		/// </summary>
		public GameScreen NextScreen { get; set; }

		public GraphicsDeviceManager Graphics { get { return Game.Graphics; } }

		public GraphicsDevice Device { get { return Game.GraphicsDevice; } }

		public Viewport Viewport { get { return Device.Viewport; } }

		public ContentManager Content { get { return Game.Content; } }

		public Color BackgroundColor { get; protected set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues GameScreen-Objekt und initialisiert dieses mit einem Knot3Game-Objekt.
		/// </summary>
		public GameScreen (Knot3Game game)
		{
			this.Game = game;
			this.NextScreen = this;
			this.CurrentRenderEffects = new RenderEffectStack (
			    screen: this,
			    defaultEffect: new StandardEffect (this)
			);
			this.PostProcessingEffect = new StandardEffect (this);
			this.Input = new InputManager (this);
			this.BackgroundColor = Color.Black;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Beginnt mit dem Füllen der Spielkomponentenliste des XNA-Frameworks und fügt sowohl für Tastatur- als auch für
		/// Mauseingaben einen Inputhandler für Widgets hinzu. Wird in Unterklassen von GameScreen reimplementiert und fügt zusätzlich weitere
		/// Spielkomponenten hinzu.
		/// </summary>
		public virtual void Entered (GameScreen previousScreen, GameTime time)
		{
			Console.WriteLine ("Entered: " + this);
			AddGameComponents (time, Input, new WidgetKeyHandler (this), new WidgetMouseHandler (this));
		}

		/// <summary>
		/// Leert die Spielkomponentenliste des XNA-Frameworks.
		/// </summary>
		public virtual void BeforeExit (GameScreen nextScreen, GameTime time)
		{
			Console.WriteLine ("BeforeExit: " + this);
			Game.Components.Clear ();
		}

		/// <summary>
		/// Zeichnet die Teile des GameScreens, die keine Spielkomponenten sind.
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

		#endregion

	}
}

