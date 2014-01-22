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
		public int Step { get; set; }

		public Action OnValueChanged = () => {};
		private float minXSliderRectangle = -1.0f;
		private float maxXSliderRectangle = -1.0f;
		private Vector2 coordinateRec;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines SliderItem-Objekts und initialisiert diese
		/// mit dem zugehörigen GameScreen-Objekt. Zudem ist die Angabe der Zeichenreihenfolge,
		/// einem minimalen einstellbaren Wert, einem maximalen einstellbaren Wert und einem Standardwert Pflicht.
		/// </summary>
		public SliderItem (GameScreen screen, DisplayLayer drawOrder, string text, int max, int min, int step, int value)
		: base(screen, drawOrder, text)
		{
			MaxValue = max;
			MinValue = min;
			Step = step;
			_value = value;
		}

		#endregion

		#region Methods

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			spriteBatch.Begin ();

			int lineWidth = 300;
			int lineHeight = 2;

			int rectangleWidth = 20;
			int rectangleHeight = (int)ScaledSize.Y;

			Texture2D line = new Texture2D (Screen.Device, lineWidth, lineHeight);
			Texture2D rectangle = new Texture2D (Screen.Device, rectangleWidth, rectangleHeight);

			Color[] dataLine = new Color[lineWidth * lineHeight];
			for (int i = 0; i < dataLine.Length; ++i) {
				dataLine [i] = Color.White;
			}
			line.SetData (dataLine);

			Color[] dataRec = new Color[rectangleWidth * rectangleHeight];
			for (int i = 0; i < dataRec.Length; ++i) {
				dataRec [i] = Color.YellowGreen;
			}
			rectangle.SetData (dataRec);

			Vector2 coordinateLine = ScaledPosition;
			coordinateLine.X += ScaledSize.X / 2;
			coordinateLine.Y += ScaledSize.Y / 2;

			if (minXSliderRectangle < 0) {
				coordinateRec = ScaledPosition;
				coordinateRec.X += ScaledSize.X / 2 + (Value / Step) * (280 / (MaxValue / Step));
				minXSliderRectangle = coordinateLine.X;
				maxXSliderRectangle = coordinateLine.X + 280;

			}

			spriteBatch.Draw (line, coordinateLine, Color.White);
			spriteBatch.Draw (rectangle, coordinateRec, Color.YellowGreen);

			spriteBatch.End ();
		}

		public override void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			Vector2 mousePosition = position;
			Console.WriteLine ("" + mousePosition.X + " rect " + coordinateRec.X);
			if (mousePosition.X > minXSliderRectangle && mousePosition.X < minXSliderRectangle + 300) {
				coordinateRec.X = mousePosition.X - 10.0f;
				if (coordinateRec.X < minXSliderRectangle) {
					coordinateRec.X = minXSliderRectangle;
				}
				else if (coordinateRec.X > maxXSliderRectangle) {
					coordinateRec.X = maxXSliderRectangle;
				}
				Value = ((int)coordinateRec.X - (int)this.minXSliderRectangle)
					/ (280 / (this.MaxValue / this.Step))
					* this.Step;
			}
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

