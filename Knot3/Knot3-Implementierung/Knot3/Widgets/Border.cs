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

		public int Padding { get; set; }

		private Lines lines;
		private Vector2 lastPosition = Vector2.Zero;
		private Vector2 lastSize = Vector2.Zero;

		public override bool IsEnabled
		{
			get {
				return base.IsEnabled;
			}
			set {
				base.IsEnabled = value;
				lines.IsEnabled = value;
			}
		}

		#endregion

		#region Constructors

		public Border (IGameScreen screen, DisplayLayer drawOrder, Widget widget, int lineWidth = 2, int padding = 0)
		: base(screen, drawOrder)
		{
			LineWidth = lineWidth;
			Padding = padding;
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
				Vector2 padding = Vector2.One*0.001f*Padding;
				lines.AddPoints (
				    position.X - padding.X,
				    position.Y - padding.Y,
				    position.X + size.X + padding.X,
				    position.Y + size.Y + padding.Y,
				    position.X - padding.X,
				    position.Y - padding.Y
				);
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

