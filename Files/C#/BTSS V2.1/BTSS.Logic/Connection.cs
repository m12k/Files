using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BTSS.Common;
using BTSS.Data;
 

namespace BTSS.Logic
{
    public class Connection: Interfaces.IConnection 
    {
        Core.DataProvider dataProvider;
        Data.CheckConnection checkConnection;
          
        public Connection(string connectionString, Core.DataProvider provider)             
        {
            checkConnection = new Data.CheckConnection(connectionString, provider);
            dataProvider = provider; 
        }
        
        public bool HasConnection()
        {
            try
            {
                return checkConnection.HasConnection();
            }
            catch (Exception)
            {
                return false;
            }
        }


         
    }
}
