using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace penktas
{
    public class Positions
    {
        public string TeamName { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        /// <summary>
        /// A constructor for getting the values to the class
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        /// <param name="surname">The surname of the player</param>
        /// <param name="name">The name of the player</param>
        /// <param name="position">The position of the player</param>
        public Positions(string teamName, string surname, string name, string position)
        {
            this.TeamName = teamName;
            this.Surname = surname;
            this.Name = name;
            this.Position = position;
        }
        /// <summary>
        /// Finds if two objects are equal
        /// </summary>
        /// <param name="obj">The other object that is used to compare</param>
        /// <returns>True if they are equal, false if not</returns>
        public override bool Equals(object obj)
        {
            return obj is Positions positions &&
                   TeamName == positions.TeamName &&
                   Surname == positions.Surname &&
                   Name == positions.Name;
        }

        /// <summary>
        /// Gets the hash code of the needed values
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode()
        {
            int hashCode = -539194669;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TeamName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}