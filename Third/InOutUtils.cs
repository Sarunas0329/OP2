using System;
using System.IO;

namespace Antras
{
    public class InOutUtils
    {
        public static void ReadSubscribers(Stream fv, LinkedList<Subscriber> subscribers)
        {
            string line;
            using(var reader = new StreamReader(fv))
            {
                while((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(';');
                    string surname = values[0];
                    string adress = values[1];
                    int startMonth = int.Parse(values[2]);
                    int duration = int.Parse(values[3]);
                    int code = int.Parse(values[4]);
                    int count = int.Parse(values[5]);
                    Subscriber sub = new Subscriber(surname, adress, startMonth, duration, code, count);
                    subscribers.Add(sub);
                }
            }
        }
        public static void ReadPublishments(Stream fv, LinkedList<Publishment> publishments)
        {
            string line;
            using (var reader = new StreamReader(fv))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(';');
                    int code = int.Parse(values[0]);
                    string name = values[1];
                    decimal price = decimal.Parse(values[2]);
                    Publishment pub = new Publishment(code, name, price);
                    publishments.Add(pub);
                }
            }
        }
    }
}