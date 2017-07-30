using System;

namespace Matrix
{
    /// <summary>
    /// This class implements Square matrix
    /// </summary>
    /// <typeparam name="T">any type</typeparam>
    public class SquareMatrix<T>
    {
        private T[,] matrix;
        private int capacity = 8;
        private static int counter = 0;
        /// <summary>
        /// Property, that contains Matrix itself
        /// </summary>
        public virtual T[,] Matrix
        {
            get => matrix;
            set
            {
                if (value.GetLength(0) != value.GetLength(1))
                    throw new ArgumentException();
                if (value.GetLength(0) > capacity * counter)
                    ResizeArray();
                matrix = (T[,]) value.Clone();
            }
        }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="value">two dimentional array</param>
        public SquareMatrix(T[,] value)
        {
            Matrix = value;
        }
        /// <summary>
        /// ctor
        /// </summary>
        public SquareMatrix()
        {
            ResizeArray();
        }
        /// <summary>
        /// Event handler
        /// </summary>
        public event EventHandler ElementChanged;

        protected virtual void OnElementChanged(EventArgs e)
        {
            ElementChanged?.Invoke(this, e);
        }

        protected void ResizeArray()
        {
            counter++;
            T[,] resized = new T[capacity * counter, capacity * counter];
            matrix = resized;
        }
        /// <summary>
        /// Change elenemt on position j i
        /// </summary>
        /// <param name="i">Number of string</param>
        /// <param name="j">Number of column</param>
        /// <param name="value">On which to change</param>
        public virtual void ChangeElement(int i, int j, T value)
        {
            while (Matrix.GetLength(0) > i || Matrix.GetLength(0) > j)
                ResizeArray();
            Matrix[i, j] = value;
            OnElementChanged(EventArgs.Empty);
        }
        /// <summary>
        /// Operator plus
        /// </summary>
        /// <param name="lhs">Left member to add</param>
        /// <param name="rhs">Right member to add</param>
        /// <returns>Sun of two members</returns>
        public static SquareMatrix<T> operator +(SquareMatrix<T> lhs, SquareMatrix<T> rhs)
        {
            SquareMatrix<T> result = new SquareMatrix<T>();
            for (int i = 0; i < lhs.GetLength(); i++)
            {
                for (int j = 0; j < rhs.GetLength(); j++)
                {
                    result.ChangeElement(i, j, default(T)); //lhs.Matrix[i, j] + rhs.Matrix[i, j]);
                }
            }
            return result;
        }
        /// <summary>
        /// Overrided equality operator
        /// </summary>
        /// <param name="lhs">Left member</param>
        /// <param name="rhs">Right member</param>
        /// <returns>is equal</returns>
        public static bool operator ==(SquareMatrix<T> lhs, SquareMatrix<T> rhs)
        {
            if (lhs.GetLength() != rhs.GetLength())
                return false;
            for (int i = 0; i < lhs.GetLength(); i++)
            {
                for (int j = 0; j < rhs.GetLength(); j++)
                {
                    if (!Equals(lhs.Matrix[i, j] , rhs.Matrix[i, j]))
                        return false;
                }

            }
            return true;
        }
        /// <summary>
        /// Overrided unequal operator
        /// </summary>
        /// <param name="lhs">Left member</param>
        /// <param name="rhs">Right member</param>
        /// <returns>is not equal</returns>
        public static bool operator !=(SquareMatrix<T> lhs, SquareMatrix<T> rhs) => !(lhs == rhs);
        

        /// <summary>
        /// Get number of strings or columns
        /// </summary>
        /// <returns>Number of strings or columns</returns>
        public int GetLength() => Matrix.GetLength(0);
        /// <summary>
        /// Iimplements transposing
        /// </summary>
        /// <returns>Transopsed matrix</returns>
        public SquareMatrix<T> Transpose()
        {
            T t;
            SquareMatrix<T> mtx = new SquareMatrix<T>(Matrix);
            for (int i = 0; i < GetLength(); i++)
            {
                for (int j = i; j < GetLength(); j++)
                {
                    t = mtx.Matrix[i, j];
                    mtx.Matrix[i, j] = mtx.Matrix[j, i];
                    mtx.Matrix[j, i] = t;
                }
            }
            return mtx;
        }

    }
}
