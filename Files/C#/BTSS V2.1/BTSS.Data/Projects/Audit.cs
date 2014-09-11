using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTSS.Data.Projects
{
    public class Audit
    {
        #region Declarations

        private Datasource datasource;

        #endregion
         
        #region Constructor

        public Audit(string connectionString, BTSS.Common.Core.DataProvider provider)
        {
            datasource = new Datasource(connectionString,provider); 
        }

        #endregion

        #region Private Methods

        private string GetTablesListSQL()
        {
            string sql;
            sql = 
                "SELECT " +
                "TABLE_SCHEMA + '.' + TABLE_NAME TableName " +
                "FROM " +
                "INFORMATION_SCHEMA.TABLES " +
                "WHERE " +
                "TABLE_TYPE = 'BASE TABLE' ";

            return sql;
        }

        private string GetUsersListSQL()
        {
            string sql;
            sql = "SELECT DISTINCT UserName FROM Audit";
            return sql;
        }

        private string GetAuditListByTableSQL(string tableName)
        {
            string sql;
            sql =
                "SELECT " +
                "[Operation], " +
                "[TableName], " +
                "[PrimaryKey], " +
                "[UserName], " +
                "[CreatedDate], " +
                "[Memo] " +
                "FROM [dbo].[Audit] " +
                "WHERE " +
                "TableName = '" + tableName + "'" +
                "ORDER BY [CreatedDate] DESC";
            return sql; 
        }

        private string GetAuditListByUserSQL(string userName)
        {
            string sql;
            sql = 
                "SELECT " +  
                "[Operation], " +
                "[TableName], " +
                "[PrimaryKey], " +
                "[UserName], " +
                "[CreatedDate], " +
                "[Memo] " +
                "FROM [dbo].[Audit] " +
                "WHERE " +
                "UserName = '"+ userName +"'" +
                "ORDER BY [CreatedDate] DESC";

            return sql;
        }

        #endregion

        #region Public Methods

        public System.Data.DataTable GetTablesList()
        {
            try
            {
                return datasource.ExecuteQuery(GetTablesListSQL()).Tables[0];
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
                return datasource.ExecuteQuery(GetUsersListSQL()).Tables[0];
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
                return datasource.ExecuteQuery(GetAuditListByTableSQL(tableName)).Tables[0];
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
                return datasource.ExecuteQuery(GetAuditListByUserSQL(userName)).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
