using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Knot3.Core;
using Knot3.GameObjects;
using Knot3.Screens;
using Knot3.RenderEffects;
using Knot3.Widgets;

namespace Knot3.KnotData
{
	/// <summary>
	/// Eine doppelt verkettete Liste.
	/// </summary>
	public class Circle<T> : IEnumerable<T>
	{
		public T Content { get; set; }

		public Circle<T> Next { get; set; }

		public Circle<T> Previous { get; set; }

		public Circle (T content)
		{
			Content = content;
			Previous = this;
			Next = this;
		}

		public Circle (IEnumerable<T> list)
		{
			bool first = true;
			Circle<T> inserted = this;
			foreach (T obj in list) {
				if (first) {
					Content = obj;
					Previous = this;
					Next = this;
				}
				else {
					inserted = inserted.InsertAfter (obj);
				}
				first = false;
			}
		}

		public Circle<T> InsertBefore (T obj)
		{
			Circle<T> insert = new Circle<T> (obj);
			insert.Previous = this.Previous;
			insert.Next = this;
			this.Previous.Next = insert;
			this.Previous = insert;
			return insert;
		}

		public Circle<T> InsertAfter (T obj)
		{
			//Console.WriteLine (this + ".InsertAfter(" + obj + ")");
			Circle<T> insert = new Circle<T> (obj);
			insert.Next = this.Next;
			insert.Previous = this;
			this.Next.Previous = insert;
			this.Next = insert;
			return insert;
		}

		public void Remove ()
		{
			Previous.Next = Next;
			Next.Previous = Previous;
		}

		public int Count
		{
			get {
				Circle<T> current = this;
				int count = 0;
				do {
					++count;
					current = current.Next;
				}
				while (current != this);
				return count;
			}
		}

		public bool Contains (T obj, out IEnumerable<Circle<T>> item)
		{
			item = Find (obj);
			return item.Count () > 0;
		}

		public bool Contains (Func<T, bool> func, out IEnumerable<Circle<T>> item)
		{
			item = Find (func);
			return item.Count () > 0;
		}

		public bool Contains (T obj, out Circle<T> item)
		{
			item = Find (obj).ElementAtOrDefault(0);
			return item != null;
		}

		public bool Contains (Func<T, bool> func, out Circle<T> item)
		{
			item = Find (func).ElementAtOrDefault(0);
			return item != null;
		}

		public IEnumerable<Circle<T>> Find (T obj)
		{
			return Find ((t) => t.Equals (obj));
		}

		public IEnumerable<Circle<T>> Find (Func<T, bool> func)
		{
			Circle<T> current = this;
			do {
				if (func (current.Content)) {
					yield return current;
				}
				current = current.Next;
			}
			while (current != this);
			yield break;
		}

		public IEnumerable<T> RangeTo (Circle<T> other)
		{
			Circle<T> current = this;
			do {
				yield return current.Content;
				current = current.Next;
			}
			while (current != other && current != this);
		}

		public IEnumerator<T> GetEnumerator ()
		{
			Circle<T> current = this;
			do {
				//Console.WriteLine (this + " => " + current.Content);
				yield return current.Content;
				current = current.Next;
			}
			while (current != this);
		}

		// explicit interface implementation for nongeneric interface
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator (); // just return the generic version
		}

		public override string ToString ()
		{
			return "Circle(" + Content + ")";
		}
	}
}

