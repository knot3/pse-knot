﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Wenn der Code neu generiert wird, gehen alle Änderungen an dieser Datei verloren
// </auto-generated>
//------------------------------------------------------------------------------
namespace KnotData
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

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

