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
	/// Eine Spielkomponente, die in einem IGameScreen verwendet wird und eine bestimmte Priorität hat.
	/// </summary>
	public abstract class GameScreenComponent : GameComponent, IGameScreenComponent
	{
		#region Properties

		/// <summary>
		/// Die Zeichen- und Eingabepriorität.
		/// </summary>
		public DisplayLayer Index { get; set; }

		/// <summary>
		/// Der zugewiesene Spielzustand.
		/// </summary>
		public IGameScreen Screen { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines IGameScreenComponent-Objekts und initialisiert diese mit dem zugehörigen IGameScreen und der zugehörigen Zeichenreihenfolge. Diese Spielkomponente kann nur in dem zugehörigen IGameScreen verwendet werden.
		/// </summary>
		public GameScreenComponent (IGameScreen screen, DisplayLayer index)
		: base(screen.Game)
		{
			this.Screen = screen;
			this.Index = index;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gibt Spielkomponenten zurück, die in dieser Spielkomponente enthalten sind.
		/// [returntype=IEnumerable<IGameScreenComponent>]
		/// </summary>
		public virtual IEnumerable<IGameScreenComponent> SubComponents (GameTime GameTime)
		{
			yield break;
		}

		#endregion
	}
}

