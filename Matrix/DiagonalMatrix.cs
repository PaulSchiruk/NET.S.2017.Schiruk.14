using System;

namespace Matrix
{
    /// <summary>
    /// This class implements Diagonal matrix
    /// </summary>
    /// <typeparam name="T">any type</typeparam>
    class DiagonalMatrix<T> : SquareMatrix<T> //where T: IEquatable<T>
    {
        private T[,] matrix;
        /// <summary>
        /// Property, that contains Matrix itself
        /// </summary>
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
        /// <summary>
        /// Event handler
        /// </summary>
        public event EventHandler ElementChanged;
        protected override void OnElementChanged(EventArgs e)
        {
            ElementChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Change elenemt on position j i
        /// </summary>
        /// <param name="i">Number of string</param>
        /// <param name="j">Number of column</param>
        /// <param name="value">On whick to change</param>
        public override void ChangeElement(int i, int j, T value)
        {
            while (Matrix.GetLength(0) > i || Matrix.GetLength(0) > j)
                ResizeArray();
            if(!DiagCheck())
                throw new ArgumentException();
            Matrix[i, j] = value;
            OnElementChanged(EventArgs.Empty);
        }
        
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
