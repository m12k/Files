using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationTesting
{
    public class LinkedList
    { 
        private Node first;
        public LinkedList()
        {
            first = null;
        }
        public bool isEmpty()
        {
            return (first == null);
        }
        public void insert(int val)//inserts at beginning of list
        {
            Node newNode = new Node(val);
            newNode.next = first;
            first = newNode;
        }
        public Node delete()//deletes at beginning of list
        {
            Node temp = first;
            first = first.next;
            return temp;
        }
        public void display()
        {
            Console.WriteLine("List items from first to last :");
            Node current = first;
            while(current != null)
            {
                current.displayNode();
                current = current.next; 
            }
            Console.WriteLine("");
        }
        public void reverse()
        {
            Node current = first;
            first = null;
            while (current != null)
            {
                Node save = current;
                current = current.next;
                save.next = first;
                first = save;
            }
        }
        public Node search(int val)
        {
            Node current = first;
            while (current.item != val)
            {
                if (current.next == null)
                    return null;
                else
                    current = current.next;
            }
            return current;
        }
        public Node delete(int val)
        {
            Node current = first;
            Node previous = first;
            while (current.item != val)
            {
                if (current.next == null)
                    return null;
                else
                {
                    previous = current;
                    current = current.next;
                }
            }
            if (current == first)
                first = first.next;
            else
                previous.next = current.next;
            return current;
        }

        public Node FindLoop()
        {
            Node tortoise = first;
            Node hare = first;

            while (hare.next != null)
            {
                tortoise = tortoise.next;
                hare = hare.next.next;
                if (tortoise == hare)
                {
                    break;
                }
            }

            if (hare.next == null)
            {
                return null;
            }

            tortoise = first;
            while (tortoise != hare)
            {
                tortoise = tortoise.next;
                hare = hare.next;
            }
            return hare;
        }
    }
}
