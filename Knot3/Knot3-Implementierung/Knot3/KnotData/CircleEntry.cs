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
	public class CircleEntry<T> : IEnumerable<T>
	{
		public T Value { get; set; }

		public CircleEntry<T> Next { get; set; }

		public CircleEntry<T> Previous { get; set; }

		public CircleEntry (T value)
		{
			Value = value;
			Previous = this;
			Next = this;
		}

		public CircleEntry(IEnumerable<T> list)
		{
			bool first = true;
			CircleEntry<T> inserted = this;
			foreach (T obj in list) {
				if (first) {
					Value = obj;
					Previous = this;
					Next = this;
				}
				else {
					inserted = inserted.InsertAfter (obj);
				}
				first = false;
			}
		}

		public CircleEntry<T> InsertBefore (T obj)
		{
			CircleEntry<T> insert = new CircleEntry<T> (obj);
			insert.Previous = this.Previous;
			insert.Next = this;
			this.Previous.Next = insert;
			this.Previous = insert;
			return insert;
		}

		public CircleEntry<T> InsertAfter (T obj)
		{
			//Log.Debug (this + ".InsertAfter(" + obj + ")");
			CircleEntry<T> insert = new CircleEntry<T> (obj);
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
				CircleEntry<T> current = this;
				int count = 0;
				do {
					++count;
					current = current.Next;
				}
				while (current != this);
				return count;
			}
		}

		public bool Contains (T obj, out IEnumerable<CircleEntry<T>> item)
		{
			item = Find (obj);
			return item.Count () > 0;
		}

		public bool Contains (Func<T, bool> func, out IEnumerable<CircleEntry<T>> item)
		{
			item = Find (func);
			return item.Count () > 0;
		}

		public bool Contains (T obj, out CircleEntry<T> item)
		{
			item = Find (obj).ElementAtOrDefault (0);
			return item != null;
		}

		public bool Contains (Func<T, bool> func, out CircleEntry<T> item)
		{
			item = Find (func).ElementAtOrDefault (0);
			return item != null;
		}

		public IEnumerable<CircleEntry<T>> Find (T obj)
		{
			return Find ((t) => t.Equals (obj));
		}

		public IEnumerable<CircleEntry<T>> Find (Func<T, bool> func)
		{
			CircleEntry<T> current = this;
			do {
				if (func (current.Value)) {
					yield return current;
				}
				current = current.Next;
			}
			while (current != this);
			yield break;
		}

		public IEnumerable<T> RangeTo (CircleEntry<T> other)
		{
			CircleEntry<T> current = this;
			do {
				yield return current.Value;
				current = current.Next;
			}
			while (current != other.Next && current != this);
		}

		public IEnumerator<T> GetEnumerator ()
		{
			CircleEntry<T> current = this;
			do {
				//Log.Debug (this + " => " + current.Content);
				yield return current.Value;
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
			return "CircleEntry(" + Value + ")";
		}

		public static CircleEntry<T> operator + (CircleEntry<T> circle, int i)
		{
			CircleEntry<T> next = circle;
			while (i > 0) {
				next = next.Next;
				i--;
			}
			while (i < 0) {
				next = next.Previous;
				i++;
			}
			return next;
		}

		public T this [int index]
		{
			get {
				return (this + index).Value;
			}
		}

		public static CircleEntry<T> operator - (CircleEntry<T> circle, int i)
		{
			return circle + (-i);
		}

		public static CircleEntry<T> operator ++ (CircleEntry<T> circle)
		{
			return circle.Next;
		}

		public static CircleEntry<T> operator -- (CircleEntry<T> circle)
		{
			return circle.Previous;
		}

		public static implicit operator T (CircleEntry<T> circle)
		{
			return circle.Value;
		}
	}
}
