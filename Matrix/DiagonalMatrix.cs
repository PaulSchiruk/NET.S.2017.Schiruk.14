using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    class DiagonalMatrix<T> : SquareMatrix<T> //where T: IEquatable<T>
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
                if(!DiagCheck())
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
            if(!DiagCheck())
                throw new ArgumentException();
            Matrix[i, j] = value;
            OnElementChanged(EventArgs.Empty);
        }

        public static DiagonalMatrix<T> operator +(DiagonalMatrix<T> lhs, DiagonalMatrix<T> rhs)
            => lhs + rhs;
        private bool DiagCheck()
        {
            for (int i = 0; i < GetLength(); i++)
            {
                for (int j = 0; j < GetLength(); j++)
                {
                    if (i != j)
                        if (Equals(Matrix[i, j], default(T)))
                            return false;

                }
            }
            return true;
        }
    }
}
