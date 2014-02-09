using System;

using Microsoft.Xna.Framework;

using Knot3.Widgets;

namespace Knot3.Utilities
{
	public sealed class HfGDesign : IDesign
	{
		public HfGDesign ()
		{
		}

		public void Apply ()
		{
			Design.MenuFontName = "font-menu";
			Design.DefaultLineColor = new Color (0xb4, 0xff, 0x00);
			Design.DefaultOutlineColor = new Color (0x3b, 0x54, 0x00);
			Design.WidgetBackground = Color.Black;
			Design.WidgetForeground = Color.White;
			Design.InGameBackground = Color.Black;
			Design.ScreenBackground = Color.Black;
			Design.WidgetBackgroundColorFunc = WidgetBackgroundColor;
			Design.WidgetForegroundColorFunc = WidgetForegroundColor;
			Design.MenuItemBackgroundColorFunc = MenuItemBackgroundColor;
			Design.MenuItemForegroundColorFunc = MenuItemForegroundColor;
		}

		private static Color WidgetBackgroundColor (State state)
		{
			if (state == State.None || state == State.Hovered) {
				return Color.Transparent;
			}
			else if (state == State.Selected) {
				return Color.Black;
			}
			else {
				return Color.CornflowerBlue;
			}
		}

		private static Color WidgetForegroundColor (State state)
		{
			if (state == State.Hovered) {
				return Color.White;
			}
			else if (state == State.None) {
				return Color.White * 0.7f;
			}
			else if (state == State.Selected) {
				return Color.White;
			}
			else {
				return Color.CornflowerBlue;
			}
		}

		private Color MenuItemBackgroundColor (State state)
		{
			return Color.Transparent;
		}

		private Color MenuItemForegroundColor (State state)
		{
			if (state == State.Hovered) {
				return Color.White;
			}
			else {
				return Color.White * 0.7f;
			}
		}
	}
}
