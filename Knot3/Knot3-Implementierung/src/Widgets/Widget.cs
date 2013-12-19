using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Widgets
{
	using Core;

	/// <remarks>
	/// Komponente
	/// 
	/// </remarks>
	public abstract class Widget : DrawableGameScreenComponent
	{
		public virtual Vector2 RelativeSize
		{
			get;
			set;
		}

		public virtual Vector2 RelativePosition
		{
			get;
			set;
		}

		public virtual bool IsVisible
		{
			get;
			set;
		}

		public virtual Func<Color> BackgroundColor
		{
			get;
			set;
		}

		public virtual Func<Color> ForegroundColor
		{
			get;
			set;
		}

		public virtual HorizontalAlignment AlignX
		{
			get;
			set;
		}

		public virtual VerticalAlignment AlignY
		{
			get;
			set;
		}

		public virtual HorizontalAlignment HorizontalAlignment
		{
			get;
			set;
		}

		public virtual VerticalAlignment VerticalAlignment
		{
			get;
			set;
		}

		public virtual Rectangle BoundingBox()
		{
			throw new System.NotImplementedException();
		}

		public Widget(GameScreen screen, DisplayLayer drawOrder)
		{
		}

	}
}

