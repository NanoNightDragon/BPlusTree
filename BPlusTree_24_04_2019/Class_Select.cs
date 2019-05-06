using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPlusTree_24_04_2019
{
    public class Class_Select
    {
        private int G_Size;
        public Class_Select()
        {
            
        }

        public List<int> RandomNum(int size)
        {
            Random random = new Random();
            List<int> NL = new List<int>(size);
            for (int i = 0; i < size; i++)
                NL.Add(random.Next(177));

            return NL;

        }

        public bool Is_Even_Number(int elem)
        {
            if (elem % 2 == 0)
                return true;
            return false;
        }

        public IEnumerable<int> Select(IEnumerable<int> sequence,Func<int,bool> call_back_fun)
        {

            if (sequence == null)
            {
                throw new ArgumentException("sequence cann't be null");

            }
                
            BPlusTree TempTree = new BPlusTree();
            foreach(int elem in sequence)
            {
                if (call_back_fun(elem))
                    TempTree.insert(elem);
            }

            return TempTree.Get_all_data(1000);
        }



    }
}
