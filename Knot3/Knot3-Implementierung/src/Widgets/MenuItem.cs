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
	/// Kompositum
	/// 
	/// </remarks>
	public abstract class MenuItem : Widget, IKeyEventListener, IMouseEventListener
	{
		public virtual ItemState ItemState
		{
			get;
			set;
		}

		public virtual int ItemOrder
		{
			get;
			set;
		}

		public virtual string Text
		{
			get;
			set;
		}

		public abstract void OnLeftClick(Vector2 position, ClickState state, GameTime time);

		public abstract void OnRightClick(Vector2 position, ClickState state, GameTime time);

		public abstract void OnKeyEvent();

		public abstract Rectangle Bounds();

	}
}

