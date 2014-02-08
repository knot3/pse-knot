using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;

using Knot3.Widgets;
using Knot3.Core;

namespace Knot3.UnitTests.Tests.Widgets
{
	/// <summary>
	/// Zusammenfassungsbeschreibung für Test_Bounds
	/// </summary>
	[TestFixture]
	public class Test_Bounds
	{
		FakeScreen fakeScreen;
		ScreenPoint point;
		ScreenPoint size;
		ScreenPoint testPoint;
		Bounds bound;

		[SetUp]
		public void Init()
		{
			fakeScreen = new FakeScreen();
			point = new ScreenPoint(fakeScreen, 0, 0);
			size = new ScreenPoint(fakeScreen, 1, 1);
			testPoint = new ScreenPoint(fakeScreen, 0.5f, 0.5f);
			bound = new Bounds(point, size);
		}
		[Test]
		public void Bounds_Contains_Test()
		{
			Assert.AreEqual(true , bound.Contains(testPoint));
		}

		[Test]
		public void Bounds_FromDirection_Test()
		{
			Bounds compareBound = new Bounds(point, new ScreenPoint(fakeScreen,1f, 0.9f));
            Assert.AreEqual(true, boundsEqual(compareBound, bound.FromTop(0.9f)), "top");

            //Irgendwie will er hier bei bottom, right und left noch nicht mit der boundsEquals arbeiten habe es nun noch nicht geändert
            compareBound = new Bounds(new ScreenPoint(fakeScreen, 0f, 0.1f), new ScreenPoint(fakeScreen, 1f, 0.9f));
            Assert.AreEqual(compareBound.ToString(), bound.FromBottom(0.9f).ToString(), "bottom");
            compareBound = new Bounds(new ScreenPoint(fakeScreen, 0.1f, 0f), new ScreenPoint(fakeScreen, 0.9f, 1f));
            Assert.AreEqual(compareBound.ToString(), bound.FromRight(0.9f).ToString(), "right");
            compareBound = new Bounds(point, new ScreenPoint(fakeScreen, 0.9f, 1f));
            Assert.AreEqual(compareBound.ToString(), bound.FromLeft(0.9f).ToString(), "left");

		}

        [Test]
        public void Bounds_Set_Test()
        {
            ScreenPoint newSize = new ScreenPoint(fakeScreen, 1f, 0.9f);
            Bounds compareBound = new Bounds(point, newSize);
            bound.Size = newSize;
            Assert.AreEqual(true, boundsEqual(compareBound, bound));
        }

        private bool boundsEqual (Bounds a, Bounds b)
        {
            if (a.Position.Equals(b.Position) && a.Size.Equals(b.Size)) {
                return true;
            } else {
                return false;
            }
        }
	}
}
