using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Antras
{
    public sealed class LinkedList<type> : IEnumerable<type> 
        where type : 
             IComparable<type>, IEquatable<type>
    {
        private sealed class Node<type> where type :
            IComparable<type>, IEquatable<type>
        {
            public type Data { get; set; }
            public Node<type> Link { get; set; }
            public Node() { }

            public Node(type Data, Node<type> Link)
            {
                this.Data = Data;
                this.Link = Link;
            }
        }
        private Node<type> current;
        private Node<type> head;
        private Node<type> tail;
        public LinkedList()
        {
            this.current = null;
            this.head = null;
            this.tail = null;
        }
        /// <summary>
        /// Adds a subscriber to the list
        /// </summary>
        /// <param name="route">Information about type to add to the list</param>
        public void Add(type route)
        {
            var add = new Node<type>(route, null);
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public IEnumerator<type> GetEnumerator()
        {
            for (Node<type> dd = head; dd != null; dd = dd.Link)
            {
                yield return dd.Data;
            }
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
        public type Get()
        {
            return current.Data;
        }
        /// <summary>
        /// A deep copy of a type of list
        /// </summary>
        /// <returns>A new list thats the requested type</returns>
        public LinkedList<type> Copy()
        {
            LinkedList<type> ret = new LinkedList<type>();
            for (Begin(); Exist(); Next())
            {
                ret.Add(Get());
            }
            return ret;
        }
        /// <summary>
        /// Sorts the list
        /// </summary>
        public void Sort()
        {
            bool flag = true;
            Node<type> d1, d2;
            while(flag)
            {
                if (head != null && head.Link != null)
                {
                    flag = false;
                    d1 = head;
                    d2 = head.Link;
                    while(d2 != null)
                    {
                        if(d2.Data.CompareTo(d1.Data) > 1)
                        {
                            type temp = d1.Data;
                            d1.Data = d2.Data;
                            d2.Data = temp;
                            flag = true;
                        }
                        d1 = d2;
                        d2 = d2.Link;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}