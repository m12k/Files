using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using BTSS.Common; 

namespace BTSS.Data
{
    public class Project 
    {

        Datasource datasource;

        #region Constructor

        public Project(string connectionString) 
        {
            datasource = new Datasource(connectionString, Core.DataProvider.SQL);
        }

        #endregion

        #region Methods 
        private string GetProjectListForReportSQL()
        {
            string sql;
            sql = "EXECUTE GetProjectListForReport";
            return sql;
        } 
          
        public DataTable GetProjectListForReport()
        {
            try
            {
                return datasource.ExecuteQuery(GetProjectListForReportSQL()).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
         
         
        #endregion

    }
     
}
