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

namespace Knot3.Widgets
{
	/// <summary>
	/// Eine Wertesammlung der möglichen Klickzustände einer Maustaste.
	/// </summary>
	public enum ClickState {
		/// <summary>
		/// Wenn der Klickzustand nicht zugeordnet werden konnte. Undefiniert.
		/// </summary>
		None=0,
		/// <summary>
		/// Ein Einzelklick.
		/// </summary>
		SingleClick=1,
		/// <summary>
		/// Ein Doppelklick.
		/// </summary>
		DoubleClick=2,
	}
}
