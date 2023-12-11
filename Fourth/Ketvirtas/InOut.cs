using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;

namespace Ketvirtas
{
    public class InOut
    {
        /// <summary>
        /// Reads the data file to a list of members
        /// </summary>
        /// <param name="fileName">The path of the data file</param>
        /// <returns>A list of all members in the data file</returns>
        public static Representatives ReadMembers(string fileName)
        {
            string line;
            Representatives members = new Representatives();
            using (StreamReader sr = new StreamReader(fileName))
            {
                int year = int.Parse(sr.ReadLine());
                members.AddYear(year);
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    string surname = parts[0];
                    string name = parts[1];
                    DateTime birthDate = DateTime.Parse(parts[2]);
                    int number = int.Parse(parts[3]);
                    
                    if (!int.TryParse(parts[5],out _))
                    {
                        int jobStartYear = int.Parse(parts[4]);
                        string jobSite = parts[5];
                        Graduate grad = new Graduate(surname, name, birthDate, number, jobStartYear, jobSite);
                        members.Add(grad);
                    }
                    else
                    {
                        int IdNumber = int.Parse(parts[4]);
                        int studyStartYear = int.Parse(parts[5]);
                        Student stud = new Student(surname, name, birthDate, number, IdNumber, studyStartYear);
                        members.Add(stud);
                    }
                }
            }
            return members;
        }
        /// <summary>
        /// Reads the directory for files that fit the given parameters
        /// </summary>
        /// <param name="directory">A directory where all of the files are</param>
        /// <returns>A list of all representatives</returns>
        public static AllRepresentatives ReadFiles(DirectoryInfo directory)
        {
            AllRepresentatives all = new AllRepresentatives();
            string[] files = Directory.GetFiles(directory.FullName, "*.txt");

            foreach(string file in files)
            {
                Representatives reps = ReadMembers(file);
                all.Add(reps);
            }
            return all;
        }
        /// <summary>
        /// Prints the list of representatives to a CSV file
        /// </summary>
        /// <param name="fileName">The path where the result file is located</param>
        /// <param name="members">a list of all representatives</param>
        public static void PrintToCSVAll(string fileName, AllRepresentatives members)
        {
            using(var writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < members.Count(); i++)
                {
                    writer.WriteLine(members.Get(i).GetYear());
                    for (int j = 0; j < members.Get(i).Count(); j++)
                    {
                        Member member = members.Get(i).Get(j);
                        if(member is Student stud)
                        {
                            writer.WriteLine(stud.ToStringCSV());
                        }
                        if(member is Graduate grad)
                        {
                            writer.WriteLine(grad.ToStringCSV());
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Prints the list of members to a CSV file
        /// </summary>
        /// <param name="fileName">The path where the result file is located</param>
        /// <param name="members">A list of members</param>
        public static void PrintToCSVSingle(string fileName, Representatives members)
        {
            using (var writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < members.Count(); i++)
                {
                    Member member = members.Get(i);
                    if (member is Student stud)
                    {
                        writer.WriteLine(stud.ToStringCSV());
                    }
                    if (member is Graduate grad)
                    {
                        writer.WriteLine(grad.ToStringCSV());
                    }
                }
                
            }
        }
    }
}