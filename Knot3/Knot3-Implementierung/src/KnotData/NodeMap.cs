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
	public class NodeMap
	{
		public virtual Node From(Edge edge)
		{
			throw new System.NotImplementedException();
		}

		public virtual Node To(Edge edge)
		{
			throw new System.NotImplementedException();
		}

		public virtual void OnEdgesChanged()
		{
			throw new System.NotImplementedException();
		}

	}
}

