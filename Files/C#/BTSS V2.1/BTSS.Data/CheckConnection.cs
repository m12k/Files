using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BTSS.Common;

namespace BTSS.Data 
{
    public class CheckConnection 
    { 
        Datasource datasource;

        public CheckConnection(string connectionString, Core.DataProvider provider) 
        {
            datasource = new Datasource(connectionString, provider);  
        }
        
        public bool HasConnection()
        {
            try
            {
                string[] arr = new string[5];

                arr[0] = "set_user";
                arr[1] = "set_group";
                arr[2] = "set_module";
                arr[3] = "set_user_access";
                arr[4] = "set_group_access"; 

                string sql = "";

                foreach (string table in arr)
                {
                    sql = "SELECT TOP 1 * FROM " + table + "";                     
                    datasource.ExecuteQuery(sql);
                }
                
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        

    }
}
