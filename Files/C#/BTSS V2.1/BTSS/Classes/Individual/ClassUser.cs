using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; 

namespace Biztech.Classes 
{
    class ClassUser:Datasource
    {


        #region "Declarations"
        Global.DataProvider _provider;
        #endregion 

        #region "Constructor"

        public ClassUser(string connectionString, Global.DataProvider provider) : base(connectionString, provider) { _provider = provider;}

        #endregion

        #region "Properties"
        
        private string _Id;
        public string Id { get { return _Id; } set { _Id = value; } }

        private string _LastName;
        public string LastName { get { return _LastName; } set { _LastName = value; } }

        private string _FirstName;
        public string FirstName { get { return _FirstName;}set{_FirstName = value;} }

        private string _MiddleName;
        public string Middlename { get { return _MiddleName; } set { _MiddleName = value; } }

        private string _UserName;
        public string Username { get { return _UserName; } set { _UserName = value; } }
        
        private string _Password;
        public string Password { get { return _Password; } set { _Password = value; } }

        private DataTable _Data;
        public DataTable Data { get { return _Data; } set { _Data = value; } }

        private Global.Operation _Operation;
        public Global.Operation Operation { get { return _Operation; } set { _Operation = value; } }

        #endregion
        
        #region "Private Methods"

        private List<string> SaveSQL()
        {
            List<string> sql = new List<string>();

            switch (_Operation)
            {
                case Global.Operation.INSERT:
                    _Id = GetNextCode("set_user", "user_id", "001");
                    sql.Add("INSERT INTO set_user(user_id, user_name,user_last_name,user_first_name,user_middle_name,user_password)VALUES(" +
                        "'" + _Id + "'," +
                        "'" + _UserName + "'," +
                        "'" + _LastName + "'," +
                        "'" + _FirstName + "'," +
                        "'" + _MiddleName + "'," +
                        "'" + _Password + "'" +                         
                        ")");
                    break;
                case Global.Operation.UPDATE:
                    sql.Add("UPDATE set_user " +
                        "SET " +
                        "user_name = '" + _UserName + "', " +
                        "user_last_name = '" + _LastName + "', " +
                        "user_first_name = '" + _FirstName + "', " +
                        "user_middle_name = '" + _MiddleName + "', " +
                        "user_password = '" + _Password + "' " + 
                        "WHERE user_id = '" + _Id + "'");
                    break;
                case Global.Operation.DELETE:
                    sql.Add("DELETE FROM set_user WHERE user_id = '" + _Id + "'");
                    sql.Add("DELETE FROM set_user_access WHERE user_id = '" + _Id + "'"); 
                    break;
            }

            if (Operation != Global.Operation.DELETE)
            {  
                foreach (DataRow row in _Data.Select("", "", DataViewRowState.ModifiedOriginal))
                {
                    if (row.IsNull("is_selected"))
                    {
                        sql.Add("DELETE FROM set_user_access WHERE user_id = '" + _Id + "' AND grp_id = '" + row["grp_id"] + "'");
                    }
                    else
                    {
                        sql.Add("INSERT INTO set_user_access(user_id,grp_id)VALUES('"+ _Id +"','"+ row["grp_id"] +"')");
                    } 
                }
            }

            return sql;
        }

        private string GetUserListSQL()
        {
            string sql;
            sql = "SELECT user_id, user_last_name, user_first_name, user_middle_name, user_name, user_password FROM set_user WHERE user_name <> 'Biztech'";
            return sql;
        }

        private string GetUserDetailsSQL(string id)
        {
            string sql;
            sql = "SELECT user_last_name, user_first_name, user_middle_name, user_name, user_password FROM set_user WHERE user_id = '"+ id +"'";
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
                sql = "SELECT user_id, grp_id FROM set_user_access WHERE user_id = '"+ id +"'";
                return sql;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 
        
        #region "Public Methods"

        public void Save()
        {
            try
            { 
                ExecuteScalar(SaveSQL());   
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
                return ExecuteQuery(GetUserListSQL()).Tables[0];
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
                return ExecuteQuery(GetUserDetailsSQL(id)).Tables[0].Rows[0];
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
                return ExecuteQuery(GetGroupAccessSQL()).Tables[0];
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
                return ExecuteQuery(GetUserGroupSQL(id)).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        #endregion
        
    }
}
