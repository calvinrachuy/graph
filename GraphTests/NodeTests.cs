using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Tests
{
    [TestClass()]
    public class NodeTests
    {
        [TestMethod()]
        public void NodeTest()
        {
            const int value = 5;
            Node<int> n = new Node<int>(value);

            Assert.AreEqual(value, n.Value);
            Assert.IsTrue(n.Neighbors.Count == 0);
            Assert.IsTrue(n.Weights.Count == 0);
        }

        [TestMethod()]
        public void AddNeighborTest()
        {
            const int weight = 6;
            Node<int> n = new Node<int>(1);
            Node<int> n2 = new Node<int>(2);

            Assert.IsTrue(n.AddNeighbor(n2, weight));

            Assert.IsTrue(n.Neighbors.Count == 1);
            Assert.IsTrue(n.Weights.Count == 1);
            Assert.AreEqual(n2, n.Neighbors[0]);
            Assert.AreEqual(weight, n.Weights[0]);
        }

        [TestMethod()]
        public void RemoveNeighborTest()
        {
            Node<int> n = new Node<int>(1);
            Node<int> n2 = new Node<int>(2);
            n.AddNeighbor(n2, 6);

            n.RemoveNeighbor(n2);

            Assert.IsTrue(n.Neighbors.Count == 0);
            Assert.IsTrue(n.Weights.Count == 0);
        }

        [TestMethod()]
        public void RemoveAllNeighborsTest()
        {
            Node<int> n = new Node<int>(1);
            Node<int> n2 = new Node<int>(2);
            Node<int> n3 = new Node<int>(3);
            n.AddNeighbor(n2, 6);
            n.AddNeighbor(n3, 7);

            n.RemoveAllNeighbors();

            Assert.IsTrue(n.Neighbors.Count == 0);
            Assert.IsTrue(n.Weights.Count == 0);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Node<int> n = new Node<int>(1);
            Node<int> n2 = new Node<int>(2);
            Node<int> n3 = new Node<int>(3);
            n.AddNeighbor(n2, 6);
            n.AddNeighbor(n3, 7);

            string str = n.ToString();

            Assert.AreEqual("1: 2 w6, 3 w7, ", str);
        }
    }
}