using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        { 
            Console.WriteLine("Happi BDay Kynth!");

            bool willTreatUs = false;
            while (willTreatUs == false)
            {
                Console.WriteLine("Will treat us? Yes/No"); 

                string choice = Console.ReadLine();

                if (choice == "Yes")
                {
                    willTreatUs = true;
                    Console.WriteLine("Yehey!");                    
                }
                else
                {
                    Console.WriteLine(":("); 
                } 
            }
        }
    }
}
