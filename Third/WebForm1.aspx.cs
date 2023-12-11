using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace Antras
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        LinkedList<Subscriber> subscribers = new LinkedList<Subscriber>();
        LinkedList<Publishment> publishments = new LinkedList<Publishment>();
        LinkedList<Publishment> lessThanAveragePublishments = new LinkedList<Publishment>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// A button that starts the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Start_Click(object sender, EventArgs e)
        {
            InOutUtils.ReadSubscribers(FileUpload1.PostedFile.InputStream, subscribers);
            InOutUtils.ReadPublishments(FileUpload2.PostedFile.InputStream, publishments);
            if (TaskUtils.HasInformation(subscribers, publishments))
            {
                if (!TaskUtils.IncorrectDuration(subscribers))
                {
                    PrintSubscribersData(subscribers, startingData);
                    PrintPublishmentData(publishments, startingData);
                    List<int> yearProfits = TaskUtils.MonthlyProfitList(subscribers, publishments);
                    for (int i = 0; i < 12; i++)
                    {
                        TableRow rowHead = new TableRow();
                        TableCell cellHead = new TableCell();
                        if (yearProfits[i] != 0)
                        {
                            cellHead.Text = string.Format("{0} {1}", i + 1, yearProfits[i]);
                        }
                        else
                        {
                            cellHead.Text = "Joks leidinys neprenumeruotas siam menesiui";
                        }
                        rowHead.Cells.Add(cellHead);
                        results.Rows.Add(rowHead);
                    }
                    TaskUtils.LessThanAverage(publishments, subscribers, lessThanAveragePublishments);
                    lessThanAveragePublishments.Sort();
                    TableRow row = new TableRow();
                    TableCell cell = new TableCell();
                    cell.Text = "Leidiniai kuriu pajamos mazesnes nei vidutines: ";
                    row.Cells.Add(cell);
                    results.Rows.Add(row);
                    PrintPublishmentData(lessThanAveragePublishments, results);
                }
                else
                {
                    TableRow row = new TableRow();
                    TableCell cell = new TableCell();
                    cell.Text = "Per ilgas prenumeratos laikotarpis.";
                    row.Cells.Add(cell);
                    results.Rows.Add(row);
                }
            }
            else
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.Text = "Nera informacijos duomenu failuose.";
                row.Cells.Add(cell);
                results.Rows.Add(row);
            }
        }
        /// <summary>
        /// A button that when pressed creates a list of subscribers of a specific publishment at a month 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void createListButton_Click(object sender, EventArgs e)
        {
            InOutUtils.ReadSubscribers(FileUpload1.PostedFile.InputStream, subscribers);
            InOutUtils.ReadPublishments(FileUpload1.PostedFile.InputStream, publishments);
            if (!TaskUtils.HasWords(codeText.Text) && !TaskUtils.HasWords(monthText.Text))
            {
                
                int code = int.Parse(codeText.Text);
                if (TaskUtils.PublishmentExist(publishments, code))
                {
                    int month = int.Parse(monthText.Text);
                    if (month < 12 || month > 1)
                    {
                        LinkedList<Subscriber> newList = TaskUtils.SubscriberList(code, month, subscribers, publishments);
                        if (newList.Count() > 0)
                        {
                            PrintSubscribersData(newList, newListTable);
                        }
                        else
                        {
                            TableRow rowHead = new TableRow();
                            TableCell cellHead = new TableCell();
                            cellHead.Text = string.Format("Niekas nera prenumeraves");
                            rowHead.Cells.Add(cellHead);
                            newListTable.Rows.Add(rowHead);
                        }
                    }
                    else
                    {
                        TableRow rowHead = new TableRow();
                        TableCell cellHead = new TableCell();
                        cellHead.Text = string.Format("Toks menesis neegzistuoja");
                        rowHead.Cells.Add(cellHead);
                        newListTable.Rows.Add(rowHead);
                    }
                }
                else
                {
                    TableRow rowHead = new TableRow();
                    TableCell cellHead = new TableCell();
                    cellHead.Text = string.Format("Toks leidinys neegzistuoja");
                    rowHead.Cells.Add(cellHead);
                    newListTable.Rows.Add(rowHead);
                }
            }
            else
            {
                TableRow rowHead = new TableRow();
                TableCell cellHead = new TableCell();
                cellHead.Text = string.Format("Neteisinga ivestis, tik skaiciai");
                rowHead.Cells.Add(cellHead);
                newListTable.Rows.Add(rowHead);
            }
        }
        /// <summary>
        /// Prints subscribers to a table 
        /// </summary>
        /// <param name="subscribers">A list of subscribers</param>
        /// <param name="table">A table of data</param>
        public void PrintSubscribersData(LinkedList<Subscriber> subscribers, Table table)
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
            cell2.Text = "Adresas";
            row.Cells.Add(cell2);
            cell3.Text = "Pradinis menesis";
            row.Cells.Add(cell3);
            cell4.Text = "Prenumeratos ilgis";
            row.Cells.Add(cell4);
            cell5.Text = "Leidinio kodas";
            row.Cells.Add(cell5);
            cell6.Text = "Leidiniu kiekis";
            row.Cells.Add(cell6);
            table.Rows.Add(row);

            for (subscribers.Begin(); subscribers.Exist(); subscribers.Next())
            {
                Subscriber sub = subscribers.Get();
                row = new TableRow();
                cell1 = new TableCell();
                cell2 = new TableCell();
                cell3 = new TableCell();
                cell4 = new TableCell();
                cell5 = new TableCell();
                cell6 = new TableCell();

                cell1.Text = sub.Surname;
                row.Cells.Add(cell1);
                cell2.Text = sub.Adress;
                row.Cells.Add(cell2);
                cell3.Text = sub.StartingMonth.ToString();
                row.Cells.Add(cell3);
                cell4.Text = sub.Duration.ToString();
                row.Cells.Add(cell4);
                cell5.Text = sub.Code.ToString();
                row.Cells.Add(cell5);
                cell6.Text = sub.Count.ToString();
                row.Cells.Add(cell6);
                table.Rows.Add(row);
            }
        }
        /// <summary>
        /// Prints publishments to a table 
        /// </summary>
        /// <param name="publishments">A list of publishments</param>
        /// <param name="table">A table of data</param>
        public void PrintPublishmentData(LinkedList<Publishment> publishments, Table table)
        {
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            cell1.Text = "Leidinio kodas";
            row.Cells.Add(cell1);
            cell2.Text = "Leidinio Pavadinimas";
            row.Cells.Add(cell2);
            cell3.Text = "Leidinio kaina";
            row.Cells.Add(cell3);
            table.Rows.Add(row);

            for (publishments.Begin(); publishments.Exist(); publishments.Next())
            {
                Publishment pub = publishments.Get();
                row = new TableRow();
                cell1 = new TableCell();
                cell2 = new TableCell();
                cell3 = new TableCell();

                cell1.Text = pub.Code.ToString();
                row.Cells.Add(cell1);
                cell2.Text = pub.Name;
                row.Cells.Add(cell2);
                cell3.Text = pub.Price.ToString();
                row.Cells.Add(cell3);
                table.Rows.Add(row);
            }
        }
    }
}