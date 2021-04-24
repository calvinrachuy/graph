using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dijkstra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra.Tests
{
    [TestClass()]
    public class NodeTests
    {
        [TestMethod()]
        public void NodeTest()
        {
            const int value = 5;
            GraphNode<int> n = new GraphNode<int>(value);

            Assert.AreEqual(value, n.Value);
            Assert.IsTrue(n.Neighbors.Count == 0);
            Assert.IsTrue(n.Weights.Count == 0);
        }

        [TestMethod()]
        public void AddNeighborTest()
        {
            const int weight = 6;
            GraphNode<int> n = new GraphNode<int>(1);
            GraphNode<int> n2 = new GraphNode<int>(2);

            Assert.IsTrue(n.AddNeighbor(n2, weight));

            Assert.IsTrue(n.Neighbors.Count == 1);
            Assert.IsTrue(n.Weights.Count == 1);
            Assert.AreEqual(n2, n.Neighbors[0]);
            Assert.AreEqual(weight, n.Weights[0]);
        }

        [TestMethod()]
        public void RemoveNeighborTest()
        {
            GraphNode<int> n = new GraphNode<int>(1);
            GraphNode<int> n2 = new GraphNode<int>(2);
            n.AddNeighbor(n2, 6);

            n.RemoveNeighbor(n2);

            Assert.IsTrue(n.Neighbors.Count == 0);
            Assert.IsTrue(n.Weights.Count == 0);
        }

        [TestMethod()]
        public void RemoveAllNeighborsTest()
        {
            GraphNode<int> n = new GraphNode<int>(1);
            GraphNode<int> n2 = new GraphNode<int>(2);
            GraphNode<int> n3 = new GraphNode<int>(3);
            n.AddNeighbor(n2, 6);
            n.AddNeighbor(n3, 7);

            n.RemoveAllNeighbors();

            Assert.IsTrue(n.Neighbors.Count == 0);
            Assert.IsTrue(n.Weights.Count == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            GraphNode<int> n = new GraphNode<int>(1);
            GraphNode<int> n2 = new GraphNode<int>(2);
            GraphNode<int> n3 = new GraphNode<int>(3);
            n.AddNeighbor(n2, 6);
            n.AddNeighbor(n3, 7);

            string str = n.ToString();

            Assert.AreEqual("1: 2 w6, 3 w7, ", str);
        }
    }
}