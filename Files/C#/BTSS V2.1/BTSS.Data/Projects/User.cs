using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BTSS.Data.Projects
{
    public class User
    {

        #region "Declarations"
        
        private Datasource datasource;

        #endregion 

        #region "Constructor"

        public User(string connectionString, BTSS.Common.Core.DataProvider provider)
        {
            datasource = new Datasource(connectionString, provider);
        }
        
        #endregion

        #region "Private Methods"

        private List<string> SaveSQL(Logic.Projects.User user)
        {
            List<string> sql = new List<string>();

            switch (user.Operation)
            {
                case BTSS.Common.Core.Operation.INSERT: 
                    user.Id = datasource.GetNextCode("set_user", "user_id", "001");
                    sql.Add("INSERT INTO set_user(user_id, user_name,user_last_name,user_first_name,user_middle_name)VALUES(" +
                        "'" + user.Id + "'," +
                        "'" + user.UserName + "'," +
                        "'" + user.LastName + "'," +
                        "'" + user.FirstName + "'," +
                        "'" + user.MiddleName + "'" + 
                        ")");
                    break;
                case BTSS.Common.Core.Operation.UPDATE: 
                    sql.Add("UPDATE set_user " +
                        "SET " +
                        "user_name = '" + user.UserName + "', " +
                        "user_last_name = '" + user.LastName + "', " +
                        "user_first_name = '" + user.FirstName + "', " +
                        "user_middle_name = '" + user.MiddleName + "' " + 
                        "WHERE user_id = '" + user.Id + "'");
                    break;
                case BTSS.Common.Core.Operation.DELETE:
                    sql.Add("DELETE FROM set_user WHERE user_id = '" + user.Id + "'");
                    sql.Add("DELETE FROM set_user_access WHERE user_id = '" + user.Id + "'");
                    break;
            }

            if (user.Operation != BTSS.Common.Core.Operation.DELETE)
            {
                foreach (DataRow row in user.Data.Select("", "", DataViewRowState.ModifiedOriginal))
                {
                    if (row.IsNull("is_selected"))
                    {
                        sql.Add("DELETE FROM set_user_access WHERE user_id = '" + user.Id + "' AND grp_id = '" + row["grp_id"] + "'");
                    }
                    else
                    {
                        sql.Add("INSERT INTO set_user_access(user_id,grp_id)VALUES('" + user.Id + "','" + row["grp_id"] + "')");
                    }
                }
            }

            return sql;
        }

        private string GetUserListSQL()
        {
            string sql;
            sql = "SELECT user_id, user_last_name, user_first_name, user_middle_name, user_name FROM set_user WHERE user_name <> 'Biztech'";
            return sql;
        }

        private string GetUserDetailsSQL(string id)
        {
            string sql;
            sql = "SELECT user_last_name, user_first_name, user_middle_name, user_name FROM set_user WHERE user_id = '" + id + "'";
            return sql;
        }

        private string IsExistSQL(string userName)
        {
            string sql;
            sql = "SELECT user_name FROM set_user WHERE user_name = '" + userName + "'"; ;
            return sql;
        }

        private string GetGroupAccessSQL()
        {
            string sql;
            sql = "SELECT grp_id, 0 as is_selected, grp_name, grp_desc " +
                "FROM set_group";
            return sql;
        }

        private string GetUserGroupSQL(string id)
        {
            try
            {
                string sql;
                sql = "SELECT user_id, grp_id FROM set_user_access WHERE user_id = '" + id + "'";
                return sql;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetUserGroupListSQL()
        {
            try
            {
                string sql;
                sql = 
                    "SELECT u.user_last_name + ', ' + u.user_first_name AS full_name, u.user_name, g.grp_name " +
                    "FROM set_user u INNER JOIN set_user_access a ON a.user_id = u.user_id " +
                    "INNER JOIN set_group g ON g.grp_id = a.grp_id";

                return sql;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 
        
        #region "Public Methods"

        public void Save(Logic.Projects.User user)
        {
            try
            {
                datasource.ExecuteScalar(SaveSQL(user));   
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetUserList()
        {
            try
            {
                return datasource.ExecuteQuery(GetUserListSQL()).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataRow GetUserDetails(string id)
        {
            try
            {
                return datasource.ExecuteQuery(GetUserDetailsSQL(id)).Tables[0].Rows[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsExist(string userName)
        {
            try
            {
                DataTable dt;
                dt = datasource.ExecuteQuery(IsExistSQL(userName)).Tables[0];

                return dt.Rows.Count > 0;  
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public DataTable GetGroupAccess()
        {
            try
            {
                return datasource.ExecuteQuery(GetGroupAccessSQL()).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetUserGroup(string id)
        {
            try
            {
                return datasource.ExecuteQuery(GetUserGroupSQL(id)).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetUserGroupList()
        {
            try
            {
                return datasource.ExecuteQuery(GetUserGroupListSQL()).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        
    }
}
