using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace laboras
{
    public class InOutUtils
    {
        public static List<Domino> Read(string path)
        {
            List<Domino> dominos = new List<Domino>();

            string[] values = path.Split(' ');
            foreach (string value in values)
            {
                int v1 = int.Parse(value.Substring(0, 1));
                int v2 = int.Parse(value.Substring(1, 1));
                Domino dom = new Domino(v1, v2);
                dominos.Add(dom);
            }
            return dominos;
        }
        /// <summary>
        /// Joins a chain in to a string format
        /// </summary>
        /// <param name="chain">A chain of dominoes</param>
        /// <returns>The chain formatted with spaces</returns>
        public static string JoinChain(List<Domino> chain)
        {
            string line = chain[0].ToString();
            for (int i = 0; i < chain.Count - 1; i++)
            {
                line += " " + chain[i + 1].ToString();
            }
            return line;
        }
    }
}