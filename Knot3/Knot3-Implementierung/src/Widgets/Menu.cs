﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Wenn der Code neu generiert wird, gehen alle Änderungen an dieser Datei verloren
// </auto-generated>
//------------------------------------------------------------------------------
namespace Widgets
{
	using Core;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

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

		public virtual IEnumerator GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		public Menu(GameScreen screen, DisplayLayer drawOrder)
		{
		}

	}
}

