using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationTesting
{
    public class SortedArray
    {

        public int[] SortArray(int[] input)
        {

            int[] lst = new int[] {4,4,4,1,2,1,2,4,1,2,4,2,8,9,8,6,7 };
            Array.Sort(lst);
            return lst.OrderByDescending(x => x).ToArray();

        }




    }
}
