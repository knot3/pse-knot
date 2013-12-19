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

	public class DropDownMenuItem : MenuItem
	{
		private VerticalMenu dropdown
		{
			get;
			set;
		}

		public virtual IEnumerable<DropDownEntry> DropDownEntry
		{
			get;
			set;
		}

		public virtual void AddEntries(DistinctOptionInfo option)
		{
			throw new System.NotImplementedException();
		}

		public virtual void AddEntries(DropDownEntry enties)
		{
			throw new System.NotImplementedException();
		}

		public DropDownMenuItem(GameScreen screen, DisplayLayer drawOrder)
		{
		}

	}
}

