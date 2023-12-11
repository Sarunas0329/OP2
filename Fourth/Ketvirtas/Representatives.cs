using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Ketvirtas
{
    public class Representatives
    {
        public List<Member> members;
        public int year;

        /// <summary>
        /// An empty constructor
        /// </summary>
        public Representatives()
        {
            members = new List<Member>();
        }
        /// <summary>
        /// A constructor to change the current values, changes the whole list to the given parameter list
        /// </summary>
        /// <param name="newMembers">A list of new members that will be in the list</param>
        public Representatives(List<Member> newMembers)
        {
            this.members = newMembers;
        }

        /// <summary>
        /// Adds a member to the list
        /// </summary>
        /// <summary>
        /// Creates a header for printing the data of students
        /// </summary>
        /// <param name="table">The needed table</param>
        public void Add(Member member)
        {
            members.Add(member);
        }
        /// <summary>
        /// Adds the current year given in the data file
        /// </summary>
        /// <param name="year">The year of the data file</param>
        public void AddYear(int year) 
        {
            this.year = year;
        }
        /// <summary>
        /// Gets the needed member from the list
        /// </summary>
        /// <param name="index">The index location of the member in the list</param>
        /// <returns>The information of the member</returns>
        public Member Get(int index)
        {
            return members[index];
        }
        /// <summary>
        /// Gets the year in this list
        /// </summary>
        /// <returns>The year that was given in the list</returns>
        public int GetYear()
        {
            return year;
        }
        /// <summary>
        /// Finds the count of members in the list
        /// </summary>
        /// <returns>The count of members</returns>
        public int Count()
        {
            return members.Count;
        }
        /// <summary>
        /// Sorts the list by the compareTo method using bubble sort
        /// </summary>
        public void Sort()
        {
            for (int i = 0; i < Count(); i++)
            {
                int smallest = i;
                for (int j = i + 1; j < Count(); j++) 
                {
                    if (members[j].CompareTo(members[smallest]) > 0)
                    {
                        smallest = j;
                    }
                }

                Member temp = members[smallest];
                members[smallest] = members[i];
                members[i] = temp;
            }
        }
    }
}