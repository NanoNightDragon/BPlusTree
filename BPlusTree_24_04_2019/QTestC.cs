using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPlusTree_24_04_2019
{
    class QTestC
    {


        public QTestC()
        {
            BPlusTree a = new BPlusTree();
            a.insert(3);
            a.insert(2);
            a.insert(4);

            //a.insert(-1);//test
            a.insert(5);
            a.insert(7);
            a.insert(6);
            a.insert(12);
            a.insert(10);
            a.insert(100);
            a.insert(5);
            a.insert(100);
            a.insert(17);
            a.insert(22);
            a.insert(34);
            a.insert(177);
            a.insert(188);

            //test 
            a.insert(14);
            a.insert(7);
            a.insert(28);
            a.insert(26);

            int v; Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                v = random.Next(1000);
                a.insert(v);
                Console.WriteLine(a.FindE(100, a.Root) + " v:" + v + " 1# " + i);
                Console.WriteLine(a.FindE(12, a.Root) + " v:" + v + " 2# " + i);
            }
            a.insert(-2);

            List<int> DATA = a.Get_all_data(1050);

            for (int i = 0; i < DATA.Count; i++)
            {
                Console.Write(DATA[i] + " ");
                if (i % 10 == 0 && i != 0)
                    Console.WriteLine();
            }



            


            //a.insert(8);
            //a.insert(10);

            Console.WriteLine(a.FindE(100, a.Root));
            Console.ReadLine();

        }

    }
}
