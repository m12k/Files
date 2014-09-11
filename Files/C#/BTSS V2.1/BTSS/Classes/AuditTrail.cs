using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biztech.Classes
{
    class AuditTrail
    {

        #region "Declarations"

        BTSSContext ctx;

        #endregion

        #region "Constructor"

        public AuditTrail(string connectionString){ ctx = new BTSSContext(connectionString);}

        #endregion
        
        #region "Properties"



        #endregion
        
        #region "Private Methods"

        #endregion 

        #region "Public Methods"

        public List<Audit> GetListByUser(string userName)
        {
            try
            {
                var audit = from s in ctx.Audit
                            where s.UserName == userName 
                            select s;

                return audit.ToList();
                 
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<string> GetUsers()
        {
            try
            {
                var user = from s in ctx.User 
                           select s.UserName;

                return user.ToList();
                          
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> GetTables()
        {
            try
            {
                var tables = from s in ctx.GetTableNames()
                             select s.TableName;

                return tables.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetAuditListResult> GetAuditList(string userName, string tableName) 
        {
            try
            {
                return ctx.GetAuditList(userName, tableName).ToList<GetAuditListResult>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


    }
}
