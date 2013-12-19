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

	public class KnotMetaData
	{
		public virtual string Name
		{
			get;
			set;
		}

		public virtual IKnotIO Format
		{
			get;
			private set;
		}

		public virtual Func<Integer> CountEdges
		{
			get;
			private set;
		}

		public virtual string Filename
		{
			get;
			private set;
		}

		public KnotMetaData(string name, Func<Integer> countEdges, IKnotIO format, string filename)
		{
		}

		public KnotMetaData(string name, Func<Integer> countEdges)
		{
		}

	}
}

