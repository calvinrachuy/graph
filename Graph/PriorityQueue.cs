using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    public class PriorityQueue<T>
    {
        private Dictionary<T, SortedListNode> itemToNodeMap = new();
        // TODO replace with SortedList or MinHeap
        private SortedListNode root;
        public T First { get { return root != null ? root.Item : default; } }

        class SortedListNode
        {
            public T Item;
            public int Priority;
            public SortedListNode Previous;
            public SortedListNode Next;
            public SortedListNode(T item, int priority)
            {
                this.Item = item;
                this.Priority = priority;
            }
        }

        void InsertRight(SortedListNode n, SortedListNode previous, SortedListNode next)
        {
            if (n == null) return;

            if (next == null)
            {
                n.Previous = previous;
                n.Next = null;
                previous.Next = n;
            }
            else if (n.Priority < next.Priority)
            {
                n.Next = next;
                n.Previous = previous;
                previous.Next = n;
                next.Previous = n;
            }
            else
            {
                InsertRight(n, next, next.Next);
            }
        }
        void InsertLeft(SortedListNode n, SortedListNode previous, SortedListNode next)
        {
            if (n == null) return;

            if (previous == null)
            {
                n.Next = next;
                n.Previous = null;
                next.Previous = n;
                root = n;
            }
            else if (n.Priority >= previous.Priority)
            {
                n.Next = next;
                n.Previous = previous;
                previous.Next = n;
                next.Previous = n;
            }
            else
            {
                InsertLeft(n, previous.Previous, previous);
            }
        }
        public void Push(T item, int priority)
        {
            SortedListNode n = new(item, priority);
            itemToNodeMap.Add(item, n);

            if (root == null)
            {
                root = n;
            }
            else if (priority < root.Priority)
            {
                root.Previous = n;
                n.Next = root;
                root = n;
            }
            else
            {
                InsertRight(n, root, root.Next);
            }
        }
        public T Pop()
        {
            SortedListNode n = root;
            root = root.Next;
            root.Previous = null;
            itemToNodeMap.Remove(n.Item);
            return n.Item;
        }
        public void Update(T item, int priority)
        {
            if (!itemToNodeMap.ContainsKey(item)) throw new KeyNotFoundException();

            SortedListNode n = itemToNodeMap[item];
            n.Priority = priority;
            SortedListNode previous = n.Previous;
            SortedListNode next = n.Next;

            if ((previous == null || previous.Priority <= priority) && (next == null || priority <= next.Priority)) return;

            n.Next = null;
            n.Previous = null;
            if (next != null) next.Previous = previous;
            if (previous != null) previous.Next = next;

            InsertLeft(n, previous, next);
            InsertRight(n, previous, next);
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
                SortedListNode current = root;
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
