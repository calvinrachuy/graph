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
        public LinkedList<GraphNode<int>> FindPath(GraphNode<int> start, GraphNode<int> end)
        {
            return Graph<int>.FindPath(start, end);
        }
        [TestMethod()]
        public void FindShortestPathNull()
        {
            GraphNode<int> start = null;
            GraphNode<int> end = new GraphNode<int>(12);

            var result = FindPath(start, end);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void FindPathSelf()
        {
            GraphNode<int> start = new GraphNode<int>(0);
            LinkedList<GraphNode<int>> expected = new();
            expected.AddFirst(start);

            var result = FindPath(start, start);

            Assert.AreEqual(expected, result);
        }
    }
}