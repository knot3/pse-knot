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
		float X, Y, Z;
		float rX, rY, rZ;
		float redianX;
		float redianY;
		float redianZ;
		int hash1;
		int hash2;
		Angles3 angle1;
		Angles3 angle2;
		Angles3 angle3;
		Vector3 redian;
		object obj;

		[SetUp]
		public void Init()
		{
			X = 120;
			Y = 50;
			Z = 280;

			rX = 2.0943951023931953f;
			rY = 0.8726646259971648f;
			rZ = 4.886921905584122f;

			redianX = X * ((float)Math.PI / 180);
			redianY = Y * ((float)Math.PI / 180);
			redianZ = Z * ((float)Math.PI / 180);

			redian = new Vector3(redianX, redianY, redianZ);
			angle1 = new Angles3(redian);
			angle3 = new Angles3(rX, rY, rZ);
			obj = angle1;
		}

		[Test]
		public void Angles3_FromDegrees_Test()
		{
			angle2 = Angles3.FromDegrees(X, Y, Z);
			Assert.AreEqual(angle1, angle2);
		}

		[Test]
		public void Angles3_ToDegrees_Test()
		{
			angle1.ToDegrees(out X,out Y, out Z);
			Assert.AreEqual(angle1, angle3);
		}

		[Test]
		public void Angles3_Equals_Test()
		{
			Assert.AreEqual(true, angle1.Equals(angle1));
			Assert.AreEqual(true, angle1.Equals(obj));
		}

		[Test]
		public void Angles3_ToString_Test()
		{
			string text1 = angle1.ToString();
			string text2 = "Angles3(120,50,280)";
			Assert.AreEqual(text1,text2);
		}

		[Test]
		public void Angles3_GetHashCode_Test()
		{
			hash1 = angle1.GetHashCode();
			hash2 = 7;
			Assert.AreEqual(hash1, hash2);
		}
	}
}
