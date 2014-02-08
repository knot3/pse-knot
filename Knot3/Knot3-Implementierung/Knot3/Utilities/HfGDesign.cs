using System;

using Microsoft.Xna.Framework;

namespace Knot3.Utilities
{
	public class HfGDesign : IDesign
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
		}
	}
}
