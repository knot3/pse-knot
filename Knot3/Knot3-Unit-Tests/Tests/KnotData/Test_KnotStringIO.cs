using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using NUnit.Framework;


using Knot3.KnotData;


namespace Knot3.UnitTests.Tests.KnotData
{
    class Test_KnotStringIO
    {

        [TestFixture]
        public void test_EncodeDecodeEdge()
        {
            Edge[] edges = {Edge.Left, Edge.Right, Edge.Up, Edge.Down};

            foreach (Edge edge in edges) {
                
                // EncodeEdge(edge);
            }
        }
    }
}
