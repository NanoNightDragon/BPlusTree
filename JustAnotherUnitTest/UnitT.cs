using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPlusTree_24_04_2019;

namespace JustAnotherUnitTest
{
    [TestClass]
    public class UnitT
    {
        [TestMethod]
        public void SimpleTest()
        {
            
            int[] TOS = { 7, 6, 5, 4, 3, 2, 1 };
            int[] expected = { 2, 4, 6};

            Class_Select Final_List = new Class_Select();

            int [] REL = Final_List.Selcet( TOS, Final_List.Is_Even_Number).ToArray<int>();


            CollectionAssert.AreEqual(expected, REL);

        }
        [TestMethod]
        public void WoW_One_More_SimpleTest()
        {

            int[] TOS = { 7, 6, 5, 4, 3, 2, 1,6,12,33,23 };
            int[] expected = { 2, 4, 6,12 };

            Class_Select Final_List = new Class_Select();

            int[] REL = Final_List.Selcet(TOS, Final_List.Is_Even_Number).ToArray<int>();


            CollectionAssert.AreEqual(expected, REL);

        }

    }
}
