using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;

namespace  BTSS.Data
{
    public class Group
    {
        Datasource datasource;

#region "Constructor"

        public Group(string connectionString)
         {
             datasource = new Datasource(connectionString, BTSS.Common.Core.DataProvider.SQL);
         }

#endregion
         
#region "Private Method"

         private string GetGroupModuleSQL(string GroupId)
         {
             try
             {
                 string sql;
                 sql = "EXECUTE GetGroupModule '"+ GroupId +"'";
                 return sql;
             }
             catch (Exception)
             {
                 throw;
             }
         }

#endregion
        
#region "Public Region" 

        public DataTable GetGroupModule(string GroupId)
        {
            try
            {
                return datasource.ExecuteQuery(GetGroupModuleSQL(GroupId)).Tables[0];
            }
            catch (Exception)
            {
            throw;
            }
        }


#endregion

    }
}
