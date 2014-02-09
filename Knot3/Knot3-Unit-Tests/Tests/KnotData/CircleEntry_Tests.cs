using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;
using Knot3.KnotData;

namespace Knot3.UnitTests.Tests.KnotData
{
	/// <summary>
	/// Zusammenfassungsbeschreibung für Test_CircleEntry
	/// </summary>
	[TestFixture]
	public class Test_CircleEntry
	{
		public Test_CircleEntry ()
		{
			//
			// TODO: Konstruktorlogik hier hinzufügen
			//
		}

        public void Constructor_Tests()
        {
            CircleEntry<int> start = new CircleEntry<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int n = 0; n < 10; n++)
            {
                Assert.AreEqual(start.Value, n);
                start = start.Next;
            }
        }

		[Test]
		public void TestMethod1 ()
		{
			//
			// TODO: Testlogik hier hinzufügen
			//
		}
	}
}
