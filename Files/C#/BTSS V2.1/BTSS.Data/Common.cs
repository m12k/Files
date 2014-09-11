using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Data
{
    public class Common
    {
        private Datasource datasource;

        public Common(string connectionString, BTSS.Common.Core.DataProvider provider)
        {
            datasource = new Datasource(connectionString, provider);
        }

        public bool IsValueAlreadyExist(string tableName, string fieldName, string originalvalue, string currentValue, BTSS.Common.Core.Operation operation)
        {
            try
            {
                return datasource.IsValueAlreadyExist(tableName, fieldName, originalvalue, currentValue, operation);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
