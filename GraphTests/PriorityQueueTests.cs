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
    public class PriorityQueueTests
    {
        [TestMethod()]
        public void NewQueueIsEmpty()
        {
            PriorityQueue<GraphNode<int>> q = new();

            Assert.IsNull(q.First);
            Assert.IsTrue(q.Items.Count == 0);
        }

        [TestMethod()]
        public void AcceptsIntItems()
        {
            PriorityQueue<int> q = new();

            q.Push(5, 5);

            Assert.AreEqual(5, q.First);
        }

        [TestMethod()]
        public void AcceptsGraphNodeItems()
        {
            PriorityQueue<GraphNode<int>> q = new();
            GraphNode<int> n5 = new GraphNode<int>(5);

            q.Push(n5, 5);
            Assert.AreEqual(n5, q.First);
        }

        [TestMethod()]
        public void FirstTest()
        {
            PriorityQueue<int> q = new();

            q.Push(5, 5);

            Assert.AreEqual(5, q.First);
        }

        [TestMethod()]
        public void PushesAMoreUrgentItem()
        {
            PriorityQueue<GraphNode<int>> q = new();
            GraphNode<int> n5 = new GraphNode<int>(5);
            GraphNode<int> n99 = new GraphNode<int>(99);
            q.Push(n5, 5);

            q.Push(n99, 1);

            Assert.AreEqual(n99, q.First);
            Assert.IsTrue(q.Items.Count == 2);
        }

        [TestMethod()]
        public void PushesALessUrgentItem()
        {
            PriorityQueue<GraphNode<int>> q = new();
            GraphNode<int> n5 = new GraphNode<int>(5);
            GraphNode<int> n99 = new GraphNode<int>(99);
            q.Push(n5, 5);

            q.Push(n99, 6);

            Assert.AreEqual(n5, q.First);
            Assert.IsTrue(q.Items.Count == 2);
        }

        [TestMethod()]
        public void PushesSeveralItems()
        {
            PriorityQueue<GraphNode<int>> q = new();
            GraphNode<int> n5 = new GraphNode<int>(5);
            GraphNode<int> n6 = new GraphNode<int>(6);
            GraphNode<int> n8 = new GraphNode<int>(8);
            GraphNode<int> n53 = new GraphNode<int>(53);
            GraphNode<int> n17 = new GraphNode<int>(17);
            GraphNode<int> n2 = new GraphNode<int>(2);
            GraphNode<int> n10 = new GraphNode<int>(10);
            GraphNode<int> n3 = new GraphNode<int>(3);
            GraphNode<int> n1 = new GraphNode<int>(1);
            q.Push(n5, 5);

            q.Push(n6, 6);
            q.Push(n8, 8);
            q.Push(n53, 53);
            q.Push(n17, 17);
            q.Push(n2, 2);
            q.Push(n10, 10);
            q.Push(n3, 3);
            q.Push(n1, 1);

            List<GraphNode<int>> expected = new List<GraphNode<int>>
            {
                n1,
                n2,
                n3,
                n5,
                n6,
                n8,
                n10,
                n17,
                n53,
            };

            Assert.AreEqual(expected.Count, q.Items.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], q.Items[i]);
            }
        }

        [TestMethod()]
        public void UpdateItemNoMove()
        {
            PriorityQueue<GraphNode<int>> q = new();
            GraphNode<int> n5 = new GraphNode<int>(5);
            GraphNode<int> n99 = new GraphNode<int>(99);
            q.Push(n5, 5);
            q.Push(n99, 1);

            q.Update(n99, 4);

            Assert.AreEqual(n99, q.First);
        }

        [TestMethod()]
        public void UpdateItemNoMoveEqualPriority()
        {
            PriorityQueue<GraphNode<int>> q = new();
            GraphNode<int> n5 = new GraphNode<int>(5);
            GraphNode<int> n99 = new GraphNode<int>(99);
            q.Push(n5, 5);
            q.Push(n99, 1);

            q.Update(n99, 5);

            Assert.AreEqual(n99, q.First);
        }

        [TestMethod()]
        public void UpdateItemMovesRight()
        {
            PriorityQueue<GraphNode<int>> q = new();
            GraphNode<int> n5 = new GraphNode<int>(5);
            GraphNode<int> n99 = new GraphNode<int>(99);
            q.Push(n5, 5);
            q.Push(n99, 1);

            q.Update(n99, 6);

            Assert.AreEqual(n5, q.First);
            Assert.AreEqual(n99, q.Items[1]);
        }
    }
}