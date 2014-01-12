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
using Knot3.RenderEffects;
using Knot3.KnotData;

namespace Knot3.Widgets
{
	/// <summary>
	/// Ein Menü, das alle Einträge vertikal anordnet.
	/// </summary>
	public sealed class VerticalMenu : Menu, IMouseEventListener
	{
		#region Properties

		/// <summary>
		/// Die von der Auflösung unabhängige Größe der Menüeinträge in Prozent.
		/// </summary>
		public override Func<int, Vector2> RelativeItemPosition {
			get {
				return VerticalRelativeItemPosition;
			}
			set {
				throw new InvalidOperationException ("Cannot assign a RelativeItemPosition to a VerticalMenu! It is computed automatically.");
			}
		}
		
		/// <summary>
		/// Die von der Auflösung unabhängige Position der Menüeinträge in Prozent.
		/// </summary>
		public override Func<int, Vector2> RelativeItemSize {
			get {
				return VerticalRelativeItemSize;
			}
			set {
				throw new InvalidOperationException ("Cannot assign a RelativeItemSize to a VerticalMenu! It is computed automatically.");
			}
		}

		/// <summary>
		/// Die von der Auflösung unabhängige Höhe der Menüeinträge in Prozent.
		/// </summary>
		/// <value>
		/// The height of the relative item.
		/// </value>
		public float RelativeItemHeight { get; set; }

		#endregion

        #region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines VerticalMenu-Objekts und initialisiert diese mit dem zugehörigen GameScreen-Objekt.
		/// Zudem ist die Angaben der Zeichenreihenfolge Pflicht.
		/// </summary>
		public VerticalMenu (GameScreen screen, DisplayLayer drawOrder)
			: base(screen, drawOrder)
		{
			RelativeItemHeight = 0.040f;
		}

        #endregion

        #region Methods

		public Vector2 VerticalRelativeItemPosition (int itemNumber)
		{
			if (itemNumber < currentScrollPosition) {
				return Vector2.Zero;
			} else if (itemNumber > currentScrollPosition + pageScrollPosition - 1) {
				return Vector2.Zero;
			} else {
				float itemHeight = RelativeItemHeight + RelativePadding ().Y;
				return RelativePosition () + new Vector2 (0, itemHeight * (itemNumber - currentScrollPosition));
			}
		}

		public Vector2 VerticalRelativeItemSize (int itemNumber)
		{
			if (itemNumber < currentScrollPosition) {
				return Vector2.Zero;
			} else if (itemNumber > currentScrollPosition + pageScrollPosition - 1) {
				return Vector2.Zero;
			} else {
				return new Vector2 (RelativeSize ().X, RelativeItemHeight);
			}
		}

		/// <summary>
		/// Wird für jeden Frame aufgerufen.
		/// </summary>
		public override void Update (GameTime time)
		{
			base.Update (time);
			performScroll ();
		}

		/// <summary>
		/// Die Reaktion auf eine Bewegung des Mausrads.
		/// </summary>
		public override void OnScroll (int scrollValue)
		{
			tempScrollValue = scrollValue;
		}

		private void performScroll ()
		{
			if (Math.Abs (tempScrollValue) > 0) {
				currentScrollPosition += tempScrollValue;
				tempScrollValue = 0;
			}
		}

		private int tempScrollValue = 0;

		private int currentScrollPosition {
			get {
				return _currentScrollPosition;
			}
			set {
				_currentScrollPosition = (int)MathHelper.Clamp (value, minScrollPosition, maxScrollPosition - pageScrollPosition);
			}
		}

		private int _currentScrollPosition;

		private int minScrollPosition {
			get {
				return 0;
			}
		}

		private int maxScrollPosition {
			get {
				return items.Count;
			}
		}

		private int pageScrollPosition {
			get {
				return (int)Math.Ceiling (RelativeSize ().Y / (RelativeItemHeight + RelativePadding ().Y));
			}
		}

		/// <summary>
		/// Tut nichts.
		/// </summary>
		public void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
		}

		/// <summary>
		/// Tut nichts.
		/// </summary>
		public void OnRightClick (Vector2 position, ClickState state, GameTime time)
		{
		}
		
		/// <summary>
		/// Tut nichts.
		/// </summary>
		public void SetHovered (bool hovered)
		{
		}

        #endregion
	}
}

