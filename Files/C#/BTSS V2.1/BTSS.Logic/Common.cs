using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic
{
    public static class Common 
    {
         
        public static bool IsCodeExist(string originalValue, string currentValue, string table, string field, BTSS.Common.Core.Operation operation)
        { 
            try
            {                
                BTSSContext ctx = new BTSSContext(BTSS.Common.Core.ConnectionString);
                return  ctx.IsCodeExist(originalValue, currentValue, table, field, operation.ToString()).FirstOrDefault().IsExist.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsValueAlreadyExist(string connectionString, BTSS.Common.Core.DataProvider provider, string tableName, string fieldName, string originalvalue, string currentValue, BTSS.Common.Core.Operation operation)
        {
            try
            {
                Data.Common common = new BTSS.Data.Common(connectionString, provider);
                return common.IsValueAlreadyExist(tableName, fieldName, originalvalue, currentValue, operation);
            }
            catch (Exception)
            {
                throw;
            }
        }
         
    }


    public class DomainGroup 
    {
        public string Name { get; set; }
        public string Domain { get; set; }
    }
      
    public class DomainUser
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Domain { get; set; }
    }
}
