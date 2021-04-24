using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class GraphNode<T>
    {
        private List<GraphNode<T>> neighbors = new List<GraphNode<T>>();
        private List<int> weights = new List<int>();

        public readonly T Value;
        public IReadOnlyList<GraphNode<T>> Neighbors { get { return neighbors.AsReadOnly(); } }
        public IReadOnlyList<int> Weights {  get { return weights.AsReadOnly(); } }
        
        public GraphNode(T value)
        {
            this.Value = value;
        }

        public bool AddNeighbor(GraphNode<T> node, int weight)
        {
            if (!neighbors.Contains(node))
            {
                neighbors.Add(node);
                weights.Add(weight);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveNeighbor(GraphNode<T> node)
        {
            int i = neighbors.IndexOf(node);
            if (i == -1)
            {
                return false;
            }
            else
            {
                neighbors.RemoveAt(i);
                weights.RemoveAt(i);
                return true;
            }
        }
        
        public void RemoveAllNeighbors()
        {
            neighbors.Clear();
            weights.Clear();
        }

        public override string ToString()
        {
            string str = Value.ToString() + ": ";
            for (int i = 0; i < neighbors.Count; i++)
            {
                str += neighbors[i].Value + " w" + weights[i] + ", ";
            }
            return str;
        }
    }
}
