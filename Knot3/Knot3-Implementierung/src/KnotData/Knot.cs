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

	public class Knot : ICloneable, IEnumerable<Edge>, IEquatable<Knot>
	{
		public virtual string Name
		{
			get;
			set;
		}

		private Circle<T> edges
		{
			get;
			private set;
		}

		public virtual KnotMetaData MetaData
		{
			get;
			private set;
		}

		public virtual Action EdgesChanged
		{
			get;
			set;
		}

		public virtual IEnumerable<Edge> SelectedEdges
		{
			get;
			private set;
		}

		public virtual KnotMetaData KnotInfo
		{
			get;
			set;
		}

		public virtual Circle<T> Circle
		{
			get;
			set;
		}

		public Knot()
		{
		}

		public Knot(KnotMetaData meta, IEnumerable<Edge> edges)
		{
		}

		public virtual bool IsValidMove(Direction dir, int distance)
		{
			throw new System.NotImplementedException();
		}

		public virtual bool Move(Direction dir, int distance)
		{
			throw new System.NotImplementedException();
		}

		public virtual IEnumerator<Edge> GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		public virtual void Save()
		{
			throw new System.NotImplementedException();
		}

		public virtual Object Clone()
		{
			throw new System.NotImplementedException();
		}

		public virtual void AddToSelection(Edge edge)
		{
			throw new System.NotImplementedException();
		}

		public virtual void RemoveFromSelection(Edge edge)
		{
			throw new System.NotImplementedException();
		}

		public virtual void ClearSelection()
		{
			throw new System.NotImplementedException();
		}

		public virtual void AddRangeToSelection(Edge edge)
		{
			throw new System.NotImplementedException();
		}

		public virtual bool IsSelected(Edge edge)
		{
			throw new System.NotImplementedException();
		}

		public virtual IEnumerator GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		public virtual void Save(IKnotIO format, string filename)
		{
			throw new System.NotImplementedException();
		}

		public virtual bool Equals(T other)
		{
			throw new System.NotImplementedException();
		}

	}
}

