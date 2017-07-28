using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciYield
{
    public class Logic
    {
        /// <summary>
        /// Count fibonacci number
        /// </summary>
        /// <param name="x">Wich Fibonacci number</param>
        /// <returns>Fibonacci number</returns>
        public IEnumerable<int> Fibonacci(int x)
        {
            int prev = -1;
            int next = 1;
            for (int i = 0; i < x; i++)
            {
                int sum = prev + next;
                prev = next;
                next = sum;
                yield return sum;
            }
        }
    }
}
