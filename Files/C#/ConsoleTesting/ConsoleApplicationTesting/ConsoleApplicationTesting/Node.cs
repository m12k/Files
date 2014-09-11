using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationTesting
{
    public class Node
    {

        public int item;
        public Node next;
        public Node(int val)
        {
            item = val;
        }
        public void displayNode()
        {
            Console.WriteLine("[" + item + "] ");
        }
    }
}
