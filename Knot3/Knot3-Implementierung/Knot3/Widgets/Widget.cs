using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
	/// Eine abstrakte Klasse, von der alle Element der grafischen Benutzeroberfläche erben.
	/// </summary>
	public abstract class Widget : DrawableGameScreenComponent
	{
		#region Properties

		/// <summary>
		/// Die Ausmaße des Widgets.
		/// </summary>
		public Bounds Bounds { get; set; }

		/// <summary>
		/// Gibt an, ob das grafische Element sichtbar ist.
		/// </summary>
		public virtual bool IsVisible
		{
			get { return _isVisible && Bounds.Size.Absolute.Length () > 0; }
			set { _isVisible = value; }
		}

		private bool _isVisible;

		/// <summary>
		/// Die Hintergrundfarbe.
		/// </summary>
		public Func<Color> BackgroundColorFunc { get; set; }

		/// <summary>
		/// Die Vordergrundfarbe.
		/// </summary>
		public Func<Color> ForegroundColorFunc { get; set; }

		/// <summary>
		/// Die horizontale Ausrichtung.
		/// </summary>
		public HorizontalAlignment AlignX { get; set; }

		/// <summary>
		/// Die vertikale Ausrichtung.
		/// </summary>
		public VerticalAlignment AlignY { get; set; }

		public List<Keys> ValidKeys { get; protected set; }

		public virtual bool IsKeyEventEnabled
		{
			get { return IsVisible && ValidKeys.Count > 0; }
			set { }
		}

		public virtual bool IsMouseClickEventEnabled
		{
			get { return IsVisible; }
			set { }
		}

		public virtual bool IsMouseMoveEventEnabled
		{
			get { return IsVisible; }
			set { }
		}

		public virtual bool IsMouseScrollEventEnabled
		{
			get { return IsVisible; }
			set { }
		}

		public virtual bool IsEnabled
		{
			get { return _isEnabled; }
			set { _isEnabled = value; }
		}

		private bool _isEnabled;

		public virtual State State { get; set; }

		public virtual Color SelectedColorBackground { get; set; }

		public virtual Color SelectedColorForeground { get; set; }

		public bool IsModal { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Erstellt ein neues grafisches Benutzerschnittstellenelement in dem angegebenen Spielzustand
		/// mit der angegebenen Zeichenreihenfolge.
		/// </summary>
		public Widget (IGameScreen screen, DisplayLayer drawOrder)
		: base (screen, drawOrder)
		{
			Bounds = Bounds.Zero (screen);
			AlignX = HorizontalAlignment.Left;
			AlignY = VerticalAlignment.Center;
			ForegroundColorFunc = MenuItemForegroundColor;
			BackgroundColorFunc = MenuItemBackgroundColor;
			ValidKeys = new List<Keys> ();
			IsVisible = true;
			_isEnabled = true;
			IsModal = false;
			State = State.None;
			SelectedColorBackground = Color.White;
			SelectedColorForeground = Color.Black;
		}

		public Color MenuItemBackgroundColor ()
		{
			if (State == State.None || State == State.Hovered) {
				return Color.Transparent;
			}
			else if (State == State.Selected) {
				return SelectedColorBackground;
			}
			else {
				return Color.CornflowerBlue;
			}
		}

		public  Color MenuItemForegroundColor ()
		{
			if (State == State.Hovered) {
				return Color.White;
			}
			else if (State == State.None) {
				return Color.White * 0.7f;
			}
			else if (State == State.Selected) {
				return SelectedColorForeground;
			}
			else {
				return Color.CornflowerBlue;
			}
		}

		#endregion
	}
}
