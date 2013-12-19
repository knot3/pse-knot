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
	public class Node
	{
		public virtual int X
		{
			get;
			set;
		}

		public virtual int Y
		{
			get;
			set;
		}

		public virtual int Z
		{
			get;
			set;
		}

		private int scale
		{
			get;
			set;
		}

		public virtual Vector3 ToVector()
		{
			throw new System.NotImplementedException();
		}

		public Node(int x, int y, int z)
		{
		}

	}
}

