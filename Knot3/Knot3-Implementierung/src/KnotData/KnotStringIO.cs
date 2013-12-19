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

	public class KnotStringIO
	{
		public virtual string Name
		{
			get;
			set;
		}

		public virtual IEnumerable<Edge> Edges
		{
			get;
			set;
		}

		public virtual int CountEdges
		{
			get;
			private set;
		}

		public virtual string Content
		{
			get;
			set;
		}

		public KnotStringIO(string content)
		{
		}

		public KnotStringIO(Knot knot)
		{
		}

	}
}

