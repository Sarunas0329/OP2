using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ketvirtas
{
    public class Task
    {
        /// <summary>
        /// Creates a new list of representatives of members that have worked or studied for two or more years
        /// </summary>
        /// <param name="members">A list of representatives</param>
        /// <returns>A new list of representatives that fit the given two year requirement</returns>
        public static AllRepresentatives TwoYearsList(AllRepresentatives members)
        {
            AllRepresentatives TwoYearsMembers = new AllRepresentatives();
            for (int i = 0; i < members.Count(); i++)
            {
                Representatives TwoYears = new Representatives();
                TwoYears.AddYear(members.Get(i).GetYear());

                for (int j = 0; j < members.Get(i).Count(); j++)
                {
                    Member member = members.Get(i).Get(j);
                    if(member is Student stud)
                    {
                        if(TwoYears.GetYear() - stud.StudyStartYear > 2)
                        {
                            TwoYears.Add(stud);
                        }
                    }
                    if(member is Graduate grad) 
                    {
                        if (TwoYears.GetYear() - grad.JobStartYear > 2)
                        {
                            TwoYears.Add(grad);
                        }
                    }
                }
                TwoYearsMembers.Add(TwoYears);
            }
            return TwoYearsMembers;
        }
        /// <summary>
        /// Finds the month that the most birthdate take part in
        /// </summary>
        /// <param name="members">A list of representatives</param>
        /// <param name="maxCount">The count of birthdays in the month</param>
        /// <returns>The month number that most birthdays take place in</returns>
        public static int MostBirthdays(AllRepresentatives members, out int maxCount)
        {
            maxCount = 0;
            int month = 0;
            int count;

            for (int i = 0; i < 12; i++)
            {
                count = 0;
                for (int c = 0; c < members.Count(); c++)
                {
                    for (int j = 0; j < members.Get(c).Count(); j++)
                    {
                        if (members.Get(c).Get(j).birthDate.Month == i)
                        {
                            count++;
                        }
                    }
                }
                if(maxCount < count)
                {
                    maxCount = count;
                    month = i;
                }
            }
            return month;
        }
        /// <summary>
        /// Creates a list of members that are seniors, which means that students that are 22 years old and graduates that are 25 years old or more
        /// </summary>
        /// <param name="members">A list of representatives</param>
        /// <returns>The list of seniors</returns>
        public static Representatives SeniorsList(AllRepresentatives members)
        {
            Representatives seniorList = new Representatives();
            for (int i = 0; i < members.Count(); i++)
            {
                for (int j = 0; j < members.Get(i).Count(); j++)
                {
                    Member member = members.Get(i).Get(j);
                    if (member is Student stud)
                    {
                        if (stud.Age() > 22)
                        {
                            seniorList.Add(stud);
                        }
                    }
                    if (member is Graduate grad)
                    {
                        if (grad.Age() > 25)
                        {
                            seniorList.Add(grad);
                        }
                    }
                }
            }
            return seniorList;
        }
        /// <summary>
        /// Creates a list of students that finished university this year
        /// </summary>
        /// <param name="members">A list of representatives</param>
        /// <returns>The list of students</returns>
        public static Representatives SeniorsThisYear(AllRepresentatives members)
        {
            Representatives Seniors = new Representatives();
            for (int i = 0; i < members.Count(); i++)
            {
                for (int j = 0; j < members.Get(i).Count(); j++)
                {
                    Member member = members.Get(i).Get(j);
                    if (member is Student stud)
                    {
                        if (stud.Course() == 4)
                        {
                            Seniors.Add(stud);
                        }
                    }
                }
            }
            return Seniors;
        }
    }
}