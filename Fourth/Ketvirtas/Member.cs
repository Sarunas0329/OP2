using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Ketvirtas
{
    public abstract class Member : IComparable<Member>, IEquatable<Member>
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime birthDate { get; set; }
        public int Number { get; set; }

        /// <summary>
        /// An empty contructor
        /// </summary>
        public Member() { }
        /// <summary>
        /// A constructor to get the information to this object
        /// </summary>
        /// <param name="surname">Members last name</param>
        /// <param name="name">Members name</param>
        /// <param name="birthDate">Members birth date</param>
        /// <param name="number">Members telephone number</param>
        public Member(string surname, string name, DateTime birthDate, int number)
        {
            this.Surname = surname;
            this.Name = name;
            this.birthDate = birthDate;
            this.Number = number;
        }
        /// <summary>
        /// Creates a string that can be used when printing to a CSV file.
        /// </summary>
        /// <returns>A formatted string for printing to a CSV file</returns>
        public virtual string ToStringCSV()
        {
            return string.Format("{0},{1},{2:yyyy/MM/dd},{3}", Surname, Name, birthDate, Number);
        }
        /// <summary>
        /// Creates a string that is used to print data
        /// </summary>
        /// <returns>A formatted string for printing</returns>
        public override string ToString()
        {
            return String.Format("| {0,-15} | {1,-15} | {2,20:yyyy/MM/dd} | {3,10}",Surname,Name, birthDate, Number);
        }
        /// <summary>
        /// Finds the members current age by the month and day
        /// </summary>
        /// <returns>The members age</returns>
        public int Age()
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if(birthDate.Month >= DateTime.Now.Month)
            {
                if(birthDate.Day >= DateTime.Now.Day)
                {
                    age++;
                }
            }
            return age;
        }

        /// <summary>
        /// Finds if two members are identical
        /// </summary>
        /// <param name="other">The information of the other member</param>
        /// <returns>True if they are equal, false if not</returns>
        public bool Equals(Member other)
        {
            if (Surname == other.Surname && Name == other.Name && birthDate == other.birthDate && Number == other.Number)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Compares two members by their surname and by their name
        /// </summary>
        /// <param name="other">The information of the other member</param>
        /// <returns></returns>

        public int CompareTo(Member other)
        {
            if(String.Compare(Surname,other.Surname) > 0)
            {
                if(String.Compare(Name,other.Name) > 0)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}