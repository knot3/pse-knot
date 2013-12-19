using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Core
{
	using RenderEffects;
	using Screens;

	/// <remarks>
	/// Zustands-Muster (EN: state pattern)
	/// 
	/// Verwaltung der Spielzustände. Jeder Spielzustand hat eine eigene grafische Oberfläche und eigenes Verhalten.
	/// </remarks>
	public abstract class GameScreen
	{
		public virtual Knot3Game Game
		{
			get;
			set;
		}

		public virtual Input Input
		{
			get;
			set;
		}

		public virtual RenderEffect PostProcessingEffect
		{
			get;
			set;
		}

		public virtual RenderEffectStack CurrentRenderEffects
		{
			get;
			set;
		}

		public virtual void Entered(GameScreen previousScreen, GameTime time)
		{
			throw new System.NotImplementedException();
		}

		public virtual void BeforeExit(GameScreen nextScreen, GameTime time)
		{
			throw new System.NotImplementedException();
		}

		public virtual void Update(GameTime time)
		{
			throw new System.NotImplementedException();
		}

		public GameScreen(Knot3Game game)
		{
		}

        public virtual void AddGameComponents(params IGameScreenComponent[] components)
		{
			throw new System.NotImplementedException();
		}

        public virtual void RemoveGameComponents(params IGameScreenComponent[] components)
		{
			throw new System.NotImplementedException();
		}

	}
}

