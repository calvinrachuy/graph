using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphNS.Tests
{
    [TestClass()]
    public class StaticGraphMethodTests<T>
    {
        [TestMethod()]
        public void FindShortestPathNull()
        {
            GraphNode<int> start = null;
            GraphNode<int> end = new GraphNode<int>(12);

            var result = Graph<int>.FindPath(start, end);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void FindPathSelf()
        {
            GraphNode<int> start = new GraphNode<int>(0);
            LinkedList<GraphNode<int>> expected = new();
            expected.AddFirst(start);

            var result = Graph<int>.FindPath(start, start);

            Assert.AreEqual(expected, result);
        }
    }
}