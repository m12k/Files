using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biztech.Classes
{
    class ClassCheckConnection : Datasource 
    {
        Global.DataProvider dataProvider;

        public ClassCheckConnection(string connectionString, Global.DataProvider provider)
            : base(connectionString, provider)
        {
            dataProvider = provider;
        
        }
        
        public bool CheckConnection()
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

                    ExecuteQuery(sql);
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
