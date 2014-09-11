using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading; 
 
namespace BTSS.Presentation
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

            //if (LicenseManager.UsageMode == LicenseUsageMode.Runtime 

            //Start Of: Error Handling
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(Common.Core.UIThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through 
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Common.Core.CurrentDomain_UnhandledException);
            //End Of: Error Handling
                
            Common.Core.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BTSSSQLConnectionStringDEV"].ConnectionString;



            BTSS.Logic.User user = new BTSS.Logic.User(Common.Core.ConnectionString);

            Common.Core.UserName = Environment.UserDomainName + "\\" + Environment.UserName;
            
            if (!user.IsUserValid(Common.Core.UserName, "", "Windows Authentication"))
            {                 
                Common.Core.ShowMessage("You are not authorize to use this application.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                 
            }
            else
            {
                var result = user.GetUserDetailsByUserName(Common.Core.UserName);
                Common.Core.UserId = result.Id; 
                Common.Core.LastName = result.LastName;
                Common.Core.FirstName = result.FirstName;
                Common.Core.MiddleName = result.MiddleName;
                Common.Core.FullName = result.FullName;

                Common.Core.Initialize();
                Application.Run(new FormMain());
            } 
             
            //if ((new FormLogin()).ShowDialog() == DialogResult.OK)
            //{
            //    Common.Core.Initialize();
            //    Application.Run(new FormMain());
            //} 
        }  
    }
}
