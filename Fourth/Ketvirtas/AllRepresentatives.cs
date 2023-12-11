using System;
using System.Collections.Generic;

namespace Ketvirtas
{
    public class AllRepresentatives
    {
        public List<Representatives> contents;

        /// <summary>
        /// An empty constructor
        /// </summary>
        public AllRepresentatives()
        {
            contents = new List<Representatives>();
        }
        /// <summary>
        /// Adds a representative to the representative list
        /// </summary>
        /// <param name="reps">The information of the new representative</param>
        public void Add(Representatives reps)
        {
            contents.Add(reps);
        }
        /// <summary>
        /// Gets the needed representative from the list
        /// </summary>
        /// <param name="index">The index location of the representative</param>
        /// <returns>The information of the wanted representative</returns>
        public Representatives Get(int index)
        {
            return contents[index];
        }
        /// <summary>
        /// Gets the count of representatives in this list
        /// </summary>
        /// <returns>The count of representatives</returns>
        public int Count()
        {
            return contents.Count;
        }
        /// <summary>
        /// Find if the list contains a special type of object
        /// </summary>
        /// <typeparam name="type">The type of object, Student or Graduate</typeparam>
        /// <returns>True if it exists in the list, false if not</returns>
        public bool ContainsType<type>()
        {
            int count = 0;
            for (int i = 0; i < Count(); i++)
            {
                for (int j = 0; j < contents[i].Count(); j++)
                {
                    Member member = contents[i].Get(j);
                    if(member.GetType() == typeof(type))
                    {
                        count++;
                    }
                }
            }
            return count != 0;
        }
    }
}