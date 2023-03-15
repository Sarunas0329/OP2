using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace laboras
{
    public class TaskUtils
    {
        /// <summary>
        /// Finds if the domino is suitable for the chain
        /// </summary>
        /// <param name="domino">Information about the domino</param>
        /// <param name="chain">A chain of current dominoes</param>
        /// <returns>True or false</returns>
        public static bool Suitable(Domino domino, List<Domino> chain)
        {
            return chain == null || chain.Count == 0 || chain[chain.Count - 1].B == domino.A;
        }
    }
}