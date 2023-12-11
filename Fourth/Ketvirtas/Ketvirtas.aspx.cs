using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ketvirtas
{
    public partial class Ketvirtas : System.Web.UI.Page
    {
        AllRepresentatives allMembers = new AllRepresentatives();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Start_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////////////////
            //Read files and print starting data
            Directory.CreateDirectory(Server.MapPath(@"\Data\"));
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath(@"\Data\"));

            try
            {
                
                if(FileUpload1.PostedFiles.Count == 1 && FileUpload1.PostedFiles[0].ContentLength == 0)
                {
                    throw new Exception("Nera ikeltu failu");
                }

                foreach(HttpPostedFile file in FileUpload1.PostedFiles)
                {
                    file.SaveAs(directory + file.FileName);
                }
            }
            catch(Exception ex)
            {
                if(directory.GetFiles().Length == 0)
                {
                    Label1.Enabled = true;
                    Label1.Text = ex.Message;
                }
            }
            allMembers = InOut.ReadFiles(directory);
            PrintMembers(allMembers, startingData);

            //////////////////////////////////////////////////////////////////////////////
            //Find and print to a .csv file students or graduates that studied or worked for more than two years
            AllRepresentatives TwoYears = Task.TwoYearsList(allMembers);
            InOut.PrintToCSVAll(directory+"Patirtis.csv", TwoYears);

            //////////////////////////////////////////////////////////////////////////////
            //Find and print which month will have the most birthdays
            int count = 0;
            int birthDayMonth = Task.MostBirthdays(allMembers, out count) ;
            try
            {
                if (count == 1)
                {
                    throw new Exception("Visu gimtadieniai yra skirtingais menesiais");
                }
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                cell1.Text = "Daugiausiai gimtadieniu bus svenciama :";
                cell2.Text = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(birthDayMonth);
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                result.Rows.Add(row);
            }
            catch(Exception ex)
            {
                Label1.Enabled=true;
                Label1.Text=ex.Message;
            }

            //////////////////////////////////////////////////////////////////////////////
            //Find and sort the representatives seniors list
            Representatives seniorList = Task.SeniorsList(allMembers);
            InOut.PrintToCSVSingle(directory + "Senjorai.csv", seniorList);


            //////////////////////////////////////////////////////////////////////////////
            //Find and sort the representatives seniors list
            Representatives seniorsThisYear = Task.SeniorsThisYear(allMembers);
            InOut.PrintToCSVSingle(directory + "Seniai.csv", seniorsThisYear);
        }
        /// <summary>
        /// Creates a header for printing the data of students
        /// </summary>
        /// <param name="table">The needed table</param>
        private void CreateStudentTableHeader(Table table)
        {
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell4 = new TableCell();
            TableCell cell5 = new TableCell();
            TableCell cell6 = new TableCell();

            cell1.Text = "Pavarde";
            row.Cells.Add(cell1);
            cell2.Text = "Vardas";
            row.Cells.Add(cell2);
            cell3.Text = "Gimimo data";
            row.Cells.Add(cell3);
            cell4.Text = "Telefono numeris";
            row.Cells.Add(cell4);
            cell5.Text = "Studento pavymejimo numeris";
            row.Cells.Add(cell5);
            cell6.Text = "Studiju pradzios metai";
            row.Cells.Add(cell6);
            table.Rows.Add(row);
        }
        /// <summary>
        /// Creates a header for printing the data of graduates
        /// </summary>
        /// <param name="table">The needed table</param>
        private void CreateGraduateTableHeader(Table table)
        {
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            TableCell cell4 = new TableCell();
            TableCell cell5 = new TableCell();
            TableCell cell6 = new TableCell();

            cell1.Text = "Pavarde";
            row.Cells.Add(cell1);
            cell2.Text = "Vardas";
            row.Cells.Add(cell2);
            cell3.Text = "Gimimo data";
            row.Cells.Add(cell3);
            cell4.Text = "Telefono numeris";
            row.Cells.Add(cell4);
            cell5.Text = "Darbo pradzios metai";
            row.Cells.Add(cell5);
            cell6.Text = "Darboviete";
            row.Cells.Add(cell6);
            table.Rows.Add(row);
        }
        /// <summary>
        /// Prints the members to a table
        /// </summary>
        /// <param name="allMembers">A list of representatives filled with members</param>
        /// <param name="table">A given table to print the information to</param>
        private void PrintMembers(AllRepresentatives allMembers, Table table)
        {
            try
            {
                if (allMembers.ContainsType<Student>())
                {
                    CreateStudentTableHeader(table);
                    for (int i = 0; i < allMembers.Count(); i++)
                    {
                        PrintStudents(allMembers.Get(i), table);
                    }
                }
                else
                {
                    throw new Exception("Sarase nera studentu.");
                }

                if (allMembers.ContainsType<Graduate>())
                {
                    CreateGraduateTableHeader(table);
                    for (int i = 0; i < allMembers.Count(); i++)
                    {
                        PrintGraduates(allMembers.Get(i), table);
                    }
                }
                else
                {
                    throw new Exception("Sarase nera seniu.");
                }
            }
            catch(Exception ex)
            {
                Label1.Enabled = true;
                Label1.Text = ex.Message;
            }
        }
        
        /// <summary>
        /// Prints the students to a given table
        /// </summary>
        /// <param name="members">The information of members</param>
        /// <param name="table">The given table to print the information to</param>
        private void PrintStudents(Representatives members, Table table)
        {
            
            for (int i = 0; i < members.Count(); i++)
            {
                Member member = members.Get(i);
                if (member is Student)
                {
                    Student stud = (Student)member;
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    TableCell cell5 = new TableCell();
                    TableCell cell6 = new TableCell();

                    cell1.Text = stud.Surname;
                    row.Cells.Add(cell1);
                    cell2.Text = stud.Name;
                    row.Cells.Add(cell2);
                    cell3.Text = String.Format("{0:yyyy/MM/dd}",stud.birthDate);
                    row.Cells.Add(cell3);
                    cell4.Text = stud.Number.ToString();
                    row.Cells.Add(cell4);
                    cell5.Text = stud.IdNumber.ToString();
                    row.Cells.Add(cell5);
                    cell6.Text = stud.StudyStartYear.ToString();
                    row.Cells.Add(cell6);
                    table.Rows.Add(row);
                }
            }
        }
        /// <summary>
        /// Prints the graduates to a given table
        /// </summary>
        /// <param name="members">The information of members</param>
        /// <param name="table">The given table to print the information to</param>
        private void PrintGraduates(Representatives members, Table table)
        {
            for (int i = 0; i < members.Count(); i++)
            {
                Member member = members.Get(i);
                if (member is Graduate)
                {
                    Graduate grad = (Graduate)member;
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    TableCell cell5 = new TableCell();
                    TableCell cell6 = new TableCell();

                    cell1.Text = grad.Surname;
                    row.Cells.Add(cell1);
                    cell2.Text = grad.Name;
                    row.Cells.Add(cell2);
                    cell3.Text = String.Format("{0:yyyy/MM/dd}", grad.birthDate);
                    row.Cells.Add(cell3);
                    cell4.Text = grad.Number.ToString();
                    row.Cells.Add(cell4);
                    cell5.Text = grad.JobStartYear.ToString();
                    row.Cells.Add(cell5);
                    cell6.Text = grad.JobPlace.ToString();
                    row.Cells.Add(cell6);
                    table.Rows.Add(row);
                }
            }
        }
    }
}