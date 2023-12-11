using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ketvirtas
{
    public class Student : Member, IComparable<Student>, IEquatable<Student>
    {
        public int IdNumber { get; set; }
        public int StudyStartYear { get; set; }

        /// <summary>
        /// Empty student constructor
        /// </summary>
        public Student() { }
        /// <summary>
        /// A constructor to get the information to this object
        /// </summary>
        /// <param name="surname">Students last name</param>
        /// <param name="name">Students name</param>
        /// <param name="birthDate">Students birth date</param>
        /// <param name="number">Students telephone number</param>
        /// <param name="idnumber">Students identification number</param>
        /// <param name="studyStartYear">Which year the student started studying</param>
        public Student(string surname, string name, DateTime birthDate, int number, int idnumber, int studyStartYear) : base(surname, name, birthDate, number)
        {
            this.IdNumber = idnumber;
            this.StudyStartYear = studyStartYear;
        }
        /// <summary>
        /// Creates a string that can be used when printing to a CSV file.
        /// </summary>
        /// <returns>A formatted string for printing to a CSV file</returns>
        public override string ToStringCSV()
        {
            return string.Format("{0},{1},{2}", base.ToStringCSV(), IdNumber, StudyStartYear);
        }
        /// <summary>
        /// Creates a string that is used to print data
        /// </summary>
        /// <returns>A formatted string for printing</returns>
        public override string ToString()
        {
            return String.Format("| {0} | {1,15} | {2,10} |  ", base.ToString(), IdNumber, StudyStartYear);
        }
        /// <summary>
        /// Finds what course the student is attending
        /// </summary>
        /// <returns>The course the student is attending</returns>
        public int Course()
        {
            return DateTime.Now.Year - this.StudyStartYear;
        }
        /// <summary>
        /// Compares two students by their course then by their surname and finally by their name
        /// </summary>
        /// <param name="other">The information of the other student</param>
        /// <returns></returns>
        public int CompareTo(Student other)
        {
            if(Course() > other.Course())
            {
                if(base.CompareTo(other) > 0)
                {
                    return 1;
                }
            }
            return -1;
        }
        /// <summary>
        /// Finds if two students are identical
        /// </summary>
        /// <param name="other">The information of the other student</param>
        /// <returns>True if they are equal, false if not</returns>
        public bool Equals(Student other)
        {
            if (base.Equals(other) && IdNumber == other.IdNumber && StudyStartYear == other.StudyStartYear)
            {
                return true;
            }
            return false;
        }
    }
}