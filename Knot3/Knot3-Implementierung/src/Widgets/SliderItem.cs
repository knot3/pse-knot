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

	public class SliderItem : MenuItem
	{
		public virtual int Value
		{
			get;
			set;
		}

		public virtual int MinValue
		{
			get;
			set;
		}

		public virtual int MaxValue
		{
			get;
			set;
		}

		public virtual int Step
		{
			get;
			set;
		}

        public SliderItem(GameScreen screen, DisplayLayer drawOrder, int max, int min, int step, int value)
		{
			throw new System.NotImplementedException();
		}

	}
}

