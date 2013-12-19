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


namespace GameObjects
{
	using KnotData;

	public class PipeModelInfo : GameModelInfo
	{
		public virtual Edge Edge
		{
			get;
			set;
		}

		public virtual Knot Knot
		{
			get;
			set;
		}

		public virtual Vector3 PositionFrom
		{
			get;
			set;
		}

		public virtual Vector3 PositionTo
		{
			get;
			set;
		}

		public PipeModelInfo(Knot knot, Edge edge)
		{
		}

	}
}

