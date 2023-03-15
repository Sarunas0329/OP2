using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace laboras
{
    public class Domino
    {
        public int A { get; set; }
        public int B { get; set; }

        public Domino(int a, int b)
        {
            A = a;
            B = b;
        }
        /// <summary>
        /// Inverts a domino
        /// </summary>
        /// <returns>Inverted domino</returns>
        public Domino Invert()
        {
            return new Domino(B, A);
        }

        public override string ToString()
        {
            return $"{A}{B}";
        }
    }
}