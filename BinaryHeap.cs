using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace APA.XmlParser
{
    public class BinaryHeap<T> : IEnumerable<T>
    {
        private IComparer<T> Comparer;
        private List<T> Items = new List<T>();
        public BinaryHeap()
            : this(Comparer<T>.Default)
        {
        }
        public BinaryHeap(IComparer<T> comp)
        {
            Comparer = comp;
        }
        /// <summary>

        /// Get a count of the number of items in the collection.
        /// </summary>
        public int Count
        {
            get { return Items.Count; }
        }
        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        public void Clear()
        {
            Items.Clear();
        }
        /// <summary>
        /// Sets the capacity to the actual number of elements in the BinaryHeap,
        /// if that number is less than a threshold value.
        /// </summary>

        /// <remarks>
        /// The current threshold value is 90% (.NET 3.5), but might change in a future release.
        /// </remarks>
        public void TrimExcess()
        {
            Items.TrimExcess();
        }
        /// <summary>
        /// Inserts an item onto the heap.
        /// </summary>
        /// <param name="newItem">The item to be inserted.</param>

        public void Insert(T newItem)
        {
            int i = Count;
            Items.Add(newItem);
            while (i > 0 && Comparer.Compare(Items[(i - 1) / 2], newItem) > 0)
            {
                Items[i] = Items[(i - 1) / 2];
                i = (i - 1) / 2;
            }
            Items[i] = newItem;
        }
        /// <summary>
        /// Return the root item from the collection, without removing it.
        /// </summary>
        /// <returns>Returns the item at the root of the heap.</returns>

        public T Peek()
        {
            if (Items.Count == 0)
            {
                throw new InvalidOperationException("The heap is empty.");
            }
            return Items[0];
        }
        /// <summary>
        /// Removes and returns the root item from the collection.
        /// </summary>
        /// <returns>Returns the item at the root of the heap.</returns>
        public T RemoveRoot()
        {
            if (Items.Count == 0)
            {
                throw new InvalidOperationException("The heap is empty.");
            }
            // Get the first item
            T rslt = Items[0];
            // Get the last item and bubble it down.
            T tmp = Items[Items.Count - 1];
            Items.RemoveAt(Items.Count - 1);
            if (Items.Count > 0)
            {
                int i = 0;
                while (i < Items.Count / 2)
                {
                    int j = (2 * i) + 1;
                    if ((j < Items.Count - 1) && (Comparer.Compare(Items[j], Items[j + 1]) > 0))
                    {
                        ++j;
                    }
                    if (Comparer.Compare(Items[j], tmp) >= 0)
                    {
                        break;
                    }
                    Items[i] = Items[j];
                    i = j;
                }
                Items[i] = tmp;
            }
            return rslt;
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var i in Items)
            {
                yield return i;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
