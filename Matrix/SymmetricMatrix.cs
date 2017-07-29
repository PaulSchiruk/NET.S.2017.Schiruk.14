using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class SymmetricMatrix<T> : SquareMatrix<T>
    {
        private T[,] matrix;
        public override T[,] Matrix
        {
            get => matrix;
            set
            {
                if (value.GetLength(0) != value.GetLength(1))
                    throw new ArgumentException();
                if (value.GetLength(0) > GetLength())
                    ResizeArray();
                if (!SymmetricCheck())
                    throw new ArgumentException();
                matrix = (T[,])value.Clone();
            }
        }
        public event EventHandler ElementChanged;
        protected override void OnElementChanged(EventArgs e)
        {
            ElementChanged?.Invoke(this, e);
        }

        public override void ChangeElement(int i, int j, T value)
        {
            while (Matrix.GetLength(0) > i || Matrix.GetLength(0) > j)
                ResizeArray();
            if (!SymmetricCheck())
                throw new ArgumentException();
            Matrix[i, j] = value;
            OnElementChanged(EventArgs.Empty);
        }

        public static SymmetricMatrix<T> operator +(SymmetricMatrix<T> lhs, SymmetricMatrix<T> rhs)
            => lhs + rhs;
        private bool SymmetricCheck()
        {
            if (this == this.Transpose())
                return true;
            return false;
        }

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
