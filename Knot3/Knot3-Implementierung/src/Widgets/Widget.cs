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

