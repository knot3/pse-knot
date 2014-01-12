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
	/// Eine abstrakte Klasse, von der alle Element der grafischen Benutzeroberfläche erben.
	/// </summary>
	public abstract class Widget : DrawableGameScreenComponent
	{
        #region Properties

		/// <summary>
		/// Die von der Auflösung unabhängige Größe in Prozent.
		/// </summary>
		public Func<Vector2> RelativeSize { get; set; }

		/// <summary>
		/// Die von der Auflösung unabhängige Position in Prozent.
		/// </summary>
		public Func<Vector2> RelativePosition { get; set; }

		/// <summary>
		/// Der von der Auflösung unabhängige Abstand in Prozent.
		/// </summary>
		public Func<Vector2> RelativePadding { get; set; }

		/// <summary>
		/// Gibt an, ob das grafische Element sichtbar ist.
		/// </summary>
		public virtual bool IsVisible {
			get { return _isVisible && RelativeSize ().Length () > 0; }
			set { _isVisible = value; }
		}

		private bool _isVisible;

		/// <summary>
		/// Die Hintergrundfarbe.
		/// </summary>
		public Func<Color> BackgroundColor { get; set; }

		/// <summary>
		/// Die Vordergrundfarbe.
		/// </summary>
		public Func<Color> ForegroundColor { get; set; }

		/// <summary>
		/// Die horizontale Ausrichtung.
		/// </summary>
		public HorizontalAlignment AlignX { get; set; }

		/// <summary>
		/// Die vertikale Ausrichtung.
		/// </summary>
		public VerticalAlignment AlignY { get; set; }

		public List<Keys> ValidKeys { get; protected set; }

		public virtual bool IsKeyEventEnabled { get { return IsVisible; } }

		public virtual bool IsMouseEventEnabled { get { return IsVisible; } }

        #endregion

        #region Constructors

		/// <summary>
		/// Erstellt ein neues grafisches Benutzerschnittstellenelement in dem angegebenen Spielzustand
		/// mit der angegebenen Zeichenreihenfolge.
		/// </summary>
		public Widget (GameScreen screen, DisplayLayer drawOrder)
			: base(screen, drawOrder)
		{
			RelativePosition = () => Vector2.Zero;
			RelativeSize = () => Vector2.Zero;
			RelativePadding = () => Vector2.Zero;
			AlignX = HorizontalAlignment.Left;
			AlignY = VerticalAlignment.Center;
			ForegroundColor = () => Color.Gray;
			BackgroundColor = () => Color.Transparent;
			ValidKeys = new List<Keys> ();
			IsVisible = true;
		}

        #endregion

        #region Methods

		/// <summary>
		/// Die Ausmaße des grafischen Elements
		/// </summary>
		public Rectangle Bounds ()
		{
			Point topLeft = ScaledPosition.ToPoint ();
			Point size = ScaledSize.ToPoint ();
			return new Rectangle (topLeft.X, topLeft.Y, size.X, size.Y);
		}

		public Vector2 ScaledPosition {
			get {
				return RelativePosition ().Scale (Screen.Viewport);
			}
		}

		public Vector2 ScaledSize {
			get {
				return RelativeSize ().Scale (Screen.Viewport);
			}
		}

		public Vector2 ScaledPadding {
			get {
				return RelativePadding ().Scale (Screen.Viewport);
			}
		}

        #endregion

	}
}
