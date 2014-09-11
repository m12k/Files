using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class  Global 
    { 

        public const string name = "Mike";
        public const string ConnectionString = @"Data Source=VMANNMBPS1\SQLEXPRESS;Initial Catalog=BCUAgingReport;Integrated Security=True";

        public static DataProvider ProjectDataProvider;
        public static string ProjectConnectionString;
        public static string LastName;
        public static string FirstName;
        public static string MiddleName;
        public static string FullName;
        public static string UserName;
        public static string UserId;

        public enum Operation
        { 
            INSERT = 0,
            UPDATE = 1,
            DELETE = 2 
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
            REPORT
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

        public static void Initialize()
        { 

        }

        public static DialogResult ShowQuestion(string text)
        {
            return MessageBox.Show(text, "John Hancock", MessageBoxButtons.OKCancel);
        }

        public static void ShowMessage(string text, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            MessageBox.Show(text, "John Hancock", messageBoxButtons, messageBoxIcon);
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
                return "Data Source= " + datasource + ";Initial Catalog="+ databaseName +";Integrated Security=True;Pooling=False;user id = " + username + "; password = " + password + ";";
                
            }
            catch (Exception)
            {
                throw;
            }        
        }
         


    }
}


 