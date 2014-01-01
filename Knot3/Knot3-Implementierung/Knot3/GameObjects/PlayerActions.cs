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
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;
using System.ComponentModel;
using Knot3.Utilities;

namespace Knot3.GameObjects
{
	/// <summary>
	/// Die Aktionen, für die der Spieler die zugewiesene Taste festlegen kann.
	/// Sie können in den Controls-Einstellungen verändert werden.
	/// </summary>
	public enum PlayerActions
	{
		[Description("Move Up")]
		MoveUp,
		[Description("Move Down")]
		MoveDown,
		[Description("Move Left")]
		MoveLeft,
		[Description("Move Right")]
		MoveRight,
		[Description("Move Forward")]
		MoveForward,
		[Description("Move Backward")]
		MoveBackward,
		[Description("Rotate Up")]
		RotateUp,
		[Description("Rotate Down")]
		RotateDown,
		[Description("Rotate Left")]
		RotateLeft,
		[Description("Rotate Right")]
		RotateRight,
		[Description("Zoom In")]
		ZoomIn,
		[Description("Zoom Out")]
		ZoomOut,
	}
}
