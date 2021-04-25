using GraphNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphNS
{
    class Program
    {
        static void Main(string[] args)
        {
            GraphNode<int> start = new GraphNode<int>(0);
            LinkedList<GraphNode<int>> expected = new();
            expected.AddFirst(start);

            var result = Graph<int>.FindPath(start, start);

            Console.WriteLine("TESTS");
            Console.WriteLine("findpathself");
            Console.WriteLine(result == expected);



            Console.ReadKey();
        }
    }
}
