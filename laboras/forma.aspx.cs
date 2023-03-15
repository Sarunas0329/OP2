using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Drawing.Printing;
using System.Security.Claims;

namespace laboras
{
    public partial class forma : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Domino> dominos= new List<Domino>();
            using (var stream = new StreamReader(FileUpload1.PostedFile.InputStream))
            {
                var text = stream.ReadToEnd();
                int countHigherValues = 0;
                int countMoreValues = 0;
                dominos = InOutUtils.Read(text);
                StartingData(text);
                if (dominos.Count != 7) 
                {
                    countMoreValues++;
                }

                if(countHigherValues == 0) 
                {
                    for (int i = 0; i < dominos.Count; i++)
                    {
                        if (dominos[i].A > 6 || dominos[i].B > 6)
                        {
                            countHigherValues++;
                        }
                    }
                    if (countHigherValues == 0)
                    {
                        Combinations(new List<Domino>(), dominos);
                    }

                    else
                    {
                        TableRow row = new TableRow();
                        TableCell cell = new TableCell();
                        cell.Text = "Neteisingai ivestos kauliuku reiksmes";
                        row.Cells.Add(cell);
                        Table2.Rows.Add(row);
                    }
                }
                else
                {
                    TableRow row = new TableRow();
                    TableCell cell = new TableCell();
                    cell.Text = "Ivesta per daug reiksmiu";
                    row.Cells.Add(cell);
                    Table2.Rows.Add(row);
                }

                if(Table2.Rows.Count == 0)
                {
                    TableRow row = new TableRow();
                    TableCell cell = new TableCell();
                    cell.Text = "Nera imanomu kombinaciju";
                    row.Cells.Add(cell);
                    Table2.Rows.Add(row);
                }
            }
        }
        /// <summary>
        /// Prints the starting data
        /// </summary>
        /// <param name="path">Location where to put the starting data</param>
        public void StartingData(string path)
        {

            TableRow Row = new TableRow();
            TableCell Cell = new TableCell();
            Cell.Text = string.Format("Pradiniai duomenys");
            Row.Cells.Add(Cell);
            Table1.Rows.Add(Row);

            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Text = string.Format(path);
            row.Cells.Add(cell);
            Table1.Rows.Add(row);

        }
        /// <summary>
        /// Prints a combination of dominoes
        /// </summary>
        /// <param name="chain">A chain of dominoes</param>
        public void Print(List<Domino> chain)
        {
            
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Text = string.Format(InOutUtils.JoinChain(chain));
            row.Cells.Add(cell);
            Table2.Rows.Add(row);
            
        }
        /// <summary>
        /// Finds all the possible 7 domino combinations using recursion
        /// </summary>
        /// <param name="chain">A chain of dominoes</param>
        /// <param name="list">A list of every domino</param>
        private void Combinations(List<Domino> chain, List<Domino> list)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                Domino dom = list[i];
                if (TaskUtils.Suitable(dom, chain))
                {
                    chain.Add(dom);
                    Domino saved = list[i];
                    list.RemoveAt(i);
                    Combinations(chain, list);
                    list.Insert(i, saved);
                    chain.RemoveAt(chain.Count - 1);
                }

                dom = dom.Invert();

                if (TaskUtils.Suitable(dom, chain))
                {
                    chain.Add(dom);

                    Domino saved = list[i];
                    list.RemoveAt(i);
                    Combinations(chain, list);
                    list.Insert(i, saved);
                    chain.RemoveAt(chain.Count - 1);
                }
            }
            if (chain.Count == 7)
            {
                Print(chain);
            }
        }
    }
}