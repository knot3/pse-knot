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
	/// Ein Menüeintrag, der den ausgewählten Wert anzeigt und bei einem Linksklick ein Dropdown-Menü zur Auswahl eines neuen Wertes ein- oder ausblendet.
	/// </summary>
	public sealed class DropDownMenuItem : MenuItem
	{
		#region Properties

		/// <summary>
		/// Das Dropdown-Menü, das ein- und ausgeblendet werden kann.
		/// </summary>
		private VerticalMenu dropdown { get; set; }

		private InputItem currentValue;

		public override bool IsVisible
		{
			get { return base.IsVisible; }
			set {
				base.IsVisible = value;
				if (currentValue != null)
					currentValue.IsVisible = value;
			}
		}


		#endregion

		#region Constructors

		/// <summary>
		/// Erzeugt ein neues ConfirmDialog-Objekt und initialisiert dieses mit dem zugehörigen GameScreen-Objekt.
		/// Zudem ist die Angabe der Zeichenreihenfolge Pflicht.
		/// </summary>
		public DropDownMenuItem (GameScreen screen, DisplayLayer drawOrder, string text)
		: base(screen, drawOrder, "")
		{
			dropdown = new VerticalMenu (screen: screen, drawOrder: DisplayLayer.SubMenu);
			dropdown.RelativePosition = () => RelativePosition () + new Vector2 (x: ValueWidth * RelativeSize ().X, y: 0);
			dropdown.RelativeSize = () => new Vector2 (x: ValueWidth * RelativeSize ().X, y: RelativeSize ().Y * 10);
			dropdown.RelativePadding = () => new Vector2 (0.010f, 0.010f);
			dropdown.ItemForegroundColor = (i) => Menu.ItemForegroundColor (i);
			dropdown.ItemBackgroundColor = (i) => Menu.ItemBackgroundColor (i);
			dropdown.ItemAlignX = HorizontalAlignment.Left;
			dropdown.ItemAlignY = VerticalAlignment.Center;
			dropdown.IsVisible = false;

			currentValue = new InputItem (screen: screen, drawOrder: DisplayLayer.SubMenu, text: text, inputText: "");
			currentValue.RelativePosition = () => RelativePosition ();
			currentValue.RelativeSize = () => RelativeSize ();
			currentValue.RelativePadding = () => RelativePadding ();
			currentValue.ForegroundColor = () => ForegroundColor ();
			currentValue.BackgroundColor = () => Color.Transparent;
			currentValue.IsVisible = IsVisible;
			currentValue.IsMouseEventEnabled = false;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Fügt Einträge in das Dropdown-Menü ein, die auf Einstellungsoptionen basieren.
		/// </summary>
		public void AddEntries (DistinctOptionInfo option)
		{
			foreach (string _value in option.ValidValues) {
				string value = _value; // create a copy for the action
				Action onSelected = () => {
					Console.WriteLine ("OnClick: " + value);
					option.Value = value;
					currentValue.InputText = value;
					dropdown.IsVisible = false;
				};
				MenuButton button = new MenuButton (
				    screen: Screen,
				    drawOrder: DisplayLayer.SubMenuItem,
				    name: value,
				    onClick: onSelected
				);
				dropdown.Add (button);
			}
			currentValue.InputText = option.Value;
		}

		/// <summary>
		/// Fügt Einträge in das Dropdown-Menü ein, die nicht auf Einstellungsoptionen basieren.
		/// </summary>
		public void AddEntries (DropDownEntry enties)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Reaktionen auf einen Linksklick.
		/// </summary>
		public override void OnLeftClick (Vector2 position, ClickState state, GameTime time)
		{
			onClick ();
		}

		private void onClick ()
		{
			bool newValue = !dropdown.IsVisible;
			Menu.Collapse ();
			dropdown.IsVisible = newValue;
		}

		public override void Collapse ()
		{
			dropdown.IsVisible = false;
		}

		public override IEnumerable<IGameScreenComponent> SubComponents (GameTime time)
		{
			foreach (DrawableGameScreenComponent component in base.SubComponents(time)) {
				yield return component;
			}
			yield return dropdown;
			yield return currentValue;
		}

		public override void Draw (GameTime time)
		{
			base.Draw (time);

			// Wenn das DropDownMenuItem sichtbar ist und das Dropdown-Menü nicht...
			if (IsVisible && !dropdown.IsVisible) {
				// lade die Schrift
				SpriteFont font = HfGDesign.MenuFont (Screen);
			}
		}

		#endregion
	}
}

