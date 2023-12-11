using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI.WebControls;

namespace Antras
{
    public sealed class LinkedList
    {
        private sealed class Node
        {
            public Subscriber subscriber { get; set; }
            public Publishment publishment { get; set; }
            public Node Link { get; set; }

            public Node(Subscriber subscriber, Node Link)
            {
                this.subscriber = subscriber;
                this.Link = Link;
            }

            public Node(Publishment publishment, Node Link)
            {
                this.publishment = publishment;
                this.Link = Link;
            }
        }
        private Node current;
        private Node head;
        private Node tail;
        public LinkedList()
        {
            this.current = null;
            this.head = null;
            this.tail = null;
        }
        public void AddSubscriber(Subscriber subscriber)
        {
            var add = new Node(subscriber, null);
            if (head != null)
            {
                tail.Link = add;
                tail = add;
            }
            else
            {
                head = add;
                tail = add;
            }
        }
        public void AddPublishment(Publishment publishment)
        {
            var add = new Node(publishment, null);
            if (head != null)
            {
                tail.Link = add;
                tail = add;
            }
            else
            {
                head = add;
                tail = add;
            }
        }
        public Subscriber GetSubscriber()
        {
            return current.subscriber;
        }
        public Publishment GetPublishment()
        {
            return current.publishment;
        }
        public int Count()
        {
            int count = 0;
            for (this.Begin(); this.Exist(); this.Next())
            {
                count++;
            }
            return count;
        }
        public void Begin()
        {
            current = head;
        }
        public void Next()
        {
            current = current.Link;
        }
        public bool Exist()
        {
            return current != null;
        }

        public int MostProfitableMonthly(LinkedList publish, int month)
        {
            int publishmentCode = 0;
            decimal max = 0;
            for (Begin(); Exist(); Next()) 
            {
                Subscriber sub = GetSubscriber();
                if(sub.StartingMonth + sub.Duration <= 13)
                {
                    if(sub.StartingMonth >= month && sub.StartingMonth + sub.Duration <= month)
                    {
                        for(publish.Begin();publish.Exist();publish.Next())
                        {
                            Publishment pub = GetPublishment();
                            if(OverallProfit(pub) > max)
                            {
                                if(sub.Code == pub.Code)
                                {
                                    publishmentCode = pub.Code;
                                    max = OverallProfit(pub);
                                }
                            }
                        }
                    }
                }
            }
            return publishmentCode;
        }
        public decimal OverallProfit(Publishment pub)
        {
            decimal sum = 0;
            for (Begin(); Exist(); Next())
            {
                Subscriber sub = GetSubscriber();
                if(pub.Code == sub.Code)
                {
                    sum += pub.Price * sub.Duration * sub.Count;
                }
            }
            return sum;
        }
        public decimal TotalPublishmentProfits(LinkedList subscribers)
        {
            decimal sum = 0;
            for (subscribers.Begin(); subscribers.Exist(); subscribers.Next())
            {
                Subscriber sub = GetSubscriber();
                for (Begin(); Exist(); Next())
                {
                    Publishment pub = GetPublishment();
                    if (sub.Code == pub.Code)
                    {
                        sum += pub.Price * sub.Duration * sub.Count;
                    }
                }
            }
            return sum;
        }
        public decimal Average(LinkedList subscribers)
        {
            return TotalPublishmentProfits(subscribers) / Count();
        }
        public void LessThanAverage(LinkedList subscribers, LinkedList filtered)
        { 
            for(Begin(); Exist(); Next())
            {
                Publishment pub = GetPublishment();
                if(OverallProfit(pub) < Average(subscribers))
                {
                    filtered.AddPublishment(pub);
                }
            }
        }
    }
}