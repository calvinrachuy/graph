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
        public static LinkedList<GraphNode<T>> FindPath(GraphNode<T> start, GraphNode<T> end)
        {
            if (start == null || end == null) return null;

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

            return BuildPath(start, end, pathMap);
        }
        public static LinkedList<GraphNode<T>> BuildPath(GraphNode<T> start, GraphNode<T> end, Dictionary<GraphNode<T>, GraphNode<T>> pathMap)
        {
            LinkedList<GraphNode<T>> path = new();
            GraphNode<T> current = end;
            while(current != null)
            {
                path.AddFirst(current);
                current = pathMap[current];
            }

            // TODO handle incomplete path (you can't get from start to end)
            return path;
        }
    }
}
