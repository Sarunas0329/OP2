using System;

namespace Antras
{
    public class Publishment : IComparable<Publishment>, IEquatable<Publishment>
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Publishment(int code, string name, decimal price)
        {
            this.Code = code;
            this.Name = name;
            this.Price = price;
        }
        /// <summary>
        /// Finds if the price is lower than the given price, and if they are equal and alphabetically decreasing
        /// </summary>
        /// <param name="rhs">Information about a publishment</param>
        /// <returns>True if either price is descending or is aphabetically decreasing, if not returns false</returns>
        public int CompareTo(Publishment rhs)
        {
            if(this.Price < rhs.Price) return 1;
            if(this.Price == rhs.Price)
            {
                if(String.Compare(this.Name,rhs.Name) > 0)
                {
                    return 1;
                }
            }
            return 0;
        }
        public override string ToString()
        {
            return String.Format("| {0,15} | {1,20} | {2,10} |",Code,Name,Price);
        }

        public bool Equals(Publishment other)
        {
            return this == other;
        }
    }
}