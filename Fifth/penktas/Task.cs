using System;
using System.Collections.Generic;
using System.Linq;

namespace penktas
{
    public class Task
    {
        private List<List<Player>> matches;
        private List<Positions> positions;
        /// <summary>
        /// A constructor for getting the lists to the class
        /// </summary>
        /// <param name="matches">A list of all player lists</param>
        /// <param name="positions">A list of positions</param>
        public Task(List<List<Player>> matches, List<Positions> positions)
        {
            this.matches = matches;
            this.positions = positions;
        }
        /// <summary>
        /// Creates a player list by the given requirements
        /// </summary>
        /// <param name="position">The given position of the player</param>
        /// <param name="datefrom">The first date of the time period</param>
        /// <param name="dateto">The second date of the time period</param>
        /// <param name="count">The given count of players needed</param>
        /// <returns>A list of required players</returns>
        public List<Player> CreatePlayerList(string position, DateTime datefrom, DateTime dateto, int count)
        {
            List<Player> reqList = CreateRequired(position,datefrom,dateto);
            List<Player> mostUsefulList = CreateMostUseful(reqList);
            var list = from player in mostUsefulList.Take(count)
                       orderby player.TeamName, player.Name
                       select player;
            return list.ToList();
        }
        /// <summary>
        /// Sorts the list with the most useful players being first
        /// </summary>
        /// <param name="list">A list of players</param>
        /// <returns>The sorted player list</returns>
        public List<Player> CreateMostUseful(List<Player> list)
        {
            var newlist = from Match in list
                       orderby Match.ScoredPoints ascending, Match.PlayedMinutes descending, Match.MadeFaults descending
                       select Match;
            return newlist.ToList();
        }
        /// <summary>
        /// Creates a list of players that fit the given requirements
        /// </summary>
        /// <param name="pos">The given position of the player</param>
        /// <param name="datefrom">The first date of the time period</param>
        /// <param name="dateto">The second date of the time period</param>
        /// <returns>The list of required players</returns>
        public List<Player> CreateRequired(string pos, DateTime datefrom, DateTime dateto)
        {
            var list = from Match in matches.SelectMany(x => x)
                       from Positions in positions
                       where Match.Equals(Positions) && Positions.Position == pos && Match.date > datefrom && Match.date < dateto
                       select Match;
            return list.ToList();
        }
    }
}