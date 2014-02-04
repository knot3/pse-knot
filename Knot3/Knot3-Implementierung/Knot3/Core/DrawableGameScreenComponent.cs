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
	/// Eine zeichenbare Spielkomponente, die in einem angegebenen Spielzustand verwendet wird und eine bestimmte Priorität hat.
	/// </summary>
	public abstract class DrawableGameScreenComponent : DrawableGameComponent, IGameScreenComponent
	{
		#region Properties

		/// <summary>
		/// Der zugewiesene Spielzustand.
		/// </summary>
		public IGameScreen Screen { get; set; }

		private DisplayLayer _index;

		/// <summary>
		/// Die Zeichen- und Eingabepriorität.
		/// </summary>
		public DisplayLayer Index
		{
			get { return _index; }
			set {
				_index = value;
				DrawOrder = (int)value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines DrawableGameScreenComponent-Objekts und ordnet dieser ein IGameScreen-Objekt zu.
		/// index bezeichnet die Zeichenebene, auf welche die Komponente zu zeichnen ist.
		/// </summary>
		public DrawableGameScreenComponent (IGameScreen screen, DisplayLayer index)
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
