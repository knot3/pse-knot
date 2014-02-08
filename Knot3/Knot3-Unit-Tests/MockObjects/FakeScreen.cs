using System;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Knot3.Core;
using Knot3.Audio;
using Knot3.RenderEffects;
using Knot3.Widgets;

namespace Knot3.UnitTests
{
	public class FakeScreen : IGameScreen
	{
		#region Properties

		/// <summary>
		/// Das Spiel, zu dem der Spielzustand gehört.
		/// </summary>
		public Knot3Game Game
		{
			get { return null;}
			set {}
		}

		/// <summary>
		/// Der Inputhandler des Spielzustands.
		/// </summary>
		public InputManager Input { get; private set; }

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

		public GraphicsDeviceManager Graphics { get; private set; }

		public GraphicsDevice Device { get; private set; }

		public Viewport Viewport { get { return new Viewport(new Rectangle(0, 0, 800, 600)); } set { } }

		public ContentManager Content { get; private set; }

		public Color BackgroundColor { get; private set; }

		public Bounds Bounds
		{
			get { return new Bounds (screen: this, relX: 0f, relY: 0f, relWidth: 1f, relHeight: 1f); }
		}

		#endregion

		#region Constructors

		public FakeScreen ()
		{
			NextScreen = this;
			CurrentRenderEffects = new FakeEffectStack (
			    screen: this,
			    defaultEffect: new FakeEffect (this)
			);
			PostProcessingEffect = new FakeEffect (this);
			Input = new InputManager (this);
			//Audio = new AudioManager (this);
			BackgroundColor = Color.Black;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Beginnt mit dem Füllen der Spielkomponentenliste des XNA-Frameworks und fügt sowohl für Tastatur- als auch für
		/// Mauseingaben einen Inputhandler für Widgets hinzu. Wird in Unterklassen von IGameScreen reimplementiert und fügt zusätzlich weitere
		/// Spielkomponenten hinzu.
		/// </summary>
		public void Entered (IGameScreen previousScreen, GameTime time)
		{
		}

		/// <summary>
		/// Leert die Spielkomponentenliste des XNA-Frameworks.
		/// </summary>
		public void BeforeExit (IGameScreen nextScreen, GameTime time)
		{
		}

		/// <summary>
		/// Zeichnet die Teile des IGameScreens, die keine Spielkomponenten sind.
		/// </summary>
		public void Draw (GameTime time)
		{
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public void Update (GameTime time)
		{
		}

		/// <summary>
		/// Fügt die angegebenen GameComponents in die Components-Liste des Games ein.
		/// </summary>
		public void AddGameComponents (GameTime time, params IGameScreenComponent[] components)
		{
		}

		/// <summary>
		/// Entfernt die angegebenen GameComponents aus der Components-Liste des Games.
		/// </summary>
		public void RemoveGameComponents (GameTime time, params IGameScreenComponent[] components)
		{
		}

		#endregion
	}
}
