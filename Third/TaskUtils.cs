using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Antras
{
    public class TaskUtils
    {
        /// <summary>
        /// Creates a list of the most profitable publishments every month
        /// </summary>
        /// <param name="subscribers">A list of subscribers</param>
        /// <param name="publishments">A list of publishments</param>
        /// <returns>The created list</returns>
        public static List<int> MonthlyProfitList(LinkedList<Subscriber> subscribers, LinkedList<Publishment> publishments)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 12; i++)
            {
                int profit = MostProfitableMonthly(subscribers, publishments, i + 1);
                if (profit != 0)
                {
                    list.Add(profit);
                }
                else
                {
                    list.Add(0);
                }
                profit = 0;
            }
            return list;
        }
        /// <summary>
        /// Finds which publishment has made the most profit in a specific month
        /// </summary>
        /// <param name="subscribers">A list of subscribers</param>
        /// <param name="publish">A list of publishments</param>
        /// <param name="month">The specific month</param>
        /// <returns>The code of a publishment that made the most profit that month</returns>
        public static int MostProfitableMonthly(LinkedList<Subscriber> subscribers, LinkedList<Publishment> publish, int month)
        {
            int publishmentCode = 0;
            decimal max = 0;
            LinkedList<Subscriber> subs = subscribers.Copy();
            for (subscribers.Begin(); subscribers.Exist(); subscribers.Next())
            {
                Subscriber sub = subscribers.Get();
                if (sub.StartingMonth + sub.Duration <= 13)
                {
                    if (sub.StartingMonth <= month && sub.StartingMonth + sub.Duration >= month)
                    {
                        for (publish.Begin(); publish.Exist(); publish.Next())
                        {
                            Publishment pub = publish.Get();
                            if (MonthlyProfit(subs,pub) > max)
                            {
                                if (sub.Code == pub.Code)
                                {
                                    publishmentCode = pub.Code;
                                    max = MonthlyProfit(subs,pub);
                                }
                            }
                        }
                    }
                }
            }
            return publishmentCode;
        }
        /// <summary>
        /// Finds the total profit that all publishments made 
        /// </summary>
        /// <param name="publish">A list of publishments</param>
        /// <param name="subscribers">A list of subscribers</param>
        /// <returns>The sum of all publishment profits</returns>
        public static decimal TotalPublishmentProfits(LinkedList<Publishment> publish, LinkedList<Subscriber> subscribers)
        {
            decimal sum = 0;
            for (subscribers.Begin(); subscribers.Exist(); subscribers.Next())
            {
                Subscriber sub = subscribers.Get();
                for (publish.Begin(); publish.Exist(); publish.Next())
                {
                    Publishment pub = publish.Get();
                    if (sub.Code == pub.Code)
                    {
                        sum += pub.Price * (sub.Duration+1) * sub.Count;
                    }
                }
            }
            return sum;
        }
        /// <summary>
        /// Finds the overall average of all publishment profits
        /// </summary>
        /// <param name="publish">A list of publishments</param>
        /// <param name="subscribers">A list of subscribers</param>
        /// <returns>The average of all profits</returns>
        public static decimal Average(LinkedList<Publishment> publish, LinkedList<Subscriber> subscribers)
        {
            return TotalPublishmentProfits(publish,subscribers) / publish.Count();
        }
        /// <summary>
        /// Adds to a list every publishment that has less profits than the average
        /// </summary>
        /// <param name="publish">A list of publishments</param>
        /// <param name="subscribers">A list of subscribers</param>
        /// <param name="filtered">A new list that information is added to</param>
        public static void LessThanAverage(LinkedList<Publishment> publish, LinkedList<Subscriber> subscribers, LinkedList<Publishment> filtered)
        {
            LinkedList<Publishment> tempPublish = publish.Copy();
            for (publish.Begin(); publish.Exist(); publish.Next())
            {
                Publishment pub = publish.Get();
                decimal profit = PublishmentProfit(subscribers, pub);
                decimal average = Average(tempPublish, subscribers);
                if (profit < average)
                {
                    filtered.Add(pub);
                }
            }
        }
        /// <summary>
        /// Creates a list of subscribers that subscribe to a specific publishment at a specific month
        /// </summary>
        /// <param name="inputCode">The specific publishment code</param>
        /// <param name="inputMonth">The specific month</param>
        /// <param name="subscribers">A list of subscribers</param>
        /// <param name="publish">A list of publishments</param>
        /// <returns></returns>
        public static LinkedList<Subscriber> SubscriberList(int inputCode, int inputMonth, LinkedList<Subscriber> subscribers, LinkedList<Publishment> publish)
        {
            LinkedList<Subscriber> ret = new LinkedList<Subscriber>();
            for (subscribers.Begin(); subscribers.Exist(); subscribers.Next())
            {
                Subscriber sub = subscribers.Get();
                for (publish.Begin(); publish.Exist(); publish.Next())
                {
                    Publishment pub = publish.Get();
                    if (inputCode == pub.Code && inputCode == sub.Code)
                    {
                        if (inputMonth >= sub.StartingMonth && inputMonth <= sub.StartingMonth + sub.Duration)
                        {
                            ret.Add(sub);
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// Finds the monthly profit of a publishment
        /// </summary>
        /// <param name="subscribers">A list of subscribers</param>
        /// <param name="pub">A single publishment</param>
        /// <returns>A monthly profit of a publishment</returns>
        public static decimal MonthlyProfit(LinkedList<Subscriber> subscribers, Publishment pub)
        {
            decimal sum = 0;
            for (subscribers.Begin(); subscribers.Exist(); subscribers.Next())
            {
                Subscriber sub = subscribers.Get();
                if (pub.Code == sub.Code)
                {
                    sum += pub.Price * sub.Count;
                }
            }
            return sum;
        }
        /// <summary>
        /// Finds the total profit of a publishment
        /// </summary>
        /// <param name="subscribers">A list of subscribers</param>
        /// <param name="pub">Information about a publishment</param>
        /// <returns>The total profit of a publishment</returns>
        public static decimal PublishmentProfit(LinkedList<Subscriber> subscribers, Publishment pub)
        {
            decimal sum = 0;
            for (subscribers.Begin(); subscribers.Exist(); subscribers.Next())
            {
                Subscriber sub = subscribers.Get();
                if (sub.Code == pub.Code)
                {
                    sum += pub.Price * (sub.Duration+1) * sub.Count;
                }
            }
            return sum;
        }
        /// <summary>
        /// Finds if the given text has any letters
        /// </summary>
        /// <param name="text">Given text</param>
        /// <returns>True if the text is only digits, otherwise returns false</returns>
        public static bool HasWords(string text)
        {
            foreach(char letter in text)
            {
                if(!char.IsDigit(letter))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Finds if the durations of subscribtions are within limits
        /// </summary>
        /// <param name="subscribers">A list of subscribers</param>
        /// <returns>True if the duration is incorrect, false if correct</returns>
        public static bool IncorrectDuration(LinkedList<Subscriber> subscribers)
        {
            for (subscribers.Begin(); subscribers.Exist(); subscribers.Next())
            {
                Subscriber sub = subscribers.Get();
                if(sub.StartingMonth + sub.Duration > 13)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if the data files have any information or if there are any data files
        /// </summary>
        /// <param name="subscribers">A list of subscribers</param>
        /// <param name="publishments">A list of publishments</param>
        /// <returns>True if the data files have information, false if not</returns>
        public static bool HasInformation(LinkedList<Subscriber> subscribers, LinkedList<Publishment> publishments)
        {
            if (subscribers.Count() > 0 && publishments.Count() > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Finds if the given publishment code exists in the list of publishments
        /// </summary>
        /// <param name="publishment"> A list of publishments</param>
        /// <param name="givenCode">The given publishment code</param>
        /// <returns></returns>
        public static bool PublishmentExist(LinkedList<Publishment> publishment, int givenCode)
        {
            for(publishment.Begin();publishment.Exist();publishment.Next())
            {
                Publishment pub = publishment.Get();
                if(pub.Code == givenCode)
                {
                    return true;
                }
            }
            return false;
        }

    }
}