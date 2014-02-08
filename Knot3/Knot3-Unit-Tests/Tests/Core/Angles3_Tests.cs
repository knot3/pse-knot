using System;
using System.Text;
using System.Collections.Generic;

using NUnit.Framework;

using Knot3.Core;

using Microsoft.Xna.Framework;

namespace Knot3.UnitTests.Tests.Core
{
	/// <summary>
	/// Zusammenfassungsbeschreibung für Test_Angles3
	/// </summary>
	[TestFixture]
	public class Test_Angles3
	{
		float X;
		float Y;
		float Z;
		float redianX;
		float redianY;
		float redianZ;
		Angles3 angle1;
		Angles3 angle2;
		Vector3 redian;

		[SetUp]
		public void Init()
		{
			X = 120;
			Y = 50;
			Z = 280;
			redianX = X * ((float)Math.PI / 180);
			redianY = Y * ((float)Math.PI / 180);
			redianZ = Z * ((float)Math.PI / 180);
			redian = new Vector3(redianX, redianY, redianZ);
			angle1 = new Angles3(redian);
		}

		[Test]
		public void Angles3_FromDegrees_Test()
		{
			angle2 = Angles3.FromDegrees(X, Y, Z);
			Assert.AreEqual(angle1, angle2);
		}
		public void Angles3_ToDegrees_Test()
		{
		}

		[Test]
		public void Angles3_Equals_Test()
		{
			Assert.AreEqual(true, angle1.Equals(angle1));
		}
	}
}
