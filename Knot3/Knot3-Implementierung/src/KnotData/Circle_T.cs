using System;
using System.Collections;
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
    using System;
    using System.Collections;
    using System.Collections.Generic;

	public class Circle<T> : IEnumerable<T>
	{
		public virtual T Content
		{
			get;
			set;
		}

		public virtual Circle<T> Next
		{
			get;
			set;
		}

		public virtual Circle<T> Previous
		{
			get;
			set;
		}

		public virtual Edge Edge
		{
			get;
			set;
		}


		public Circle(T content)
		{
		}

		public virtual void InsertAfter(T next)
		{
			throw new System.NotImplementedException();
		}

		public virtual void InsertBefore(T previous)
		{
			throw new System.NotImplementedException();
		}

		public virtual void Remove()
		{
			throw new System.NotImplementedException();
		}

		public virtual IEnumerator<T> GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

	}
}

