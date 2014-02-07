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
        public void Bounds_Set_Test()
        {
            Bounds compareBound = new Bounds(point, new ScreenPoint(fakeScreen,1f, 0.9f));
            Assert.AreEqual(compareBound.ToString(), bound.FromTop(0.9f).ToString(), "top");
            compareBound = new Bounds(new ScreenPoint(fakeScreen, 0f, 0.1f), new ScreenPoint(fakeScreen, 1f, 0.9f));
            Assert.AreEqual(compareBound.ToString(), bound.FromBottom(0.9f).ToString(), "bottom");
            compareBound = new Bounds(new ScreenPoint(fakeScreen, 0.1f, 0f), new ScreenPoint(fakeScreen, 0.9f, 1f));
            Assert.AreEqual(compareBound.ToString(), bound.FromRight(0.9f).ToString(), "right");
            compareBound = new Bounds(point, new ScreenPoint(fakeScreen, 0.9f, 1f));           
            Assert.AreEqual(compareBound.ToString(), bound.FromLeft(0.9f).ToString(), "left");
        }
	}
}
