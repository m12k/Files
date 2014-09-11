using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Logic.Projects
{
    public class Audit:Interfaces.Projects.IAudit 
    {
        
        #region Declarations
         
        Data.Projects.Audit audit;

        #endregion

        #region "Constructor"

        public Audit(string connectionString, BTSS.Common.Core.DataProvider provider) 
        {
            audit = new BTSS.Data.Projects.Audit(connectionString,provider);

        }

        #endregion

        #region Methods

        public System.Data.DataTable GetTablesList()
        {
            try
            {
                return audit.GetTablesList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public System.Data.DataTable GetUsersList()
        {
            try
            {
                return audit.GetUsersList(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public System.Data.DataTable GetAuditListByTable(string tableName)
        {
            try
            {
                return audit.GetAuditListByTable(tableName); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public System.Data.DataTable GetAuditListByUser(string userName)
        {
            try
            {
                return audit.GetAuditListByUser(userName); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
