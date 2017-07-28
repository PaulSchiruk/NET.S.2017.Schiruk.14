using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Set
{

    public class Set<T> : IEnumerable<T> where T : class
    {
        private T[] array;
        private int capacity = 8;
        private int count;


        public Set()
        {
            array = new T[capacity];
        }



        public Set(IEnumerable collection) : this()
        {
            foreach (T t in collection)
            {
                Add(t);
            }
        }

        public int Count => this.count;

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
        public object Clone() => new Set<T>(this);




        public void Clear()
        {
            array = new T[capacity];
        }

        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            for (int i = arrayIndex; i < arrayIndex + this.Count; i++)
            {
                array[i] = this.array[i - arrayIndex];
            }

        }

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

