using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace laboras
{
    public class Chains
    {
        private List<List<Domino>> allChains;
        public int count = 0;
        public Chains()
        {
            allChains = new List<List<Domino>>();
            count = 0;
        }

        public void Add(List<Domino> list)
        {
            allChains.Add(list);
            count++;
        }
        public List<Domino> Get(int index)
        {
            return allChains[index];
        }
    }
}