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


namespace KnotData
{

	public class Edge
	{
		public virtual Color Color
		{
			get;
			set;
		}

		public virtual Direction Direction
		{
			get;
			private set;
		}

		public virtual List<int> Rectangles
		{
			get;
			set;
		}

		public Edge(Direction direction)
		{
		}

		public virtual Vector3 Get3DDirection()
		{
			throw new System.NotImplementedException();
		}

	}
}

