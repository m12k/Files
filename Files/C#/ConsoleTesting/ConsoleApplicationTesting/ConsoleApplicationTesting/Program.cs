using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


                //Problem1 p1 = new Problem1();
               
                //Address address1 = new Address() {  City ="", Zip = ""};
                //List<Address> address = new List<Address>();
                //address.Add(address1);

                //Person p = new Person() { LastName = "", FirstName = "", Address = address};
                //List<Person> assoc = new List<Person>();
                //assoc.Add(p);

                //Manager mgr1 = new Manager();

                //List<Manager> mgr = new List<Manager>() { new Manager(){ LastName="", FirstName="", Address = address,  Associate=assoc}};
                 
                //p1.Validate(mgr);



                Testing t = new Testing();
                t.RunTest();


                //SortedArray arr = new SortedArray();

                //int[] sortedList;
                //sortedList = arr.SortArray(new int[] { 1, 2, 3 });


            }
            catch (Exception)
            { 
                throw;
            }
        }







        void Linklist()
        { 
            LinkedList list = new LinkedList(); 
            list.insert(10);
            list.insert(20);
            list.insert(30);
            list.insert(40);
            list.insert(50);
            list.insert(60);
 
            list.display();
 
            Node tempObj1 = list.search(40);//creating a loop 60->50->40->30->20->10->40
            Node tempObj2 = list.search(10);
         
            tempObj2.next = tempObj1;
            Node loop = list.FindLoop();
            if(loop != null)
                Console.WriteLine("Loop exists and starts at : " + loop.item);
            else
                Console.WriteLine("there is no loop");
                
            list.display();
        
        }
    }
}
