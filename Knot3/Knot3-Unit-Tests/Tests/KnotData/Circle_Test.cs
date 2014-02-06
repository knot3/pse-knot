using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Knot3.KnotData;

namespace Knot3.UnitTests.Tests.KnotData
{
	[TestFixture]
	class Circle_Test
	{
		public Circle_Test()
		{
		}
		[Test]
		public void Constructor_Tests()
		{
			CircleEntry<int> start = new CircleEntry<int>(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
			for (int n = 0; n < 10; n++) {
				Assert.AreEqual(start.Value, n);
				start = start.Next;
			}
		}
	}
}
