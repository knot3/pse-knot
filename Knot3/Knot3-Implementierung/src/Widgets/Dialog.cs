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

	public abstract class Dialog : Widget, IKeyEventListener, IMouseEventListener
	{
		public virtual string Title
		{
			get;
			set;
		}

		public virtual string Text
		{
			get;
			set;
		}

		public virtual void OnKeyEvent()
		{
			throw new System.NotImplementedException();
		}

		public virtual Rectangle Bounds()
		{
			throw new System.NotImplementedException();
		}

		public virtual void OnLeftClick(Vector2 position, ClickState state, GameTime time)
		{
			throw new System.NotImplementedException();
		}

		public virtual void OnRightClick(Vector2 position, ClickState state, GameTime time)
		{
			throw new System.NotImplementedException();
		}

		public Dialog(GameScreen screen, DisplayLayer drawOrder, string title, string text)
		{
		}

	}
}

