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

using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.KnotData;
using Knot3.Widgets;

namespace Knot3.Core
{
	public interface IMouseScrollEventListener
	{
		#region Properties

		/// <summary>
		/// Die Eingabepriorität.
		/// </summary>
		DisplayLayer Index { get; }

		/// <summary>
		/// Zeigt an, ob die Klasse zur Zeit auf Mausrad-Bewegungen reagiert.
		/// </summary>
		bool IsMouseScrollEventEnabled { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Die Ausmaße des von der Klasse repräsentierten Objektes.
		/// </summary>
		Rectangle MouseScrollBounds { get; }

		/// <summary>
		/// Die Reaktion auf ein Scrollen. Der Wert ist relativ zum letzten Frame.
		/// </summary>
		void OnScroll (int scrollWheelValue);

		#endregion
	}
}

