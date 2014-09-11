using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BTSS.Common;

namespace BTSS.Data
{
    public class Authentication
    {
        Datasource datasource;

        #region Constructor

        public Authentication(string connectionString)
        {
            datasource = new Datasource(connectionString, Core.DataProvider.SQL);
        }

        #endregion

        #region Private Methods

        private string GetPasswordSQL(string userName)
        {

            string sql;
            sql = "SELECT dbo.GetUserPassword('"+ userName +"')";
            return sql;

        }

        #endregion


        #region Public Method

        public string GetPassword(string userName)
        {
            try
            {
                return datasource.ExecuteQuery(GetPasswordSQL(userName)).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 

    }
}
