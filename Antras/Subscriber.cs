using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Antras
{
    public class Subscriber
    {
        public string Surname { get; set; }
        public string Adress { get; set; }
        public int StartingMonth { get; set; }
        public int Duration { get; set; }
        public int Code { get; set; }
        public int Count { get; set; }

        public Subscriber(string surname, string adress, int startingMonth, int duration, int code, int count)
        {
            this.Surname = surname;
            this.Adress = adress;
            this.StartingMonth = startingMonth;
            this.Duration = duration;
            this.Code = code;
            this.Count = count;
        }
    }
}