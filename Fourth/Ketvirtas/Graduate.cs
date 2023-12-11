using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ketvirtas
{
    public class Graduate : Member, IComparable<Graduate>, IEquatable<Graduate>
    {
        public int JobStartYear { get; set; }
        public string JobPlace { get; set; }

        /// <summary>
        /// An empty constructor
        /// </summary>
        public Graduate() { }
        /// <summary>
        /// A constructor to get the information to this object
        /// </summary>
        /// <param name="surname">Graduates last name</param>
        /// <param name="name">Graduates name</param>
        /// <param name="birthDate">Graduates birth date</param>
        /// <param name="number">Graduates telephone number</param>
        /// <param name="jobStartYear">What year the graduate started working</param>
        /// <param name="jobPlace">The workplace the graduate is working in </param> 
        public Graduate(string surname, string name, DateTime birthDate, int number,int jobStartYear,string jobPlace): base(surname,name,birthDate,number)
        {
            this.JobStartYear = jobStartYear;
            this.JobPlace = jobPlace;
        }
        /// <summary>
        /// Creates a string that can be used when printing to a CSV file.
        /// </summary>
        /// <returns>A formatted string for printing to a CSV file</returns>
        public override string ToStringCSV()
        {
            return string.Format("{0},{1},{2}", base.ToStringCSV(), JobStartYear, JobPlace);
        }
        /// <summary>
        /// Creates a string that is used to print data
        /// </summary>
        /// <returns>A formatted string for printing</returns>
        public override string ToString()
        {
            return String.Format("| {0} | {1,10} | {2,-15} |  ", base.ToString(), JobStartYear, JobPlace);
        }
        /// <summary>
        /// Compares two graduates by their workplace then by their surname and finally by their name
        /// </summary>
        /// <param name="other">The information of the other graduate</param>
        /// <returns></returns>
        public int CompareTo(Graduate other)
        {
            if (String.Compare(JobPlace, other.JobPlace) > 0) 
            {
                if (base.CompareTo(other) > 0)
                {
                    return 1;
                }
            }
            return -1;
        }
        /// <summary>
        /// Finds if two graduates are identical
        /// </summary>
        /// <param name="other">The information of the other graduates</param>
        /// <returns>True if they are equal, false if not</returns>
        public bool Equals(Graduate other)
        {
            if (base.Equals(other) && JobPlace == other.JobPlace && JobStartYear == other.JobStartYear)
            {
                return true;
            }
            return false;
        }
    }
}