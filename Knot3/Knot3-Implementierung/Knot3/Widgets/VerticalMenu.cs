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
using Knot3.Utilities;

namespace Knot3.Widgets
{
	/// <summary>
	/// Ein Menü, das alle Einträge vertikal anordnet.
	/// </summary>
	public sealed class VerticalMenu : Menu, IMouseClickEventListener, IMouseMoveEventListener
	{
		#region Properties

		/// <summary>
		/// Die von der Auflösung unabhängige Höhe der Menüeinträge in Prozent.
		/// </summary>
		/// <value>
		/// The height of the relative item.
		/// </value>
		public float RelativeItemHeight { get; set; }

		public Rectangle MouseClickBounds { get { return Bounds; } }

		private SpriteBatch spriteBatch;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines VerticalMenu-Objekts und initialisiert diese mit dem zugehörigen IGameScreen-Objekt.
		/// Zudem ist die Angaben der Zeichenreihenfolge Pflicht.
		/// </summary>
		public VerticalMenu (IGameScreen screen, DisplayLayer drawOrder)
		: base(screen, drawOrder)
		{
			RelativeItemHeight = 0.040f;
			spriteBatch = new SpriteBatch (screen.Device);
		}
		
		private Bounds ScrollBarBounds
		{
			get {
				Bounds bounds = Bounds.FromLeft (1f);
				bounds.Size += new ScreenPoint (Screen, 0.01f, 0);
				return bounds;
			}
		}
		
		public Rectangle MouseMoveBounds
		{
			get {
				return ScrollBarBounds;
			}
		}

		private Bounds ScrollSliderBounds
		{
			get {
				Bounds moveBounds = ScrollBarBounds;
				float currentValue = (currentScrollPosition - minScrollPosition) / (maxScrollPosition - minScrollPosition);
				float visiblePercent = pageScrollPosition / (maxScrollPosition - minScrollPosition);
				Bounds bounds = new Bounds (
					position: moveBounds.FromTop (currentValue).Position,
					size: moveBounds.FromTop (visiblePercent).Size
				);
				return bounds;
			}
		}

		#endregion

		#region Methods

		protected override void assignMenuItemInformation (MenuItem item)
		{
			Bounds itemBounds = ItemBounds (item.ItemOrder);
			item.Bounds.Position = itemBounds.Position;
			item.Bounds.Size = itemBounds.Size;
			base.assignMenuItemInformation (item);
		}

		/// <summary>
		/// Die von der Auflösung unabhängigen Ausmaße der Menüeinträge.
		/// </summary>
		public Bounds ItemBounds (int itemOrder)
		{
			return new Bounds (
			           position: new ScreenPoint (Screen, () => verticalRelativeItemPosition (itemOrder)),
			           size: new ScreenPoint (Screen, () => verticalRelativeItemSize (itemOrder))
			       );
		}

		private Vector2 verticalRelativeItemPosition (int itemOrder)
		{
			if (itemOrder < currentScrollPosition) {
				return Vector2.Zero;
			}
			else if (itemOrder > currentScrollPosition + pageScrollPosition - 1) {
				return Vector2.Zero;
			}
			else {
				float itemHeight = RelativeItemHeight + Bounds.Padding.Relative.Y;
				return Bounds.Position.Relative + new Vector2 (0, itemHeight * (itemOrder - currentScrollPosition));
			}
		}

		private Vector2 verticalRelativeItemSize (int itemOrder)
		{
			if (itemOrder < currentScrollPosition) {
				return Vector2.Zero;
			}
			else if (itemOrder > currentScrollPosition + pageScrollPosition - 1) {
				return Vector2.Zero;
			}
			else {
				return new Vector2 (Bounds.Size.Relative.X, RelativeItemHeight);
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

		private int currentScrollPosition
		{
			get {
				return _currentScrollPosition;
			}
			set {
				_currentScrollPosition = (int)MathHelper.Clamp (value, minScrollPosition, maxScrollPosition - pageScrollPosition);
			}
		}

		private int _currentScrollPosition;

		private int minScrollPosition
		{
			get {
				return 0;
			}
		}

		private int maxScrollPosition
		{
			get {
				return items.Count;
			}
		}

		private int pageScrollPosition
		{
			get {
				return (int)Math.Ceiling (Bounds.Size.Relative.Y / (RelativeItemHeight + Bounds.Padding.Relative.Y));
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
		public void SetHovered (bool hovered, GameTime time)
		{
		}

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			spriteBatch.Begin ();
			Texture2D rectangleTexture = TextureHelper.Create (Screen.Device, Lines.DefaultLineColor);
			spriteBatch.Draw (rectangleTexture, ScrollSliderBounds, Lines.DefaultLineColor);
			spriteBatch.End ();
		}
		
		public void OnLeftMove (Vector2 previousPosition, Vector2 currentPosition, Vector2 move, GameTime time)
		{
			currentScrollPosition += (int)((move.Y / RelativeItemHeight)
				* ((float)minScrollPosition / (maxScrollPosition - pageScrollPosition)));
		}

		public void OnRightMove (Vector2 previousPosition, Vector2 currentPosition, Vector2 move, GameTime time)
		{
		}

		public void OnMove (Vector2 previousPosition, Vector2 currentPosition, Vector2 move, GameTime time)
		{
		}


		#endregion
	}
}

