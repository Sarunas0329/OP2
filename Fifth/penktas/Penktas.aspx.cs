using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace penktas
{
    public partial class Penktas : System.Web.UI.Page
    {
        public const string Result = "Result.txt"; 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Start_Click(object sender, EventArgs e)
        {
            startException.Enabled = false;
            if (File.Exists(Server.MapPath($"~/Result_Data/{Result}")))
            {
                File.Delete(Server.MapPath($"~/Result_Data/{Result}"));
            }
            Directory.CreateDirectory(Server.MapPath($"~/Result_Data/"));
            try
            {
                if(!Directory.Exists(Server.MapPath($"~/App_Data/Players/")))
                {
                    throw new Exception("Zaideju failas neegzistuoja");
                }
                if (!Directory.Exists(Server.MapPath($"~/App_Data/Positions/")))
                {
                    throw new Exception("Poziciju failas neegzistuoja");
                }
                if(Directory.GetFiles(Server.MapPath($"~/App_Data/Players/"), "*txt").Count() == 0)
                {
                    throw new Exception("Zaideju aplanke nera duomenu failu");
                }
                if (Directory.GetFiles(Server.MapPath($"~/App_Data/Positions/"), "*txt").Count() == 0)
                {
                    throw new Exception("Poziciju aplanke nera duomenu failu");
                }
                string[] playerFiles = Directory.GetFiles(Server.MapPath($"~/App_Data/Players1/"), "*txt");
                if(FilesHaveData(playerFiles))
                {
                    List<List<Player>> matches = InOut.ReadMatches(playerFiles);
                    InOut.PrintMatches(matches, Server.MapPath($"~/Result_Data/{Result}"));
                    Session["matches"] = matches;
                    PrintMatches(matches, startingData);
                }
                string[] positionFiles = Directory.GetFiles(Server.MapPath($"~/App_Data/Positions1/"), "*txt");
                if(positionFiles.Count() > 1)
                {
                    throw new Exception("Per daug poziciju failu.");
                }
                if(FilesHaveData(positionFiles))
                {
                    List<Positions> positions = InOut.ReadPositions(positionFiles);
                    InOut.PrintPositions(positions, Server.MapPath($"~/Result_Data/{Result}"));
                    Session["positions"] = positions;
                    PrintPositionsToTable(positions, startingDataPos);
                }
            }
            catch(Exception ex)
            {
                startException.Enabled = true;
                startException.Text = ex.Message;
            }
        }

        protected void Result_Click(object sender, EventArgs e)
        {
            posException.Enabled = false;
            countException.Enabled = false;
            fromException.Enabled = false;
            toException.Enabled = false;
            resultException.Enabled = false;
            Start_Click(sender, e);
            List<List<Player>> matches = (List<List<Player>>)Session["matches"]; 
            List<Positions> positions = (List<Positions>)Session["positions"];
            try
            {
                if(PosTextBox.Text == "")
                {
                    posException.Enabled = true;
                    posException.Text = "Pozicijos teksto laukas yra tuscias";
                }
                string position = PosTextBox.Text.ToLower();
                if (position != "centras" && position != "puolejas" && position != "gynejas")
                {
                    posException.Enabled = true;
                    posException.Text = "Pozicijos teksto laukas nera teisingo formato.";
                }

                if (Period1Box.Text == "")
                {
                    fromException.Enabled = true;
                    fromException.Text = "Periodo pradzios datos teksto laukas yra tuscias";
                }
                if (!DateTime.TryParse(Period1Box.Text, out _))
                {
                    fromException.Enabled = true;
                    fromException.Text = "Periodo pradzios data yra neteisingo formato";
                }

                if (Period2Box.Text == "")
                {
                    toException.Enabled = true;
                    toException.Text = "Periodo pabaigos datos teksto laukas yra tuscias";
                }
                if (!DateTime.TryParse(Period2Box.Text, out _))
                {
                    toException.Enabled = true;
                    toException.Text = "Periodo pabaigos data yra neteisingo formato";
                }

                if (PlayerCountBox.Text == "")
                {
                    countException.Enabled = true;
                    countException.Text = "Zaideju kiekio teksto laukas yra tuscias";
                }
                if (!int.TryParse(PlayerCountBox.Text, out _))
                {
                    countException.Enabled = true;
                    countException.Text = "Kiekio teksto laukas yra neteisingo formato";
                }
                DateTime date1 = DateTime.Parse(Period1Box.Text);
                DateTime date2 = DateTime.Parse(Period2Box.Text);
                int playerCount = int.Parse(PlayerCountBox.Text);

                Task task = new Task(matches, positions);
                List<Player> requiredPlayers = task.CreatePlayerList(position, date1, date2, playerCount);
                if (requiredPlayers.Count > 0)
                {
                    InOut.PrintRequiredPlayers(requiredPlayers, Server.MapPath($"~/Result_Data/{Result}"));
                    PrintPlayersToResult(requiredPlayers, resultTable);
                }
                else
                {
                    throw new Exception("Nera zaideju pagal siuos reikalavimus");
                }
                if(requiredPlayers.Count != playerCount)
                {
                    throw new Exception("Nebuvo pakankamai zaideju pagal kiekio nurodymus");
                }
            }
            catch(Exception ex)
            {
                resultException.Enabled = true;
                resultException.Text = ex.Message;
            }
        }
        /// <summary>
        /// Checks if the given files have lines of data
        /// </summary>
        /// <param name="files">A list of all files</param>
        /// <returns>True if all of the files have data, false if at least one has no data</returns>
        /// <exception cref="Exception">finds which file has no data</exception>
        public bool FilesHaveData(string[] files)
        {
            foreach(string file in files)
            {
                if(!IfFileHasData(file))
                {
                    throw new Exception($"{file} yra tuscias.");
                }
            }
            return true;
        }
        /// <summary>
        /// Checks if a single file has any data
        /// </summary>
        /// <param name="file">The path to a data file</param>
        /// <returns>True if it has atleast a single line of data, false if not</returns>
        public bool IfFileHasData(string file)
        {
            using( StreamReader sr = new StreamReader(file ))
            {
                if(sr.ReadLine() != null)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Creates a table header for the data of players
        /// </summary>
        /// <param name="table">The table to put the header to</param>
        private void CreatePlayerTableHeader(Table table)
        {
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell4 = new TableCell();
            TableCell cell5 = new TableCell();
            TableCell cell6 = new TableCell();

            cell1.Text = "Team name";
            row.Cells.Add(cell1);
            cell2.Text = "Surname";
            row.Cells.Add(cell2);
            cell3.Text = "Name";
            row.Cells.Add(cell3);
            cell4.Text = "Minutes played";
            row.Cells.Add(cell4);
            cell5.Text = "Scored points";
            row.Cells.Add(cell5);
            cell6.Text = "Fault count";
            row.Cells.Add(cell6);
            table.Rows.Add(row);
        }
        /// <summary>
        /// Creates a table header for the data of players
        /// </summary>
        /// <param name="table">The table to put the header to</param>
        private void CreatePositionsTableHeader(Table table)
        {
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell4 = new TableCell();

            cell1.Text = "Team name";
            row.Cells.Add(cell1);
            cell2.Text = "Surname";
            row.Cells.Add(cell2);
            cell3.Text = "Name";
            row.Cells.Add(cell3);
            cell4.Text = "Position";
            row.Cells.Add(cell4);
            table.Rows.Add(row);
        }
        /// <summary>
        /// Prints the data of all players to the given table
        /// </summary>
        /// <param name="list">Information about all the players</param>
        /// <param name="table">The given table to print the data to</param>
        private void PrintMatches(List<List<Player>> list, Table table)
        {

            CreatePlayerTableHeader(table);
            for (int i = 0; i < list.Count(); i++)
            {
                PrintPlayersToTable(list[i], table);
            }
        }
        /// <summary>
        /// Prints the data of a single players list to the given table
        /// </summary>
        /// <param name="list">Information about the players</param>
        /// <param name="table">The given table to print the data to</param>
        public void PrintPlayersToTable(List<Player> list, Table table)
        {
            TableRow daterow = new TableRow();
            TableCell datecell = new TableCell();
            datecell.Text = list[0].date.ToString("yyyy/MM/dd");
            datecell.HorizontalAlign= HorizontalAlign.Center;
            datecell.ColumnSpan = 6;
            daterow.Cells.Add(datecell);
            table.Rows.Add(daterow);
            for (int i = 0; i < list.Count(); i++)
            {
                Player player = list[i];

                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                TableCell cell4 = new TableCell();
                TableCell cell5 = new TableCell();
                TableCell cell6 = new TableCell();

                cell1.Text = player.TeamName;
                row.Cells.Add(cell1);
                cell2.Text = player.Surname;
                row.Cells.Add(cell2);
                cell3.Text = player.Name;
                row.Cells.Add(cell3);
                cell4.Text = player.PlayedMinutes.ToString();
                row.Cells.Add(cell4);
                cell5.Text = player.ScoredPoints.ToString();
                row.Cells.Add(cell5);
                cell6.Text = player.MadeFaults.ToString();
                row.Cells.Add(cell6);
                table.Rows.Add(row);
            }
        }
        /// <summary>
        /// Prints the data of required players list to the result table
        /// </summary>
        /// <param name="list">Information about the players</param>
        /// <param name="table">The given table to print the data to</param>
        public void PrintPlayersToResult(List<Player> list, Table table)
        {
            CreatePlayerTableHeader(table);
            for (int i = 0; i < list.Count(); i++)
            {
                Player player = list[i];

                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                TableCell cell4 = new TableCell();
                TableCell cell5 = new TableCell();
                TableCell cell6 = new TableCell();

                cell1.Text = player.TeamName;
                row.Cells.Add(cell1);
                cell2.Text = player.Surname;
                row.Cells.Add(cell2);
                cell3.Text = player.Name;
                row.Cells.Add(cell3);
                cell4.Text = player.PlayedMinutes.ToString();
                row.Cells.Add(cell4);
                cell5.Text = player.ScoredPoints.ToString();
                row.Cells.Add(cell5);
                cell6.Text = player.MadeFaults.ToString();
                row.Cells.Add(cell6);
                table.Rows.Add(row);
            }
        }
        /// <summary>
        /// Prints the data of positions to the given table
        /// </summary>
        /// <param name="list">Information about the positions</param>
        /// <param name="table">The given table to print the data to</param>
        public void PrintPositionsToTable(List<Positions> list, Table table)
        {
            CreatePositionsTableHeader(table);
            for (int i = 0; i < list.Count(); i++)
            {
                Positions pos = list[i];

                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                TableCell cell4 = new TableCell();

                cell1.Text = pos.TeamName;
                row.Cells.Add(cell1);
                cell2.Text = pos.Surname;
                row.Cells.Add(cell2);
                cell3.Text = pos.Name;
                row.Cells.Add(cell3);
                cell4.Text = pos.Position;
                row.Cells.Add(cell4);
                table.Rows.Add(row);
            }
        }
    }
}