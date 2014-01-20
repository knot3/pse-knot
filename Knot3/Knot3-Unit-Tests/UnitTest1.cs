using System;



using NUnit.Framework;



namespace Knot3_Unit_Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            Assert.IsNull(null);
        }

        [Test]
        public void TestMethod2()
        {
            Assert.IsNotNull(null);
        }
    }
}

