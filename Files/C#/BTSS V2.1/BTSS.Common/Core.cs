using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using System.Diagnostics;
using System.Threading;

namespace BTSS.Common
{
    public static class Core 
    { 

        public const string name = "Mike";
        public static string ConnectionString = "";//@"Data Source=VMANNMBPS1\SQLEXPRESS;Initial Catalog=BTSSSQL;Integrated Security=True";

        public static DataProvider ProjectDataProvider;
        public static Authentication UserAuthentication;
        public static string ProjectConnectionString;
        public static string ProjectName;
        public static string LastName;
        public static string FirstName;
        public static string MiddleName;
        public static string FullName;
        public static string UserName;
        public static string UserId; 
        public static string[] ADGroup = new string[2] { "PRD", "MLIDDOMAIN1"};

        public enum Preferences
        { 
            AUDIT_TRAIL = 1
        }

        public enum Operation
        { 
            INSERT = 0,
            UPDATE = 1,
            DELETE = 2,
            VIEW = 3
        };

        public enum Module
        {
            NONE,
            BTSSPROJECTS,
            BTSSGROUP,
            BTSSUSERS,
            MODULE,
            GROUP,
            USER,
            REPORT,
            PREFERENCES
        }

        public enum DataProvider
        {
            SQL = 0,
            OLEDB = 1
        }

        public enum ConnectionStatus
        { 
            Disconnected = 0,
            Connected = 1            
        }

        public enum Authentication
        { 
            Application,
            Windows
        }

        public enum ModuleReference
        { 
            Setting,
            Tools
        }
          
        public static void Initialize()
        { 

        }

        public static DialogResult ShowQuestion(string text)
        {
            return MessageBox.Show(text, "Question", MessageBoxButtons.OKCancel);
        }

        public static void ShowMessage(string text,string caption, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            MessageBox.Show(text, caption, messageBoxButtons, messageBoxIcon);
        }

        public static void ShowMessage(string text, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            MessageBox.Show(text, "Business Technologies Security System", messageBoxButtons, messageBoxIcon);
        }


        public static string GetConnectionString(string mdbFilePath, string userId, string password, bool hasOtherDetails, string mdwFilePath)
        {
            try
            {
                if (mdbFilePath.Substring(mdbFilePath.LastIndexOf(".")) == ".accdb")
                {
                     return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + mdbFilePath + ";Persist Security Info=False;";
                }
                else
                {
                    if ((hasOtherDetails) && (mdwFilePath != ""))
                        return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbFilePath + ";Persist Security Info=False;User ID = " + userId + ";Password = " + password + ";Jet OLEDB:System database= " + mdwFilePath + "";
                    else if ((hasOtherDetails) && (password != ""))
                        return "Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + mdbFilePath + ";Persist Security Info=False;Jet OLEDB:Database Password = " + password + ";";
                    else
                        return "Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + mdbFilePath + ";Persist Security Info=False;User ID = " + userId + ";Password = " + password + ";";
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static string GetConnectionString(string datasource,string databaseName, string username, string password)
        {
            try
            {
                if (username.Length == 0 && password.Length == 0)
                    return "Data Source= " + datasource + ";Initial Catalog=" + databaseName + ";Trusted_Connection = True;";
                else
                    return "Data Source= " + datasource + ";Initial Catalog=" + databaseName + ";Persist Security Info=True;User ID= " + username + "; Password= " + password + ";";
                
            }
            catch (Exception)
            {
                throw;
            }        
        }
         
        public static bool IsUserExistInActiveDirectory(string userName, System.Data.DataTable groups)
        {
            try
            {

                for(int i = 0; i<= groups.Rows.Count -1; i++)
                { 
                    
                    //Get Current Domain/Active Directory.
                    using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, groups.Rows[i][0].ToString()))
                    {  
                     
                        //Find User in the Domain/Active Directory
                        UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userName);
                                            
                        //Get Groupin the Domain/Active Directory
                        GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, groups.Rows[i][1].ToString());

                        //Display the list of users in the Group
                        //List<Principal> users = grp.GetMembers(true).ToList();

                        //Check if User exist in the active directory
                        if (user == null) break;

                        //Check if Group exist in the Doamin/Active directory
                        if (grp == null) break;

                        //Check if User is in the Group of the Domain/Active Directory
                        if (user.IsMemberOf(grp))
                            return true; 
                    }
                     
                }

                return false; 




                ////Get Current Domain/Active Directory.
                //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, Domain.GetCurrentDomain().Name);

                ////Find User in the Domain/Active Directory
                //UserPrincipal user = UserPrincipal.FindByIdentity(ctx,userName);

                //foreach (string group in groups)
                //{ 
                //     //Get Groupin the Domain/Active Directory
                //    GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, group);
                    
                //    //Display the list of users in the Group
                //    //List<Principal> users = grp.GetMembers(true).ToList();

                //    //Check if User exist in the active directory
                //    if (user == null) break;

                //    //Check if Group exist in the Doamin/Active directory
                //    if (grp == null) break;

                //    //Check if User is in the Group of the Domain/Active Directory
                //    if (user.IsMemberOf(grp))
                //        return true;
                //}
                 
                //return false;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public static bool IsActiveDirectoryGroupExist(string domain, string group)
        {
            try
            { 
                PrincipalContext ctxPRD = new PrincipalContext(ContextType.Domain, domain); 
                GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctxPRD, IdentityType.Name, group);
                 
                return grp == null ? false : true; 
             }
            catch (Exception)
            {                
                throw;
            }
         
        }

        public static void SetSettings(string key, string value)
        {
            try
            { 
                System.Configuration.Configuration con = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                bool isExist = false;

                foreach (string str in con.AppSettings.Settings.AllKeys)
                {
                    if (str == key)
                    {
                        isExist = true;
                        con.AppSettings.Settings[key].Value = value;
                        break;                    
                    }
                }

                if (!isExist)
                {
                    con.AppSettings.Settings.Add(key, value);
                }

                con.Save(System.Configuration.ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        public static void CleanSettings()
        {
            try
            {  
                System.Configuration.Configuration con = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

                foreach (string str in con.AppSettings.Settings.AllKeys)
                {
                    con.AppSettings.Settings.Remove(str);
                }

                con.Save(System.Configuration.ConfigurationSaveMode.Modified);
                System.Configuration.ConfigurationManager.RefreshSection("appSettings"); 
            }
            catch (Exception)
            {
                throw;
            }
        }
          
        public static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = ShowThreadExceptionDialog("BTSS Error", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error",
                        "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort. 
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether 
        // or not they wish to abort execution. 
        // NOTE: This exception cannot be kept from terminating the application - it can only  
        // log the event, and inform the user about it.  
        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "An application error occurred.\n\n Please contact: \n\tMichael Cajandig<mcajandig@jhancock.com>\n\tEric Petersen<peterse@jhancock.com>\n\tor\n\tMatthew Scott Welch<welchma@jhancock.com> " +
                    "\n\nWith the screenshot of the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log. 
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                //Log Exception.
                string sSource;
                string sLog;
                string sEvent;

                sSource = "BTSS";
                sLog = "Application";
                sEvent = errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace;

                if (!EventLog.SourceExists(sSource))
                    EventLog.CreateEventSource(sSource, sLog);
                 
                EventLog.WriteEntry(sSource, sEvent,
                    EventLogEntryType.Warning);

            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error",
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Creates the error message and displays it. 
        public static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred.\n\n Please contact: \n\tMichael Cajandig<mcajandig@jhancock.com>\n\tEric Petersen<peterse@jhancock.com>\n\tor\n\tMatthew Scott Welch<welchma@jhancock.com> " +
                    "\n\nWith the screenshot of the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }


    }
}


 