using System;

namespace Antras
{
    public class Subscriber : IComparable<Subscriber>, IEquatable<Subscriber>
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

        public override string ToString()
        {
            return String.Format("| {0,15} | {1,25} | {2,20} | {3,20} | {4,15} | {5,15} |", Surname, Adress, StartingMonth, Duration, Code, Count);
        }

        public int CompareTo(Subscriber other)
        {
            return this.CompareTo(other);
        }

        public bool Equals(Subscriber other)
        {
            return this == other;
        }
    }
}