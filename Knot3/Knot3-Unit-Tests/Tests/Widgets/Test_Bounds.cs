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
		[Test]
		public void Bounds1()
		{
            Knot3Game game = new Knot3Game();
            FakeScreen fakeScreen = new FakeScreen(game);
            ScreenPoint point = new ScreenPoint(fakeScreen,0, 0);
            ScreenPoint size = new ScreenPoint(fakeScreen,1, 1);
            ScreenPoint testPoint = new ScreenPoint(fakeScreen, 0.5f, 0.5f);
            Bounds bound = new Bounds(point, size);
            Console.WriteLine(bound);
            Console.WriteLine(point);
            Console.WriteLine(size);
            Assert.AreEqual(true , bound.Contains(testPoint));
                
            
            //
			// TODO: Testlogik hier hinzufügen
			//
		}
	}
}
