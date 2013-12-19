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
	public class Menu : Widget, IEnumerable<MenuItem>
	{
		public virtual Func<int, Vector2> RelativeItemSize
		{
			get;
			set;
		}

		public virtual Func<int, Vector2> RelativeItemPosition
		{
			get;
			set;
		}

		public virtual Func<ItemState, Color> ItemForegroundColor
		{
			get;
			set;
		}

		public virtual Func<ItemState, Color> ItemBackgroundColor
		{
			get;
			set;
		}

		public virtual HorizontalAlignment ItemAlignX
		{
			get;
			set;
		}

		public virtual VerticalAlignment ItemAlignY
		{
			get;
			set;
		}

		public override VerticalAlignment VerticalAlignment
		{
			get;
			set;
		}

		public override HorizontalAlignment HorizontalAlignment
		{
			get;
			set;
		}

		public virtual void Add(MenuItem item)
		{
			throw new System.NotImplementedException();
		}

		public virtual void Delete(MenuItem item)
		{
			throw new System.NotImplementedException();
		}

		public virtual MenuItem GetItem(int i)
		{
			throw new System.NotImplementedException();
		}

		public virtual int Size()
		{
			throw new System.NotImplementedException();
		}

		public virtual IEnumerator<MenuItem> GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		public Menu(GameScreen screen, DisplayLayer drawOrder)
		{
		}

	}
}

