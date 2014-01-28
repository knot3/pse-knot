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
				Bounds bounds = new Bounds (
					new ScreenPoint (Screen, Bounds.Position.Relative + Bounds.Size.OnlyX.Relative),
					new ScreenPoint (Screen, 0.02f, Bounds.Size.Relative.Y)
				);
				return bounds;
			}
		}
		
		public Rectangle MouseMoveBounds
		{
			get {
				return new Bounds(Bounds.Position, Bounds.Size+ScrollBarBounds.Size.OnlyX);
			}
		}

		private Bounds ScrollSliderInBarBounds
		{
			get {
				Bounds moveBounds = ScrollBarBounds;
				float maxValue = maxScrollPosition;
				float pageValue = pageScrollPosition;
				float visiblePercent = (pageValue / maxValue).Clamp (0.05f, 1f);
				float currentValue = (float)currentScrollPosition / (maxValue - pageValue);
				// Console.WriteLine ("currentValue=" + currentValue + ", pos=" + moveBounds.FromTop (currentValue).Position);
				Bounds bounds = new Bounds (
					position: moveBounds.Size.OnlyY * currentValue * (1f - visiblePercent),
					size: moveBounds.Size.ScaleY (visiblePercent)
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

		private float currentScrollPosition
		{
			get {
				return _currentScrollPosition;
			}
			set {
				_currentScrollPosition = MathHelper.Clamp (value, 0, maxScrollPosition - pageScrollPosition);
			}
		}

		private float _currentScrollPosition;

		private float maxScrollPosition { get { return items.Count; } }

		private float pageScrollPosition
		{
			get {
				return (int)Math.Ceiling (Bounds.Size.Relative.Y / (RelativeItemHeight + Bounds.Padding.Relative.Y));
			}
		}

		private bool HasScrollbar { get { return maxScrollPosition > pageScrollPosition; } }

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

			if (IsVisible && IsEnabled && HasScrollbar) {
				spriteBatch.Begin ();
				Texture2D rectangleTexture = TextureHelper.Create (Screen.Device, Lines.DefaultLineColor);
				Bounds sliderBounds = ScrollSliderInBarBounds.In (ScrollBarBounds);
				spriteBatch.Draw (rectangleTexture, sliderBounds, Lines.DefaultLineColor);
				// Console.WriteLine ("ScrollSliderBounds=" + sliderBounds.Rectangle);
				// Console.WriteLine ("ScrollBarBounds=" + ScrollBarBounds.Rectangle);
				spriteBatch.End ();
			}
		}
		
		public void OnLeftMove (Vector2 previousPosition, Vector2 currentPosition, Vector2 move, GameTime time)
		{
			//currentScrollPosition += (int)((move.Y / RelativeItemHeight)
			//	* ((float)minScrollPosition / (maxScrollPosition - pageScrollPosition)));

			if (IsVisible && IsEnabled && HasScrollbar) {
				Bounds slider = ScrollSliderInBarBounds;
				Bounds bar = ScrollBarBounds;

				float percentOfBar = move.Y / bar.Size.Absolute.Y;
				currentScrollPosition += percentOfBar * maxScrollPosition;

				/*
				float maxValue = maxScrollPosition;
				float pageValue = pageScrollPosition;
				float visiblePercent = (pageValue / maxValue).Clamp (0.05f, 1f);
				float sliderPosition = ScrollSliderInBarBounds.Position.Absolute.Y / ScrollBarBounds.Size.Absolute.Y;
				Console.WriteLine ("sliderPosition=" + sliderPosition + ", ScrollSliderInBarBounds=" + ScrollSliderInBarBounds);
				sliderPosition = move.Y / ScrollBarBounds.Size.Absolute.Y;

				Console.WriteLine ("sliderPosition new=" + sliderPosition + ", current.Y=" + currentPosition.Y
					+ ", bar.Size.Y=" + ScrollBarBounds.Size.Absolute.Y
				);
				currentScrollPosition = (int)(sliderPosition * (maxValue - pageValue));
				*/
			}
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

