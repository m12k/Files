using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationTesting
{
    public class Testing
    {
         
        public void RunTest()
        {
            SortNod sortNode = new SortNod();
            sortNode.Insert("One");
            sortNode.Insert("Two");
            sortNode.Insert("Three");
            sortNode.Insert("Four");
            sortNode.Insert("Five");        
        }         
    }

    
    public class nod
    {
        public string Node;
        public nod Next;

        public nod(string node)
        {
            Node = node;
        }
    }

    public class SortNod
    {

        private nod nxt;

        public SortNod()
        {
            nxt = null;
        }

        public void Insert(string node)
        {
            nod nn = new nod(node);
            nn.Next = nxt;
            nxt = nn;
        }
    }

}
