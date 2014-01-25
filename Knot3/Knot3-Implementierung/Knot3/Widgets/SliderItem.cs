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
	/// Ein Menüeintrag, der einen Schieberegler bereitstellt, mit dem man einen Wert zwischen einem minimalen
	/// und einem maximalen Wert über Verschiebung einstellen kann.
	/// </summary>
	public sealed class SliderItem : MenuItem
	{
		#region Properties

		/// <summary>
		/// Der aktuelle Wert.
		/// </summary>
		public int Value
		{
			get { return _value; }
			set { if (_value != value) { _value = value; OnValueChanged (); } }
		}

		private int _value;

		/// <summary>
		/// Der minimale Wert.
		/// </summary>
		public int MinValue { get; set; }

		/// <summary>
		/// Der maximale Wert.
		/// </summary>
		public int MaxValue { get; set; }

		/// <summary>
		/// Schrittweite zwischen zwei einstellbaren Werten.
		/// </summary>
		//public int Step { get; set; }

		/// <summary>
		/// Wird aufgerufen, wenn der Wert geändert wurde
		/// </summary>
		public Action OnValueChanged = () => {};

		/// <summary>
		/// Die Breite des Rechtecks, abhängig von der Auflösung des Viewports.
		/// </summary>
		private float SliderRectangleWidth
		{
			get {
				return new Vector2(0, 0.020f).Scale(Screen.Viewport).Y;
			}
		}
		/// <summary>
		/// Die geringste X-Position des Rechtecks (so weit links wie möglich), abhängig von der Auflösung des Viewports.
		/// </summary>
		private float SliderRectangleMinX
		{
			get {
				return ValueBounds ().X + SliderRectangleWidth/2;
			}
		}
		/// <summary>
		/// Die höchste X-Position des Rechtecks (so weit rechts wie möglich), abhängig von der Auflösung des Viewports.
		/// </summary>
		private float SliderRectangleMaxX
		{
			get {
				return SliderRectangleMinX + ValueBounds ().Width - SliderRectangleWidth/2;
			}
		}
		/// <summary>
		/// Die Position und Größe des Rechtecks.
		/// </summary>
		private Rectangle SliderRectangle
		{
			get {
				Rectangle valueBounds = ValueBounds ();
				Rectangle rect = new Rectangle();
				rect.Height = valueBounds.Height;
				rect.Width = (int)SliderRectangleWidth;
				rect.Y = valueBounds.Y;
				rect.X = (int)(SliderRectangleMinX + (SliderRectangleMaxX-SliderRectangleMinX)
				               * (Value-MinValue) / (MaxValue-MinValue) - rect.Width/2);
				return rect;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines SliderItem-Objekts und initialisiert diese
		/// mit dem zugehörigen IGameScreen-Objekt. Zudem ist die Angabe der Zeichenreihenfolge,
		/// einem minimalen einstellbaren Wert, einem maximalen einstellbaren Wert und einem Standardwert Pflicht.
		/// </summary>
		public SliderItem (IGameScreen screen, DisplayLayer drawOrder, string text, int max, int min, int step, int value)
		: base(screen, drawOrder, text)
		{
			MaxValue = max;
			MinValue = min;
			//Step = step;
			_value = value;
		}

		#endregion

		#region Methods

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			Rectangle valueBounds = ValueBounds ();

			int lineWidth = valueBounds.Width;
			int lineHeight = 2;

			Texture2D lineTexture = new Texture2D (Screen.Device, lineWidth, lineHeight);
			Texture2D rectangleTexture = new Texture2D (Screen.Device, 1, 1);

			Color[] dataLine = new Color[lineWidth * lineHeight];
			for (int i = 0; i < dataLine.Length; ++i) {
				dataLine [i] = Color.White;
			}
			lineTexture.SetData (dataLine);

			Color[] dataRec = new Color[1];
			dataRec [0] = Lines.LineColor;
			rectangleTexture.SetData (dataRec);

			Vector2 coordinateLine = new Vector2(valueBounds.X, valueBounds.Y + ScaledSize.Y / 2);

			spriteBatch.Begin ();

			spriteBatch.Draw (lineTexture, coordinateLine, Color.White);
			spriteBatch.Draw (rectangleTexture, SliderRectangle, Lines.LineColor);

			spriteBatch.End ();
		}

		public override void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			float mousePositionX = position.X.Clamp(SliderRectangleMinX, SliderRectangleMaxX);
			float percent = (mousePositionX - SliderRectangleMinX)/(SliderRectangleMaxX-SliderRectangleMinX);
			Value = (int)(MinValue + percent * (MaxValue-MinValue));
		}

		public override void Update (GameTime gameTime)
		{
			if (this.ItemState == ItemState.Hovered && InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed) {
				Vector2 position = new Vector2 (InputManager.CurrentMouseState.X, InputManager.CurrentMouseState.Y);
				OnLeftClick (position, ClickState.SingleClick, gameTime);
			}
		}

		#endregion
	}
}

