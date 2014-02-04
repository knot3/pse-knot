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
	public class ColorPickDialog : Dialog
	{
		#region Properties

		/// <summary>
		/// Die ausgewählte Farbe.
		/// </summary>
		public Color SelectedColor { get; private set; }

		private ColorPicker colorPicker;

		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues ConfirmDialog-Objekt und initialisiert dieses mit dem zugehörigen IGameScreen-Objekt.
		/// Zudem sind Angaben zur Zeichenreihenfolge, einer Zeichenkette für den Titel und für den eingeblendeten Text Pflicht.
		/// [base=screen, drawOrder, title, text]
		/// </summary>
		public ColorPickDialog (IGameScreen screen, DisplayLayer drawOrder, Color selectedColor)
		: base(screen, drawOrder, "Select a color", String.Empty)
		{
			// Die ausgewählte Farbe
			SelectedColor = selectedColor;

			// Der Titel-Text ist mittig ausgerichtet
			AlignX = HorizontalAlignment.Center;

			// Der Colorpicker
			colorPicker = new ColorPicker (Screen, Index + DisplayLayer.MenuItem, selectedColor);
			colorPicker.Bounds.Position = ContentBounds.Position;
			colorPicker.ColorSelected += OnColorSelected;
			//TODO
			//RelativeContentSize = colorPicker.RelativeSize ();

			// Diese Tasten werden akzeptiert
			ValidKeys.AddRange (new Keys[] { Keys.Escape });
		}

		#endregion

		#region Methods

		/// <summary>
		///
		/// </summary>
		public override void OnKeyEvent (List<Keys> key, KeyEvent keyEvent, GameTime time)
		{
			if (keyEvent == KeyEvent.KeyDown) {
				if (key.Contains (Keys.Escape)) {
					Close (time);
				}
			}
			base.OnKeyEvent (key, keyEvent, time);
		}

		private void OnColorSelected (Color obj, GameTime time)
		{
			SelectedColor = colorPicker.SelectedColor;
			Close (time);
		}

		public override IEnumerable<IGameScreenComponent> SubComponents (GameTime time)
		{
			foreach (DrawableGameScreenComponent component in base.SubComponents(time)) {
				yield return component;
			}
			yield return colorPicker;
		}

		#endregion
	}
}
