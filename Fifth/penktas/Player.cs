using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace penktas
{
    public class Player
    {
        public DateTime date { get; set; }
        public string TeamName { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int PlayedMinutes { get; set; }
        public int ScoredPoints { get; set; }
        public int MadeFaults { get; set; }

        /// <summary>
        /// A constructor for getting the values to the class
        /// </summary>
        /// <param name="date">The date of the match</param>
        /// <param name="teamName">The name of the team</param>
        /// <param name="surname">The surname of the player</param>
        /// <param name="name">The name of the player</param>
        /// <param name="playedMinutes">The count of minutes played</param>
        /// <param name="scoredPoints">The scored points of a player</param>
        /// <param name="madeFaults">The count of faults made</param>
        public Player(DateTime date,string teamName, string surname, string name, int playedMinutes, int scoredPoints, int madeFaults)
        {
            this.date = date;
            this.TeamName = teamName;
            this.Surname = surname;
            this.Name = name;
            this.PlayedMinutes = playedMinutes;
            this.ScoredPoints = scoredPoints;
            this.MadeFaults = madeFaults;
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