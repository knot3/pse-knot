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
	/// Ein Steuerelement der grafischen Benutzeroberfläche, das eine Auswahl von Farben ermöglicht.
	/// </summary>
	public sealed class ColorPicker : Widget, IKeyEventListener, IMouseClickEventListener
	{
		#region Properties

		/// <summary>
		/// Die ausgewählte Farbe.
		/// </summary>
		public Color SelectedColor { get; set; }

		public Action<Color, GameTime> ColorSelected { get; set; }

		private List<Color> colors;
		private List<Vector2> tiles;
		private static readonly Vector2 tileSize = new Vector2 (0.032f, 0.032f);
		private SpriteBatch spriteBatch;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt eine neue Instanz eines ColorPicker-Objekts und initialisiert diese
		/// mit der Farbe, auf welche der Farbwähler beim Aufruf aus Sicht des Spielers zeigt.
		/// </summary>
		public ColorPicker (IGameScreen screen, DisplayLayer drawOrder, Color def)
		: base(screen, drawOrder)
		{
			// Widget-Attribute
			BackgroundColor = () => Color.Black;
			ForegroundColor = () => Color.White;
			AlignX = HorizontalAlignment.Left;
			AlignY = VerticalAlignment.Top;

			// die Farb-Tiles
			colors = new List<Color> (CreateColors (64));
			colors.Sort (ColorHelper.SortColorsByLuminance);
			tiles = new List<Vector2> (CreateTiles (colors));

			// einen Spritebatch
			spriteBatch = new SpriteBatch (screen.Device);

			// Position und Größe
			RelativePosition = () => (Vector2.One - RelativeSize ()) / 2;
			RelativeSize = () => {
				float sqrt = (float)Math.Ceiling (Math.Sqrt (colors.Count));
				return tileSize * sqrt;
			};

			// Die Callback-Funktion zur Selektion setzt das SelectedColor-Attribut
			ColorSelected += (color, time) => {
				SelectedColor = color;
			};
		}

		#endregion

		#region Methods

		public override void Draw (GameTime time)
		{
			if (IsVisible) {
				spriteBatch.Begin ();

				// color tiles
				int i = 0;
				foreach (Vector2 tile in tiles) {
					Vector2 topLeft = ScaledPosition + tile.Scale (Screen.Viewport);
					Vector2 size = tileSize.Scale (Screen.Viewport);
					Rectangle rect = topLeft.CreateRectangle (size).Shrink (1);
					Texture2D dummyTexture = TextureHelper.Create (Screen.Device, colors [i]);
					spriteBatch.Draw (dummyTexture, rect, Color.White);

					++i;
				}

				spriteBatch.End ();
			}
		}

		/// <summary>
		/// Reagiert auf Tastatureingaben.
		/// </summary>
		public void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
		}

		/// <summary>
		/// Bei einem Linksklick wird eine Farbe ausgewählt und im Attribut Color abgespeichert.
		/// </summary>
		public void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			position = position.RelativeTo (Screen.Viewport);
			Console.WriteLine ("ColorPicker.OnLeftClick: positon=" + position);
			int i = 0;
			foreach (Vector2 tile in tiles) {
				Console.WriteLine ("ColorPicker: tile=" + tile + "  "
				                   + (tile.X <= position.X) + " " + (tile.X + tileSize.X > position.X) + " " + (
				                       tile.Y <= position.Y) + " " + (tile.Y + tileSize.Y > position.Y)
				                  );
				if (tile.X <= position.X && tile.X + tileSize.X > position.X
				        && tile.Y <= position.Y && tile.Y + tileSize.Y > position.Y) {
					Console.WriteLine ("ColorPicker: color=" + colors [i]);

					ColorSelected (colors [i], time);
				}
				++i;
			}
		}

		/// <summary>
		/// Bei einem Rechtsklick geschieht nichts.
		/// </summary>
		public void OnRightClick (Vector2 position, ClickState state, GameTime time)
		{
		}

		public void SetHovered (bool hovered)
		{
		}

		private static IEnumerable<Color> CreateColors (int num)
		{
			float steps = (float)Math.Pow (num, 1.0 / 3.0);
			int n = 0;
			for (int r = 0; r < steps; ++r) {
				for (int g = 0; g < steps; ++g) {
					for (int b = 0; b < steps; ++b) {
						yield return new Color (new Vector3 (r, g, b) / steps);
						++n;
					}
				}
			}
		}

		private static IEnumerable<Vector2> CreateTiles (IEnumerable<Color> _colors)
		{
			Color[] colors = _colors.ToArray ();
			float sqrt = (float)Math.Sqrt (colors.Count ());
			int row = 0;
			int column = 0;
			foreach (Color color in colors) {
				yield return new Vector2 (tileSize.X * column, tileSize.Y * row);

				++column;
				if (column >= sqrt) {
					column = 0;
					++row;
				}
			}
		}

		#endregion
	}
}

