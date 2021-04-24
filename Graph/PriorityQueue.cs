using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class PriorityQueue<T>
    {
        private Dictionary<T, PriorityQueueNode> itemToNodeMap = new();
        private PriorityQueueNode root;
        public T First { get { return root != null ? root.Item : default; } }

        public class PriorityQueueNode
        {
            public int Priority;
            public T Item;
            public PriorityQueueNode Previous;
            public PriorityQueueNode Next;
            public PriorityQueueNode(T item, int priority)
            {
                this.Item = item;
                this.Priority = priority;
            }
        }
        public PriorityQueueNode Add(T item, int priority)
        {
            PriorityQueueNode n = new(item, priority);
            itemToNodeMap.Add(item, n);

            if (root == null)
            {
                root = n;
                return n;
            }
            else
            {
                return Insert(n, root, root.Next);
            }
        }
        public PriorityQueueNode Insert(PriorityQueueNode n, PriorityQueueNode left, PriorityQueueNode right)
        {
            if (n == null) return null;

            if (left == null && right == null)
            {
                root = n;
                return n;
            }
            else if (left == null && n.Priority <= right.Priority)
            {
                right.Previous = n;
                n.Next = right;
                root = n;
                return n;
            }
            else if (right == null && n.Priority >= left.Priority)
            {
                left.Next = n;
                n.Previous = left;
                return n;
            }
            else if (left != null && n.Priority < left.Priority)
            {
                return Insert(n, left.Previous, left);
            }
            else if (right != null && n.Priority > right.Priority)
            {
                return Insert(n, right, right.Next);
            }
            else
            {
                left.Next = n;
                n.Previous = left;
                n.Next = right;
                right.Previous = n;
                return n;
            }
        }
        public T Pop()
        {
            PriorityQueueNode n = root;
            root = root.Next;
            root.Previous = null;
            itemToNodeMap.Remove(n.Item);
            return n.Item;
        }
        public void Update(T item, int priority)
        {
            if (!itemToNodeMap.ContainsKey(item)) throw new KeyNotFoundException();

            PriorityQueueNode n = itemToNodeMap[item];
            n.Priority = priority;
            PriorityQueueNode previous = n.Previous;
            PriorityQueueNode next = n.Next;

            if (previous != null) previous.Next = next;
            if (next != null) next.Previous = previous;
            n.Next = null;
            n.Previous = null;
            if (root == n) root = next;

            Insert(n, previous, next);
        }
        public int GetPriority(T item)
        {
            if (itemToNodeMap.ContainsKey(item))
            {
                return itemToNodeMap[item].Priority;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public IReadOnlyList<T> Items
        {
            get
            {
                List<T> items = new();
                PriorityQueueNode current = root;
                while (current != null)
                {
                    items.Add(current.Item);
                    current = current.Next;
                }
                return items.AsReadOnly();
            }
        }
    }
}
