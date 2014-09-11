using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.IO.Compression;


using System.ComponentModel;

namespace Biztech
{
    static class Program
    { 
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             
            //if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)

            //Global.CleanSettings();
            Global.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BTSSSQLConnectionStringDEV"].ConnectionString; 

            if ((new FormLogin()).ShowDialog() == DialogResult.OK)
            {
                Global.Initialize();
                Application.Run(new FormMain());
            } 
 
        }
    }
}
