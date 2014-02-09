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
        float scaleFactor;
        float divider;
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
            scaleFactor = 2.5f;
            divider = 1;
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

        [Test]
        public void Angles3_Operator_Test()
        {
            Angles3 angle2 = new Angles3(1, 2, 3);
            Angles3 angle4 = new Angles3(3, 2, 1);
            Angles3 sum = angle2 + angle4;
            Assert.AreEqual(sum, new Angles3(4,4,4));
            Angles3 neg = -angle2;
            Assert.AreEqual(neg, new Angles3(-1,-2,-3));
            Angles3 diff = angle2 - angle4;
            Assert.AreEqual(diff, new Angles3(-2,0,2));
            Angles3 prod = angle2 * angle4;
            Assert.AreEqual(prod, new Angles3(3,4,3));
            Angles3 scale1 = angle2 * scaleFactor;
            Angles3 scale2 = scaleFactor * angle2;
            Assert.AreEqual(scale1, new Angles3(2.5f,5,7.5f));
            Assert.AreEqual(scale2, new Angles3(2.5f, 5, 7.5f));
            Angles3 quot1 = angle2 / angle4;
            Assert.AreEqual(quot1, new Angles3(0.33333333333f, 1, 3));
            Angles3 quot2 = angle2 / divider;
            Assert.AreEqual(quot2, angle2);
            bool same1 = (angle2 == angle2);
            bool nsame1 = (angle2 == angle4);
            Assert.AreEqual(true, same1);
            Assert.AreEqual(false, nsame1);
            bool same2 = (angle2 != angle2);
            bool nsame2 = (angle2 != angle4);
            Assert.AreEqual(false, same2);
            Assert.AreEqual(true, nsame2);
       
        }
	}
}
