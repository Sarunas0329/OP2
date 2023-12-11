using Microsoft.VisualStudio.TestTools.UnitTesting;
using Antras;
using System;
using System.Collections.Generic;

namespace Antras.Tests
{
    [TestClass()]
    public class LinkedListTests
    {
        [TestMethod()]
        [DataRow(4, 1, 2)]
        [DataRow(1, int.MaxValue, 0)]
        [DataRow(7, 49851, int.MinValue)]

        public void CopyTest(int a, int b, int c)
        {
            LinkedList<int> list = new LinkedList<int>();
            LinkedList<int> copied = new LinkedList<int>();

            list.Add(a);
            list.Add(b);
            list.Add(c);

            copied = list.Copy();

            list.Begin();
            copied.Begin();
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(list.Get(),copied.Get());
                list.Next();
                copied.Next();
            }
        }

        [TestMethod()]
        [DataRow(11,12,94)]
        [DataRow(1, 1, 2)]
        [DataRow(15,16,17)]
        [DataRow(8, 8, 8)]

        public void SortThreeIntegers(int a, int b, int c)
        {
            List<int> expected = new List<int>();
            expected.Add(a);
            expected.Add(b);
            expected.Add(c);

            expected.Sort();

            LinkedList<int> list = new LinkedList<int>();

            list.Add(a);
            list.Add(b);
            list.Add(c);
            
            list.Sort();

            list.Begin();
            for (int i = 0; i < 3; i++)
            {
                int actual = list.Get();
                list.Next();
                Assert.AreEqual(expected[i], actual);
            }
            
        }

        [TestMethod()]
        [DataRow(17, 94, 15)]
        [DataRow(147, 1, 0)]
        public void GetTest(int a, int b, int c)
        {
            int count = 0;

            List<int> ints = new List<int>();
            ints.Add(a);
            ints.Add(b);
            ints.Add(c);

            
            LinkedList<int> list = new LinkedList<int>();

            list.Add(a);
            list.Add(b);
            list.Add(c);

            for (list.Begin(); list.Exist(); list.Next())
            {
                int expected = ints[count];
                int actual = list.Get();
                Assert.AreEqual(expected, actual);
                count++;
            }
        }

        [TestMethod()]
        [DataRow(147)]
        [DataRow(17)]
        [DataRow(7)]
        public void AddTest(int a)
        {
            LinkedList<int> list = new LinkedList<int>();

            list.Add(a);

            for (list.Begin(); list.Exist(); list.Next())
            {
                int actual = list.Get();
                Assert.AreEqual(a, actual);
            }
        }

        [TestMethod()]
        [DataRow(1,2,3)]
        [DataRow(2,3,4)]
        [DataRow(21,30,41)]
        [DataRow(0,0,0)]
        public void BeginTest(int a, int b, int c)
        {
            LinkedList<int> list = new LinkedList<int>();

            list.Add(a);
            list.Add(b);
            list.Add(c);

            list.Begin();
            int actual = list.Get();
            Assert.AreEqual(a, actual);
        }

        [TestMethod()]
        [DataRow(1, 2, 3)]
        [DataRow(2, 3, 4)]
        [DataRow(21, 30, 41)]
        [DataRow(0, 0, 0)]
        public void NextTest(int a, int b, int c)
        {
            LinkedList<int> list = new LinkedList<int>();

            list.Add(a);
            list.Add(b);
            list.Add(c);


            list.Begin();
            list.Next();

            int actual = list.Get();

            Assert.AreEqual(b, actual);
        }

        [TestMethod()]
        [DataRow(1, 2, 3)]
        [DataRow(2, 3, 4)]
        [DataRow(21, 30, 41)]
        [DataRow(0, 0, 0)]
        [DataRow(null, null, null)]
        public void ExistTest_WithThreeInputs(int a, int b, int c)
        {
            bool actual = true;
            LinkedList<int> list = new LinkedList<int>();

            list.Add(a);
            list.Add(b);
            list.Add(c);

            list.Begin();
            Assert.AreEqual(list.Exist(), actual);
        }
    }
}