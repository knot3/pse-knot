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
		public int Value { get; set; }

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
			this.MaxValue = max;
			this.MinValue = min;
			this.Step = step;
			this.Value = value;

		}

		#endregion

		#region Methods


		public override void Draw(GameTime time)
		{
			base.Draw(time);

			spriteBatch.Begin();

			int lineWidth = 300;
			int lineHeight = 2;

			int rectangleWidth = 20;
			int rectangleHeight = (int) ScaledSize.Y;


			Texture2D line = new Texture2D(Screen.Device, lineWidth, lineHeight);
			Texture2D rectangle = new Texture2D(Screen.Device, rectangleWidth, rectangleHeight);

			Color[] dataLine = new Color[lineWidth * lineHeight];
			for (int i = 0; i < dataLine.Length; ++i) {
				dataLine[i] = Color.White;
			}
			line.SetData(dataLine);

			Color[] dataRec = new Color[rectangleWidth * rectangleHeight];
			for (int i = 0; i < dataRec.Length; ++i) {
				dataRec[i] = Color.YellowGreen;
			}
			rectangle.SetData(dataRec);

			Vector2 coordinateLine = this.ScaledPosition;
			coordinateLine.X += this.ScaledSize.X / 2;
			coordinateLine.Y += this.ScaledSize.Y / 2;

			if (this.minXSliderRectangle < 0) {
				this.coordinateRec = this.ScaledPosition;
				this.coordinateRec.X += this.ScaledSize.X / 2 + (this.Value / this.Step) * (280 / (this.MaxValue/ this.Step));
				this.minXSliderRectangle = coordinateLine.X;
				this.maxXSliderRectangle = coordinateLine.X + 280;

			}



			spriteBatch.Draw(line, coordinateLine, Color.White);
			spriteBatch.Draw(rectangle, coordinateRec, Color.YellowGreen);

			spriteBatch.End();

		}

		public override void OnLeftClick(Vector2 position, ClickState state, GameTime time)
		{

			Vector2 mousePosition = position;
			Console.WriteLine("" + mousePosition.X + " rect " + coordinateRec.X);
			if (mousePosition.X > minXSliderRectangle  && mousePosition.X< minXSliderRectangle + 300) {


				this.coordinateRec.X = mousePosition.X -10.0f;
				if (this.coordinateRec.X < this.minXSliderRectangle) {
					this.coordinateRec.X = this.minXSliderRectangle;
				}
				else if (this.coordinateRec.X > this.maxXSliderRectangle) {
					this.coordinateRec.X = this.maxXSliderRectangle;
				}
                this.Value = ((int)coordinateRec.X - (int)this.minXSliderRectangle) / (280 / (this.MaxValue / this.Step)) * this.Step;
			}
		}

		public override void Update(GameTime gameTime)
		{
			if (this.ItemState == ItemState.Hovered && InputManager.CurrentMouseState.LeftButton == ButtonState.Pressed) {
				Vector2 position = new Vector2(InputManager.CurrentMouseState.X, InputManager.CurrentMouseState.Y);
				this.OnLeftClick(position, ClickState.SingleClick, gameTime);
				this.Draw(gameTime);

			}


		}
		#endregion

	}
}

