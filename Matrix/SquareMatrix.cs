using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class SquareMatrix<T>
    {
        private T[,] matrix;
        private int capacity = 8;
        private static int counter = 0;

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

        public SquareMatrix(T[,] value)
        {
            Matrix = value;
        }

        public SquareMatrix()
        {
            ResizeArray();
        }

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

        public virtual void ChangeElement(int i, int j, T value)
        {
            while (Matrix.GetLength(0) > i || Matrix.GetLength(0) > j)
                ResizeArray();
            Matrix[i, j] = value;
            OnElementChanged(EventArgs.Empty);
        }

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

        public static bool operator !=(SquareMatrix<T> lhs, SquareMatrix<T> rhs) => !(lhs == rhs);
        


        public int GetLength() => Matrix.GetLength(0);
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
