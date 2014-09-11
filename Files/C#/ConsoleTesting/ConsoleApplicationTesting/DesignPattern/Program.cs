using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DesignPattern
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());


            //Command Patterm
            //Behavioral.CommandStructural.MainApp s = new Behavioral.CommandStructural.MainApp();
            //s.Start();

            //Behavioral.CommandRealWorld.MainApp m = new Behavioral.CommandRealWorld.MainApp();
            //m.Start();
             
            //Factory Method Pattern
            Creational.FactoryMethodStructural.MainApp m = new Creational.FactoryMethodStructural.MainApp();
            m.Start();

            //Creational.FactoryMethodRealWorld.MainApp mr = new Creational.FactoryMethodRealWorld.MainApp();
            //mr.Start();

        }
    }
}
