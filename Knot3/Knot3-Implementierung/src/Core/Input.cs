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


namespace Core
{

	public class Input : GameScreenComponent
	{
		public virtual ClickState RightMouseButton
		{
			get;
			set;
		}

		public virtual ClickState LeftMouseButton
		{
			get;
			set;
		}

		public virtual MouseState CurrentMouseState
		{
			get;
			set;
		}

		public virtual KeyboardState CurrentKeyboardState
		{
			get;
			set;
		}

		public virtual MouseState PreviousMouseState
		{
			get;
			set;
		}

		public virtual KeyboardState PreviousKeyboardState
		{
			get;
			set;
		}

		public virtual bool GrabMouseMovement
		{
			get;
			set;
		}

		public virtual IEnumerable<ClickState> ClickState
		{
			get;
			set;
		}

		public Input(GameScreen screen)
		{
		}

		public virtual void Update(GameTime time)
		{
			throw new System.NotImplementedException();
		}

	}
}

