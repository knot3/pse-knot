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

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.KnotData;
using Knot3.Widgets;
using Knot3.Utilities;
using Knot3.RenderEffects;

namespace Knot3.UnitTests
{
	/// <summary>
	/// Eine abstrakte Klasse, die eine Implementierung von IRenderEffect darstellt.
	/// </summary>
	public class FakeEffect : IRenderEffect
	{
		#region Properties

		/// <summary>
		/// Das Rendertarget, in das zwischen dem Aufruf der Begin()- und der End()-Methode gezeichnet wird,
		/// weil es in Begin() als primäres Rendertarget des XNA-Frameworks gesetzt wird.
		/// </summary>
		public RenderTarget2D RenderTarget { get; private set; }

		/// <summary>
		/// Der Spielzustand, in dem der Effekt verwendet wird.
		/// </summary>
		protected IGameScreen screen { get; set; }

		#endregion

		#region Constructors

		public FakeEffect (IGameScreen screen)
		{
			this.screen = screen;
		}

		#endregion

		#region Methods

		/// <summary>
		/// In der Methode Begin() wird das aktuell von XNA genutzte Rendertarget auf einem Stack gesichert
		/// und das Rendertarget des Effekts wird als aktuelles Rendertarget gesetzt.
		/// </summary>
		public void Begin (GameTime time)
		{
			screen.CurrentRenderEffects.Push (this);
		}

		/// <summary>
		/// Das auf dem Stack gesicherte, vorher genutzte Rendertarget wird wiederhergestellt und
		/// das Rendertarget dieses Rendereffekts wird, unter Umständen in Unterklassen verändert,
		/// auf dieses ubergeordnete Rendertarget gezeichnet.
		/// </summary>
		public virtual void End (GameTime time)
		{
			screen.CurrentRenderEffects.Pop ();
		}

		/// <summary>
		/// Zeichnet das Spielmodell model mit diesem Rendereffekt.
		/// </summary>
		public virtual void DrawModel (GameModel model, GameTime time)
		{
			// Setze den Viewport auf den der aktuellen Spielwelt
			Viewport original = screen.Viewport;
			screen.Viewport = model.World.Viewport;

			// hier würde das Modell gezeichnet werden

			// Setze den Viewport wieder auf den ganzen Screen
			screen.Viewport = original;
		}

		protected void ModifyBasicEffect (BasicEffect effect, GameModel model)
		{
		}

		/// <summary>
		/// Beim Laden des Modells wird von der XNA-Content-Pipeline jedem ModelMeshPart ein Shader der Klasse
		/// BasicEffect zugewiesen. Für die Nutzung des Modells in diesem Rendereffekt kann jedem ModelMeshPart
		/// ein anderer Shader zugewiesen werden.
		/// </summary>
		public virtual void RemapModel (Model model)
		{
		}

		/// <summary>
		/// Zeichnet das Rendertarget.
		/// </summary>
		protected virtual void DrawRenderTarget (GameTime GameTime)
		{
		}

		public void DrawLastFrame (GameTime time)
		{
		}

		#endregion
	}
}

