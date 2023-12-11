using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI.WebControls;

namespace penktas
{
    public class InOut
    {
        /// <summary>
        /// Reads all of the data files to a list of player lists 
        /// </summary>
        /// <param name="paths">All different file paths</param>
        /// <returns>The list with all the data</returns>
        public static List<List<Player>> ReadMatches(string[] paths)
        {
            List<List<Player>> matches = new List<List<Player>>();
            foreach(string path in paths)
            {
                matches.Add(ReadMatch(path));
            }            
            return matches;
        }

        /// <summary>
        /// Reads a single file path to a list of players
        /// </summary>
        /// <param name="path">A path of a single data file</param>
        /// <returns>A list of players</returns>
        public static List<Player> ReadMatch(string path)
        {
            List<Player> match = new List<Player>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {

                    string[] values = line.Split(';');
                    DateTime date = DateTime.Parse(values[0]);
                    string teamName = values[1];
                    string surname = values[2];
                    string name = values[3];
                    int playedMinutes = int.Parse(values[4]);
                    int scoredPoints = int.Parse(values[5]);
                    int faults = int.Parse(values[6]);
                    Player player = new Player(date,teamName,surname,name,playedMinutes,scoredPoints,faults);
                    match.Add(player);
                }
            }
            return match;
        }

        /// <summary>
        /// Reads the data file of positions
        /// </summary>
        /// <param name="paths">The paths of position files</param>
        /// <returns>A list of player positions</returns>
        public static List<Positions> ReadPositions(string[] paths)
        {
            List<Positions> positions = new List<Positions>();
            using (StreamReader sr = new StreamReader(paths[0]))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(';');
                    string teamName = values[0];
                    string surname = values[1];
                    string name = values[2];
                    string position = values[3];
                    Positions pos = new Positions(teamName, surname, name, position);
                    positions.Add(pos);
                }
            }
            return positions;
        }

        /// <summary>
        /// Prints the list of player lists to the result file
        /// </summary>
        /// <param name="matches">A list of all player lists</param>
        /// <param name="path">The path to the result file</param>
        public static void PrintMatches(List<List<Player>> matches, string path)
        {
            using(StreamWriter sw = new StreamWriter(path,true))
            {
                sw.WriteLine(new string('-', 114));
                sw.WriteLine(String.Format("| {0,-15} | {1,-20} | {2,-15} | {3,-15} | {4,-15} | {5,-15} |", "Team name", "Surname", "Name", "Played minutes", "Scored points", "Earned faults"));
                sw.WriteLine(new string('-', 114));
                foreach (List<Player> match in matches)
                {
                    for (int i = 0; i < match.Count(); i++)
                    {
                        Player play = match[i];
                        sw.WriteLine(String.Format("| {0,-15} | {1,-20} | {2,-15} | {3,15} | {4,15} | {5,15} |", play.TeamName, play.Surname, play.Name, play.PlayedMinutes, play.ScoredPoints, play.MadeFaults));
                    }
                }
                sw.WriteLine(new string('-', 114));
            }
        }
        /// <summary>
        /// Prints the list of positions to the result file
        /// </summary>
        /// <param name="positions">A list of positions</param>
        /// <param name="path">The path to the result file</param>
        public static void PrintPositions(List<Positions> positions, string path)
        {
            using (StreamWriter sw = new StreamWriter(path,true))
            {
                sw.WriteLine(new string('-', 78));
                sw.WriteLine(String.Format("| {0,-15} | {1,-20} | {2,-15} | {3,-15} |", "Team name", "Surname", "Name","Position"));
                sw.WriteLine(new string('-', 78));
                foreach (Positions pos in positions)
                {
                    sw.WriteLine(String.Format("| {0,-15} | {1,-20} | {2,-15} | {3,-15} |", pos.TeamName, pos.Surname, pos.Name, pos.Position));
                }
                sw.WriteLine(new string('-', 78));
            }
        }

        /// <summary>
        /// Prints the required player list to the result file
        /// </summary>
        /// <param name="players">The list of required players</param>
        /// <param name="path">The path to the result file</param>
        public static void PrintRequiredPlayers(List<Player> players, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(new string('-', 114));
                sw.WriteLine(String.Format("| {0,-15} | {1,-20} | {2,-15} | {3,-15} | {4,-15} | {5,-15} |", "Team name", "Surname", "Name", "Played minutes", "Scored points", "Earned faults"));
                sw.WriteLine(new string('-', 114));
                for (int i = 0; i < players.Count(); i++)
                {
                    Player play = players[i];
                    sw.WriteLine(String.Format("| {0,-15} | {1,-20} | {2,-15} | {3,15} | {4,15} | {5,15} |", play.TeamName, play.Surname, play.Name, play.PlayedMinutes, play.ScoredPoints, play.MadeFaults));
                }
                sw.WriteLine(new string('-', 114));
            }
        }
    }
}