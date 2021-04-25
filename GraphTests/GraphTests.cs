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
    public class GraphTests
    {
        [TestMethod()]
        public void FindShortestPathNull()
        {
            GraphNode<int> start = null;
            GraphNode<int> end = new GraphNode<int>(12);

            var result = Graph<int>.FindPath(start, end).path;

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void FindPath1Node()
        {
            GraphNode<int> start = new GraphNode<int>(0);

            var result = Graph<int>.FindPath(start, start).path;

            Assert.AreEqual(start, result.First.Value);
            Assert.AreEqual(start, result.Last.Value);
        }

        [TestMethod()]
        public void FindPath2Nodes()
        {
            GraphNode<int> start = new(0);
            GraphNode<int> end = new(1);
            start.AddNeighbor(end, 1);

            var result = Graph<int>.FindPath(start, end).path;

            Assert.AreEqual(start, result.First.Value);
            Assert.AreEqual(end, result.Last.Value);
        }

        [TestMethod()]
        public void FindPathString2Nodes()
        {
            GraphNode<int> start = new(0);
            GraphNode<int> end = new(1);
            start.AddNeighbor(end, 1);

            (var path, var distance) = Graph<int>.FindPath(start, end);
            string result = Graph<int>.PathToString(path, distance);

            Assert.AreEqual("0, 1, :1", result);
        }

        [TestMethod()]
        public void FindPathString3Nodes()
        {
            GraphNode<int> start = new(0);
            GraphNode<int> n = new(1);
            GraphNode<int> end = new(2);
            start.AddNeighbor(n, 1);
            n.AddNeighbor(end, 2);

            (var path, var distance) = Graph<int>.FindPath(start, end);
            string result = Graph<int>.PathToString(path, distance);

            Assert.AreEqual("0, 1, 2, :3", result);
        }

        [TestMethod()]
        public void FindPathSeveralStringNodes()
        {
            GraphNode<string> a = new("a");
            GraphNode<string> b = new("b");
            GraphNode<string> c = new("c");
            GraphNode<string> d = new("d");
            GraphNode<string> e = new("e");
            GraphNode<string> f = new("f");
            GraphNode<string> g = new("g");
            GraphNode<string> h = new("h");
            a.AddNeighbor(c, 2);
            c.AddNeighbor(e, 2);
            e.AddNeighbor(h, 1);

            (var path, var distance) = Graph<string>.FindPath(a, h);
            string result = Graph<string>.PathToString(path, distance);

            Assert.AreEqual("a, c, e, h, :5", result);
        }

        [TestMethod()]
        public void FindPathPreferIndirectPath()
        {
            GraphNode<string> a = new("a");
            GraphNode<string> b = new("b");
            GraphNode<string> c = new("c");
            GraphNode<string> d = new("d");
            GraphNode<string> e = new("e");
            GraphNode<string> f = new("f");
            GraphNode<string> g = new("g");
            GraphNode<string> h = new("h");
            a.AddNeighbor(b, 3);
            a.AddNeighbor(h, 6);

            a.AddNeighbor(c, 2);
            c.AddNeighbor(e, 2);
            e.AddNeighbor(h, 1);

            (var path, var distance) = Graph<string>.FindPath(a, h);
            string result = Graph<string>.PathToString(path, distance);

            Assert.AreEqual("a, c, e, h, :5", result);
        }

        [TestMethod()]
        public void FindPathPreferDirectPath()
        {
            GraphNode<string> a = new("a");
            GraphNode<string> b = new("b");
            GraphNode<string> c = new("c");
            GraphNode<string> d = new("d");
            GraphNode<string> e = new("e");
            GraphNode<string> f = new("f");
            GraphNode<string> g = new("g");
            GraphNode<string> h = new("h");
            a.AddNeighbor(b, 1);
            b.AddNeighbor(f, 1);
            f.AddNeighbor(g, 2);
            g.AddNeighbor(h, 5);

            a.AddNeighbor(h, 8);

            a.AddNeighbor(c, 3);
            c.AddNeighbor(e, 5);
            e.AddNeighbor(h, 1);

            (var path, var distance) = Graph<string>.FindPath(a, h);
            string result = Graph<string>.PathToString(path, distance);

            Assert.AreEqual("a, h, :8", result);
        }

        [TestMethod()]
        public void FindPathPreferShortestPath()
        {
            GraphNode<string> a = new("a");
            GraphNode<string> b = new("b");
            GraphNode<string> c = new("c");
            GraphNode<string> d = new("d");
            GraphNode<string> e = new("e");
            GraphNode<string> f = new("f");
            GraphNode<string> g = new("g");
            GraphNode<string> h = new("h");
            a.AddNeighbor(b, 1);
            b.AddNeighbor(f, 1);
            f.AddNeighbor(g, 2);
            g.AddNeighbor(h, 6);

            a.AddNeighbor(h, 9);

            a.AddNeighbor(c, 4);
            c.AddNeighbor(e, 3);
            e.AddNeighbor(h, 1);

            var path = Graph<string>.FindPath(a, h);
            string result = Graph<string>.PathToString(path.path, path.distance);

            Assert.AreEqual("a, c, e, h, :8", result);
        }

        [TestMethod()]
        public void FindPathAnotherPath()
        {
            GraphNode<string> a = new("a");
            GraphNode<string> b = new("b");
            GraphNode<string> c = new("c");
            GraphNode<string> d = new("d");
            GraphNode<string> e = new("e");
            GraphNode<string> f = new("f");
            GraphNode<string> g = new("g");
            GraphNode<string> h = new("h");
            a.AddNeighbor(b, 2);
            a.AddNeighbor(g, 1);
            a.AddNeighbor(e, 8);

            b.AddNeighbor(h, 24);
            b.AddNeighbor(g, 1);
            b.AddNeighbor(c, 3);

            c.AddNeighbor(e, 3);

            e.AddNeighbor(h, 2);

            g.AddNeighbor(b, 0);

            var t = Graph<string>.FindPath(a, h);
            string result = Graph<string>.PathToString(t.path, t.distance);

            Assert.AreEqual("a, g, b, c, e, h, :9", result);
        }

        [TestMethod()]
        public void FindPathHandlesNoPath()
        {
            GraphNode<string> a = new("a");
            GraphNode<string> b = new("b");
            GraphNode<string> c = new("c");
            GraphNode<string> d = new("d");
            GraphNode<string> e = new("e");
            GraphNode<string> f = new("f");
            GraphNode<string> g = new("g");
            GraphNode<string> h = new("h");
            a.AddNeighbor(b, 2);

            b.AddNeighbor(g, 1);
            b.AddNeighbor(c, 5);

            c.AddNeighbor(e, 3);

            g.AddNeighbor(b, 0);

            var t = Graph<string>.FindPath(a, h);

            Assert.AreEqual((null, int.MinValue), t);
        }

        [TestMethod()]
        public void BuildPathHandlesNoPath()
        {
            GraphNode<string> a = new("a");
            GraphNode<string> b = new("b");
            GraphNode<string> c = new("c");
            GraphNode<string> d = new("d");
            GraphNode<string> e = new("e");
            GraphNode<string> f = new("f");
            GraphNode<string> g = new("g");
            GraphNode<string> h = new("h");
            a.AddNeighbor(b, 2);
            b.AddNeighbor(g, 1);
            b.AddNeighbor(c, 5);
            c.AddNeighbor(g, 3);
            g.AddNeighbor(b, 0);

            // Subgraph, not reached
            f.AddNeighbor(h, 3);
            h.AddNeighbor(d, 2);
            d.AddNeighbor(f, 5);

            Dictionary<GraphNode<string>, GraphNode<string>> pathMap = new();
            pathMap.Add(b, a);
            pathMap.Add(g, b);
            pathMap.Add(c, g);
            pathMap.Add(e, c);

            var result = Graph<string>.BuildPath(a, h, pathMap);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void PathToStringHandlesNoPath()
        {
            GraphNode<string> a = new("a");
            GraphNode<string> b = new("b");
            GraphNode<string> c = new("c");
            GraphNode<string> d = new("d");
            GraphNode<string> e = new("e");
            GraphNode<string> f = new("f");
            GraphNode<string> g = new("g");
            GraphNode<string> h = new("h");
            a.AddNeighbor(b, 2);

            b.AddNeighbor(g, 1);
            b.AddNeighbor(c, 5);

            c.AddNeighbor(e, 3);

            g.AddNeighbor(b, 0);

            var t = Graph<string>.FindPath(a, h);
            string result = Graph<string>.PathToString(t.path, t.distance);

            Assert.IsNull(result);
        }
    }
}