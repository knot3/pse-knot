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
	public interface IGameScreen
	{
		#region Properties

		/// <summary>
		/// Das Spiel, zu dem der Spielzustand gehört.
		/// </summary>
		Knot3Game Game { get; set; }

		/// <summary>
		/// Der Inputhandler des Spielzustands.
		/// </summary>
		InputManager Input { get; }

		AudioManager Audio { get; }

		/// <summary>
		/// Der aktuelle Postprocessing-Effekt des Spielzustands
		/// </summary>
		IRenderEffect PostProcessingEffect { get; set; }

		/// <summary>
		/// Ein Stack, der während dem Aufruf der Draw-Methoden der Spielkomponenten die jeweils aktuellen Rendereffekte enthält.
		/// </summary>
		IRenderEffectStack CurrentRenderEffects { get; set; }

		/// <summary>
		/// Der nächste Spielstand, der von Knot3Game gesetzt werden soll.
		/// </summary>
		IGameScreen NextScreen { get; set; }

		GraphicsDeviceManager Graphics { get; }

		GraphicsDevice Device { get; }

		Viewport Viewport { get; set; }

		ContentManager Content { get; }

		Color BackgroundColor { get; }

		Bounds Bounds { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Beginnt mit dem Füllen der Spielkomponentenliste des XNA-Frameworks und fügt sowohl für Tastatur- als auch für
		/// Mauseingaben einen Inputhandler für Widgets hinzu. Wird in Unterklassen von IGameScreen reimplementiert und fügt zusätzlich weitere
		/// Spielkomponenten hinzu.
		/// </summary>
		void Entered (IGameScreen previousScreen, GameTime time);

		/// <summary>
		/// Leert die Spielkomponentenliste des XNA-Frameworks.
		/// </summary>
		void BeforeExit (IGameScreen nextScreen, GameTime time);

		/// <summary>
		/// Zeichnet die Teile des IGameScreens, die keine Spielkomponenten sind.
		/// </summary>
		void Draw (GameTime time);

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		void Update (GameTime time);

		/// <summary>
		/// Fügt die angegebenen GameComponents in die Components-Liste des Games ein.
		/// </summary>
		void AddGameComponents (GameTime time, params IGameScreenComponent[] components);

		/// <summary>
		/// Entfernt die angegebenen GameComponents aus der Components-Liste des Games.
		/// </summary>
		void RemoveGameComponents (GameTime time, params IGameScreenComponent[] components);

		#endregion
	}
}
