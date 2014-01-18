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
	public class Border : Widget
	{
		#region Properties

		public int LineWidth { get; set; }

		private Lines lines;
		private Vector2 lastPosition = Vector2.Zero;
		private Vector2 lastSize = Vector2.Zero;

		#endregion

		#region Constructors

		public Border (GameScreen screen, DisplayLayer drawOrder, Widget widget, int lineWidth)
		: base(screen, drawOrder)
		{
			LineWidth = lineWidth;
			lines = new Lines (screen, drawOrder, lineWidth);
			RelativePosition = () => widget.RelativePosition ();
			RelativeSize = () => widget.RelativeSize ();
		}

		#endregion

		#region Methods

		public override void Update (GameTime time)
		{
			Vector2 position = RelativePosition ();
			Vector2 size = RelativeSize ();

			if (position != lastPosition || size != lastSize) {
				lastPosition = position;
				lastSize = size;
				lines.AddPoints (position.X, position.Y, position.X + size.X, position.Y + size.Y, position.X, position.Y);
			}

			base.Update (time);
		}

		public override IEnumerable<IGameScreenComponent> SubComponents (GameTime time)
		{
			foreach (DrawableGameScreenComponent component in base.SubComponents(time)) {
				yield return component;
			}
			yield return lines;
		}

		#endregion
	}
}

