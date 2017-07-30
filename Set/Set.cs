using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Set
{
    /// <summary>
    /// This class implements Set
    /// </summary>
    /// <typeparam name="T">any class</typeparam>
    public class Set<T> :ICloneable, IEnumerable<T> where T : class
    {
        private T[] array;
        private int capacity = 8;
        private int count;
        /// <summary>
        /// ctor
        /// </summary>
        public Set()
        {
            array = new T[capacity];
        }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="collection">Create set based on IEnumerable collection</param>
        public Set(IEnumerable collection) : this()
        {
            foreach (T t in collection)
            {
                Add(t);
            }
        }
        /// <summary>
        /// How much members contains
        /// </summary>
        public int Count => this.count;
        /// <summary>
        /// Unite 2 sets
        /// </summary>
        /// <param name="left">One set</param>
        /// <param name="right">Other set</param>
        /// <returns>United set</returns>
        public static Set<T> UniteSet(Set<T> left, Set<T> right)
        {
            Set<T> result = new Set<T>();
            foreach (var t in left.array)
            {
                result.Add(t);
            }
            foreach (var t in right.array)
            {
                result.Add(t);
            }
            return result;
        }
        /// <summary>
        /// Add member to the set
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Is add</returns>
        public bool Add(T item)
        {
            if (item == null)
                return false;
            if (count == 0)
            {
                array[count] = item;
                count++;
                return true;
            }
            if (!this.Contains(item))
            {
                if (IsFull())
                {
                    capacity = 2 * capacity;
                    Array.Resize(ref array, capacity);
                }
                array[count] = item;
                count++;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Is item contains in the set
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <returns>Is Contains</returns>
        public bool Contains(int item)
        {
            if (item == null)
                return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (array[i].Equals(item))
                    return true;
            }
            return false;
        }

        private bool IsFull() => count == capacity;
        object ICloneable.Clone() => Clone();
        /// <summary>
        /// Create Set clone
        /// </summary>
        /// <returns>Deep Set Clone</returns>
        public Set<T> Clone() => new Set<T>(this);
        /// <summary>
        /// Delete all Set members
        /// </summary>
        public void Clear()
        {
            array = new T[capacity];
        }
        /// <summary>
        /// Copy to array
        /// </summary>
        /// <param name="array">To whick array copy to</param>
        /// <param name="arrayIndex">From which index</param>
        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            for (int i = arrayIndex; i < arrayIndex + this.Count; i++)
            {
                array[i] = this.array[i - arrayIndex];
            }

        }
        /// <summary>
        /// Remove item
        /// </summary>
        /// <param name="item">Which item should remove</param>
        /// <returns>Is removed</returns>
        public bool Remove(T item)
        {
            var commonSet = new Set<T>(array);
            if (commonSet.Contains(item))
            {
                foreach (T t in commonSet)
                {
                    if (t == item)
                    {
                        continue;
                    }
                    Add(t);
                }
                this.Clear();
                foreach (T t in commonSet)
                {
                    this.Add(t);
                }
                return true;
            }
            return false;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new IteratorSet(this);
        /// <summary>
        /// Iterator
        /// </summary>
        /// <returns>IEnumerator</returns>
        public IEnumerator GetEnumerator() => new IteratorSet(this);

        private class IteratorSet : IEnumerator<T>
        {
            private Set<T> set;

            public IteratorSet(Set<T> set)
            {
                this.set = set;
            }

            private int iterator = -1;
            public bool MoveNext() => ++iterator < set.count;
            public T Current => set.array[iterator];
            object IEnumerator.Current => Current;

            public void Reset()
            {
                iterator = -1;
            }

            public void Dispose()
            {
            }
        }

    }
}

