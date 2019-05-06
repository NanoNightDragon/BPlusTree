using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPlusTree_24_04_2019
{
    class Testoflab
    {
        public Testoflab()
        {
            Class_Select select = new Class_Select();
            List<int> test_List = select.RandomNum(100);
            //List<int> test_List = null;
            
            List<int> REL = select.Select(test_List, select.Is_Even_Number).ToList<int>();
            
            int k = 1;
            foreach (int elem in REL)
            {
                Console.Write(elem + " ");
                if (k % 10 == 0)
                    Console.WriteLine();
                k++;
            }
            Console.ReadLine();

        }


    }
}
