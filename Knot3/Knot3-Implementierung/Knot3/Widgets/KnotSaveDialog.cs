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
	/// <summary>
	///
	/// </summary>
	public sealed class KnotSaveDialog : TextInputDialog
	{
		#region Properties

		/// <summary>
		///
		/// </summary>
		public Action OnSave { get; set; }

		public Knot Knot { get; private set; }

		#endregion

		#region Constructors

		public KnotSaveDialog (GameScreen screen, DisplayLayer drawOrder, Knot knot, Action onSave)
		: base(screen, drawOrder, "Save Knot", "fuck you", knot != null ? knot.Name : "Untitled Knot")
		{
			OnSave = onSave;
			Knot = knot;
		}

		#endregion

		#region Methods

		#endregion
	}
}

