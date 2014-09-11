using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Data;
using BTSS.Common;
  
namespace BTSS.Data 

{
    public class User
    {
        Datasource datasource;

        #region Constructor

        public User(string connectionString)
        {
            datasource = new Datasource(connectionString, Core.DataProvider.SQL);
        }

        #endregion

        #region Private Methods

        private string GetUsersProjectListSQL(string UserId)
        {
            string sql;
            sql = "EXEC GetUserProjectList '" + UserId + "'";
            return sql;
        }

        private string GetUserGroupSQL(string UserId)
        {
            string sql;
            sql = "EXECUTE GetUserGroup '" + UserId + "'";
            return sql;
        } 
        
        #endregion
        
        #region Public Method
        	 
        public DataTable GetUsersProjectList(string UserId)
        {
            try
            {
                return datasource.ExecuteQuery(GetUsersProjectListSQL(UserId)).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        public DataTable GetUserGroup(string UserId)
        {
            try
            {
                return datasource.ExecuteQuery(GetUserGroupSQL(UserId)).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
          
        #endregion

    }
}