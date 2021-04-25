using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphNS
{
    public class Graph<T>
    {
        // TODO implement this BFS style, without the Queue at all
        // remember to compare distance
        public static (LinkedList<GraphNode<T>> path, int distance) FindPath(GraphNode<T> start, GraphNode<T> end)
        {
            if (start == null || end == null) return (null, int.MinValue);

            Dictionary<GraphNode<T>, GraphNode<T>> pathMap = new();
            // TODO dict default value?
            Dictionary<GraphNode<T>, int> distanceMap = new();
            PriorityQueue<GraphNode<T>> q = new();

            pathMap.Add(start, null);
            distanceMap.Add(start, 0);
            q.Add(start, 0);

            while(!q.IsEmpty)
            {
                GraphNode<T> current = q.Pop();
                for(int i = 0; i < current.Neighbors.Count; i++)
                {
                    GraphNode<T> n = current.Neighbors[i];
                    int weight = current.Weights[i];
                    int distance = distanceMap[current] + weight;
                    if ((distanceMap.ContainsKey(n) && distance < distanceMap[n]) || !distanceMap.ContainsKey(n))
                    {
                        distanceMap[n] = distance;

                        if (!pathMap.ContainsKey(n)) q.Add(n, distance);
                        pathMap[n] = current;
                    }
                }
            }
            LinkedList<GraphNode<T>> path = BuildPath(start, end, pathMap);
            if (path == null)
            {
                return (null, int.MinValue);
            }
            else
            {
                return (path, distanceMap[end]);   
            }
        }
        public static LinkedList<GraphNode<T>> BuildPath(GraphNode<T> start, GraphNode<T> end, Dictionary<GraphNode<T>, GraphNode<T>> pathMap)
        {
            if (start == null || end == null) return null;
            if (!(pathMap.ContainsKey(end) && pathMap.ContainsKey(start))) return null;

            LinkedList<GraphNode<T>> path = new();
            GraphNode<T> current = end;
            while(current != null)
            {
                path.AddFirst(current);
                current = pathMap[current];
            }

            if (!(path.First.Value == start && path.Last.Value == end))
                return null;

            return path;
        }

        public static string PathToString(LinkedList<GraphNode<T>> path, int distance)
        {
            if (path == null) return null;
            string str = "";
            foreach (var n in path)
            {
                str += n.Value.ToString() + ", ";
            } 

            return str + ":" + distance;
        }
    }
}
