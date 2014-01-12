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
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.GameObjects
{
	/// <summary>
	/// Eine abstrakte Klasse, die ein Vorschau-Spielobjekt darstellt.
	/// </summary>
	public abstract class ShadowGameObject : IGameObject
	{
        #region Properties

		/// <summary>
		/// Enthält Informationen über das Vorschau-Spielobjekt.
		/// </summary>
		public GameObjectInfo Info { get; private set; }

		/// <summary>
		/// Eine Referenz auf die Spielwelt, in der sich das Spielobjekt befindet.
		/// </summary>
		public World World {
			get { return decoratedObject.World; }
			set {}
		}

		/// <summary>
		/// Die Position, an der das Vorschau-Spielobjekt gezeichnet werden soll.
		/// </summary>
		public Vector3 ShadowPosition { get; set; }

		/// <summary>
		/// Die Position, an der sich das zu dekorierende Objekt befindet.
		/// </summary>
		public Vector3 OriginalPosition {
			get { return decoratedObject.Info.Position; }
		}
		
		protected IGameObject decoratedObject { get; private set; }

		protected GameScreen screen;

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt ein neues Vorschauobjekt in dem angegebenen Spielzustand für das angegebene zu dekorierende Objekt.
		/// </summary>
		public ShadowGameObject (GameScreen screen, IGameObject decoratedObj)
		{
			this.screen = screen;
			this.decoratedObject = decoratedObj;

			Info = new GameObjectInfo (position: Vector3.Zero, isVisible: true, isSelectable: false, isMovable: false);
		}

        #endregion

        #region Methods

		/// <summary>
		/// Die Position, an der das Vorschau-Spielobjekt gezeichnet werden soll.
		/// </summary>
		public virtual Vector3 Center ()
		{
			return ShadowPosition;
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public virtual void Update (GameTime GameTime)
		{
			Info.IsVisible = Math.Abs ((ShadowPosition - OriginalPosition).Length ()) > 50;
		}

		/// <summary>
		/// Zeichnet das Vorschau-Spielobjekt.
		/// </summary>
		public virtual void Draw (GameTime time)
		{
			Vector3 originalPositon = decoratedObject.Info.Position;
			decoratedObject.Info.Position = ShadowPosition;
			decoratedObject.Draw (time);
			decoratedObject.Info.Position = originalPositon;
		}

		/// <summary>
		/// Prüft, ob der angegebene Mausstrahl das Vorschau-Spielobjekt schneidet.
		/// </summary>
		public virtual GameObjectDistance Intersects (Ray Ray)
		{
			return null;
		}

        #endregion

	}
}

