using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

namespace Biztech
{
    public static class  Global 
    { 

        public const string name = "Mike";
        public static string ConnectionString = "";//@"Data Source=VMANNMBPS1\SQLEXPRESS;Initial Catalog=BTSSSQL;Integrated Security=True";

        public static DataProvider ProjectDataProvider;
        public static string ProjectConnectionString;
        public static string LastName;
        public static string FirstName;
        public static string MiddleName;
        public static string FullName;
        public static string UserName;
        public static string UserId;

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

        public static void Initialize()
        { 

        }

        public static DialogResult ShowQuestion(string text)
        {
            return MessageBox.Show(text, "Business Technology Security System", MessageBoxButtons.OKCancel);
        }

        public static void ShowMessage(string text, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            MessageBox.Show(text, "Business Technology Security System", messageBoxButtons, messageBoxIcon);
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
        
        public static bool IsCodeExist(string originalValue, string currentValue, string table, string field, Operation operation)
        {
            try 
	        {	
                BTSSContext ctx = new BTSSContext(ConnectionString);
     
                return ctx.IsCodeExist(originalValue,currentValue,table,field,operation.ToString()).FirstOrDefault().IsExist.Value;                 
	        }
	        catch (Exception)
	        { 
		    throw;
	        }
        }

        public static bool IsUserExistInActiveDirectory(string userName, List<string> groups)
        {
            try
            {
                //Get Current Domain/Active Directory.
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, Domain.GetCurrentDomain().Name);

                //Find User in the Domain/Active Directory
                UserPrincipal user = UserPrincipal.FindByIdentity(ctx,userName);

                foreach (string group in groups)
                { 
                     //Get Groupin the Domain/Active Directory
                    GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, group);
                    
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
                 
                return false;
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
    }
}


 