using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Antras
{
    public class Publishment
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
    }
}