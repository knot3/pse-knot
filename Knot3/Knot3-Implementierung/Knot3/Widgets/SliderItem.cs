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

            int width = 100;
            int height = 2;
            

            Texture2D line = new Texture2D(Screen.Device, width, height);

            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            line.SetData(data);


            Vector2 coordinate = this.ScaledPosition;
            coordinate.X += this.ScaledSize.X / 2;
            coordinate.Y += this.ScaledSize.Y / 2;

            spriteBatch.Draw(line, coordinate, Color.White);

			
            //Rectangle line = Bounds();
            //line.Height = 5;
            //line.Width = line.Width / 2;
            //spriteBatch.DrawColoredRectangle(Color.White, line);

			spriteBatch.End();

		}
		#endregion

	}
}

